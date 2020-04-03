using System;
using System.Collections.Generic;
using System.Text;

using Realms;

namespace TabletApp.Models
{
    class KidsLoc : RealmObject
    {
        [PrimaryKey]

        public string Id { get; set; }
        public string Master { get; set; }

    }

}
