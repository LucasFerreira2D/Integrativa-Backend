using Integrativa.Domain.Enums;

namespace Integrativa.Application.DTOs;

public record ParteDto(
    Guid Id,
    string Nome,
    TipoParte Tipo,
    DateTime DataAlteracao,
    string UsuarioAlteracao);