using System.ComponentModel.DataAnnotations;

namespace Integrativa.Application.DTOs;

public record CriarAndamentoRequest(
    [Required(ErrorMessage = "Data é obrigatória.")]
    DateTime Data,

    [Required(ErrorMessage = "Descrição é obrigatória.")]
    [MaxLength(1000, ErrorMessage = "Descrição deve ter no máximo 1000 caracteres.")]
    string Descricao);