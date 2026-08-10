using Integrativa.Domain.Enums;

namespace Integrativa.Domain.Entities;

public class Processo
{
    public Guid Id { get; private set; }
    public string Numero { get; private set; } = null!;
    public string Assunto { get; private set; } = null!;
    public DateTime DataCriacao { get; private set; }
    public StatusProcesso Status { get; private set; }
    public DateTime DataAlteracao { get; private set; }
    public string UsuarioAlteracao { get; private set; } = null!;
    
    public Processo()
    {
    }


    public Processo(Guid id, string numero, string assunto, DateTime dataCriacao, StatusProcesso status, DateTime dataAlteracao, string usuarioAlteracao)
    {
        Id = id;
        Numero = numero;
        Assunto = assunto;
        DataCriacao = dataCriacao;
        Status = status;
        DataAlteracao = dataAlteracao;
        UsuarioAlteracao = usuarioAlteracao;
    }
}