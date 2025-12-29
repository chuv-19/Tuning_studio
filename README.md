# Tuning Studio

**WPF-приложение для заказов в тюнинг-ателье**  
Автоматизация заказов: регистрация, выбор услуг, оформление заказов, отзывы.

---

## Функции
- 🔐 **Регистрация** — создание аккаунта с валидацией  
- 🔑 **Авторизация** — вход в систему  
- 🛒 **Оформление заказа** — выбор услуг и создание заказа  
- ⭐ **Отзывы** — добавление отзыва с рейтингом  


---

## Стек технологий
| Компонент      | Технология                |
|----------------|---------------------------|
| Frontend       | WPF (XAML/C#)             |
| Backend        | .NET 6                    |
| Database       | SQLite + EF Core           |
| Архитектура    | MVVM                      |
| Тестирование   | xUnit, Moq                |
| Контроль версий | Git, GitHub               |

---

## База данных
```sql
CREATE TABLE Users (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    FullName TEXT NOT NULL,
    Phone TEXT NOT NULL,
    Email TEXT NOT NULL UNIQUE,
    PasswordHash TEXT NOT NULL,
    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE Orders (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    UserId INTEGER NOT NULL,
    ServiceName TEXT NOT NULL,
    Status TEXT DEFAULT 'pending',
    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (UserId) REFERENCES Users(Id)
);

CREATE TABLE Reviews (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    UserId INTEGER NOT NULL,
    Rating INTEGER CHECK(Rating BETWEEN 1 AND 5),
    Comment TEXT,
    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (UserId) REFERENCES Users(Id)
);

## Быстрый запуск
git clone https://github.com/yourusername/OrderTuningStudio.git
cd OrderTuningStudio
dotnet restore
dotnet run --project src/OrderTuningStudio.Client


## Сборка релиза:

dotnet publish -c Release -o ./publish

## Тесты

Модульные: 85%

Интеграционные: 70%

UI: 50%

dotnet test tests/UnitTests/
dotnet test tests/IntegrationTests/

## Архитектура
WPF View (UI)
      │
      ▼
ViewModel (логика UI)
      │
      ▼
Model (бизнес-логика)
      │
      ▼
Сервисы / Репозитории / DB
