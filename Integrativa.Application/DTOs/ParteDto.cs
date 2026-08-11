using Integrativa.Domain.Enums;

namespace Integrativa.Application.DTOs;

public record ParteDto(
    Guid Id,
    string Nome,
    TipoParte TipoParte,
    DateTime DataAlteracao,
    string UsuarioAlteracao);