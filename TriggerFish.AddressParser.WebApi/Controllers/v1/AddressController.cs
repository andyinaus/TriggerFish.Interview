using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Geocoding.Google;
using TriggerFish.AddressParser.WebApi.Configuration;
using TriggerFish.AddressParser.WebApi.Models.v1;

namespace TriggerFish.AddressParser.WebApi.Controllers.v1
{
    public class AddressController : ApiController
    {
        private readonly AppSettings _appSettings;

        public AddressController(AppSettings appSettings)
        {
            _appSettings = appSettings ?? throw new ArgumentNullException(nameof(appSettings));
        }

        [HttpPost]
        //TODO: unit
        public async Task<IHttpActionResult> Post([FromBody] PostCodeLookupRequest request)
        {
            //TODO: Investigate why fluent validation failed to validate parent node
            if (request == null) return BadRequest("The list of addresses must be provided in the correct format.");

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var tasks = request.Addresses.Select(TryGetFirstMatchingPostCode);

            return Ok(new PostCodeLookupResponse
            {
                PostCodes = (await Task.WhenAll(tasks.ToArray())).Distinct()
            });
        }

        //TODO: Refactor + unit
        private async Task<string> TryGetFirstMatchingPostCode(string address)
        {
            var googleAddresses = await new GoogleGeocoder(_appSettings.GoogleApiKey).GeocodeAsync(address);

            foreach (var googleAddress in googleAddresses)
            {
                if (googleAddress.IsPartialMatch && !_appSettings.IsPartialMatchAcceptable) continue;

                foreach (var googleAddressComponent in googleAddress.Components)
                {
                    if (googleAddressComponent.Types.Any(t => t == GoogleAddressType.PostalCode))
                    {
                        return googleAddressComponent.LongName;
                    }
                }
            }

            return "Unknown";
        }
    }
}
