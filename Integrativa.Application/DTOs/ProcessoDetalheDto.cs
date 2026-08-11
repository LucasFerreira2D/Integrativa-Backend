using Integrativa.Domain.Enums;

namespace Integrativa.Application.DTOs;

public record ProcessoDetalheDto(
    Guid Id,
    string Numero,
    string Assunto,
    DateTime DataCriacao,
    StatusProcesso Status,
    IReadOnlyList<ParteDto> Partes,
    IReadOnlyList<AndamentoDto> Andamentos,
    DateTime DataAlteracao,
    string UsuarioAlteracao);