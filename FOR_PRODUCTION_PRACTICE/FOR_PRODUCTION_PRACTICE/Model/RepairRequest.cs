using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOR_PRODUCTION_PRACTICE.UserWindow
{
    public class RepairRequest
    {
        public int Id_request { get; set; }
        public string RequestNumber { get; set; }
        public DateTime DateReceived { get; set; }
        public string EquipmentType { get; set; }
        public int EquipmentId { get; set; }
        public string Description { get; set; }
        public int Id_requester { get; set; }
        public string Status { get; set; }
        public int? Id_engineer { get; set; }
        public string Priority { get; set; }
        public DateTime? DateAssigned { get; set; }
        public DateTime? DateCompleted { get; set; }

       
        public string RequesterName { get; set; }
        public string EngineerName { get; set; }

        public string StatusRussian
        {
            get
            {
                switch (Status)
                {
                    case "Новая": return "Новая";
                    case "Принята": return "Принята";
                    case "В работе": return "В работе";
                    case "Выполнена": return "Выполнена";
                    case "Отклонена": return "Отклонена";
                    default: return Status;
                }
            }
        }
    }
}
