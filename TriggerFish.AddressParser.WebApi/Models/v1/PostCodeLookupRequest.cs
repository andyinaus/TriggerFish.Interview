using System.Collections.Generic;

namespace TriggerFish.AddressParser.WebApi.Models.v1
{
    public class PostCodeLookupRequest
    {
        public IEnumerable<string> Addresses { get; set; }
    }
}