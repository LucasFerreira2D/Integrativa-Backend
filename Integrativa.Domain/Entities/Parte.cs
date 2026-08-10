using Integrativa.Domain.Enums;

namespace Integrativa.Domain.Entities;

public class Parte
{
    public Guid Id { get; private set; }
    public Guid ProcessoId { get; private set; }
    public string Nome { get; private set; } = null!;
    public TipoParte TipoParte { get; private set; }
    public DateTime DataAlteracao { get; private set; }
    public string UsuarioAlteracao { get; private set; } = null!;

    private  Parte()
    {
    }

    public Parte(Guid id, Guid processoId, string nome, TipoParte tipoParte, DateTime dataAlteracao, string usuarioAlteracao)
    {
        Id = id;
        ProcessoId = processoId;
        Nome = nome;
        TipoParte = tipoParte;
        DataAlteracao = dataAlteracao;
        UsuarioAlteracao = usuarioAlteracao;
    }
}