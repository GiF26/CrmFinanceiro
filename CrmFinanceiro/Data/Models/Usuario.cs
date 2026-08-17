using System.Numerics;

namespace CrmFinanceiro.Data.Models;

public class Usuario
{
    protected Usuario() { }
    public Usuario(string nome, string email, string senha, string cargo)
    {
        Nome = nome;
        Email = email;
        Senha = senha;
        Cargo = cargo;
        DataCriacao = DateTime.UtcNow;
    }

    public long Id { get; private set; }
    public string Nome { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string Senha { get; private set; } = string.Empty;
    public string Cargo { get; private set; } = string.Empty;
    public DateTime DataCriacao { get; private set; } = DateTime.Now;

}
