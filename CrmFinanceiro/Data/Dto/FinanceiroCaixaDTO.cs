namespace CrmFinanceiro.Data.Dto;

public record FinanceiroCaixaDTO(decimal TotalReceber, decimal TotalPagar)
{
    public decimal SaldoProjetado => TotalReceber - TotalPagar;
}
