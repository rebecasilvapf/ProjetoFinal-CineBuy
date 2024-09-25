# CineBuy - Sistema de Reserva de Ingressos para Sessões de Filmes

## Descrição

O **CineBuy** é um sistema de reserva de ingressos para sessões de filmes que permite aos usuários:

- Visualizar filmes disponíveis
- Selecionar horários disponíveis
- Reservar assentos
- Gerar confirmações de reserva
- Cancelar reservas

## Funcionalidades

- 🎬 **Visualização de Filmes**: Veja todos os filmes em cartaz.
- ⏰ **Seleção de Horários**: Escolha horários disponíveis para os filmes.
- 🪑 **Reserva de Assentos**: Escolha e reserve seus assentos.
- 📩 **Geração de Confirmações**: Receba confirmação imediata da sua reserva.
- ❌ **Cancelamento de Reservas**: Cancele sua reserva se necessário.

## Tecnologias Utilizadas

- 🖥️ **C#**
- 🌐 **ASP.NET**
- 📋 **ASP.NET Web Forms**
- 🔗 **API Web ASP.NET Core**
- 🔍 **Dapper**
- 💾 **SQL Server**
- 🌍 **HTML/CSS**

## Pré-requisitos

Antes de executar o projeto, certifique-se de ter os seguintes itens instalados:

- [SQL Server Management Studio (SSMS)](https://docs.microsoft.com/sql/ssms/download-sql-server-management-studio-ssms)
- [Visual Studio 2022](https://visualstudio.microsoft.com/)

## Configuração do Banco de Dados

1. **Executar Scripts SQL:**
   - Navegue até a pasta `BancodeDados` no diretório do projeto.
   - Encontre o arquivo `QueryDbSistemaCineBuy` que contém as consultas para criar e preencher as tabelas necessárias.
   - Execute o arquivo `QueryDbSistemaCineBuy` no SQL Server Management Studio (SSMS) para criar a estrutura do banco de dados.

2. **Atualizar a String de Conexão:**
   - Abra o arquivo `appsettings.json` no seu projeto que se encontra no projeto `ApiSistemaReservaIngressos`.
   - Localize a seção `ConnectionStrings`.
   - Atualize a string de conexão para apontar para a sua instância do SQL Server.

   ```json
   "ConnectionStrings": {
     "conexao": "Server=SeuServidor;Database=DbSistemaCineBuy;Trusted_Connection=True;"
   }
   ```

   **Certifique-se de substituir a propriedade `Server` pelo nome do servidor da sua máquina.**

## Como Rodar o Projeto

1. **Clone o Repositório:**

   ```bash
   git clone https://github.com/rebecasilvapf/ProjetoFinal-CineBuy.git
   cd ProjetoFinal-CineBuy
   ```

2. **Abra o Projeto no Visual Studio:**
   - Abra o Visual Studio e selecione "Abrir projeto ou solução".
   - Navegue até o diretório clonado e selecione o arquivo `SistemaReservaIngressos.sln`.

3. **Executar o Projeto:**
   - Pressione `F5` para compilar e executar o projeto.
   - **Observação:** Certifique-se de que os dois projetos estão configurados para executar juntos.

## Documentação

A documentação completa pode ser encontrada no arquivo Documentação-CineBuy – Sistema de Reserva de Ingressos de Filmes

## Contato

Para dúvidas ou suporte, entre em contato:

- **Nome:** Rebeca Paulino Feitosa Silva
- **E-mail:** rebeca.paulino.df@gmail.com

