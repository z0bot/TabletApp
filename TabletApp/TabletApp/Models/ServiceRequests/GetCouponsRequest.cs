using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TabletApp.Models.ServiceRequests
{
    class GetCouponsRequest : ServiceRequest
    {
        //the endpoint we are trying to hit
        public override string Url { get; set; } = "https://dijkstras-steakhouse-restapi.herokuapp.com/coupons";
        //the type of request
        public override HttpMethod Method => HttpMethod.Get;

        //Send request for list of all coupons
        public static async Task<bool> SendGetCouponsRequest()
        {
            //make a new request object
            var serviceRequest = new GetCouponsRequest();
            //get a response
            var response = await ServiceRequestHandler.MakeServiceCall<CouponsList>(serviceRequest);

            if (response == null)
            {
                //call failed
                return false;
            }
            else
            {
                //add the response into the local database
                RealmManager.RemoveAll<CouponsList>();
                CouponsList l = new CouponsList((response.Coupons.Where((Coupon c) => c.active && c.couponType == "Restaurant").ToList()));
                //l.Coupons = response.Coupons.Where((Coupon c) => c.active && c.couponType == "Restaurant").ToList; // 
                RealmManager.AddOrUpdate<CouponsList>(l);
                //call succeeded
                return true;
            }
        }
    }
}
