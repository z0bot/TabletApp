using TabletApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace TabletApp
{
    class ServiceRequestHandler
    {
        //returns an object of type "T" from the server
        public static async Task<T> MakeServiceCall<T>(ServiceRequest req, object body = null, Dictionary<string, string> urlParams = null) where T : new()
        {
            HttpClient client = new HttpClient();

            //makes actual request object
            using (var request = CreateHttpRequest(req, body, urlParams))
            {
                //generic response object
                T obj = new T();
                try
                {
                    //the response from the server
                    HttpResponseMessage response = await client.SendAsync(request);
                    //string response (a.k.a. the JSON response)
                    var content = await response.Content.ReadAsStringAsync();

                    //if we get a 200 series back
                    if (response.IsSuccessStatusCode)
                    {
                        try //to deserialize the object
                        {
                            obj = JsonConvert.DeserializeObject<T>(content);
                            return obj;
                        }
                        catch (Exception ex) //error deserializing object
                        {
                            Debug.WriteLine("Malformed response. Message: " + ex.Message);
                            return obj;
                        }
                    }
                    else //response has an error status
                    {
                        return obj;
                    }
                }
                catch (Exception ex)//any other error
                {
                    Debug.WriteLine("An error occured. Message: " + ex.Message);
                    return obj;
                }
            }
        }

        private static HttpRequestMessage CreateHttpRequest(ServiceRequest serviceRequest, object body = null, Dictionary<string, string> urlParams = null)
        {
            var url = (urlParams == null) ? serviceRequest.Url : AddURLParams(serviceRequest.Url, urlParams); //if there are URL params >> add them to url
            var httpRequestMessage = new HttpRequestMessage(serviceRequest.Method, url);

            if (body != null)
            {
                try //serialize body object and add it to the request
                {
                    var bodyJson = JsonConvert.SerializeObject(body);

                    var requestBody = new StringContent(bodyJson, Encoding.UTF8, "application/json");
                    httpRequestMessage.Content = requestBody;
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            return httpRequestMessage;
        }

        // Builds the URL paramaters into the URL
        private static string AddURLParams(string url, Dictionary<string, string> urlParams)
        {
            url += "?";
            foreach (KeyValuePair<string, string> entry in urlParams)
            {
                url += $"{entry.Key}={HttpUtility.UrlEncode(entry.Value)}&";
            }

            return url.TrimEnd('&');
        }
    }
}