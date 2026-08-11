using Integrativa.Domain.Enums;

namespace Integrativa.Domain.Entities;

public class Processo
{
    private readonly List<Parte> _partes = new List<Parte>();
    private readonly List<Andamento> _andamentos = new List<Andamento>();
    public Guid Id { get; private set; }
    public string Numero { get; private set; } = null!;
    public string Assunto { get; private set; } = null!;
    public DateTime DataCriacao { get; private set; }
    public StatusProcesso Status { get; private set; }
    public DateTime DataAlteracao { get; private set; }
    public string UsuarioAlteracao { get; private set; } = null!;
    public IReadOnlyCollection<Parte> Partes { get { return _partes.AsReadOnly(); } }
    public IReadOnlyCollection<Andamento> Andamentos { get { return _andamentos.AsReadOnly(); } }

    private Processo()
    {
    }

    private Processo(Guid id, string numero, string assunto, DateTime dataCriacao, StatusProcesso status)
    {
        Id = id;
        Numero = numero;
        Assunto = assunto;
        DataCriacao = dataCriacao;
        Status = status;
    }

    public static Processo Criar(string numero, string assunto, StatusProcesso status)
    {
        return new Processo(Guid.NewGuid(), numero, assunto, DateTime.UtcNow, status);
    }

    public void Atualizar(string numero, string assunto, StatusProcesso status)
    {
        Numero = numero;
        Assunto = assunto;
        Status = status;
    }

    public void RegistrarAlteracao(string usuario)
    {
        DataAlteracao = DateTime.UtcNow;
        UsuarioAlteracao = usuario;
    }

    public Parte AdicionarParte(string nome, TipoParte tipo, string usuario)
    {
        Parte parte = Parte.Criar(Id, nome, tipo, usuario);
        _partes.Add(parte);
        return parte;
    }

    public void RemoverParte(Guid parteId)
    {
        Parte? parte = _partes.FirstOrDefault(p => p.Id == parteId);

        if (parte != null)
            _partes.Remove(parte);
    }

    public Andamento AdicionarAndamento(DateTime data, string descricao, string usuario)
    {
        Andamento andamento = Andamento.Criar(Id, data, descricao, usuario);
        _andamentos.Add(andamento);
        return andamento;
    }

    public void RemoverAndamento(Guid andamentoId)
    {
        Andamento? andamento = _andamentos.FirstOrDefault(a => a.Id == andamentoId);

        if (andamento != null)
            _andamentos.Remove(andamento);
    }
}