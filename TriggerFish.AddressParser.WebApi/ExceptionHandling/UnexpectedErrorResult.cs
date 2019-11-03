using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using TriggerFish.AddressParser.WebApi.Constants;
using TriggerFish.AddressParser.WebApi.Models.v1;

namespace TriggerFish.AddressParser.WebApi.ExceptionHandling
{
    public class UnexpectedErrorResult : IHttpActionResult
    {
        public const string ErrorMessage = "Oops! Please contact support@triggerfish.fake.com.au for help.";

        public HttpRequestMessage Request { get; set; }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = new HttpResponseMessage(HttpStatusCode.ServiceUnavailable)
            {
                Content = new ObjectContent<UnexpectedError>(new UnexpectedError
                {
                    Message = ErrorMessage
                }, new JsonMediaTypeFormatter(), MediaTypes.Json),
                RequestMessage = Request
            };

            return Task.FromResult(response);
        }
    }
}