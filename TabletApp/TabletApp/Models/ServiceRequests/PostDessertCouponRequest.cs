using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TabletApp.Models.ServiceRequests
{
    class PostDessertCouponRequest : ServiceRequest
    {
        //the endpoint we are trying to hit
        public override string Url { get; set; } = "https://dijkstras-steakhouse-restapi.herokuapp.com/coupons";
        //the type of request
        public override HttpMethod Method => HttpMethod.Post;
        //Body of coupon
        public SerializableCoupon Body;

        // Constructor containing the employee ID and tip amount to be sent
        PostDessertCouponRequest()
        {
            Body = new SerializableCoupon();

        }

        // Request body content object
        public class SerializableCoupon
        {
            public string couponType = "Customer";
            public IList<IDResponse> requiredItems = new List<IDResponse>();
            public IList<IDResponse> appliedItems = new List<IDResponse>();
            public int discount = 100;
            public bool active = true;
            public bool repeatable = false;
            public string description = "Enjoy 100% off of our dessert cookie";

            public SerializableCoupon()
            {
                string ID = "5e9675ebcd0dd200049ca257";
                requiredItems.Add(new IDResponse(ID));
                appliedItems.Add(new IDResponse(ID));
            }

        }

        //Class for ID Response
        public class IDResponse
        {
            public string _id;

            public IDResponse() { }

            public IDResponse(string ID)
            {
                _id = ID;
            }
        }

        // Posts a tip to the database
        public static async Task<string> SendPostDessertCouponRequest()
        {
            //make a new request object
            var serviceRequest = new PostDessertCouponRequest();
            //get a response
            var response = await ServiceRequestHandler.MakeServiceCall<IDResponse>(serviceRequest, serviceRequest.Body);

            if (response._id == null)
            {
                //call failed
                return null;
            }
            else
            {
                //call succeeded
                return response._id;
            }
        }
    }
}