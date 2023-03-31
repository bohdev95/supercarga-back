using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SuperCarga.Application.Domain.Location.Abstraction;
using SuperCarga.Application.Domain.Location.Dto;
using SuperCarga.Application.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarga.Domain.Domain.Location
{
    public class Location
    {
        public string Lat { get; set; }
        public string Lon { get; set; }
    }

    public class Distance
    {
        public List<Route> Routes { get; set; }
    }

    public class Route
    {
        public decimal Distance { get; set; }
    }

    public class DistanceService : IDistanceService
    {
        private readonly LocationConfig locationConfig;
        private readonly ILogger<DistanceService> logger;

        public DistanceService(LocationConfig locationConfig, ILogger<DistanceService> logger)
        {
            this.locationConfig = locationConfig;
            this.logger = logger;
        }

        public async Task<int?> CheckDistance(AddressDto origin, AddressDto destination)
        {
            var originLocationTask = GetLocation(origin);
            var destinationLocationTask = GetLocation(destination);

            await Task.WhenAll(originLocationTask, destinationLocationTask);

            var originLocation = originLocationTask.Result;
            var destinationLocation = destinationLocationTask.Result;

            if(originLocation == null || destinationLocation == null)
            {
                return null;
            }

            var distance = await GetDistance(originLocation, destinationLocation);

            return distance;
        }

        private async Task<Location> GetLocation(AddressDto address)
        {
            var location = new Location();

            try
            {
                var client = new HttpClient();

                var parameters = new Dictionary<string, string>
                {
                    { "format", "json" },
                    { "accept-language", "en" },
                    { "addressdetails", "1" },
                    { "limit", "8" },
                    { "city", address.City },
                    { "street", address.Street },
                    { "postalcode", address.PostCode },
                    { "bounded", "1" }
                };

                var url = BuildUrl(locationConfig.LocalizationServiceBaseUrl, parameters);

                var res = await client.GetStringAsync(url);

                var locations = JsonConvert.DeserializeObject<List<Location>>(res);

                location = locations.First();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                location = null;
            }

            return location;
        }

        private string BuildUrl(string baseUrl, Dictionary<string, string> parameters)
        {
            var url = baseUrl;

            if (parameters != null && parameters.Any())
            {
                var queryParameters = parameters
                .Select(x => $"{x.Key}={x.Value}").ToList();

                var query = string.Join("&", queryParameters);

                url = $"{url}?{query}";
            }

            return url;
        }

        private async Task<int?> GetDistance(Location origin, Location destination)
        {
            int? distance = null;

            try
            {
                var client = new HttpClient();

                var query = $"{origin.Lon},{origin.Lat};{destination.Lon},{destination.Lat}";
                var url = $"{locationConfig.DistanceServiceBaseUrl}/{query}";

                var res = await client.GetStringAsync(url);

                var distanceDto = JsonConvert.DeserializeObject<Distance>(res);

                var distanceInMeters = (int)distanceDto.Routes.First().Distance;

                distance = distanceInMeters / 1000;
            }
            catch(Exception ex)
            {
                logger.LogError(ex, ex.Message);
                distance = null;
            }

            return distance;
        }

        
    }
}
