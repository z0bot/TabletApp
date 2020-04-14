
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TabletApp.Models.ServiceRequests
{
    class UpdateOrderMenuItemsRequest : ServiceRequest
    {
        string orderID;
        //the endpoint we are trying to hit
        public override string Url { get; set; }
        //the type of request
        public override HttpMethod Method => HttpMethod.Put;

        public IList<UpdaterObject> Body;

        // Constructor containing menu items (order items) that need to be updated
        UpdateOrderMenuItemsRequest(string ID, IList<OrderItem> toUpdate)
        {
            orderID = ID;

            Url = "https://dijkstras-steakhouse-restapi.herokuapp.com/orders/" + orderID;

            UpdaterObject UO = new UpdaterObject();
            foreach (OrderItem o in toUpdate)
                UO.value.Add(new SerializableOrderItem(o));

            Body = new List<UpdaterObject>();
            Body.Add(UO);
        }

        // Request body content object
        public class UpdaterObject
        {
            public string propName = "menuItems";
            public IList<SerializableOrderItem> value = new List<SerializableOrderItem>();
        }

        // Objects to contain just the information needed to update the order. 
        // Since OrderItems extend RealmObject, they contain a bunch of extra stuff we don't need
        public class SerializableOrderItem
        {
            public IList<string> ingredients;

            public bool prepared;

            public bool paid;

            public string special_instruct;

            public string name;

            public double price;

            public string _id;

            public SerializableOrderItem(OrderItem o)
            {
                _id = o._id;

                ingredients = o.ingredients;

                prepared = o.prepared;

                paid = o.paid;

                price = o.price;

                name = o.name;

                special_instruct = o.special_instruct;
            }
        }

        public static async Task<bool> SendUpdateOrderMenuItemsRequest(string ID, IList<OrderItem> toUpdate)
        {
            //make a new request object
            var serviceRequest = new UpdateOrderMenuItemsRequest(ID, toUpdate);
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