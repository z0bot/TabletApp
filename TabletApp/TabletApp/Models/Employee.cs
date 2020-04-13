using Realms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabletApp.Models
{
    public class Employee : RealmObject
    {
        public IList<string> tables { get; }
        public string _id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public int position { get; set; }
    }
}
