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
        public string _id { get; set; }
        public IList<Ingredient> ingredients { get; }
        public string name { get; set; }
        public string picture { get; set; }
        public string desctription { get; set; }
        public float price { get; set; }
        public string nutrition { get; set; }
        public string item_type { get; set; }
        public string category { get; set; }
        public bool paid { get; set; }
    }
}
