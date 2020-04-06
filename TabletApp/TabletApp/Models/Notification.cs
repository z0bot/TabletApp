using Realms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabletApp.Models
{
    public class Notification : RealmObject
    {
        [PrimaryKey]
        public string _id { get; set; }
        public string employee_id { get; set; }
        public string sender { get; set; }
        public string notificationType { get; set; }
    }
}
