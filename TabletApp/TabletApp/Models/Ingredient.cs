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
        public string id { get; set; }
        public string name { get; set; }
        public int quantity { get; set; }

        public string NameAndAmount
        {
            get
            {
                if (quantity > 1)
                {
                    return name + ": " + quantity + " units";
                }
                else if (quantity == 1)
                {
                    return name + ": " + quantity + " unit";
                }
                else
                {
                    return name + ": No units left";
                }
            }
            set
            {
                NameAndAmount = value;
            }
        }
    }
}