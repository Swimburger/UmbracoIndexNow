using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Umbraco.Core.Logging;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;
using Umbraco.Web.WebApi;

namespace UmbracoIndexNow.IndexNow
{
    public class IndexNowController : UmbracoAuthorizedApiController
    {
        public static readonly HttpClient httpClient = new HttpClient();
        private readonly ILogger logger;

        public IndexNowController(ILogger logger)
        {
            this.logger = logger;
        }

        [HttpPost]
        public async Task<object> Submit(int nodeId)
        {
            // get URL of the Umbraco content
            var nodeUrl = Umbraco.Content(nodeId).Url(mode: UrlMode.Relative);

            // create IndexNow object
            var indexNowObject = new
            {
                host = IndexNowConfiguration.Host,
                key = IndexNowConfiguration.Key,
                keyLocation = IndexNowConfiguration.KeyLocation,
                urlList = new string[]
                {
                    $"{IndexNowConfiguration.BaseUrl}{nodeUrl}"
                }
            };

            // convert IndexNow object to JSON string
            var jsonString = JsonConvert.SerializeObject(indexNowObject);

            // send JSON string to IndexNow service
            var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(IndexNowConfiguration.Url, content);

            // if response is status code 200, return object with `success` = true
            if (response.IsSuccessStatusCode)
            {
                return new { success = true };
            }

            // if response statuscode is not 200
            // log an error message
            logger.Error<IndexNowController>(
                "Submitting URL to {IndexNowUrl} failed, status code: {StatusCode}",
                IndexNowConfiguration.Url,
                response.StatusCode
            );
            // log the response body as error for debugging purposes
            var responseContent = await response.Content.ReadAsStringAsync();
            logger.Error<IndexNowController>("IndexNow response: {IndexNowResponse}", responseContent);

            // return object with `success` = false and a user-friendly error message
            return new
            {
                success = false,
                errorMessage = $"Submitting URL to {IndexNowConfiguration.Url} failed, status code: {response.StatusCode}"
            };
        }
    }
}
