using Realms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabletApp.Models
{
    public class Ingredient : RealmObject
    {
        [PrimaryKey]
        public string _id { get; set; }
        public string name { get; set; }
        public int quantity { get; set; }
    }
}