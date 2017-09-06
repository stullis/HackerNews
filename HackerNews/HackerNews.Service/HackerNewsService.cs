using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using Newtonsoft.Json.Linq;
using System.IO;

namespace HackerNews.Service
{
    public interface IHackerNewsRestService
    {
        JArray getBestStories();
    }
    public class HackerNewsRestService : IHackerNewsRestService
    {
        private string url { get; set; } = "https://hacker-news.firebaseio.com/v0/";

        public JArray getBestStories()
        {
            JArray storyIDs = (JArray)RestRequest("beststories", Method.GET, "");
            JArray storyItems = new JArray();
            foreach (var storyID in storyIDs.ToObject<List<string>>())
            {
                JObject storyItem = (JObject)RestRequest("item/" + storyID, Method.GET, "");
                JObject thinStoryItem = new JObject(new JProperty("title", storyItem["title"]), new JProperty("author", storyItem["by"]), new JProperty("url", storyItem["url"]));
                storyItems.Add(thinStoryItem);
            }

            return storyItems;
        }

        private Object RestRequest(String resource, Method restMethod, String requestBody)
        {
            RestClient client = new RestClient(url);
            RestRequest rest = new RestRequest(resource + ".json?print=pretty", restMethod);

            rest.RequestFormat = DataFormat.Json;
            rest.AddHeader("Content-Type", "application/json");

            if (String.IsNullOrEmpty(requestBody) == false)
            {
                rest.AddParameter("application/json", requestBody, ParameterType.RequestBody);
            }
            IRestResponse response = client.Execute(rest);
            using (JsonTextReader jsreader = new JsonTextReader(new StringReader(response.Content)))
            {
                try
                {
                    return new JsonSerializer().Deserialize(jsreader);
                }
                catch (JsonReaderException jsonException)
                {

                    string outerExceptionText = string.Format("Error deserializing json\r\nResponse code: {0}\r\nEndpoint: {1}\r\nResponse content:\r\n{2}"
                        , response.StatusCode, resource, response.Content);
                    throw new Exception(outerExceptionText, jsonException);
                }
            }
        }

    }
}
