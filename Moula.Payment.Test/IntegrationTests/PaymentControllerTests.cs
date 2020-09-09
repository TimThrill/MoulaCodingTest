using Moula.Payment.Domain;
using Moula.Payment.GateWay;
using Moula.Payment.GateWay.Application.Commands;
using Moula.Payment.GateWay.Application.ViewModels;
using Moula.Payment.Test.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
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
            _client = _factory.CreateClient();
        }

        [Fact]
        public async Task Get_HealthCheck_Ok()
        {
            // Act
            var response = await _client.GetAsync("api/Payment/HealthCheck");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        #region Test CreatePayment endpoint
        [Fact]
        public async Task Post_CreatePaymentWithAmountLessThanZero_BadRequest()
        {
            var content = new StringContent(JsonSerializer.Serialize<CreatePaymentCommand>(
                new CreatePaymentCommand
                {
                    UserId = 1,
                    // Test amount less than zero
                    Amount = -1,
                    CreatedDate = DateTimeOffset.UtcNow
                }), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("api/Payment/CreatePayment", content);
            var responseContent = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Post_CreatePaymentWithNonExistingUser_BadRequest()
        {
            var content = new StringContent(JsonSerializer.Serialize<CreatePaymentCommand>(
                new CreatePaymentCommand
                {
                    // Test non-existing user
                    UserId = -1,
                    Amount = 100,
                    CreatedDate = DateTimeOffset.UtcNow
                }), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("api/Payment/CreatePayment", content);
            var responseContent = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Post_CreatePaymentWithInsufficientFund_Ok()
        {
            var content = new StringContent(JsonSerializer.Serialize<CreatePaymentCommand>(
                new CreatePaymentCommand
                {
                    UserId = 1,
                    // Test insufficient fund
                    Amount = Utilities.InitialBalance + 1,
                    CreatedDate = DateTimeOffset.UtcNow
                }), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("api/Payment/CreatePayment", content);
            var responseContent = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        #endregion

        #region Test ProcessPaymentAsync endpoint
        [Fact]
        public async Task Post_ProcessPendingPayment_Ok()
        {
            var content = new StringContent(JsonSerializer.Serialize<ProcessPaymentCommand>(
                new ProcessPaymentCommand
                {
                    PaymentId = Utilities.PendingPaymentForProcess.Id
                }), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("api/Payment/ProcessPayment", content);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Post_ProcessClosedOrProcessedPayment_BadRequest()
        {
            var content = new StringContent(JsonSerializer.Serialize<ProcessPaymentCommand>(
                new ProcessPaymentCommand
                {
                    PaymentId = Utilities.ClosedPayment.Id
                }), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("api/Payment/ProcessPayment", content);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            content = new StringContent(JsonSerializer.Serialize<ProcessPaymentCommand>(
                new ProcessPaymentCommand
                {
                    PaymentId = Utilities.ProcessedPayment.Id
                }), Encoding.UTF8, "application/json");

            // Act
            response = await _client.PostAsync("api/Payment/ProcessPayment", content);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
        #endregion

        #region Test CancelPaymentAsync endpoint
        [Fact]
        public async Task Post_CancelNonExistingPayment_BadRequest()
        {
            var content = new StringContent(JsonSerializer.Serialize<CancelPaymentCommand>(
                new CancelPaymentCommand
                {
                    // Non-existing Guid
                    PaymentId = Guid.Empty
                }), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("api/Payment/CancelPayment", content);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Post_CancelProcessedOrClosedPayment_BadRequest()
        {
            var content = new StringContent(JsonSerializer.Serialize<CancelPaymentCommand>(
                new CancelPaymentCommand
                {
                    // Closed payment id
                    PaymentId = Utilities.ClosedPayment.Id
                }), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("api/Payment/CancelPayment", content);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            content = new StringContent(JsonSerializer.Serialize<CancelPaymentCommand>(
                new CancelPaymentCommand
                {
                    // Closed payment id
                    PaymentId = Utilities.ProcessedPayment.Id
                }), Encoding.UTF8, "application/json");

            // Act
            response = await _client.PostAsync("api/Payment/CancelPayment", content);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Post_CancelPendingPayment_Ok()
        {
            var content = new StringContent(JsonSerializer.Serialize<CancelPaymentCommand>(
                new CancelPaymentCommand
                {
                    // Closed payment id
                    PaymentId = Utilities.PendingPaymentForCancel.Id
                }), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("api/Payment/CancelPayment", content);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        #endregion

        #region Test GetBalanceAndPayments endpoint
        [Fact]
        public async Task Get_GetAccountBalanceAndPaymentsWithoutUserId_NotFound()
        {
            var response = await _client.GetAsync("api/Payment/GetBalanceAndPayments");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Get_GetUserAccountBalanceAndPayments_Ok()
        {
            // Act
            var response = await _client.GetAsync($"api/Payment/GetBalanceAndPayments?userId={Utilities.DefaultUserId}");
            var userAccountBalanceAndPayment = JsonSerializer
                .Deserialize<BalanceAndPaymentsViewModel>(await response.Content.ReadAsStringAsync()
                , new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        #endregion
    }
}
