# Sistema de Gestão de Processos

API REST para cadastrar e acompanhar processos, com as partes envolvidas (interessadas e contrárias) e o histórico de andamentos de cada um.

Este repositório tem só o backend. A interface web está em [Integrativa-Frontend](https://github.com/LucasFerreira2D/Integrativa-Frontend).

## Stack

- .NET 10 / C#
- ASP.NET Core Web API
- Entity Framework Core 10
- PostgreSQL

## Como rodar

Você vai precisar do SDK do .NET 10, do PostgreSQL rodando e da ferramenta `dotnet-ef`:

```bash
dotnet tool install --global dotnet-ef
```

Clone o projeto e restaure as dependências:

```bash
git clone https://github.com/LucasFerreira2D/Integrativa-Backend.git
cd Integrativa-Backend
dotnet restore
```

Crie o banco e o usuário (os mesmos valores que estão no `appsettings.json`):

```sql
CREATE USER integrativa WITH PASSWORD 'integrativa';
CREATE DATABASE integrativa OWNER integrativa;
```

Se preferir subir o Postgres em container:

```bash
docker run --name integrativa-db -e POSTGRES_DB=integrativa \
  -e POSTGRES_USER=integrativa -e POSTGRES_PASSWORD=integrativa \
  -p 5432:5432 -d postgres:16
```

Usando outras credenciais, ajuste a connection string `ConnectionStrings:Default` em `Integrativa.Api/appsettings.json`. A aplicação não sobe sem ela.

Crie as tabelas com as migrations:

```bash
dotnet ef database update --project Integrativa.Infrastructure --startup-project Integrativa.Api
```

E rode a API:

```bash
dotnet run --project Integrativa.Api
```

Ela sobe em `http://localhost:5122` (ou `https://localhost:7122` com o perfil `https`). Para testar:

```bash
curl http://localhost:5122/api/processos
```

## Organização do código

A solução tem quatro projetos:

- **Integrativa.Domain**: as entidades (`Processo`, `Parte`, `Andamento`) e os enums. Não depende de nada.
- **Integrativa.Application**: o `ProcessoService` com as regras, os DTOs de entrada e saída e as interfaces de repositório.
- **Integrativa.Infrastructure**: o `AppDbContext`, os mapeamentos do EF, as migrations e a implementação dos repositórios.
- **Integrativa.Api**: os controllers, o tratamento de erros e o registro das dependências.

`Processo` é o agregado: as propriedades têm setter privado, a criação passa por `Processo.Criar(...)` e as coleções são expostas como somente leitura. Partes e andamentos só são adicionados ou removidos pelos métodos do próprio processo, então não dá pra deixar o objeto em estado inválido de fora.

As consultas de listagem e de detalhe projetam direto para DTO com `AsNoTracking()`. Só a escrita carrega a entidade rastreada.

## Banco

Três tabelas: `processos`, `partes` e `andamentos`. As duas últimas apontam para `processos` com `ON DELETE CASCADE`, então excluir um processo leva junto suas partes e andamentos.

O número do processo tem índice único. Os andamentos têm índice composto em `(ProcessoId, DataCriacao)`, porque a tela de detalhes sempre lista do mais recente para o mais antigo e a ordenação vem do banco.

Os enums são gravados como texto (`Ativo`, `Interessada`, ...) em vez de número: o dump do banco continua legível e mudar a ordem do enum no código não estraga os dados. As datas são todas UTC (`timestamptz`).

## Endpoints

Processos:

- `GET /api/processos`: lista paginada, com filtros
- `GET /api/processos/{id}`: detalhe, já com partes e andamentos
- `POST /api/processos`: cria, retorna 201
- `PUT /api/processos/{id}`: atualiza número, assunto e status
- `DELETE /api/processos/{id}`: exclui

Partes e andamentos:

- `POST /api/processos/{id}/partes`
- `DELETE /api/processos/{id}/partes/{parteId}`
- `POST /api/processos/{id}/andamentos`
- `DELETE /api/processos/{id}/andamentos/{andamentoId}`

Na listagem dá pra filtrar por `status` (`Ativo`, `Finalizado` ou `Arquivado`) e por `numero` (busca parcial, ignorando maiúsculas), além de `page` e `pageSize` (padrão 1 e 10, máximo de 100 por página):

```bash
curl "http://localhost:5122/api/processos?status=Ativo&numero=0001&page=1&pageSize=10"
```

A resposta traz os itens junto com `page`, `pageSize`, `totalItems` e `totalPages`.

Criando um processo:

```bash
curl -X POST http://localhost:5122/api/processos \
  -H "Content-Type: application/json" \
  -d '{"numero":"0001234-56.2026.8.26.0100","assunto":"Ação de cobrança","status":"Ativo"}'
```

Adicionando uma parte e um andamento:

```bash
curl -X POST http://localhost:5122/api/processos/{id}/partes \
  -H "Content-Type: application/json" \
  -d '{"nome":"João da Silva","tipo":"Interessada"}'

curl -X POST http://localhost:5122/api/processos/{id}/andamentos \
  -H "Content-Type: application/json" \
  -d '{"data":"2026-08-11T14:00:00Z","descricao":"Petição inicial protocolada"}'
```

Os enums vão e voltam sempre pelo nome, tanto no corpo do JSON quanto na query string: `status` aceita `Ativo`, `Finalizado` ou `Arquivado`, e `tipo` aceita `Interessada` ou `Contraria`. Quem cuida do corpo é o `JsonStringEnumConverter` registrado no `Program.cs`; na query string, o model binder já convertia por nome desde sempre.

No detalhe do processo os andamentos vêm do mais recente para o mais antigo e as partes em ordem alfabética.

## Erros

Todo erro sai no formato `ProblemDetails`. Payload inválido dá 400 (a validação é por Data Annotations nos requests, com o 400 gerado automaticamente pelo `[ApiController]`); recurso inexistente dá 404; número de processo repetido dá 409, e essa unicidade é garantida tanto no serviço quanto por índice único no banco. Qualquer falha não prevista cai no handler global, que loga o stack trace e devolve 500.

## O que ficou de fora

- Testes automatizados
- Dockerfile / docker-compose
- Swagger (o contrato está descrito aqui mesmo)
- CORS, que precisa ser configurado no `Program.cs` para um frontend em outra origem conseguir consumir a API. Em desenvolvimento o frontend não precisa: o dev server do Angular tem um proxy que repassa `/api` para cá, então o browser enxerga tudo na mesma origem
- Autenticação: os campos de auditoria (`DataAlteracao` e `UsuarioAlteracao`) já existem nas tabelas, mas hoje o usuário é fixo (`"Sistema"`)
