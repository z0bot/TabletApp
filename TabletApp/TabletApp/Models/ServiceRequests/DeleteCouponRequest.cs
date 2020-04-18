using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TabletApp.Models.ServiceRequests
{
    class DeactivateCouponRequest : ServiceRequest
    {
        //ID of coupon used
        string couponID;
        //the endpoint we are trying to hit
        public override string Url { get; set; }
        //the type of request
        public override HttpMethod Method => HttpMethod.Put;

        //Body of request
        public IList<UpdaterObject> Body;

        // Constructor containing the order ID to be sent
        DeactivateCouponRequest(string ID)
        {
            couponID = ID;

            Url = "https://dijkstras-steakhouse-restapi.herokuapp.com/coupons/" + couponID;

            UpdaterObject UO = new UpdaterObject();

            Body = new List<UpdaterObject>();
            Body.Add(UO);
        }

        // Request body content object
        public class UpdaterObject
        {
            public string propName = "active";
            public bool value = false;
        }


        // Named strangely, but this updates the order at ID to be 'sent'
        public static async Task<bool> SendDeactivateCouponRequest(string ID)
        {
            //make a new request object
            var serviceRequest = new DeactivateCouponRequest(ID);
            //get a response
            var response = await ServiceRequestHandler.MakeServiceCall<DeleteResponse>(serviceRequest, serviceRequest.Body);

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
