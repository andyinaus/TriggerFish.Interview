using System;
using System.Net.Http;
using System.Threading.Tasks;
using Geocoding.Google;

namespace TriggerFish.AddressParser.Core
{
    public class AddressParser
    {
        private readonly GoogleGeocoder _googleGeocoder;
        private readonly HttpClient _client;
        public const string GoogleGeoCodeBaseUrl = "https://maps.googleapis.com/maps/api/geocode/json";

        public AddressParser(GoogleGeocoder googleGeocoder)
        {
            _googleGeocoder = googleGeocoder ?? throw new ArgumentNullException(nameof(googleGeocoder));
        }

        public async Task<LookupResult> GetPostCodeByAddress(string address)
        {
            if (string.IsNullOrWhiteSpace(address)) throw new ArgumentNullException(nameof(address));

            var uri = new Uri($"{GoogleGeoCodeBaseUrl}?address=");

            _client.GetAsync(new Uri(g))
        }
    }
}
