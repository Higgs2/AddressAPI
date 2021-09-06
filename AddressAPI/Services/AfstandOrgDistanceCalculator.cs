using AddressAPI.Services.Interfaces;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;


namespace AddressAPI.Services
{
    /// <summary>
    /// Calculates distance between two cities using the Afstand.Org API.
    /// </summary>
    public class AfstandOrgDistanceCalculator : IDistanceCalculator
    {
        private readonly RestClient _restClient;

        public AfstandOrgDistanceCalculator()
        {
            _restClient = new RestClient();
            _restClient.BaseUrl = new Uri("https://nl.afstand.org/route.json");
            _restClient.ThrowOnAnyError = true;
        }

        /// <inheritdoc/>
        public float CalculateDistance(string from, string to)
        {
            RestRequest distanceRequest = new RestRequest();

            distanceRequest.AddParameter("stops", $"{from}|{to}");

            var distanceRequestResult = _restClient.Execute(distanceRequest);

            if (distanceRequestResult.StatusCode == System.Net.HttpStatusCode.OK)
            {
                JObject data = JObject.Parse(distanceRequestResult.Content);
                return ((float) data["distance"]);
            }
            return 0;
        }
    }
}
