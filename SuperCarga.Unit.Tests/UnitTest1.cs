using NUnit.Framework;
using SuperCarga.Application.Domain.Location.Dto;
using SuperCarga.Domain.Domain.Location;
using System;
using System.IO;
using System.Threading.Tasks;

namespace SuperCarga.Unit.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task Test1()
        {
            var distanceSrv = new DistanceService(new Application.Settings.LocationConfig
            {
                LocalizationServiceBaseUrl = "http://186.46.63.250:4000/nominatim/search.php",
                DistanceServiceBaseUrl = "https://routing.openstreetmap.de/routed-car/route/v1/driving"
            }, null);

            var from = new AddressDto
            {
                City = "Manta",
                Street = "av flavio reyes",
                PostCode = "130203"
            };

            var to = new AddressDto
            {
                City = "Quito",
                Street = "Carlos Tobar",
                PostCode = "170517"
            };

            var res = await distanceSrv.CheckDistance(from, to);
        }
    }
}