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
        public string _id { get; set; }
        public IList<OrderItem> menuItems { get; }
    }
}
