using System.ComponentModel.DataAnnotations;
using Integrativa.Domain.Enums;

namespace Integrativa.Application.DTOs;

public record CriarParteRequest(
    [Required(ErrorMessage = "Nome é obrigatório.")]
    [MaxLength(200, ErrorMessage = "Nome deve ter no máximo 200 caracteres.")]
    string Nome,

    [EnumDataType(typeof(TipoParte), ErrorMessage = "Tipo de parte inválido.")]
    TipoParte TipoParte);