namespace CrmFinanceiro.Data.DTOs;

public record ResumoCaixaDTO(decimal TotalReceber, decimal TotalPagar, int QtdTitulosPagar, int QtdTitulosReceber)
{
    public decimal SaldoProjetado => TotalReceber - TotalPagar;
}
