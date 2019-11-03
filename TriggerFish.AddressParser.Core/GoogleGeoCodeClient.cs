using System;
using System.Net.Http;

namespace TriggerFish.AddressParser.Core
{
    public class GoogleGeoCodeClient
    {
        private readonly HttpClient _client;

        public const string GoogleGeoCodeBaseUrl = "https://maps.googleapis.com/maps/api/geocode/json";

        public GoogleGeoCodeClient(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public 
    }
}