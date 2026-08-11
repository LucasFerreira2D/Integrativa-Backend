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

    private Parte()
    {
    }

    private Parte(Guid id, Guid processoId, string nome, TipoParte tipoParte, string usuarioAlteracao)
    {
        Id = id;
        ProcessoId = processoId;
        Nome = nome;
        TipoParte = tipoParte;
        DataAlteracao = DateTime.UtcNow;
        UsuarioAlteracao = usuarioAlteracao;
    }

    public static Parte Criar(Guid processoId, string nome, TipoParte tipoParte, string usuario)
    {
        return new Parte(Guid.NewGuid(), processoId, nome, tipoParte, usuario);
    }
}