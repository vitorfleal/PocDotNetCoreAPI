## Instruções

Necessário antes de rodar a aplicação, abrir o projeto e executar os commandos da migration no Package Manager Console para criar o banco de dados e as tabelas. Segue os comandos abaixo:

## Comando para criar migration: 

add-migration nomeDaMigration. 
Exemplo: add-migration primeiraMigration</p>

## Comando para criar bancos e tabelas: 

update-database

A connection atring está configurada para o banco de dados local. Segue a connection string: 
"Server=CBYK-88;Database=healthsystem;Uid=sa;Pwd=serverdev01;MultipleActiveResultSets=Truee"

A connection string tem que ser alterada apontando para sua base de dados SQL Server local, no meu caso está no modelo de autenticação do SQL Server.

Caso a sua autenticação seja do Windows usar a connection string abaixo fazendo o apontamento para a instância do seu servidor local informando a base de dados healthsystem
"Server=SeuServidor.;Database=healthsystem;Trusted_Connection=True;MultipleActiveResultSets=true"

Caso não queria criar o banco de dados via migrations, rodar o script sql.

## Endpoints Serviço de Paciente
            
Funções: Obter Consultas por Paciente, Cadastrar, Atualizar e Excluir Paciente 
            
## Endpoints Serviço de Médico

Funções: Obter Médico por Id, Obter Médicos, Cadastrar, Atualizar e Excluir Médico

## Endpoints Serviço de Consulta

Funções: Obter Consultas por nome do Médico e Agendar Consulta

A API foi desenvolvida com o intuito de servir como referência no desenvolvimento de soluções com DotNET Core.
A mesma contempla as seguintes tecnologias:
1. DotNET Core 3.1
2. EntityFramework Core
3. Dapper
4. FluentValidation
5. Swagger
6. AutoMapper
7. Serilog

No desenvolvimento foi utilizado o padrão de modelagem de software orientado a objetos Domain Driven Design, também contempla principios SOLID e Clean Architecture
O projeto contém uma camada de testes unitário com XUnit e os testes integrados está em desenvolvimento

Qualquer dúvida enviar um e-mail para vitorfleal@hotmail.com

