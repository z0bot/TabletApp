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

        public OrderList()
        {

        }

        public OrderList(OrderList o)
        {
            for (int i = 0; i < o.orderItems.Count(); i++)
                orderItems.Add(o.orderItems[i]);
        }
    }
}