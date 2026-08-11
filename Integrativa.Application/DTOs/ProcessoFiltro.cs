using Integrativa.Domain.Enums;

namespace Integrativa.Application.DTOs;

public record ProcessoFiltro(StatusProcesso? Status, string? Numero, int Page = 1, int PageSize = 10);