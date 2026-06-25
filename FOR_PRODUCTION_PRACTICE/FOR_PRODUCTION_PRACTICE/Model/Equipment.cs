using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOR_PRODUCTION_PRACTICE.UserWindow
{
    public class Equipment
    {
        public int Id_equipment { get; set; }
        public string InventoryNumber { get; set; }
        public string Type { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Configuration { get; set; }
        public DateTime DateInstalled { get; set; }
        public string Status { get; set; }
        public int? Id_people { get; set; }
        public string EmployeeName { get; set; }
    }
}
