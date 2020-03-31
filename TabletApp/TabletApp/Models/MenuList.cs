using Realms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabletApp.Models
{
    class MenuList : RealmObject
    {
        public IList<MenuItem> menuItems { get; }
    }
}