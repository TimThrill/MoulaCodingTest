﻿using Moula.Payment.Domain;
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

        #region Test CreatePayment endpoint
        [Fact]
        public async Task Post_CreatePaymentWithAmountLessThanZero_BadRequest()
        {
            var content = new StringContent(JsonSerializer.Serialize<CreatePaymentCommand>(
                new CreatePaymentCommand
                {
                    Amount = -1,
                    CreatedDate = DateTimeOffset.UtcNow
                }), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("api/Payment/CreatePayment", content);
            var responseContent = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
        #endregion

        #region Test GetBalanceAndPayments endpoint
        [Fact]
        public async Task Get_GetAccountBalanceAndPaymentsWithoutUserId_ReturnNotFound()
        {
            var response = await _client.GetAsync("api/Payment/GetBalanceAndPayments");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Get_GetUserInitialAccountBalanceAndPayments_ReturnOk()
        {
            // Act
            var response = await _client.GetAsync("api/Payment/GetBalanceAndPayments?userId=1");
            var userAccountBalanceAndPayment = JsonSerializer
                .Deserialize<BalanceAndPaymentsViewModel>(await response.Content.ReadAsStringAsync()
                , new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(Utilities.InitialBalance, userAccountBalanceAndPayment.Balance);

            var firstPayment = userAccountBalanceAndPayment.Payments.OrderBy(p => p.CreatedDate).First();
            Assert.Equal(Utilities.DefaultPaymentAmount, firstPayment.Amount);
            Assert.Equal(Enum.GetName(typeof(PaymentStatus), PaymentStatus.Pending), firstPayment.Status);
            Assert.Null(firstPayment.ClosedReason);
        }
        #endregion
    }
}
