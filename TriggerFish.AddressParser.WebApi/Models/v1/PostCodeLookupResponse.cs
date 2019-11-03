using System.Collections.Generic;

namespace TriggerFish.AddressParser.WebApi.Models.v1
{
    public class PostCodeLookupResponse
    {
        public IEnumerable<string> PostCodes { get; set; }
    }
}