using Realms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabletApp.Models
{
    public class MenuItem : RealmObject
    {
        [PrimaryKey]
        public string id { get; set; }
        public string name { get; set; }
        public string picture { get; set; }
        public string description { get; set; }
        public float price { get; set; }
        public string nutrition { get; set; }
        public string item_type { get; set; }
        public string category { get; set; }
        public bool paid { get; set; }
    }
}
