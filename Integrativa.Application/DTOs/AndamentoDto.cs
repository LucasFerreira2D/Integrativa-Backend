namespace Integrativa.Application.DTOs;

public record AndamentoDto(
    Guid Id,
    DateTime DataCriacao,
    string Descricao,
    DateTime DataAlteracao,
    string UsuarioAlteracao);