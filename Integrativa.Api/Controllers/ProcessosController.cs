using Integrativa.Application.Common;
using Integrativa.Application.DTOs;
using Integrativa.Application.Services;
using Integrativa.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Integrativa.Api.Controllers;

[ApiController]
[Route("api/processos")]
public class ProcessosController : ControllerBase
{
    private readonly ProcessoService _service;

    public ProcessosController(ProcessoService service)
    {
        _service = service;
    }

    [HttpGet]
    public ActionResult<PagedResult<ProcessoListItemDto>> Listar(
        [FromQuery] StatusProcesso? status,
        [FromQuery] string? numero,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        var filtro = new ProcessoFiltro(status, numero, page, pageSize);
        return Ok(_service.Listar(filtro));
    }

    [HttpGet("{id:guid}")]
    public ActionResult<ProcessoDetalheDto> Obter(Guid id)
    {
        return Ok(_service.Obter(id));
    }

    [HttpPost]
    public IActionResult Criar([FromBody] CriarProcessoRequest request)
    {
        Guid id = _service.Criar(request);
        return CreatedAtAction(nameof(Obter), new { id }, new { id });
    }

    [HttpPut("{id:guid}")]
    public IActionResult Atualizar(Guid id, [FromBody] AtualizarProcessoRequest request)
    {
        _service.Atualizar(id, request);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public IActionResult Excluir(Guid id)
    {
        _service.Excluir(id);
        return NoContent();
    }

    [HttpPost("{id:guid}/partes")]
    public ActionResult<ParteDto> AdicionarParte(Guid id, [FromBody] CriarParteRequest request)
    {
        ParteDto parte = _service.AdicionarParte(id, request);
        return CreatedAtAction(nameof(Obter), new { id }, parte);
    }

    [HttpDelete("{id:guid}/partes/{parteId:guid}")]
    public IActionResult RemoverParte(Guid id, Guid parteId)
    {
        _service.RemoverParte(id, parteId);
        return NoContent();
    }

    [HttpPost("{id:guid}/andamentos")]
    public ActionResult<AndamentoDto> AdicionarAndamento(Guid id, [FromBody] CriarAndamentoRequest request)
    {
        AndamentoDto andamento = _service.AdicionarAndamento(id, request);
        return CreatedAtAction(nameof(Obter), new { id }, andamento);
    }

    [HttpDelete("{id:guid}/andamentos/{andamentoId:guid}")]
    public IActionResult RemoverAndamento(Guid id, Guid andamentoId)
    {
        _service.RemoverAndamento(id, andamentoId);
        return NoContent();
    }
}