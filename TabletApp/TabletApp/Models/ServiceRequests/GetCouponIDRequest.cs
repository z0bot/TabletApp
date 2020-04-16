using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TabletApp.Models.ServiceRequests
{
    class GetCouponsByIDRequest : ServiceRequest
    {
        public string ID;
        //the endpoint we are trying to hit
        public override string Url { get; set; }
        //the type of request
        public override HttpMethod Method => HttpMethod.Get;

        GetCouponsByIDRequest(string couponID)
        {
            ID = couponID;

            Url = "https://dijkstras-steakhouse-restapi.herokuapp.com/coupons/" + ID;
        }

        public static async Task<bool> SendGetCouponsByIDRequest(string ID)
        {
            //make a new request object
            var serviceRequest = new GetCouponsByIDRequest(ID);
            //get a response
            var response = await ServiceRequestHandler.MakeServiceCall<Coupon>(serviceRequest);

            if (response._id == null) // No null or inactive coupons
            {
                //call failed
                return false;
            }
            else
            {
                //add the response into the local database
                RealmManager.AddOrUpdate<Coupon>(response);
                //call succeeded
                return true;
            }
        }
    }
}