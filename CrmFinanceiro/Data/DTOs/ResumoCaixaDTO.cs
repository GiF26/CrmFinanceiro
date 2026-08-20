namespace CrmFinanceiro.Data.DTOs;

public record ResumoCaixaDTO(decimal TotalReceber, decimal TotalPagar, int QtdTitulosPagar, int QtdTitulosReceber)
{

    public decimal SaldoProjetado => TotalReceber - TotalPagar;

    public StatusSaldoDTO Indicador
    {
        get
        {
            if (TotalPagar > TotalReceber)
            {
                return StatusSaldoDTO.Critico;
            }else if (TotalPagar < TotalReceber)
            {
                return StatusSaldoDTO.Saudavel;
            }
            else {
                return StatusSaldoDTO.Alerta;
            }
        }
    }

    public string ClasseCorCrm => Indicador switch
    {
        StatusSaldoDTO.Saudavel => "primary",
        StatusSaldoDTO.Alerta => "warning",
        StatusSaldoDTO.Critico => "danger",
        _ => "secondary"
    };
}
