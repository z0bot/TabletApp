using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TabletApp.Models.ServiceRequests
{
    public class PutNotificationRequest : ServiceRequest
    {
        //the endpoint we are trying to hit
        public override string Url => "https://dijkstras-steakhouse-restapi.herokuapp.com/notifications";
        //the type of request
        public override HttpMethod Method => HttpMethod.Post;
        //headers if we ever need them
        public override Dictionary<string, string> Headers => null;

        public static async Task<bool> SendPutNotificationRequest(Notification newNotification)
        {
            //make a new request object
            var serviceRequest = new PutNotificationRequest();
            //get a response
            var response = await ServiceRequestHandler.MakeServiceCall<Notification>(serviceRequest, newNotification);

            if (response == null)
            {
                //call failed
                return false;
            }
            else
            {
                //call succeeded
                return true;
            }
        }
    }
}