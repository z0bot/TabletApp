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
                if (_table == null) _table = RealmManager.All<Table>().First().tableNumberString;

                return _table;
            }
            set
            {
                _table = value;
            }
        }

        static string _employeeID { get; set; }
        static string employeeID
        {
            get
            {
                if (_employeeID == null) _employeeID = RealmManager.All<Table>().First().employee_id;

                return _employeeID;
            }
            set
            {
                _employeeID = value;
            }
        }

        string _employee_ID { get; set; }

        public static async Task<bool> SendNotification(string type)
        {
            return await AddNotificationRequest.SendAddNotificationRequest(employeeID, table, type);
        }
    }
}
