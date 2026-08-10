namespace Integrativa.Domain.Entities;

public class Andamento
{
    public Guid Id { get; private set; }
    public Guid ProcessoId { get; private set; }
    public string Descricao { get; private set; } = null!;
    public DateTime DataCriacao { get; private set; }
    public DateTime DataAlteracao { get; private set; }
    public string UsuarioAlteracao { get; private set; } = null!;

    public Andamento()
    {
    }

    public Andamento(Guid id, Guid processoId, string descricao, DateTime dataCriacao, DateTime dataAlteracao, string usuarioAlteracao)
    {
        Id = id;
        ProcessoId = processoId;
        Descricao = descricao;
        DataCriacao = dataCriacao;
        DataAlteracao = dataAlteracao;
        UsuarioAlteracao = usuarioAlteracao;
    }
}