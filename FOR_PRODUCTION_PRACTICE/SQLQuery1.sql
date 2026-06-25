UPDATE Human 
SET 
    Должность = 'Сотрудник КПП',
    Role = 'User'
WHERE Фамилия = 'Петров' AND Имя = 'Петр';
GO

-- Проверяем
SELECT 
    h.Фамилия,
    h.Имя,
    h.Отчество,
    h.Должность,
    h.Role,
    a.login_user
FROM Human h
JOIN Autorization a ON h.Id_people = a.id_people
WHERE h.Фамилия = 'Петров';
GO