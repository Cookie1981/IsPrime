using System;
using System.Net.Http;
using Banshee;
using Bivouac.Abstractions;
using Bivouac.Events;
using IsPrime;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace IsPrimeTests
{
    public class IsPrimeTests
    {
        private HttpResponseMessage _response;
        private LightweightWebApiHost _testHost;

        public IsPrimeTests()
        {
            Action<IServiceCollection> configureServices = services =>
            {
                Startup.ConfigureServices(services);
                services.AddTransient<IHttpServerEventCallback, NoOpHttpServerEventCallback>();
            };
            _testHost = new LightweightWebApiHost(configureServices, Startup.Configure);
        }

        [Fact]
        public void Test()
        {
            _response = _testHost.Get("api/prime/2");

            var content = _response.Content.ReadAsStringAsync().Result.Contains("true");

//            var primeChecker = new IsPrime.Controllers.PrimeController();
//
//            var result = primeChecker.Get(2);

            Assert.True(content);
        }

        [Theory]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(29)]
        [InlineData(997)]
        public void ShoudlReturnTrueForPrimeNumbers(int numberToCheck)
        {
            _response = _testHost.Get("api/prime/" + numberToCheck.ToString());

            var content = _response.Content.ReadAsStringAsync().Result.Contains("true");

            Assert.True(content);
        }

    }
}
