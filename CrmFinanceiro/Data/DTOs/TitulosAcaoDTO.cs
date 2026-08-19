namespace CrmFinanceiro.Data.DTOs;

public record TitulosAcaoDTO(string documento, string parceiro, string tipo, decimal valor)
{
    public readonly string flagTipo = tipo == "Saída" ? "badge bg-danger" : "badge bg-success";
}
