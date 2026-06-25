using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;
//using FOR_PRODUCTION_PRACTICE.User;
using FOR_PRODUCTION_PRACTICE.UserWindow;

namespace FOR_PRODUCTION_PRACTICE.Database
{
    public class DatabaseHelper
    {
        
        private static string Connect = @"Data Source=KSP1-763\BAZA;Initial Catalog=For_production_practice;Integrated Security=True;Connect Timeout=30;";

      
        public bool Validate(string login, string password)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Connect))
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM Autorization WHERE login_user = @login AND password_user = @password";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@login", login);
                        cmd.Parameters.AddWithValue("@password", password);
                        int count = (int)cmd.ExecuteScalar();
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка подключения к базе данных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        public static Usermodel GetUserinfo(string login)
        {
            string query = @"
                SELECT 
                    h.Id_people,
                    h.Фамилия,
                    h.Имя,
                    h.Отчество,
                    h.Должность,
                    h.Номер_телефона,
                    h.Email,
                    h.Role,
                    a.login_user
                FROM Human h
                JOIN Autorization a ON h.Id_people = a.id_people
                WHERE a.login_user = @login";

            try
            {
                using (SqlConnection conn = new SqlConnection(Connect))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@login", login);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new Usermodel
                                {
                                    Id_people = Convert.ToInt32(reader["Id_people"]),
                                    Login = reader["login_user"].ToString(),
                                    LastName = reader["Фамилия"].ToString(),
                                    FirstName = reader["Имя"].ToString(),
                                    MiddleName = reader["Отчество"]?.ToString() ?? "",
                                    position = reader["Должность"].ToString(),
                                    PhoneNumber = reader["Номер_телефона"].ToString(),
                                    Email = reader["Email"]?.ToString() ?? "",
                                    Role = reader["Role"]?.ToString() ?? "User"
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка получения данных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return null;
        }

        
        public static List<Usermodel> GetAllUsers()
        {
            List<Usermodel> users = new List<Usermodel>();
            string query = @"
                SELECT 
                    h.Id_people,
                    h.Фамилия,
                    h.Имя,
                    h.Отчество,
                    h.Должность,
                    h.Номер_телефона,
                    h.Email,
                    h.Role,
                    a.login_user
                FROM Human h
                JOIN Autorization a ON h.Id_people = a.id_people";

            try
            {
                using (SqlConnection conn = new SqlConnection(Connect))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                users.Add(new Usermodel
                                {
                                    Id_people = Convert.ToInt32(reader["Id_people"]),
                                    Login = reader["login_user"].ToString(),
                                    LastName = reader["Фамилия"].ToString(),
                                    FirstName = reader["Имя"].ToString(),
                                    MiddleName = reader["Отчество"]?.ToString() ?? "",
                                    position = reader["Должность"].ToString(),
                                    PhoneNumber = reader["Номер_телефона"].ToString(),
                                    Email = reader["Email"]?.ToString() ?? "",
                                    Role = reader["Role"]?.ToString() ?? "User"
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка получения списка пользователей: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return users;
        }

        
        public static List<RepairRequest> GetAllRequests()
        {
            List<RepairRequest> requests = new List<RepairRequest>();
            string query = @"
                SELECT 
                    r.*,
                    req.Фамилия + ' ' + req.Имя as RequesterName,
                    eng.Фамилия + ' ' + eng.Имя as EngineerName
                FROM RepairRequests r
                LEFT JOIN Human req ON r.Id_requester = req.Id_people
                LEFT JOIN Human eng ON r.Id_engineer = eng.Id_people
                ORDER BY r.DateReceived DESC";

            try
            {
                using (SqlConnection conn = new SqlConnection(Connect))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                requests.Add(new RepairRequest
                                {
                                    Id_request = Convert.ToInt32(reader["Id_request"]),
                                    RequestNumber = reader["RequestNumber"].ToString(),
                                    DateReceived = Convert.ToDateTime(reader["DateReceived"]),
                                    EquipmentType = reader["EquipmentType"].ToString(),
                                    EquipmentId = Convert.ToInt32(reader["EquipmentId"]),
                                    Description = reader["Description"]?.ToString() ?? "",
                                    Id_requester = Convert.ToInt32(reader["Id_requester"]),
                                    Status = reader["Status"].ToString(),
                                    Id_engineer = reader["Id_engineer"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["Id_engineer"]),
                                    Priority = reader["Priority"].ToString(),
                                    RequesterName = reader["RequesterName"]?.ToString() ?? "",
                                    EngineerName = reader["EngineerName"]?.ToString() ?? ""
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка получения заявок: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return requests;
        }

        public static bool AddRequest(RepairRequest request)
        {
            string query = @"
                INSERT INTO RepairRequests (RequestNumber, DateReceived, EquipmentType, EquipmentId, 
                                            Description, Id_requester, Status, Priority)
                VALUES (@RequestNumber, @DateReceived, @EquipmentType, @EquipmentId,
                        @Description, @Id_requester, @Status, @Priority)";

            try
            {
                using (SqlConnection conn = new SqlConnection(Connect))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@RequestNumber", request.RequestNumber);
                        cmd.Parameters.AddWithValue("@DateReceived", request.DateReceived);
                        cmd.Parameters.AddWithValue("@EquipmentType", request.EquipmentType);
                        cmd.Parameters.AddWithValue("@EquipmentId", request.EquipmentId);
                        cmd.Parameters.AddWithValue("@Description", request.Description);
                        cmd.Parameters.AddWithValue("@Id_requester", request.Id_requester);
                        cmd.Parameters.AddWithValue("@Status", request.Status);
                        cmd.Parameters.AddWithValue("@Priority", request.Priority);

                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка добавления заявки: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }


        public static bool UpdateRequestStatus(int requestId, string status, int? engineerId = null)
        {
            string query = "UPDATE RepairRequests SET Status = @Status";
            if (engineerId.HasValue)
                query += ", Id_engineer = @EngineerId, DateAssigned = GETDATE()";
            if (status == "Выполнена")
                query += ", DateCompleted = GETDATE()";
            query += " WHERE Id_request = @Id";

            try
            {
                using (SqlConnection conn = new SqlConnection(Connect))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", requestId);
                        cmd.Parameters.AddWithValue("@Status", status);
                        if (engineerId.HasValue)
                            cmd.Parameters.AddWithValue("@EngineerId", engineerId.Value);

                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка обновления заявки: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }
        public static List<Equipment> GetAllEquipment()
        {
            List<Equipment> equipment = new List<Equipment>();
            string query = @"
                SELECT 
                    e.*,
                    h.Фамилия + ' ' + h.Имя as EmployeeName
                FROM Equipment e
                LEFT JOIN Human h ON e.Id_people = h.Id_people";

            try
            {
                using (SqlConnection conn = new SqlConnection(Connect))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                equipment.Add(new Equipment
                                {
                                    Id_equipment = Convert.ToInt32(reader["Id_equipment"]),
                                    InventoryNumber = reader["InventoryNumber"].ToString(),
                                    Type = reader["Type"].ToString(),
                                    Brand = reader["Brand"].ToString(),
                                    Model = reader["Model"].ToString(),
                                    Configuration = reader["Configuration"]?.ToString() ?? "",
                                    DateInstalled = Convert.ToDateTime(reader["DateInstalled"]),
                                    Status = reader["Status"].ToString(),
                                    Id_people = reader["Id_people"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["Id_people"]),
                                    EmployeeName = reader["EmployeeName"]?.ToString() ?? ""
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка получения оборудования: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return equipment;
        }
        public static bool AddUser(Usermodel user)
        {
            if (user.Password.Length < 4)
            {
                MessageBox.Show("Пароль должен быть не менее 4 символов!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            using (SqlConnection conn = new SqlConnection(Connect))
            {
                conn.Open();
                using (SqlTransaction tx = conn.BeginTransaction())
                {
                    try
                    {
                        
                        string queryHuman = @"
                    INSERT INTO Human (Фамилия, Имя, Отчество, Должность, Номер_телефона, Email, Role) 
                    VALUES (@фамилия, @имя, @отчество, @должность, @телефон, @email, @role);
                    SELECT SCOPE_IDENTITY();";

                        int idPeople;
                        using (SqlCommand cmd = new SqlCommand(queryHuman, conn, tx))
                        {
                            cmd.Parameters.AddWithValue("@фамилия", user.LastName);
                            cmd.Parameters.AddWithValue("@имя", user.FirstName);
                            cmd.Parameters.AddWithValue("@отчество", string.IsNullOrWhiteSpace(user.MiddleName) ? (object)DBNull.Value : user.MiddleName);
                            cmd.Parameters.AddWithValue("@должность", string.IsNullOrWhiteSpace(user.position) ? (object)DBNull.Value : user.position);
                            cmd.Parameters.AddWithValue("@телефон", user.PhoneNumber);
                            cmd.Parameters.AddWithValue("@email", string.IsNullOrWhiteSpace(user.Email) ? (object)DBNull.Value : user.Email);
                            cmd.Parameters.AddWithValue("@role", user.Role ?? "User");

                            idPeople = Convert.ToInt32(cmd.ExecuteScalar());
                        }

                        
                        string queryAuth = @"
                    INSERT INTO Autorization (id_people, login_user, password_user) 
                    VALUES (@id_people, @login, @password)";

                        using (SqlCommand cmd = new SqlCommand(queryAuth, conn, tx))
                        {
                            cmd.Parameters.AddWithValue("@id_people", idPeople);
                            cmd.Parameters.AddWithValue("@login", user.Login);
                            cmd.Parameters.AddWithValue("@password", user.Password);
                            cmd.ExecuteNonQuery();
                        }

                        tx.Commit();
                        return true;
                    }
                    catch (SqlException ex)
                    {
                        tx.Rollback();
                        if (ex.Number == 2627)
                        {
                            MessageBox.Show("Такой логин или номер телефона уже существует!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        else
                        {
                            MessageBox.Show($"Ошибка регистрации: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        return false;
                    }
                }
            }
        }
    }
}