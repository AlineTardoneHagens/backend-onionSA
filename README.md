# OnionSA

## Descrição

O projeto OnionSA é uma aplicação dividida em três camadas distintas para seguir a arquitetura limpa e separar as responsabilidades de forma organizada. 
Este README fornece uma visão geral do projeto e instruções sobre como rodá-lo.

O projeto é dividido em três partes principais:

1. **OnionSA.API**: Contém a implementação da API, incluindo os controladores e configuração do Swagger.
2. **OnionSA.Domain**: Contém as entidades, serviços e interfaces de domínio.
3. **OnionSA.Database**: Contém o contexto do banco de dados e configurações relacionadas.

## Estrutura do Projeto

### 1. OnionSA.API
- **Responsabilidade**: Fornece a interface de comunicação com a aplicação através de endpoints HTTP.
- **Tecnologias**: ASP.NET Core Web API, Swagger para documentação da API.

### 2. OnionSA.Domain
- **Responsabilidade**: Define as regras de negócios e lógica da aplicação.
- **Tecnologias**: C# e .NET.

### 3. OnionSA.Database
- **Responsabilidade**: Gerencia a persistência de dados e acesso ao banco de dados.
- **Tecnologias**: Entity Framework Core com banco de dados em memória para testes e desenvolvimento.

## Pré-requisitos

- .NET 6.0 ou superior
- Visual Studio 2022 ou superior (opcional, mas recomendado)
- Ferramenta de linha de comando `dotnet` (se não estiver usando um IDE)

## Como Rodar o Projeto

### 1. Clone o Repositório

``
git clone https://github.com/AlineTardoneHagens/backend-onionSA.git
``

``
cd onionsa
``

### 2. Restaurar Dependências
Navegue para o diretório do projeto OnionSA.API e restaure as dependências.

``
cd OnionSA.API
``

``
dotnet restore
``
### 3. Executar a Aplicação
Para executar o projeto e iniciar a API, use o comando:

``
dotnet run``

A API estará disponível em http://localhost:5087 por padrão. Você pode acessar a documentação do Swagger em http://localhost:5087/swagger/index.html.
