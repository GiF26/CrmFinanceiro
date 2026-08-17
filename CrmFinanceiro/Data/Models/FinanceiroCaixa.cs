using System.Numerics;

namespace CrmFinanceiro.Data.Models;

public class FinanceiroCaixa
{
    protected FinanceiroCaixa() { }
    public FinanceiroCaixa(string numeroDocumento, string tipoDocumento, int pessoaId, decimal valor, DateTime dataEmissao, DateTime dataVencimento)
    {
        NumeroDocumento = numeroDocumento;
        TipoDocumento = tipoDocumento;
        PessoaId = pessoaId; 
        Valor = valor;
        DataEmissao = dataEmissao;
        DataVencimento = dataVencimento;
        IsConciliado = false;
        StatusTitulo = 1;
    }

    public long Id {  get; private set; }
    public string NumeroDocumento { get; private set; } = string.Empty;
    public string TipoDocumento { get; private set; } = string.Empty;
    
    public long PessoaId{ get; private set; }
    public Pessoa Pessoa { get; private set; } = null!;
    
    public decimal Valor { get; set; }
    public DateTime DataEmissao { get; set; }
    public DateTime DataVencimento { get; set; }
    public DateTime? DataPagamento { get; set; }
    public int StatusTitulo { get; set; }
    public bool IsConciliado { get; private set; }

}
