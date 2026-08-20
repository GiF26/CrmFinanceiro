namespace CrmFinanceiro.Data.DTOs;

public record TitulosAcaoDTO(string Documento, 
                            string Parceiro, 
                            string Tipo,
                            DateTime DataEmissao,
                            DateTime DataVencimento,
                            DateTime? DataPagamento,
                            decimal Valor)
{
    public readonly string flagTipo = Tipo == "Saída" ? "badge bg-danger" : "badge bg-success";
}
