using Moula.Payment.GateWay;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Moula.Payment.Test.IntegrationTests
{
    public class PaymentControllerTests: IClassFixture<MoulaWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;
        private readonly MoulaWebApplicationFactory<Startup> _factory;

        public PaymentControllerTests(MoulaWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Get_HealthCheck_ReturnOk()
        {
            // Act
            var response = await _client.GetAsync("api/Payment/HealthCheck");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
