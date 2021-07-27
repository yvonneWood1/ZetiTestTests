using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using ZetiTest.Controllers;

namespace ZetiTest.Tests {
    public class Tests {

        private static HttpClient _client = HttpClientFactory.Create();
        private static CancellationToken _cancellationToken = new CancellationToken();
        private static string _startDate = "2021-02-01T00:00:00+00:00";
        private static string _endDate = "2021-02-28T23:59:00Z";
        private BillingSwaggerClient _billingClient;
        // Feed in Meters => Miles
        private static double _metersToMiles = 1609.34;
        // In £ per mile
        private static double _ratePerMile = 0.207;

        [SetUp]
        public void Setup() {
            _billingClient = new BillingSwaggerClient(_client);
        }

        [Test]
        public async Task GetVehiclesAllAsyncListType() {
            var response = _billingClient.HistoryAsync(_startDate).Result;
            var collection = await Task.FromResult(response);
            var history = new VehicleHistory();
            CollectionAssert.AllItemsAreInstancesOfType(collection, history.GetType());
            await Task.FromResult(collection);
        }

        [Test]
        public async Task GetVehiclesAllAsyncWrongType() {
            var response = _billingClient.HistoryAsync(_startDate).Result;
            var collection = await Task.FromResult(response);
            var stringTest = new List<string>();
            stringTest.Add("Nope");
            stringTest.Add("Not");
            CollectionAssert.IsNotSubsetOf(stringTest, collection);
            await Task.FromResult(stringTest);
        }
    }
}