# WebAPI-Hortifruti

## Descrição

Este projeto implementa uma API REST completa para gerenciamento de frutas, seguindo o padrão MVC e arquitetura desacoplada. A solução inclui:

- CRUD completo para entidade Frutas  
- Estrutura de cache para otimização de desempenho  
- Log de exceções para monitoramento  
- Uso de Interfaces para facilitar manutenção e desacoplamento  
- Configurações centralizadas no `appsettings` e `web.config`, acessadas via `config.cs`

Este projeto é o trabalho final do curso de programação da JN Moura.

---

## Tecnologias Utilizadas

- C#  
- .NET Framework 4.3  
- SQL Server Express  
- Visual Studio 2022 (ambiente de desenvolvimento)  
- Postman (para testes das APIs)

---

## Como executar

1. Clone este repositório localmente.  
2. Abra a solução no Visual Studio 2022.  
3. Configure a connection string no arquivo `web.config` com os dados do seu banco SQL Server Express.  
4. Execute a API via IIS Express ou pelo próprio Visual Studio.  
5. Utilize o Postman ou outra ferramenta REST para testar os endpoints.

---

## Endpoints da API

| Método | Rota                 | Descrição               |
|--------|----------------------|-------------------------|
| GET    | `/api/Frutas`        | Retorna todas as frutas |
| GET    | `/api/Frutas/{id}`   | Retorna fruta por ID    |
| GET    | `/api/Frutas/{nome}` | Retorna frutas por nome |
| POST   | `/api/Frutas`        | Cadastra nova fruta     |
| PUT    | `/api/Frutas/{id}`   | Atualiza fruta existente|
| DELETE | `/api/Frutas/{id}`   | Remove fruta por ID     |

*Obs:* As rotas seguem o padrão gerado pelo controlador REST MVC.

---

## Licença

Este projeto está disponível sob uma licença livre, permitindo uso e modificações sem restrições.

---

## Contato

Desenvolvedor: Rodrigo Demarque da Silva  
Email: rodrigodemarque@gmail.com  
LinkedIn: [linkedin.com/in/rodrigodemarque](https://linkedin.com/in/rodrigodemarque)

---

## Contribuições

Contribuições são bem-vindas! Sinta-se à vontade para abrir issues, enviar pull requests e sugerir melhorias.

---
