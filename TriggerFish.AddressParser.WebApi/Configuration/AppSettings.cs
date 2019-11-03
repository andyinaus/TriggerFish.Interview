using System;
using System.Configuration;

namespace TriggerFish.AddressParser.WebApi.Configuration
{
    public class AppSettings
    {
        private AppSettings() { }

        public string GoogleApiKey { get; private set; }

        public bool IsPartialMatchAcceptable { get; private set; }

        public static AppSettings LoadFromConfiguration()
        {
            if (string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["GoogleApiKey"])) throw new ApplicationException($"'{nameof(GoogleApiKey)}' is either missing or invalid in appSettings of web.config.");
            if (!bool.TryParse(ConfigurationManager.AppSettings["IsPartialMatchAcceptable"], out var isPartialMatchAcceptable)) throw new ApplicationException($"'{nameof(IsPartialMatchAcceptable)}' is either missing or invalid in appSettings of web.config.");

            return new AppSettings
            {
                GoogleApiKey = ConfigurationManager.AppSettings["GoogleApiKey"],
                IsPartialMatchAcceptable = isPartialMatchAcceptable
            };
        }
    }
}