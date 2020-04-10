using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TabletApp.Models.ServiceRequests
{
    public class GetEmployeesRequest : ServiceRequest
    {
        //the endpoint we are trying to hit
        public override string Url { get; set; }
        //the type of request
        public override HttpMethod Method => HttpMethod.Get;

        public GetEmployeesRequest()
        {
            Url = "https://dijkstras-steakhouse-restapi.herokuapp.com/employees";
        }

        public static async Task<bool> SendGetEmployeesRequest()
        {
            //make a new request object
            var serviceRequest = new GetMenuItemsRequest();
            //get a response
            var response = await ServiceRequestHandler.MakeServiceCall<EmployeeList>(serviceRequest);

            if (response == null)
            {
                //call failed
                return false;
            }
            else
            {
                //add the response into the local database
                RealmManager.AddOrUpdate<EmployeeList>(response);
                //call succeeded
                return true;
            }
        }
    }
}