using CrmFinanceiro.Data.Dto;
using CrmFinanceiro.Data.DTOs;
using CrmFinanceiro.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CrmFinanceiro.Data.Services;

public class FinanceiroCaixaService
{
    private readonly AppDbContext _context;
    private readonly DateTime hoje = DateTime.Today;

    public FinanceiroCaixaService(AppDbContext context)
    {
        _context = context;
    }

    // Transformamos o método em assíncrono para liberar o servidor durante a consulta
    public async Task<ResumoCaixaDTO> CarregaResumoDiaAsync()
    {
        // Aqui estamos disparando as duas buscas em paralelo para ficar ainda mais rápido!
        var receberTask = CalcularReceberAsync();
        var pagarTask = CalcularPagarAsync();

        // Aguardamos ambas terminarem
        await Task.WhenAll(receberTask, pagarTask);

        return new ResumoCaixaDTO(receberTask.Result, pagarTask.Result);
    }

    private async Task<decimal> CalcularReceberAsync()
    {
        return await _context.FinanceiroCaixa
            .Where(t => t.StatusTitulo == 1 
            && t.TipoDocumento == "Entrada"
            /*&& t.DataVencimento == hoje*/)
            .SumAsync(t => t.Valor);
    }

    private async Task<decimal> CalcularPagarAsync()
    {
        return await _context.FinanceiroCaixa
            .Where(t => t.StatusTitulo == 1 
            && t.TipoDocumento == "Saída"
            /*&& t.DataVencimento == hoje*/)
            .SumAsync(t => t.Valor);
    }

    public async Task<List<TitulosAcaoDTO>> CarregarTitulos()
    {
        return await _context.FinanceiroCaixa
            .Include(t => t.Pessoa)
            .Select(c => new TitulosAcaoDTO(
                    c.NumeroDocumento, c.Pessoa.Nome,
                    c.TipoDocumento, c.Valor))
            //.Where(t => t.DataVencimento == hoje)
            .ToListAsync();
    }
}
