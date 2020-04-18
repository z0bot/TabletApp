using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TabletApp.Models.ServiceRequests
{
    public class AddNotificationRequest : ServiceRequest
    {
        //URL of Database
        public override string Url { get; set; }

        //HTTP POST Method used
        public override HttpMethod Method => HttpMethod.Post;

        //Body of Request
        public AddNotificationRequestBody Body;

        //Constructor
        public AddNotificationRequest(string inID, string inSend, string inType)
        {
            Url = "https://dijkstras-steakhouse-restapi.herokuapp.com/notifications";

            Body = new AddNotificationRequestBody
            {
                employee_id = inID,
                sender = inSend,
                notificationType = inType
            };
        }

        //Send Notification to database
        public static async Task<bool> SendAddNotificationRequest(string inID, string inSend, string inType)
        {
            var sendAddNotificationRequest = new AddNotificationRequest(inID, inSend, inType);

            var response = await ServiceRequestHandler.MakeServiceCall<PostResponse>(sendAddNotificationRequest, sendAddNotificationRequest.Body);

            if (response.message == null) return false;
            else return true;
        }
    }

    //Class for body of Notification
    public class AddNotificationRequestBody
    {
        public string employee_id { get; set; }
        public string sender { get; set; }
        public string notificationType { get; set; }
    }
}