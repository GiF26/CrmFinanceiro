using System.Numerics;

namespace CrmFinanceiro.Data.Models;

public class HistoricoAcao
{
    protected HistoricoAcao() { }
    public HistoricoAcao(FinanceiroCaixa titulo, Usuario usuario, DateTime dataAcao, int tipoAcao, string observacao)
    {
        Titulo = titulo;
        Usuario = usuario;
        DataAcao = DateTime.UtcNow;
        TipoAcao = tipoAcao;
        Observacao = observacao;
    }

    public long Id { get; private set; }

    public long TituloId { get; private set; }
    public FinanceiroCaixa Titulo { get; private set; } = null!;

    public long UsuarioId { get; private set; }
    public Usuario Usuario { get; private set; } = null!;

    public DateTime DataAcao { get; private set; } 
    public int TipoAcao { get; private set; }
    public string Observacao{ get; set; } = string.Empty;

}
