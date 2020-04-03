using System;
using System.Collections.Generic;
using System.Text;

using Realms;

namespace TabletApp.Models
{
    public class MenuIngredient : RealmObject
    {
        [PrimaryKey]
        public string id { get; set; }
        public string name { get; set; }
    }
}
