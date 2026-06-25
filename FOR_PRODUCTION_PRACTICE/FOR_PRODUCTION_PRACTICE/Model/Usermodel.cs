using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents.DocumentStructures;

namespace FOR_PRODUCTION_PRACTICE.UserWindow
{
    public class Usermodel
    {
        public int Id_people { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string position { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Fullname => $"{LastName} {FirstName} {MiddleName}".Trim();
        public string Shortname => $"{LastName} {FirstName[0]} {MiddleName[0]}".Trim(' ', '.');
        public string Role { get; set; }
        public string RoleRussian
        {
            get
            {
                if (Role == "Admin")
                    return "Администратор";
                else if (Role == "Nachalnik")
                    return "Начальник";
                else if (Role == "Engineer")
                    return "Инженер-электроник";
                else if (Role == "User")
                    return "Сотрудник";
                else
                    return "Пользователь";
            }
        }

        
        public string RoleColor
        {
            get
            {
                if (Role == "Admin" || Role == "Nachalnik")
                    return "#E74C3C"; 
                else if (Role == "Engineer")
                    return "#3498DB";
                else if (Role == "User")
                    return "#2ECC71";  
                else
                    return "#95A5A6";  
            }
        }
    }
}
