namespace CrmFinanceiro.Data.DTOs;

public record ChartProjecaoSemanalDTO(List<string> Labels, List<decimal> Entradas, List<decimal> Saidas)
{
}
