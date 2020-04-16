using NUnit.Framework;
using TabletApp;
using TabletApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TabletApp.Models.ServiceRequests;

/// <summary>
/// Tests
/// Tests class contains a series of functions
/// designed to test the logic of certain code
/// snipets within the TabletApp project
/// </summary>
namespace TabletTesting
{
    public class TabletTests
    {
        private HttpClient client = new HttpClient();
        private TabletApp.Models.MenuList testRealm;

        [SetUp]
        async public Task Setup(){
            await GetMenuItemsRequest.SendGetMenuItemsRequest();
            testRealm = RealmManager.All<MenuList>().First();
        }
        ///Test List
        ///
        ///ServiceRequest.cs tests
        ///MakeServiceCall_CorrectObjectDeserialized
        ///MakeServiceCall_IncorrectObjectDeserialized
        ///MakeServiceCall_HttpResponse_Success
        ///MakeServiceCall_HttpResponse_NotFound
        ///CreateHttpRequest_CorrectObjectSerialize
        ///menuPage_ViewMenuItems
        ///RefillButton_CorrectEmployeeId
        ///RefillButton_IncorrectEmployeeId
        ///CheckOut_MenuItemsAdd

        /// <summary>
        /// MakeServiceCall_CorrectObjectDeserialized
        /// Tests to be sure an exception is not thrown
        /// when string is formated properly
        /// </summary>        
        [Test]
        public void MakeServiceCall_CorrectObjectDeserialized() {

            String content = "{'menuItems':[{'ingredients':[{'_id':'5e7fa943016004000436733c','name':'steak'}]," +
                "'prepared':false,'paid':false,'special_instruct':'No intructions provided.','_id':" +
                "'5e865ed02eccf8000445d5f2','name':'Steak and eggs ','picture':'This will be a picture','description':" +
                "'This is a steak and some potatoes','price':29.99,'nutrition':'Meat, caloires','item_type':'Steak'," +
                "'category':'Entree','__v':0},{'ingredients':[{'_id':'5e852fca070c080004bc03db','name':" +
                "'Waitstaff Organs'}],'prepared':false,'paid':false,'special_instruct':'No intructions provided.'" +
                ",'_id':'5e8660d161b17c0004e46c8a','name':'Omelette du Waitstaff','picture':'GoodFood','description':" +
                "'This is a delectable omelette with tender waitstaff flesh folded in','price':19.99,'nutrition':" +
                "'Calories: 200\nAllergen: Human flesh','item_type':'Eggs','category':'Appetizers','__v':0}]}";

            //try to deserialize correct content
            try {
                JsonConvert.DeserializeObject(content);
            }
            catch (JsonException e) {
                Assert.Fail("JsonSerializationException thrown");
            }

            Assert.Pass();
        }
        /// <summary>
        /// MakeServiceCall_IncorrectObjectDeserialized
        /// Tests to be sure an exception is thrown when
        ///string is formated properly
        /// </summary>
        [Test]
        public void MakeServiceCall_IncorrectObjectDeserialized() {
            //Alter content to be incorrect (removes '"')
            String content = "{ menuItems :[{ ingredients :[{ _id : 5e7fa943016004000436733c , name : steak }], " +
                "prepared :false, paid :false, special_instruct : No intructions provided. , _id : " +
                "5e865ed02eccf8000445d5f2 , name : Steak and eggs  , picture : This will be a picture , description : " +
                "This is a steak and some potatoes , price :29.99, nutrition : Meat, caloires , item_type : Steak , " +
                "category : Entree , __v :0},{ ingredients :[{ _id : 5e852fca070c080004bc03db , name : Waitstaff Organs }" +
                "], prepared :false, paid :false, special_instruct : No intructions provided. , _id : " +
                "5e8660d161b17c0004e46c8a , name : Omelette du Waitstaff , picture : GoodFood , description : " +
                "This is a delectable omelette with tender waitstaff flesh folded in , price :19.99, nutrition : " +
                "Calories: 200\nAllergen: Human flesh , item_type : Eggs , category : Appetizers , __v :0}]}";


            //try to deserialize correct content
            try {
                JsonConvert.DeserializeObject(content);
            }
            catch (JsonException e) {
                Assert.Pass("JsonSerializationException thrown");
            }

            Assert.Fail();
        }

