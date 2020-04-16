using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TabletApp.Models.ServiceRequests
{
    class FinishOrderRequest : ServiceRequest
    {
        public string tableID;
        //the endpoint we are trying to hit
        public override string Url { get; set; }
        //the type of request
        public override HttpMethod Method => HttpMethod.Post;

        // Constructor containing the table ID
        FinishOrderRequest(string ID)
        {
            tableID = ID;

            Url = "https://dijkstras-steakhouse-restapi.herokuapp.com/tables/finishorder/" + tableID;
        }

        // Posts a tip to the database
        public static async Task<bool> SendFinishOrderRequest(string tableID)
        {
            //make a new request object
            var serviceRequest = new FinishOrderRequest(tableID);
            //get a response
            var response = await ServiceRequestHandler.MakeServiceCall<DeleteResponse>(serviceRequest);


            //No response is set so just assume it worked
            return true;
        }
    }
}
