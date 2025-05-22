## Инструкция пользования:
### 1. Открываем SSMS(еще не подключаемся!!!), Docker Dekstop, Среду разработки (в моем случае это Visual Studio)
### 2. Запускаем проект в Visual Studio проект через Docker Compose.
### 3. Подключаемся в SSMS по данным ниже:
- **Имя сервера:** localhost,1433
- **Проверка подлинности:** проверка подлинности SQL Server
- **Имя для входа:** sa
- **Пароль:** YourStrong@Passw0rd
- **Обязательно** ставим галочку на чекбокс "Доверять сертификату сервера" !!!
### 4. Восстановаление Базы Данных
- В папке **Events-Web-Application/DB** лежат исходники базы данных
- В SSMS ПКМ по БД и нажимаем "Создать запрос".
- Пишем:
```sql
RESTORE FILELISTONLY
FROM DISK = '/var/opt/mssql/backup/Events_Web_Application.bak'
```
- Далее
```sql
RESTORE DATABASE EventsAppDb
FROM DISK = '/var/opt/mssql/backup/Events_Web_Application.bak'
WITH MOVE 'Events_Web_Application' TO '/var/opt/mssql/data/EventsAppDb.mdf',
     MOVE 'Events_Web_Application_log' TO '/var/opt/mssql/data/EventsAppDb_log.ldf',
     REPLACE;
```
- Готово
### 5. В Веб-приложение переходим на путь /swagger
### 6. Регестрируемся (/api/Auth/register) , если заходим(/api/Auth/login):
### User: 
- **Name:** Ivan
- **Email:** ivan.petrov@example.com

### Admin: 
- **Name:** Maria 
- **Email:** maria.sidorova@example.com

### 7. В ответе будет длинный токен к полю "accessToken", копируем его.
### 8. Справа выше находится кнопка "Authorize" нажимаем и вставляем туда ранее скопируемый токен и нажимаем там кнопку "Authorize".
### 9. Приложение готово к использованию, ниже объяснены все конечные пути и для каких пользователей он доступо.

## Auth
- **POST /api/Auth/register** - Регистрация User-а. Доступна всем пользователям. 
- **POST /api/Auth/login** - вход в аккаунт User-а. Доступна всем пользователям.
- **POST /api/Auth/refresh** - обновление токенов User-a. Любому авторизованному пользователю.

## Events
- **GET /api/Events** - получение всех событий. Любому авторизованному пользователю.
- **POST /api/Events** - добавление нового пользователя. Только админ. Пример тела запроса:
- **GET /api/Events/name** - получение события по его названию. Любому авторизованному пользователю.
- **GET /api/Events/date** - получение события по его дате. Любому авторизованному пользователю.
- **GET /api/Events/place** - получение события по его месту. Любому авторизованному пользователю.
- **GET /api/Events/category** - получение события по его категории. Любому авторизованному пользователю.
- **GET /api/Events/id** - получение события по его id. Любому авторизованному пользователю.
- **PUT /api/Events/id** - изменение события по его id. Только админ.
- **DELETE /api/Events/id** - удаление события по его id. Только админ.
- **POST /api/Events/{id}/photo** - Добавление фото к событию по его Id. Только админ. 

## User
- **GET /api/User/event/{eventId}** - получение всех пользоавтелей по событию, на которое они зарегестрированы. Только админ.
- **GET /api/User/{Id}** - получение пользователя по его id. Только админ.
- **GET /api/User/Changes** - получение изменения событий, на которое пользователям зарегестрирован. Любому авторизованному пользователю(но только для самого себя. Изменения на событие, на которое он не зарегестрирован, просматривать он не может).
- **POST /api/User/cancel** - отмена регистрации на событие. Любому авторизованному пользователю(опять же, только свою регистрацию он может отменить. Чужую не может).

## Пример тела запроса на Пользователя:
```json
{
  "email": "string",
  "name": "string",
  "surname": "string",
  "birthdayDate": "2025-06-07"
}
```

## Пример тела запроса на Событие:
```json
{
  "name": "E-Sports Championship",
  "description": "E-sports tournament.",
  "date": "2025-10-20",
  "place": "New York, Convention Center",
  "category": 2,
  "maxUser": 200,
  "photoPath": ["images/event1.jpg"]
}
```

## Пример тела запроса на добавление фото:
```json
{
  "photoPath": ["images/event1.jpg"]
}
```

- Так же в проекте кэшируется поле "photoPath" у события
- (reactclient не действителен, забыл удалить с проекта)
