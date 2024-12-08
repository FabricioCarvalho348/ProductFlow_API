<div>
    <p align="center">
      <img src="https://img.shields.io/badge/ProductFlow-API-purple" height="130" alt="product-flow-api">
    </p>
</div>

![Status de Desenvolvimento](https://img.shields.io/badge/Status-Concluido-green)

# Sobre
<p>Projeto de uma API RESTful, que tem como objetivo resolver o problema de você cadastrar, deletar, atualizar, listar produtos e filtar pelo estoque. Além disso tem fluxo para o usuário como se registar, logar, deletar e atualizar dados.</p>

# Exemplo de Requisição
```
GET https://localhost:44379/api/Products
```

# Exemplo de Json

- Cadastro de produto
```json
{
  "name": "Celular",
  "status": 1,
  "price": 1500,
  "stockQuantity": 10,
  "categories": [
    0
  ]
}
```

Para verificar quais informações são válidas visite a camada de Dominio da aplicação.

# Todos os Endpoints

![imagem](/docs/endpoints-api.jpg)

# Tecnologias

<div>
  <img src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/csharp/csharp-original.svg" height="30" alt="csharp logo"  />
  <img src="https://skillicons.dev/icons?i=dotnet" height="30" alt="dot-net logo"  />
<img src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/microsoftsqlserver/microsoftsqlserver-plain.svg" height="30" alt="microsoftsqlserver logo"  />
</div>

# Executar o projeto

Instale alguma das IDEs para o desenvolvimento .NET
temos o Rider e o Visual Studio
[link para o Rider](https://www.jetbrains.com/pt-br/rider/)
[link para o Visual Studio](https://visualstudio.microsoft.com/pt-br/downloads/)

1. Tenha o .NET 8 Instalado

para instalar o .NET tem o link diretamente da microsoft [aqui](https://dotnet.microsoft.com/pt-br/download).

2. Clone o repositório

    ```
    git@github.com:FabricioCarvalho348/ProductFlow_API.git
    ```
3. String de conexão e Signing Key do JWT
      Dentro de: appsettings.Development.json, coloque sua string de conexão para o Banco de Dados SQL Server

Caso não tenha o SQL Server instalado, tem o link da documentação [aqui](https://learn.microsoft.com/pt-br/sql/database-engine/install-windows/install-sql-server?view=sql-server-ver16).

SigningKey: você precisar gerar uma signingkey que deve ter 32 caracteres também deve ser colocada em appsettings.Development.json, para gerar recomendo esse site [aqui](https://www.lastpass.com/pt/features/password-generator).

Obs: Coloque no número de caracteres 32.

### Com isso feito pode executar o projeto, mas pode dar erro nas migrations, com isso deixo os passos seguinte:

4. Migrations

- Caso o seu banco de dados não tenha sido criado execute os seguintes comandos dentro do projeto ProductFlow.Infrastructure

Primeiro comando:
```
dotnet ef migrations add InitialMigration -o Migrations -s ../ProductFlow.API
```

Segundo Comando:
```
dotnet ef database update -s ../ProductFlow.API
```
      
Com tudo isso feito, realize a execução do projeto, clicando no botão Run da sua IDE.
Pode utilizar o terminal também para verificar se está tudo funcionando corretamente e executar utilize os seguintes comandos:
### Antes de executar o projeto e testar os endpoints leia o próximo tópico.
```
dotnet clean
```
```
dotnet build
```
```
dotnet run
```

# (IMPORTANTE) Autorizações dos Endpoints de Produtos
Atualmente no projeto os endpoints estão bloqueados da seguinte maneira
Usuários com a Role membro pode fazer as consultas (GETs na aplicação) nos produtos.
Usuários com a Role Manager ou Employee podem Registrar e Atualizar produtos.
Usuários com a Role Manager podem deletar produtos.

## IMPORTANTE: O usuário vem com a Role Membro por padrão, não tem mudança de role ao se cadastrar, então isso tem que ser mudado dentro do banco de dados, sabemos que isso não é uma prática que deve ser realizada, mas para executar os endpoints normalmente faça isso.

# Desenvolvedor

<a href="https://www.linkedin.com/in/inacio-fabricio-carvalho/"><img src="https://media.licdn.com/dms/image/v2/D4D03AQE8bq-qVWrQtg/profile-displayphoto-shrink_800_800/profile-displayphoto-shrink_800_800/0/1704545822952?e=1739404800&v=beta&t=XE95hR5WyuvzNqPoWPYRUiNINoL6EDKl_JqqVtTPX5Y" alt="Foto do Fabricio Carvalho" width="115"/></a> |
|:-:
<a href="https://www.linkedin.com/in/inacio-fabricio-carvalho/">Fabricio Carvalho</a> |
