# 📚 BookTime

BookTime é uma plataforma de reservas (**Booking System**) desenvolvida em **.NET 8** utilizando uma arquitetura de **Microsserviços**, com foco em escalabilidade, baixo acoplamento e separação de responsabilidades.

## 🚀 Arquitetura

* Microsserviços
* Domain-Driven Design (DDD)
* Clean Architecture
* API Gateway com Ocelot
* Comunicação assíncrona com RabbitMQ + MassTransit
* Banco de dados independente para cada serviço (PostgreSQL 16)

## 🛠 Tecnologias

* .NET 8
* ASP.NET Core Web API
* Entity Framework Core
* PostgreSQL 16
* RabbitMQ
* MassTransit 7.3.1
* Ocelot API Gateway
* JWT Bearer Authentication
* Docker & Docker Compose
* Swagger / OpenAPI
* xUnit
* Moq

## 📂 Estrutura

```text
src
├── Gateways
    ├── ApiGateway
├── Libs
    ├── Communication.Http
    ├── Communication.MessageBus
├── Services
    ├── AuthService
    ├── UserService
    ├── BookingService
    ├── ResourceService
```

Cada microsserviço é organizado em camadas:

```text
API
Application
Domain
Infrastructure
```

## 🔐 Autenticação

A autenticação é realizada por meio de **JWT Bearer**, garantindo acesso seguro aos endpoints protegidos.

## 📨 Comunicação entre serviços

* **Síncrona:** REST APIs
* **Assíncrona:** RabbitMQ + MassTransit (Event-Driven)

## ▶️ Executando o projeto

```bash
docker-compose up -d
```

Ou execute os serviços individualmente:

```bash
dotnet restore
dotnet run
```

## ✅ Testes

O projeto possui uma suíte de testes automatizados para garantir a qualidade e a confiabilidade da aplicação.

* **Testes Unitários** utilizando **xUnit** e **Moq**.
* **Testes de Integração** com **xUnit**.

## 📖 Documentação

Após iniciar a aplicação, acesse o Swagger de cada serviço para visualizar e testar os endpoints disponíveis.

---

Este projeto tem como objetivo demonstrar a implementação de uma arquitetura moderna em **ASP.NET Core**, aplicando boas práticas de microsserviços, DDD, autenticação com JWT e mensageria orientada a eventos.
