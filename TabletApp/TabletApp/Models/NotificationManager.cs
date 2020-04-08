using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TabletApp.Models.ServiceRequests;

namespace TabletApp.Models
{
    public class NotificationManager
    {
        static string _table { get; set; }
        static string table
        {
            get
            {
                if (_table == null) _table = RealmManager.All<TableManager>().First().table;

                return _table;
            }
            set
            {
                _table = value;
            }
        }

        string _employee_ID { get; set; }

        public static async Task<bool> SendNotification(string type)
        {
            return await AddNotificationRequest.SendAddNotificationRequest("Test78217418401", table, type);
        }
    }
}
