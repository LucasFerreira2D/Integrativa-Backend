using Integrativa.Application.Common;
using Integrativa.Application.DTOs;
using Integrativa.Application.Repositories;
using Integrativa.Domain.Entities;

namespace Integrativa.Application.Services;

public class ProcessoService
{
    private const string Usuario = "Sistema";

    private readonly IProcessoRepository _repository;

    public ProcessoService(IProcessoRepository repository)
    {
        _repository = repository;
    }

    public PagedResult<ProcessoListItemDto> Listar(ProcessoFiltro filtro)
    {
        return _repository.Listar(filtro);
    }

    public ProcessoDetalheDto Obter(Guid id)
    {
        ProcessoDetalheDto? processo = _repository.ObterParaLeitura(id);

        if (processo == null)
            throw new NotFoundException("Processo " + id + " não encontrado.");

        return processo;
    }

    public Guid Criar(CriarProcessoRequest request)
    {
        GarantirNumeroDisponivel(request.Numero, null);

        Processo processo = Processo.Criar(request.Numero, request.Assunto, request.Status);
        processo.RegistrarAlteracao(Usuario);

        _repository.Adicionar(processo);
        _repository.Salvar();

        return processo.Id;
    }

    public void Atualizar(Guid id, AtualizarProcessoRequest request)
    {
        Processo processo = Carregar(id);
        GarantirNumeroDisponivel(request.Numero, id);

        processo.Atualizar(request.Numero, request.Assunto, request.Status);
        processo.RegistrarAlteracao(Usuario);

        _repository.Salvar();
    }

    public void Excluir(Guid id)
    {
        Processo processo = Carregar(id);

        _repository.Remover(processo);
        _repository.Salvar();
    }

    public ParteDto AdicionarParte(Guid processoId, CriarParteRequest request)
    {
        Processo processo = Carregar(processoId);

        Parte parte = processo.AdicionarParte(request.Nome, request.TipoParte, Usuario);
        processo.RegistrarAlteracao(Usuario);

        _repository.Salvar();

        return new ParteDto(parte.Id, parte.Nome, parte.TipoParte, parte.DataAlteracao, parte.UsuarioAlteracao);
    }

    public void RemoverParte(Guid processoId, Guid parteId)
    {
        Processo processo = Carregar(processoId);

        if (!processo.Partes.Any(p => p.Id == parteId))
            throw new NotFoundException("Parte " + parteId + " não encontrada no processo " + processoId + ".");

        processo.RemoverParte(parteId);
        processo.RegistrarAlteracao(Usuario);

        _repository.Salvar();
    }

    public AndamentoDto AdicionarAndamento(Guid processoId, CriarAndamentoRequest request)
    {
        Processo processo = Carregar(processoId);

        Andamento andamento = processo.AdicionarAndamento(request.Data, request.Descricao, Usuario);
        processo.RegistrarAlteracao(Usuario);

        _repository.Salvar();

        return new AndamentoDto(andamento.Id, andamento.DataCriacao, andamento.Descricao,
            andamento.DataAlteracao, andamento.UsuarioAlteracao);
    }

    public void RemoverAndamento(Guid processoId, Guid andamentoId)
    {
        Processo processo = Carregar(processoId);

        if (!processo.Andamentos.Any(a => a.Id == andamentoId))
            throw new NotFoundException("Andamento " + andamentoId + " não encontrado no processo " + processoId + ".");

        processo.RemoverAndamento(andamentoId);
        processo.RegistrarAlteracao(Usuario);

        _repository.Salvar();
    }

    private Processo Carregar(Guid id)
    {
        Processo? processo = _repository.ObterPorId(id);

        if (processo == null)
            throw new NotFoundException("Processo " + id + " não encontrado.");

        return processo;
    }

    private void GarantirNumeroDisponivel(string numero, Guid? ignorarId)
    {
        if (_repository.NumeroExiste(numero.Trim(), ignorarId))
            throw new ConflictException("Já existe um processo com o número '" + numero + "'.");
    }
}