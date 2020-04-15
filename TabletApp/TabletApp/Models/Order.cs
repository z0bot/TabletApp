using Realms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabletApp.Models
{
    public class Order : RealmObject
    {
        [PrimaryKey]
        public string _id { get; set; }
        public string employee_id { get; set; }
        public bool send_to_kitchen { get; set; }
        public IList<OrderItem> menuItems { get; }
    }
}
