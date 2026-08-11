using Integrativa.Application.Common;
using Integrativa.Application.DTOs;
using Integrativa.Application.Repositories;
using Integrativa.Domain.Entities;
using Integrativa.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Integrativa.Infrastructure.Repositories;

public class ProcessoRepository : IProcessoRepository
{
    private readonly AppDbContext _context;

    public ProcessoRepository(AppDbContext context)
    {
        _context = context;
    }

    public PagedResult<ProcessoListItemDto> Listar(ProcessoFiltro filtro)
    {
        int page = filtro.Page < 1 ? 1 : filtro.Page;
        int pageSize = filtro.PageSize < 1 || filtro.PageSize > 100 ? 10 : filtro.PageSize;

        IQueryable<Processo> query = _context.Processos.AsNoTracking();

        if (filtro.Status != null)
            query = query.Where(p => p.Status == filtro.Status);

        if (!string.IsNullOrWhiteSpace(filtro.Numero))
        {
            string termo = filtro.Numero.Trim();
            query = query.Where(p => EF.Functions.ILike(p.Numero, "%" + termo + "%"));
        }

        int totalItems = query.Count();

        List<ProcessoListItemDto> items = query
            .OrderByDescending(p => p.DataCriacao)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(p => new ProcessoListItemDto(
                p.Id,
                p.Numero,
                p.Assunto,
                p.DataCriacao,
                p.Status,
                p.Partes.Count,
                p.Andamentos.Count,
                p.DataAlteracao,
                p.UsuarioAlteracao))
            .ToList();

        return new PagedResult<ProcessoListItemDto>(items, page, pageSize, totalItems);
    }

    public Processo? ObterPorId(Guid id)
    {
        return _context.Processos
            .Include(p => p.Partes)
            .Include(p => p.Andamentos)
            .AsSplitQuery()
            .FirstOrDefault(p => p.Id == id);
    }

    public ProcessoDetalheDto? ObterParaLeitura(Guid id)
    {
        return _context.Processos
            .AsNoTracking()
            .Where(p => p.Id == id)
            .Select(p => new ProcessoDetalheDto(
                p.Id,
                p.Numero,
                p.Assunto,
                p.DataCriacao,
                p.Status,
                p.Partes
                    .OrderBy(x => x.Nome)
                    .Select(x => new ParteDto(x.Id, x.Nome, x.TipoParte, x.DataAlteracao, x.UsuarioAlteracao))
                    .ToList(),
                p.Andamentos
                    .OrderByDescending(x => x.DataCriacao)
                    .Select(x => new AndamentoDto(x.Id, x.DataCriacao, x.Descricao, x.DataAlteracao, x.UsuarioAlteracao))
                    .ToList(),
                p.DataAlteracao,
                p.UsuarioAlteracao))
            .FirstOrDefault();
    }

    public bool NumeroExiste(string numero, Guid? ignorarId)
    {
        return _context.Processos
            .AsNoTracking()
            .Any(p => p.Numero == numero && (ignorarId == null || p.Id != ignorarId));
    }

    public void Adicionar(Processo processo)
    {
        _context.Processos.Add(processo);
    }

    public void Remover(Processo processo)
    {
        _context.Processos.Remove(processo);
    }

    public void Salvar()
    {
        _context.SaveChanges();
    }
}