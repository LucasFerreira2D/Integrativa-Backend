namespace Integrativa.Application.DTOs;

public record AndamentoDto(
    Guid Id,
    DateTime Data,
    string Descricao,
    DateTime DataAlteracao,
    string UsuarioAlteracao);