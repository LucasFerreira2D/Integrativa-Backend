using Integrativa.Domain.Enums;

namespace Integrativa.Application.DTOs;

public record ProcessoListItemDto(
    Guid Id,
    string Numero,
    string Assunto,
    DateTime DataCriacao,
    StatusProcesso Status,
    int TotalPartes,
    int TotalAndamentos,
    DateTime DataAlteracao,
    string UsuarioAlteracao);