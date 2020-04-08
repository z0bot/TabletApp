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
        public override string Url { get; set; }

        public override HttpMethod Method => HttpMethod.Post;

        public AddNotificationRequestBody Body;

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

        public static async Task<bool> SendAddNotificationRequest(string inID, string inSend, string inType)
        {
            var sendAddNotificationRequest = new AddNotificationRequest(inID, inSend, inType);

            var response = await ServiceRequestHandler.MakeServiceCall<PostResponse>(sendAddNotificationRequest, sendAddNotificationRequest.Body);

            if (response.message == null) return false;
            else return true;
        }
    }

    public class AddNotificationRequestBody
    {
        public string employee_id;
        public string sender;
        public string notificationType;
    }
}