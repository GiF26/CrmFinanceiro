using CrmFinanceiro.Data.DTOs;
using CrmFinanceiro.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CrmFinanceiro.Data.Services;

public class FinanceiroCaixaService
{
    private readonly AppDbContext _context;

    public FinanceiroCaixaService(AppDbContext context)
    {
        _context = context;
    }

    private IQueryable<FinanceiroCaixa> ObterQueryFiltrada(FiltrosConciliacaoDTO f)
    {
        var query = _context.FinanceiroCaixa.AsQueryable();

        if (!string.IsNullOrWhiteSpace(f.Documento))
            query = query.Where(t => t.NumeroDocumento.Contains(f.Documento));

        if (!string.IsNullOrWhiteSpace(f.Parceiro))
            query = query.Where(t => t.Pessoa.Nome.Contains(f.Parceiro));

        if (!string.IsNullOrWhiteSpace(f.Tipo))
            query = query.Where(t => t.TipoDocumento == f.Tipo);

        query = query.Where(t => t.DataVencimento.Date >= f.DataIni.Date);
        query = query.Where(t => t.DataVencimento.Date <= f.DataFim.Date);

        return query;
    }

    public async Task<ResumoCaixaDTO> CarregaResumoDiaAsync(FiltrosConciliacaoDTO f)
    {
        var receberTask = CalcularReceberAsync(f);
        var pagarTask = CalcularPagarAsync(f);
        var qtdPagar = CalcularTitulosPagar(f);
        var qtdReceber = CalcularTitulosReceber(f);

        await Task.WhenAll(receberTask, pagarTask, qtdPagar, qtdReceber);

        return new ResumoCaixaDTO(receberTask.Result, pagarTask.Result, qtdPagar.Result, qtdReceber.Result);
    }

    private async Task<decimal> CalcularReceberAsync(FiltrosConciliacaoDTO f)
    {
        return await ObterQueryFiltrada(f)
            .Where(t => t.StatusTitulo == 1 
            && t.TipoDocumento == "Entrada")
            .SumAsync(t => t.Valor);
    }

    private async Task<decimal> CalcularPagarAsync(FiltrosConciliacaoDTO f)
    {
        return await ObterQueryFiltrada(f)
            .Where(t => t.StatusTitulo == 1 
            && t.TipoDocumento == "Saída")
            .SumAsync(t => t.Valor);
    }

    private async Task<int> CalcularTitulosReceber(FiltrosConciliacaoDTO f)
    {
        return await ObterQueryFiltrada(f)
            .Where(t => t.StatusTitulo == 1
            && t.TipoDocumento == "Entrada")
            .CountAsync();
    }

    private async Task<int> CalcularTitulosPagar(FiltrosConciliacaoDTO f)
    {
        return await ObterQueryFiltrada(f)
            .Where(t => t.StatusTitulo == 1
            && t.TipoDocumento == "Saída")
            .CountAsync();
    }

    public async Task<List<TitulosAcaoDTO>> CarregarTitulos(FiltrosConciliacaoDTO f)
    {
        return await ObterQueryFiltrada(f)
            .Select(c => new TitulosAcaoDTO(
                c.NumeroDocumento,
                c.Pessoa.Nome,
                c.TipoDocumento,
                c.DataEmissao,
                c.DataVencimento,
                c.DataPagamento,
                c.Valor))
            .ToListAsync();
    }

    public async Task<ChartProjecaoSemanalDTO> CarregaProjecaoSemanalAsync(FiltrosConciliacaoDTO f)
    {

        var datasProjetadas = Enumerable.Range(0, 7)
                                    .Select(offset => f.DataIni.AddDays(offset).Date)
                                    .ToList();

        var labels = datasProjetadas.Select(d => d.ToString("dd/MM")).ToList();
        var dataFinal = datasProjetadas.Last();

        var toraisAgrupados = await _context.FinanceiroCaixa
            .Where(t => t.DataVencimento.Date >= f.DataIni.Date && t.DataVencimento.Date <= dataFinal)
            .GroupBy(t => new { t.DataVencimento.Date, t.TipoDocumento })
            .Select(g => new
            {
                Data = g.Key.Date,
                Tipo = g.Key.TipoDocumento,
                Total = g.Sum(x => x.Valor)
            }).ToListAsync();

        var entradas = new List<decimal>();
        var saidas = new List<decimal>();

        foreach(var dia in datasProjetadas)
        {
            entradas.Add(toraisAgrupados.FirstOrDefault(t => t.Data == dia && t.Tipo == "Entrada")?.Total ?? 0);
            saidas.Add(toraisAgrupados.FirstOrDefault(t => t.Data == dia && t.Tipo == "Saída")?.Total ?? 0);
        }

        return new ChartProjecaoSemanalDTO(labels, entradas, saidas);
    }

    public async Task<ChartEntradasParceirosDTO> CarregaConcentracaoParceirosAsync(FiltrosConciliacaoDTO f)
    {
        var dadosAgrupados = await ObterQueryFiltrada(f)
            .Include(t => t.Pessoa) 
            .Where(t => t.TipoDocumento == "Entrada" && t.StatusTitulo == 1)
            .GroupBy(t => t.Pessoa.Nome)
            .Select(g => new
            {
                Parceiro = g.Key,
                Total = g.Sum(x => x.Valor)
            })
            .OrderByDescending(x => x.Total)
            .ToListAsync();

        // 2. Paleta de Cores Oficial do Bootstrap
        string[] paletaCores = { "#0d6efd", "#0dcaf0", "#ffc107", "#198754", "#dc3545", "#6f42c1", "#fd7e14" };

        var labels = new List<string>();
        var valores = new List<decimal>();
        var cores = new List<string>();

        // 3. Distribuição otimizada das cores
        for (int i = 0; i < dadosAgrupados.Count; i++)
        {
            labels.Add(dadosAgrupados[i].Parceiro);
            valores.Add(dadosAgrupados[i].Total);
            // O operador % faz o índice voltar ao zero se os parceiros passarem de 7
            cores.Add(paletaCores[i % paletaCores.Length]);
        }

        return new ChartEntradasParceirosDTO(labels, valores, cores);
    }
}
