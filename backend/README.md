\# TaskManagement Backend



веб-приложение для управления задачами



backend

\## Технологии



\- .NET 10

\- ASP.NET Core Web API

\- C#

\- Entity Framework Core

\- PostgreSQL

\- MediatR

\- FluentValidation

\- xUnit — unit-тесты



\### TaskManagement.Api



Внешний HTTP-слой приложения:



\- контроллеры и обработка HTTP-запросов;

\- API-контракты;

\- обработка исключений;





\### TaskManagement.Application



\- команды и запросы для работы с задачами;

\- обработчики MediatR;

\- DTO;

\- интерфейсы репозиториев;

\- валидация входных данных.



\### TaskManagement.Domain



Доменная модель и бизнес-правила



\### TaskManagement.Infrastructure



Инфраструктурные реализации:



\- Entity Framework Core;

\- контекст базы данных;

\- миграции;

\- репозитории.







\## Настройка базы данных



Перед запуском API необходимо настроить строку подключения к PostgreSQL.



Пример в appsettings.json

&#x20; "ConnectionStrings": {

&#x20;   "DefaultConnection": "Host=localhost;Port=5432;Database=task\_management;Username=postgres;Password=postgres"

&#x20; }





\## Запуск API



Из каталога `backend`:



```powershell

dotnet restore .\\TaskManagement.slnx

dotnet build .\\TaskManagement.slnx

dotnet run --project .\\src\\TaskManagement.Api\\TaskManagement.Api.csproj

```





\## Миграции Entity Framework Core



Для создания базы данных и применения миграций используйте EF Core CLI.



Пример команды применения существующих миграций:



```powershell

dotnet ef database update `

&#x20; --project .\\src\\TaskManagement.Infrastructure\\TaskManagement.Infrastructure.csproj `

&#x20; --startup-project .\\src\\TaskManagement.Api\\TaskManagement.Api.csproj

```



Для этой команды может потребоваться пакет `Microsoft.EntityFrameworkCore.Design` в startup-проекте и установленный инструмент `dotnet-ef`. Конкретную конфигурацию следует проверить в `.csproj`.



\## Unit-тесты



Тестовый проект находится в `tests/TaskManagement.UnitTests`.



В нём размещаются тесты доменной модели и прикладной логики, не требующие реальной базы данных.



Запуск тестов после завершения настройки:



```powershell

dotnet test .\\tests\\TaskManagement.UnitTests\\TaskManagement.UnitTests.csproj

```