        /// <summary>
        /// MakeServiceCall_ReturnObjectError
        /// Tests if correct URL gets Http success
        /// status code
        /// </summary>
        [Test]
        async public Task MakeServiceCall_HttpResponse_Success() {
            //Create Http request with correct URL
            HttpMethod testMethod = new HttpMethod("GET");
            HttpRequestMessage testRequest = new HttpRequestMessage(testMethod,
                "https://dijkstras-steakhouse-restapi.herokuapp.com/menuItems");
            CancellationToken testToken = new CancellationToken(false);

            //Make request
            HttpResponseMessage response = await client.SendAsync(testRequest);
            //Success is response is successful
            if (response.IsSuccessStatusCode) { Assert.Pass(); }
            else { Assert.Fail(); }

        }

        /// <summary>
        /// MakeServiceCall_HttpResponse_NotFound
        /// Tests if incorrect URL gets an Http 
        /// NotFound stutus code
        /// </summary>
        [Test]
        async public Task MakeServiceCall_HttpResponse_NotFound() {
            //Create Http request with correct URL
            HttpMethod testMethod = new HttpMethod("GET");
            //URL adjusted to be incorrect (i.e. not part of the server)
            HttpRequestMessage testRequest = new HttpRequestMessage(testMethod,
                "https://dijkstras-steakhouse-restapi.herokuapp.com/listOfFavoriteMovies");
            CancellationToken testToken = new CancellationToken(false);

            //Make request
            HttpResponseMessage response = await client.SendAsync(testRequest);
            //Success if status code is 404
            Assert.AreEqual((int)response.StatusCode, 404);

        }

        /// <summary>
        /// CreateHttpRequest_CorrectObjectSerialize
        /// Tests if the json serialize works on
        /// general object.  Fails if not
        /// </summary>
        [Test]
        public void CreateHttpRequest_CorrectObjectSerialize() {
            object testObject = new TestClass(5, "poop", "getoutofmyswamp");

            try {
                JsonConvert.SerializeObject(testObject);
            }
            catch (JsonException e) {
                Assert.Fail();
            }
            Assert.Pass();
        }

        /// <summary>
        /// menuPage_ViewMenuItems
        /// Tests if menu items are present
        /// in server
        /// </summary>
        
        [Test]
        public void menuPage_ViewMenuItems(){
            //await GetMenuItemsRequest.SendGetMenuItemsRequest();
            
            int menuItemCount = testRealm.menuItems.Count();
            Assert.AreEqual(menuItemCount, 8);
            
        }
        

        /// <summary>
        /// RefillButton_CorrectEmployeeId
        /// Tests if Refill Button successfully
        /// sends refill request with correct
        /// employee id
        /// </summary>
        /// <returns></returns>
        [Test]
        async public Task RefillButton_CorrectEmployeeId(){
            bool testResponse = await AddNotificationRequest.SendAddNotificationRequest("5e8e6d4b9696520004639e73", "1", "Refill");
            Assert.IsTrue(testResponse);
        }

        /// <summary>
        /// RefillButton_IncorrectEmployeeId
        /// Tests if Refill Button fails to
        /// sends refill request with incorrect
        /// employee id
        /// </summary>
        /// <returns></returns>
        [Test]
        async public Task RefillButton_IncorrectEmployeeId(){
            bool testResponse = await AddNotificationRequest.SendAddNotificationRequest("05e8e6d4b9696520004639e73", "1", "Refill");
            Assert.IsTrue(!testResponse);
        }

        /// <summary>
        /// CheckOut_MenuItemsAdd
        /// Tests if Menu Items get added
        /// to orders correctly
        /// </summary>
        /// <returns></returns>
        [Test]
        public void CheckOut_MenuItemsAdd(){
            //await GetMenuItemsRequest.SendGetMenuItemsRequest();
            OrderList testOrder = new OrderList();

            
            //TabletApp.Models.MenuItem testItem = RealmManager.All<MenuList>().First().menuItems[0];
            //TabletApp.Models.MenuItem testItem = testRealm.menuItems[0];

            //testOrder.orderItems.Add(testItem);
            TabletApp.Models.MenuItem testItem = testRealm.menuItems[0];
            TabletApp.Models.OrderItem testOrderItem = new OrderItem(testItem);
            Assert.AreEqual(testOrder.orderItems[0].name, "Dijktra's Jalenpeno Poppers");
            
            
            
        }

        
        [Test]
        public void CheckOut_MenuItemsRemove(){
            //await GetMenuItemsRequest.SendGetMenuItemsRequest();

            OrderList testOrder = new OrderList();
            TabletApp.Models.MenuItem testItem = testRealm.menuItems[0];
            TabletApp.Models.OrderItem testOrderItem = new OrderItem(testItem);

            testOrder.orderItems.Add(testOrderItem);
            testOrder.orderItems.Remove(testOrderItem);

            Assert.IsEmpty(testOrder.orderItems);

        }
        

    }
}