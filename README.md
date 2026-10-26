# TaskManagement

## Тестовое задание для Middle .NET Core + Angular Developer

Разработать простое веб-приложение для управления задачами.

## Backend

### Технологии

- .NET 8+
- ASP.NET Core Web API
- Entity Framework Core
- PostgreSQL

### Модель задачи

```csharp
public class TaskItem
{
    public Guid Id { get; set; }

    public string Title { get; set; }

    public string? Description { get; set; }

    public bool IsCompleted { get; set; }

    public DateTime CreatedAt { get; set; }
}
```

### Методы API

| Метод | Endpoint | Описание |
|---|---|---|
| GET | `/api/tasks` | Получить список задач |
| GET | `/api/tasks?isCompleted=true` | Получить задачи с указанным статусом выполнения |
| GET | `/api/tasks/{id}` | Получить задачу по идентификатору |
| POST | `/api/tasks` | Создать задачу |
| PUT | `/api/tasks/{id}` | Обновить задачу |
| DELETE | `/api/tasks/{id}` | Удалить задачу |

### Требования к backend

- Использовать Entity Framework Core.
- Использовать PostgreSQL.
- Настроить миграции базы данных.
- Использовать DTO для запросов и ответов.
- Реализовать валидацию:
  - `Title` обязателен.
  - Максимальная длина `Title` — 200 символов.
- Следовать принципам REST API.
- Использовать Dependency Injection.

## Frontend

### Технологии

- Angular 17+
- TypeScript

### Функциональность

#### Страница со списком задач

- Отображение названия задачи.
- Отображение даты создания.
- Отображение статуса выполнения.

#### Добавление новой задачи

Форма создания задачи содержит поля:

- `Title`
- `Description`

#### Действия над задачей

- Отметить задачу как выполненную.
- Удалить задачу.

### Желательные улучшения

- Обработка ошибок API.
- Индикатор загрузки.
- Базовая адаптивная вёрстка.

---
