namespace CrmFinanceiro.Data.Dto;

public record ResumoCaixaDTO(decimal TotalReceber, decimal TotalPagar)
{
    public decimal SaldoProjetado => TotalReceber - TotalPagar;
}
