using CrmFinanceiro.Data;
using CrmFinanceiro.Data.Dto;
using Microsoft.EntityFrameworkCore;

namespace CrmFinanceiro.Data.Services;

public class FinanceiroCaixaService
{
    private readonly AppDbContext _context;

    public FinanceiroCaixaService(AppDbContext context)
    {
        _context = context;
    }

    public FinanceiroCaixaDTO carregaResumoDia()
    {
        return new FinanceiroCaixaDTO(CalcularReceber(), CalcularPagar());
    }

    private decimal CalcularReceber()
    {
        var titulosAReceber = _context.FinanceiroCaixa
            .Where(t => t.StatusTitulo ==1 && t.TipoDocumento.Equals("Entrada"))
            .Sum(t => t.Valor);
        return titulosAReceber;
    }

    private decimal CalcularPagar()
    {
        var titulosAPagar = _context.FinanceiroCaixa
            .Where(t => t.StatusTitulo == 1 && t.TipoDocumento.Equals("Saída"))
            .Sum(t => t.Valor);
        return titulosAPagar;
    }
    
}
