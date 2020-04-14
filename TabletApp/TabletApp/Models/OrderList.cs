using Realms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabletApp.Models
{
    public class OrderList : RealmObject
    {
        public IList<OrderItem> orderItems { get; }
    }
}