using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TabletApp.Models.ServiceRequests
{
    class PostReviewRequest : ServiceRequest
    {
        //the endpoint we are trying to hit
        public override string Url { get; set; } = "https://dijkstras-steakhouse-restapi.herokuapp.com/reviews/";
        //the type of request
        public override HttpMethod Method => HttpMethod.Post;

        public ReviewObject Body;

        // Constructor containing the employee ID and tip amount to be sent
        PostReviewRequest(string OrderID, string EmployeeID, int rating1, string reason1, int rating2, string reason2, int rating3, string reason3)
        {
            ReviewObject r = new ReviewObject();

            r.order_id = OrderID;
            r.employee_id = EmployeeID;
            r.question01_rating = rating1;
            r.question01_reason = reason1;

            r.question02_rating = rating2;
            r.question02_reason = reason2;

            r.question03_rating = rating3;
            r.question03_reason = reason3;

            Body = r;
        }

        // Request body content object
        public class ReviewObject
        {
            public string order_id;
            public string employee_id;
            public int question01_rating;
            public string question01_reason;

            public int question02_rating;
            public string question02_reason;

            public int question03_rating;
            public string question03_reason;
        }


        // Posts a tip to the database
        public static async Task<bool> SendPostReviewRequest(string OrderID, string EmployeeID, int rating1, string reason1, int rating2, string reason2, int rating3, string reason3)
        {
            //make a new request object
            var serviceRequest = new PostReviewRequest(OrderID, EmployeeID, rating1, reason1, rating2, reason2, rating3, reason3);
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