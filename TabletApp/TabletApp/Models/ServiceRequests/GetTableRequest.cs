using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TabletApp.Models.ServiceRequests
{
    class GetTableRequest : ServiceRequest
    {
        public string tableNum;
        //the endpoint we are trying to hit
        public override string Url { get; set; }
        //the type of request
        public override HttpMethod Method => HttpMethod.Get;

        GetTableRequest(int table)
        {
            tableNum = table.ToString();

            Url = "https://dijkstras-steakhouse-restapi.herokuapp.com/tables/" + tableNum;
        }

        public static async Task<bool> SendGetTableRequest(int tableNum)
        {
            //make a new request object
            var serviceRequest = new GetTableRequest(tableNum);
            //get a response
            var response = await ServiceRequestHandler.MakeServiceCall<Table>(serviceRequest);

            if (response == null)
            {
                //call failed
                return false;
            }
            else
            {
                RealmManager.RemoveAll<Table>();
                //add the response into the local database
                RealmManager.AddOrUpdate<Table>(response);

                //call succeeded
                return true;
            }
        }
    }
}