# Hotel Management System

Полнофункциональное веб-приложение для управления отелем с системой бронирования, управлением номерами, клиентами и платежами.

## Содержание

- [Введение](#введение)
- [Возможности](#возможности)
- [Технологический стак](#технологический-стак)
- [Требования](#требования)
- [Установка](#установка)
- [Конфигурация](#конфигурация)
- [Запуск приложения](#запуск-приложения)
- [Структура проекта](#структура-проекта)
- [API Документация](#api-документация)
- [Руководство пользователя](#руководство-пользователя)
- [Безопасность](#безопасность)

## Введение

Hotel Management System представляет собой комплексное решение для автоматизации управления гостиничным бизнесом. Система предоставляет инструменты для управления клиентами, номерами, бронированиями, проживанием, услугами, платежами и формирования отчетов.

## Возможности

### Управление клиентами
- Добавление и редактирование информации клиентов
- Хранение паспортных данных и контактной информации
- Просмотр истории бронирований клиента

### Управление номерами
- Управление каталогом номеров
- Различные типы номеров (Single, Double, Suite)
- Отслеживание статуса номеров (Available, Occupied, Maintenance)
- Установление цен за ночь

### Система бронирования
- Создание и отмена бронирований
- Проверка доступности номеров
- Управление датами заезда и выезда

### Управление проживанием
- Регистрация проживания гостей
- Добавление услуг к проживанию
- Расчет общей стоимости

### Управление услугами
- Каталог дополнительных услуг (Завтрак, Парковка, Спа, Трансфер)
- Управление ценами на услуги
- Добавление услуг к проживанию

### Управление платежами
- Регистрация платежей
- Различные способы оплаты
- Отслеживание статуса платежей

### Отчетность
- Общая статистика по бронированиям
- Информация о завершенных проживаниях
- Расчет общей выручки
- Информация об занятых и свободных номерах

### Система аутентификации
- JWT токены
- Роли доступа (Admin, Manager, Staff)
- Защита маршрутов

## Технологический стак

### Backend
- Язык программирования: C# (.NET 8)
- Фреймворк: ASP.NET Core
- ORM: Entity Framework Core
- База данных: PostgreSQL
- Аутентификация: JWT (JSON Web Tokens)
- API Документация: Swagger/OpenAPI
- Маппинг объектов: AutoMapper
- Хеширование паролей: BCrypt.Net

### Frontend
- Язык программирования: JavaScript (ES6+)
- Фреймворк: React 18
- Маршрутизация: React Router v6
- HTTP клиент: Axios
- Стилизация: Tailwind CSS
- Управление состоянием: React Context API

## Требования

### Для Backend
- .NET 8 SDK или выше
- PostgreSQL 12 или выше

### Для Frontend
- Node.js 16 или выше
- npm или yarn

## Установка

### 1. Клонирование репозитория

```bash
git clone https://github.com/favesle/Coursework.git
cd Coursework
```

### 2. Установка Backend зависимостей

```bash
cd backend
dotnet restore
```

### 3. Установка Frontend зависимостей

```bash
cd frontend
npm install
```

## Конфигурация

### Backend - appsettings.json

Файл `backend/appsettings.json` содержит конфигурацию приложения:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=hotel_db;Username=hotel_user;Password=1234"
  },
  "Jwt": {
    "Key": "supersecretkeywithatleast32characterslong!!!",
    "ExpireDays": 7
  },
  "AllowedHosts": "*"
}
```

Измените параметры подключения к PostgreSQL согласно вашей конфигурации:
- Host: адрес сервера базы данных
- Database: имя базы данных
- Username: пользователь базы данных
- Password: пароль базы данных
- Jwt:Key: секретный ключ для подписания JWT токенов (минимум 32 символа)

### Frontend - конфигурация API

Файл `frontend/src/services/api.js` содержит конфигурацию подключения к API сервису. По умолчанию настроено подключение к http://localhost:5000. При необходимости измените адрес сервера.

## Запуск приложения

### Запуск Backend

```bash
cd backend
dotnet run
```

API будет доступен по адресу: http://localhost:5000
Документация Swagger UI: http://localhost:5000/swagger

### Запуск Frontend

```bash
cd frontend
npm start
```

Приложение будет доступно по адресу: http://localhost:3000

### Тестовые учетные данные для входа

Администратор:
- Логин: admin
- Пароль: admin123

Менеджер:
- Логин: manager
- Пароль: manager123

Персонал:
- Логин: staff
- Пароль: staff123

## Структура проекта

```
Coursework/
├── backend/
│   ├── Controllers/                    API контроллеры
│   │   ├── AuthController.cs           Аутентификация
│   │   ├── ClientsController.cs        Управление клиентами
│   │   ├── RoomsController.cs          Управление номерами
│   │   ├── BookingsController.cs       Управление бронированиями
│   │   ├── StaysController.cs          Управление проживанием
│   │   ├── ServicesController.cs       Управление услугами
│   │   ├── PaymentsController.cs       Управление платежами
│   │   └── ReportsController.cs        Формирование отчетов
│   │
│   ├── DTOs/                           Data Transfer Objects
│   │   ├── ClientDto.cs
│   │   ├── RoomDto.cs
│   │   ├── BookingDto.cs
│   │   ├── StayDto.cs
│   │   ├── ServiceDto.cs
│   │   ├── PaymentDto.cs
│   │   ├── AuthResponseDto.cs
│   │   └── ReportDto.cs
│   │
│   ├── Models/                         Модели данных
│   │   ├── Client.cs
│   │   ├── Room.cs
│   │   ├── Booking.cs
│   │   ├── Stay.cs
│   │   ├── StayService.cs
│   │   ├── Service.cs
│   │   ├── Payment.cs
│   │   ├── User.cs
│   │   └── Log.cs
│   │
│   ├── Data/
│   │   ├── ApplicationDbContext.cs     Entity Framework Core контекст
│   │   └── DbInitializer.cs            Инициализация базы данных
│   │
│   ├── Repositories/                   Слой доступа к данным
│   │   ├── IClientRepository.cs
│   │   ├── ClientRepository.cs
│   │   ├── IRoomRepository.cs
│   │   ├── RoomRepository.cs
│   │   ├── IBookingRepository.cs
│   │   ├── BookingRepository.cs
│   │   ├── IStayRepository.cs
│   │   ├── StayRepository.cs
│   │   ├── IServiceRepository.cs
│   │   ├── ServiceRepository.cs
│   │   ├── IPaymentRepository.cs
│   │   ├── PaymentRepository.cs
│   │   ├── ILogRepository.cs
│   │   ├── LogRepository.cs
│   │   ├── IUserRepository.cs
│   │   └── UserRepository.cs
│   │
│   ├── Services/                       Слой бизнес-логики
│   │   ├── IAuthService.cs
│   │   ├── AuthService.cs
│   │   ├── IClientService.cs
│   │   ├── ClientService.cs
│   │   ├── IRoomService.cs
│   │   ├── RoomService.cs
│   │   ├── IBookingService.cs
│   │   ├── BookingService.cs
│   │   ├── IStayService.cs
│   │   ├── StayService.cs
│   │   ├── IServiceService.cs
│   │   ├── ServiceService.cs
│   │   ├── IPaymentService.cs
│   │   ├── PaymentService.cs
│   │   ├── IReportService.cs
│   │   ├── ReportService.cs
│   │   ├── ILogService.cs
│   │   └── LogService.cs
│   │
│   ├── Middleware/                     Middleware компоненты
│   │   └── ErrorHandlingMiddleware.cs  Обработка ошибок
│   │
│   ├── Mappings/                       AutoMapper профили
│   │   └── MappingProfile.cs
│   │
│   ├── Program.cs                      Конфигурация приложения
│   ├── appsettings.json                Конфигурация
│   └── WeatherForecast.cs
│
├── frontend/
│   ├── public/
│   │   └── index.html                  HTML шаблон
│   │
│   ├── src/
│   │   ├── components/                 Переиспользуемые компоненты
│   │   │   └── PrivateRoute.js         Защита маршрутов
│   │   │
│   │   ├── context/                    React Context для управления состоянием
│   │   │   └── AuthContext.js          Контекст аутентификации
│   │   │
│   │   ├── layouts/                    Макеты страниц
│   │   │   └── Layout.js               Основной макет приложения
│   │   │
│   │   ├── pages/                      Страницы приложения
│   │   │   ├── LoginPage.js            Страница входа
│   │   │   ├── Dashboard.js            Главный экран
│   │   │   ├── ClientsPage.js          Управление клиентами
│   │   │   ├── RoomsPage.js            Управление номерами
│   │   │   ├── BookingsPage.js         Управление бронированиями
│   │   │   ├── StaysPage.js            Управление проживанием
│   │   │   ├── ServicesPage.js         Управление услугами
│   │   │   ├── PaymentsPage.js         Управление платежами
│   │   │   └── ReportsPage.js          Отчеты и статистика
│   │   │
│   │   ├── services/                   API сервисы
│   │   │   └── api.js                  Конфигурация Axios
│   │   │
│   │   ├── App.js                      Главный компонент приложения
│   │   ├── index.js                    Точка входа приложения
│   │   └── index.css                   Глобальные стили
│   │
│   ├── package.json                    Зависимости проекта
│   └── tailwind.config.js              Конфигурация Tailwind CSS
│
└── README.md                           Документация проекта
```

## API Документация

### Эндпоинты аутентификации

POST /api/auth/login
Аутентификация пользователя и получение JWT токена.

### Эндпоинты управления клиентами

GET /api/clients - Получить список всех клиентов
GET /api/clients/{id} - Получить информацию о конкретном клиенте
POST /api/clients - Создать нового клиента
PUT /api/clients/{id} - Обновить информацию о клиенте
DELETE /api/clients/{id} - Удалить клиента

### Эндпоинты управления номерами

GET /api/rooms - Получить список всех номеров
GET /api/rooms/{id} - Получить информацию о конкретном номере
POST /api/rooms - Создать новый номер
PUT /api/rooms/{id} - Обновить информацию о номере
DELETE /api/rooms/{id} - Удалить номер
PUT /api/rooms/{id}/status - Обновить статус номера

### Эндпоинты управления бронированиями

GET /api/bookings - Получить список всех бронирований
GET /api/bookings/{id} - Получить информацию о конкретном бронировании
POST /api/bookings - Создать новое бронирование
DELETE /api/bookings/{id} - Отменить бронирование

### Эндпоинты управления проживанием

GET /api/stays - Получить список всех проживаний
GET /api/stays/{id} - Получить информацию о конкретном проживании
POST /api/stays - Создать новое проживание
PUT /api/stays/{id} - Обновить информацию о проживании
POST /api/stays/{id}/services - Добавить услугу к проживанию

### Эндпоинты управления услугами

GET /api/services - Получить список всех услуг
GET /api/services/{id} - Получить информацию о конкретной услуге
POST /api/services - Создать новую услугу
PUT /api/services/{id} - Обновить информацию об услуге
DELETE /api/services/{id} - Удалить услугу

### Эндпоинты управления платежами

GET /api/payments - Получить список всех платежей
POST /api/payments - Создать новый платеж

### Эндпоинты отчетности

GET /api/reports - Получить статистику по системе

Полная интерактивная документация доступна в Swagger UI по адресу http://localhost:5000/swagger после запуска backend приложения.

## Руководство пользователя

### Начало работы

1. Запустите backend приложение выполнив команду dotnet run в папке backend
2. Запустите frontend приложение выполнив команду npm start в папке frontend
3. Откройте веб-браузер и перейдите по адресу http://localhost:3000
4. Выполните вход с использованием одной из тестовых учетных данных

### Основные операции

Добавление нового клиента: Перейдите в раздел "Клиенты", нажмите кнопку "Добавить клиента", заполните форму и сохраните.

Управление номерами: Перейдите в раздел "Номера", просмотрите список номеров, отредактируйте или добавьте новые номера.

Создание бронирования: Перейдите в раздел "Бронирования", выберите клиента и номер, укажите даты заезда и выезда.

Регистрация проживания: Перейдите в раздел "Проживание", создайте новое проживание на основе бронирования.

Добавление услуг: В раздел проживания добавьте дополнительные услуги.

Регистрация платежей: Перейдите в раздел "Платежи", введите данные платежа и сохраните.

Просмотр отчетов: Перейдите в раздел "Отчеты" для просмотра статистики и анализа данных.

## Безопасность

Приложение реализует следующие меры безопасности:

- JWT аутентификация для защиты API эндпоинтов
- Ролевой контроль доступа (RBAC) для различных типов пользователей
- CORS защита для предотвращения несанкционированного доступа с других доменов
- Хеширование паролей с использованием BCrypt алгоритма
- Защита маршрутов на frontend приложении с использованием PrivateRoute компонента
- Обработка ошибок и валидация входных данных

## Информация об авторе

Автор: Basil (favesle)

Дата создания: 2026

Это учебный проект, созданный в качестве курсовой работы.
