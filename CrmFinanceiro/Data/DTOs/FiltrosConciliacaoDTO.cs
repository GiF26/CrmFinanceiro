namespace CrmFinanceiro.Data.DTOs;

public record FiltrosConciliacaoDTO(string? Documento, string? Parceiro, string? Tipo, DateTime DataIni, DateTime DataFim);
