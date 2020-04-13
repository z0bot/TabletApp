using System;
using System.Collections.Generic;
using System.Text;

using Realms;

namespace TabletApp.Models
{
    public class Picture : RealmObject
    {
        public string type { get; set; }
        public IList<int> data { get; }
    }
}
