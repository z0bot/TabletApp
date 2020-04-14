using Realms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabletApp.Models
{
    public class Table : RealmObject
    {
        [PrimaryKey]
        public string _id { get; set; }
        public IList<string> user_ids { get; }
        public int table_number { get; set; }
        public string employee_id { get; set; }
        public Order order_id { get; set; }
        public string tableNumberString => "Table" + table_number.ToString();
    }
}
