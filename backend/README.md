# TaskManagement Backend

Веб-приложение для управления задачами — серверная часть (backend).

## Технологии

- .NET 10
- ASP.NET Core Web API
- C#
- Entity Framework Core
- PostgreSQL
- MediatR
- FluentValidation
- xUnit — unit-тесты

## Архитектура проекта

### TaskManagement.Api

Внешний HTTP-слой приложения:

- Контроллеры и обработка HTTP-запросов.
- API-контракты.
- Обработка исключений.

### TaskManagement.Application

Прикладная логика:

- Команды и запросы для работы с задачами.
- Обработчики MediatR.
- DTO.
- Интерфейсы репозиториев.
- Валидация входных данных.

### TaskManagement.Domain

Доменная модель и бизнес-правила.

### TaskManagement.Infrastructure

Инфраструктурные реализации:

- Entity Framework Core.
- Контекст базы данных.
- Миграции.
- Репозитории.

## Настройка базы данных

Перед запуском API необходимо настроить строку подключения к PostgreSQL.

Пример конфигурации в `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=task_management;Username=postgres;Password=postgres"
  }
}
```



## Запуск API

Выполните команды из каталога `backend`:

```powershell
dotnet restore .\TaskManagement.slnx
dotnet build .\TaskManagement.slnx
dotnet run --project .\src\TaskManagement.Api\TaskManagement.Api.csproj
```

## Миграции Entity Framework Core

Для применения существующих миграций используйте EF Core CLI:

```powershell
dotnet ef database update `
  --project .\src\TaskManagement.Infrastructure\TaskManagement.Infrastructure.csproj `
  --startup-project .\src\TaskManagement.Api\TaskManagement.Api.csproj
```

Для выполнения команды могут потребоваться:

- Установленный инструмент `dotnet-ef`.
- Пакет `Microsoft.EntityFrameworkCore.Design` в соответствующем проекте.
- Настроенное подключение к PostgreSQL.

Перед применением миграций убедитесь, что строка подключения задана корректно.

## Unit-тесты

Тестовый проект находится в каталоге `tests/TaskManagement.UnitTests`.

В нём размещаются тесты доменной модели и прикладной логики, не требующие реальной базы данных.

Команда запуска тестов:

```powershell
dotnet test .\tests\TaskManagement.UnitTests\TaskManagement.UnitTests.csproj
```

Перед запуском убедитесь, что тестовый проект содержит необходимые пакеты и компилируется.

