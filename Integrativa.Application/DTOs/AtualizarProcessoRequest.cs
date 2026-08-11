using System.ComponentModel.DataAnnotations;
using Integrativa.Domain.Enums;

namespace Integrativa.Application.DTOs;

public record AtualizarProcessoRequest(
    [Required(ErrorMessage = "Número é obrigatório.")]
    [MaxLength(50, ErrorMessage = "Número deve ter no máximo 50 caracteres.")]
    string Numero,

    [Required(ErrorMessage = "Assunto é obrigatório.")]
    [MaxLength(500, ErrorMessage = "Assunto deve ter no máximo 500 caracteres.")]
    string Assunto,

    [EnumDataType(typeof(StatusProcesso), ErrorMessage = "Status inválido.")]
    StatusProcesso Status);