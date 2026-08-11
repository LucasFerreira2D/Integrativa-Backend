using Integrativa.Application.Common;
using Integrativa.Application.DTOs;
using Integrativa.Domain.Entities;

namespace Integrativa.Application.Repositories;

public interface IProcessoRepository
{
    PagedResult<ProcessoListItemDto> Listar(ProcessoFiltro filtro);

    Processo? ObterPorId(Guid id);

    ProcessoDetalheDto? ObterParaLeitura(Guid id);

    bool NumeroExiste(string numero, Guid? ignorarId);

    void Adicionar(Processo processo);

    void Remover(Processo processo);

    void Salvar();
}