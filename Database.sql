IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'For_production_practice')
BEGIN
    CREATE DATABASE For_production_practice;
END
GO

USE For_production_practice;
GO

-- 2. Удаление старых таблиц (если существуют)
DROP TABLE IF EXISTS WriteOffActs;
DROP TABLE IF EXISTS RepairRequests;
DROP TABLE IF EXISTS SpareParts;
DROP TABLE IF EXISTS Equipment;
DROP TABLE IF EXISTS Autorization;
DROP TABLE IF EXISTS Human;
GO

-- СОЗДАНИЕ ТАБЛИЦ


-- 3. Сотрудники
CREATE TABLE Human (
    Id_people INT PRIMARY KEY IDENTITY,
    Фамилия VARCHAR(50) NOT NULL,
    Имя VARCHAR(50) NOT NULL,
    Отчество VARCHAR(50) NULL,
    Должность VARCHAR(70) NOT NULL,
    Номер_телефона VARCHAR(50) NOT NULL UNIQUE,
    Email VARCHAR(50) NULL,
    Role VARCHAR(30) NOT NULL DEFAULT 'User'
);
GO

-- 4. Авторизация
CREATE TABLE Autorization (
    id_autorization INT PRIMARY KEY IDENTITY,
    id_people INT NOT NULL UNIQUE,
    login_user VARCHAR(50) NOT NULL UNIQUE,
    password_user VARCHAR(255) NOT NULL,
    FOREIGN KEY (id_people) REFERENCES Human(Id_people) ON DELETE CASCADE
);
GO

-- 5. Оборудование
CREATE TABLE Equipment (
    Id_equipment INT PRIMARY KEY IDENTITY,
    InventoryNumber VARCHAR(50) NOT NULL UNIQUE,
    Type VARCHAR(30) NOT NULL,
    Brand VARCHAR(50) NOT NULL,
    Model VARCHAR(50) NOT NULL,
    Configuration TEXT NULL,
    DateInstalled DATE NOT NULL,
    Status VARCHAR(30) DEFAULT 'В эксплуатации',
    Id_people INT NULL,
    FOREIGN KEY (Id_people) REFERENCES Human(Id_people) ON DELETE SET NULL
);
GO

-- 6. Заявки на ремонт
CREATE TABLE RepairRequests (
    Id_request INT PRIMARY KEY IDENTITY,
    RequestNumber VARCHAR(20) NOT NULL UNIQUE,
    DateReceived DATE NOT NULL,
    EquipmentType VARCHAR(30) NOT NULL,
    EquipmentId INT NOT NULL,
    Description TEXT NULL,
    Id_requester INT NOT NULL,
    Status VARCHAR(30) DEFAULT 'Новая',
    Id_engineer INT NULL,
    Priority VARCHAR(20) DEFAULT 'Обычный',
    DateAssigned DATE NULL,
    DateCompleted DATE NULL,
    FOREIGN KEY (Id_requester) REFERENCES Human(Id_people),
    FOREIGN KEY (Id_engineer) REFERENCES Human(Id_people)
);
GO

-- 7. Запчасти
CREATE TABLE SpareParts (
    Id_part INT PRIMARY KEY IDENTITY,
    Name VARCHAR(100) NOT NULL,
    Type VARCHAR(50) NOT NULL,
    Compatibility TEXT NULL,
    Quantity INT DEFAULT 0,
    MinQuantity INT DEFAULT 5,
    Price DECIMAL(10,2) NULL
);
GO

-- 8. Акты списания
CREATE TABLE WriteOffActs (
    Id_act INT PRIMARY KEY IDENTITY,
    ActNumber VARCHAR(20) NOT NULL UNIQUE,
    EquipmentType VARCHAR(30) NOT NULL,
    EquipmentId INT NOT NULL,
    DateWriteOff DATE NOT NULL,
    Reason TEXT NULL,
    Id_approver INT NOT NULL,
    Status VARCHAR(30) DEFAULT 'На рассмотрении',
    FOREIGN KEY (Id_approver) REFERENCES Human(Id_people)
);
GO



INSERT INTO Human (Фамилия, Имя, Отчество, Должность, Номер_телефона, Email, Role) VALUES 
('Смирнов', 'Алексей', 'Петрович', 'Администратор системы', '+79990001122', 'admin@system.ru', 'Admin'),
('Тараканов', 'Вячеслав', 'Александрович', 'Начальник подразделения', '+79876543210', 'tarakanov@mail.ru', 'Chief'),
('Горбунов', 'Александр', 'Васильевич', 'Инженер-электроник', '+79123456789', 'aleksandr@mail.ru', 'Engineer'),
('Петров', 'Петр', 'Сидорович', 'Сотрудник КПП', '+79234567890', 'petr@mail.ru', 'User'),
('Сидорова', 'Анна', 'Ивановна', 'Участковый', '+79345678901', 'anna@mail.ru', 'User');
GO

INSERT INTO Autorization (id_people, login_user, password_user) VALUES 
(1, 'Admin', 'admin123'),
(2, 'Tarakanov', 'admin123'),
(3, 'Aleksandr', 'admin'),
(4, 'petr', 'password'),
(5, 'anna', 'mypass');
GO

INSERT INTO Equipment (InventoryNumber, Type, Brand, Model, Configuration, DateInstalled, Status, Id_people) VALUES 
('PC-001', 'Компьютер', 'HP', 'ProDesk 600 G4', 'Intel Core i5-8500/16GB/SSD 256GB', '2022-01-15', 'В эксплуатации', 1),
('PC-002', 'Компьютер', 'Dell', 'OptiPlex 7070', 'Intel Core i7-9700/16GB/SSD 512GB', '2022-03-20', 'В эксплуатации', 3),
('NB-001', 'Ноутбук', 'Lenovo', 'ThinkPad T490', 'Intel Core i5-8265U/16GB/SSD 512GB', '2022-06-15', 'В эксплуатации', 1),
('PR-001', 'Принтер', 'HP', 'LaserJet MFP M227fdw', 'Ресурс картриджа: 1500', '2022-01-15', 'В эксплуатации', NULL);
GO

INSERT INTO RepairRequests (RequestNumber, DateReceived, EquipmentType, EquipmentId, Description, Id_requester, Status, Priority) VALUES 
('ЗАЯВ-20260115-001', '2026-01-15', 'Компьютер', 1, 'Не включается системный блок', 3, 'Новая', 'Критичный'),
('ЗАЯВ-20260120-002', '2026-01-20', 'Ноутбук', 3, 'Не работает Wi-Fi', 4, 'В работе', 'Высокий');
GO

INSERT INTO SpareParts (Name, Type, Compatibility, Quantity, MinQuantity, Price) VALUES 
('DDR4 8GB', 'ОЗУ', 'DDR4', 10, 5, 2500),
('DDR4 16GB', 'ОЗУ', 'DDR4', 5, 3, 4500),
('SSD 256GB', 'SSD', 'SATA 2.5"', 8, 4, 3500),
('SSD 512GB', 'SSD', 'SATA 2.5"', 4, 2, 5500);
GO


-- ПРОВЕРКА

SELECT 
    h.Фамилия,
    h.Имя,
    h.Role,
    a.login_user,
    a.password_user
FROM Human h
JOIN Autorization a ON h.Id_people = a.id_people
ORDER BY h.Role;
GO

SELECT 'База данных создана успешно!' as Result;
GO