using System.Numerics;

namespace CrmFinanceiro.Data.Models;

public class Pessoa
{
    public Pessoa() { }
    public Pessoa(string nome, string cpfcnpj, int tipoPessoa)
    {
        Nome = nome;
        CpfCnpj = cpfcnpj;
        TipoPessoa = tipoPessoa;
    }

    public long Id { get; private set; }
    public string Nome { get; private set; } = string.Empty;
    public string CpfCnpj { get; private set; } = string.Empty;
    public int TipoPessoa { get; private set; }
}
