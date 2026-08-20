using CrmFinanceiro.Data.DTOs;
using Microsoft.EntityFrameworkCore;

namespace CrmFinanceiro.Data.Services;

public class FinanceiroCaixaService
{
    private readonly AppDbContext _context;

    public FinanceiroCaixaService(AppDbContext context)
    {
        _context = context;
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
        var query = _context.FinanceiroCaixa
                    .AsQueryable();

        if (!string.IsNullOrWhiteSpace(f.Documento)) query = query.Where(t => t.NumeroDocumento.Contains(f.Documento));

        if (!string.IsNullOrWhiteSpace(f.Parceiro)) query = query.Where(t => t.Pessoa.Nome.Contains(f.Parceiro));

        if (!string.IsNullOrWhiteSpace(f.Tipo)) query = query.Where(t => t.TipoDocumento == f.Tipo);

        query = query.Where(t => t.DataVencimento.Date >= f.DataIni.Date);

        query = query.Where(t => t.DataVencimento.Date <= f.DataFim.Date);

        return await query
            .Where(t => t.StatusTitulo == 1 
            && t.TipoDocumento == "Entrada")
            .SumAsync(t => t.Valor);
    }

    private async Task<decimal> CalcularPagarAsync(FiltrosConciliacaoDTO f)
    {
        var query = _context.FinanceiroCaixa
                    .AsQueryable();

        if (!string.IsNullOrWhiteSpace(f.Documento)) query = query.Where(t => t.NumeroDocumento.Contains(f.Documento));

        if (!string.IsNullOrWhiteSpace(f.Parceiro)) query = query.Where(t => t.Pessoa.Nome.Contains(f.Parceiro));

        if (!string.IsNullOrWhiteSpace(f.Tipo)) query = query.Where(t => t.TipoDocumento == f.Tipo);

        query = query.Where(t => t.DataVencimento.Date >= f.DataIni.Date);

        query = query.Where(t => t.DataVencimento.Date <= f.DataFim.Date);

        return await query
            .Where(t => t.StatusTitulo == 1 
            && t.TipoDocumento == "Saída")
            .SumAsync(t => t.Valor);
    }

    private async Task<int> CalcularTitulosReceber(FiltrosConciliacaoDTO f)
    {
        var query = _context.FinanceiroCaixa
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(f.Documento)) query = query.Where(t => t.NumeroDocumento.Contains(f.Documento));

        if (!string.IsNullOrWhiteSpace(f.Parceiro)) query = query.Where(t => t.Pessoa.Nome.Contains(f.Parceiro));

        if (!string.IsNullOrWhiteSpace(f.Tipo)) query = query.Where(t => t.TipoDocumento == f.Tipo);

        query = query.Where(t => t.DataVencimento.Date >= f.DataIni.Date);

        query = query.Where(t => t.DataVencimento.Date <= f.DataFim.Date);

        return await query
            .Where(t => t.StatusTitulo == 1
            && t.TipoDocumento == "Entrada")
            .CountAsync();
    }

    private async Task<int> CalcularTitulosPagar(FiltrosConciliacaoDTO f)
    {
        var query = _context.FinanceiroCaixa
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(f.Documento)) query = query.Where(t => t.NumeroDocumento.Contains(f.Documento));

        if (!string.IsNullOrWhiteSpace(f.Parceiro)) query = query.Where(t => t.Pessoa.Nome.Contains(f.Parceiro));

        if (!string.IsNullOrWhiteSpace(f.Tipo)) query = query.Where(t => t.TipoDocumento == f.Tipo);

        query = query.Where(t => t.DataVencimento.Date >= f.DataIni.Date);

        query = query.Where(t => t.DataVencimento.Date <= f.DataFim.Date);

        return await query
            .Where(t => t.StatusTitulo == 1
            && t.TipoDocumento == "Saída")
            .CountAsync();
    }

    public async Task<List<TitulosAcaoDTO>> CarregarTitulos(FiltrosConciliacaoDTO f)
    {
        var query = _context.FinanceiroCaixa
        .Include(t => t.Pessoa)
        .AsQueryable();

        if (!string.IsNullOrWhiteSpace(f.Documento)) query = query.Where(t => t.NumeroDocumento.Contains(f.Documento));

        if (!string.IsNullOrWhiteSpace(f.Parceiro)) query = query.Where(t => t.Pessoa.Nome.Contains(f.Parceiro));

        if (!string.IsNullOrWhiteSpace(f.Tipo)) query = query.Where(t => t.TipoDocumento == f.Tipo);

        query = query.Where(t => t.DataVencimento.Date >= f.DataIni.Date);

        query = query.Where(t => t.DataVencimento.Date <= f.DataFim.Date);

        return await query
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
}
