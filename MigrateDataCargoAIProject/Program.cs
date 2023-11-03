using Microsoft.Azure.Cosmos;
using MigrateDataCargoAIProject.DataModel;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Globalization;
using System.Security.Principal;

namespace MigrateDataCargoAIProject
{
    internal class Program
    {
        private static string _conenctionStringSource = "AccountEndpoint=https://canary-cargo-ai-db.documents.azure.com:443/;AccountKey=xESejGmRavXm0MLTVxso82J3VzSbx0DwCHSzaeLoqY7n1dTedDp10d9mHy2VLdZ43bixHhkr6nnhACDbr1Ms5Q==;";
        /**Change this **/
        private static string _connectionStringProd = "AccountEndpoint=https://cargoai-beta-cosmo-db.documents.azure.com:443/;AccountKey=rpm0vxBOfHfOszYxQnQJheC4ldRSNgUEkKGwI9KRAFVNiYzxmXO8UtQCCe4uk8CkfidpuhGdEV89ACDb2jQjVQ==;";

        private static CosmosClient _client = new CosmosClient(_conenctionStringSource);
        private static Database _databaseSource = null;

        private static CosmosClient _clientProd = new CosmosClient(_connectionStringProd);
        private static Database _databaseProd = null;

        static async Task Main(string[] args)
        {
            Console.WriteLine($"Begin at: {DateTime.Now}");
            _databaseSource = _client.GetDatabase("CargoAI");

            //Production cosmo db
            _databaseProd = await _clientProd.CreateDatabaseIfNotExistsAsync("CargoAI");


            await GetUsers();

            await GetServiceActivation();

            await GetAirlines();

            await GetSubscriptionTrack();

            await GetEventsNonDelivered();

            await GetEventsDelivered();

            await GetAutoTrackingClients();

            await GetAutoTrackingTransactionData();

            Console.WriteLine($"Finish at: {DateTime.Now}");
            Console.ReadKey();

        }

        static async Task GetUsers()
        {
            Console.WriteLine($"Get Users_______________");
            Container containerUsers = _databaseSource.GetContainer("Users");
            string query = "select * from c where c.PartitionKey = \"15133\" or c.PartitionKey = \"35705\" or c.PartitionKey = \"37152\" or c.PartitionKey = \"37339\" or c.PartitionKey = \"37357\" \r\nor c.PartitionKey = \"37819\" or c.PartitionKey = \"37845\"";
            var records = containerUsers.GetItemQueryIterator<UserSettingEntity>(query);
            var list = await records.ReadNextAsync();

            Console.WriteLine($"   Users Elements Count: {list.Count}");

            Container containerProd = await _databaseProd.CreateContainerIfNotExistsAsync("Users", "/PartitionKey");
            for (int i = 0; i < list.Count; i = i + 1000)
            {
                var items = list.Skip(i).Take(1000);
                Console.WriteLine($"   __________Insert Elements Count: {items.Count()}");
                foreach (var item in items)
                {
                    try
                    {
                        var contcreated = await containerProd.CreateItemAsync<UserSettingEntity>(item);
                    }
                    catch (Exception ex)
                    {
                        continue;
                    }
                }
                Console.WriteLine($"   __________Finish Insert Elements Count: {items.Count()}");
            }

            Console.WriteLine($"****Users data was migrated succefull****");
        }

        static async Task GetServiceActivation()
        {
            Console.WriteLine($"Get ServiceActivation_______________");
            Container container = _databaseSource.GetContainer("ServiceActivation");
            string query = "select * from c where c.PartitionKey = \"15133\" or c.PartitionKey = \"35705\" or c.PartitionKey = \"37152\" or c.PartitionKey = \"37339\" or c.PartitionKey = \"37357\" \r\nor c.PartitionKey = \"37819\" or c.PartitionKey = \"37845\"";
            
            var records = container.GetItemQueryIterator<ServiceActivationEntity>(query);
            var list = await records.ReadNextAsync();

            Console.WriteLine($"   ServiceActivation Elements Count: {list.Count}");

            Container containerProd = await _databaseProd.CreateContainerIfNotExistsAsync("ServiceActivation", "/PartitionKey");
            for (int i = 0; i < list.Count; i = i + 1000)
            {
                var items = list.Skip(i).Take(1000);
                Console.WriteLine($"   __________Insert Elements Count: {items.Count()}");
                foreach (var item in items)
                {
                    try
                    {
                        var contcreated = await containerProd.CreateItemAsync<ServiceActivationEntity>(item);
                    }
                    catch (Exception ex)
                    {
                        continue;
                    }
                }
                Console.WriteLine($"   __________Finish Insert Elements Count: {items.Count()}");
            }

            Console.WriteLine($"****ServiceActivation data was migrated succefull****");
        }

        static async Task GetAirlines()
        {
            Console.WriteLine($"Get Airlines_______________");
            Container container = _databaseSource.GetContainer("Airline");
            string query = "select * from c";
            var records = container.GetItemQueryIterator<AirlinesEntity>(query);
            var list = await records.ReadNextAsync();

            Console.WriteLine($"   Airlines Elements Count: {list.Count}");

            Container containerProd = await _databaseProd.CreateContainerIfNotExistsAsync("Airline", "/PartitionKey");
            for (int i = 0; i < list.Count; i = i + 1000)
            {
                var items = list.Skip(i).Take(1000);
                Console.WriteLine($"   __________Insert Elements Count: {items.Count()}");
                foreach (var item in items)
                {
                    try
                    {
                        var contcreated = await containerProd.CreateItemAsync<AirlinesEntity>(item);
                    }
                    catch (Exception ex)
                    {
                        continue;
                    }
                }
                Console.WriteLine($"   __________Finish Insert Elements Count: {items.Count()}");
            }

            Console.WriteLine($"****Airlines data was migrated succefull****");
        }

        static async Task GetSubscriptionTrack()
        {
            var list = new List<SubscriptionTrackEntity>();
            Console.WriteLine($"Get SubscriptionTrack_______________");
            Container container = _databaseSource.GetContainer("SubscriptionTrack");
            string query = "select * from c where c.PartitionKey = \"15133\" or c.PartitionKey = \"35705\" or c.PartitionKey = \"37152\" or c.PartitionKey = \"37339\" or c.PartitionKey = \"37357\" \r\nor c.PartitionKey = \"37819\" or c.PartitionKey = \"37845\"";
            
            var records = container.GetItemQueryIterator<SubscriptionTrackEntity>(query);
            list.AddRange(await records.ReadNextAsync());

            while (records.HasMoreResults)
            {
                var response = await records.ReadNextAsync();
                list.AddRange(response);
            }

            Console.WriteLine($"   SubscriptionTrack Elements Count: {list.Count}");

            Container containerProd = await _databaseProd.CreateContainerIfNotExistsAsync("SubscriptionTrack", "/PartitionKey");
            for (int i = 0; i < list.Count; i = i + 1000)
            {
                var items = list.Skip(i).Take(1000);
                Console.WriteLine($"   __________Insert Elements Count: {items.Count()}");
                foreach (var item in items)
                {
                    try
                    {
                        var contcreated = await containerProd.CreateItemAsync<SubscriptionTrackEntity>(item);
                    }
                    catch (Exception ex)
                    {
                        continue;
                    }
                }
                Console.WriteLine($"   __________Finish Insert Elements Count: {items.Count()}");
            }

            Console.WriteLine($"****SubscriptionTrack data was migrated succefull****");
        }

        static async Task GetEventsNonDelivered()
        {
            var list = new List<EventsNonDeliveredEntity>();
            Console.WriteLine($"Get EventsNonDelivered_______________");
            Container container = _databaseSource.GetContainer("EventsNonDelivered");
            string query = "select * from c where c.CreatedDate >= \"2023-08-01T00:00:00.0000000Z\" and " +
              "(c.PartitionKey = \"15133\" or c.PartitionKey = \"35705\" or c.PartitionKey = \"37152\" or c.PartitionKey = \"37339\" or c.PartitionKey = \"37357\" or c.PartitionKey = \"37819\" or c.PartitionKey = \"37845\")";
            
            var records = container.GetItemQueryIterator<EventsNonDeliveredEntity>(query);
            list.AddRange(await records.ReadNextAsync());

            while (records.HasMoreResults)
            {
                var response = await records.ReadNextAsync();
                list.AddRange(response);
            }

            Console.WriteLine($"   EventsNonDelivered Elements Count: {list.Count}");

            Container containerProd = await _databaseProd.CreateContainerIfNotExistsAsync("EventsNonDelivered", "/PartitionKey");
            for (int i = 0; i < list.Count; i = i + 1000)
            {
                var items = list.Skip(i).Take(1000);
                Console.WriteLine($"   __________Insert Elements Count: {items.Count()}");
                foreach (var item in items)
                {
                    try
                    {
                        var contcreated = await containerProd.CreateItemAsync<EventsNonDeliveredEntity>(item);
                    }
                    catch (Exception ex)
                    {
                        continue;
                    }
                }
                Console.WriteLine($"   __________Finish Insert Elements Count: {items.Count()}");
            }

            Console.WriteLine($"****EventsNonDelivered data was migrated succefull****");
        }

        static async Task GetEventsDelivered()
        {
            var list = new List<EventsDeliveredEntity>();
            Console.WriteLine($"Get EventsDelivered_______________");
            Container container = _databaseSource.GetContainer("EventsDelivered");
            string query = "select * from c where c.PartitionKey = \"15133\" or c.PartitionKey = \"35705\" or c.PartitionKey = \"37152\" or c.PartitionKey = \"37339\" or c.PartitionKey = \"37357\" or c.PartitionKey = \"37819\" or c.PartitionKey = \"37845\"";
            
            var records = container.GetItemQueryIterator<EventsDeliveredEntity>(query);
            list.AddRange(await records.ReadNextAsync());

            while (records.HasMoreResults)
            {
                var response = await records.ReadNextAsync();
                list.AddRange(response);
            }

            Console.WriteLine($"   EventsDelivered Elements Count: {list.Count}");

            Container containerProd = await _databaseProd.CreateContainerIfNotExistsAsync("EventsDelivered", "/PartitionKey");
            for (int i = 0; i < list.Count; i = i + 1000)
            {
                var items = list.Skip(i).Take(1000);
                Console.WriteLine($"   __________Insert Elements Count: {items.Count()}");
                foreach (var item in items)
                {
                    try
                    {
                        var contcreated = await containerProd.CreateItemAsync<EventsDeliveredEntity>(item);
                    }
                    catch (Exception ex)
                    {
                        continue;
                    }
                }
                Console.WriteLine($"   __________Finish Insert Elements Count: {items.Count()}");
            }

            Console.WriteLine($"****EventsDelivered data was migrated succefull****");
        }

        static async Task GetAutoTrackingClients()
        {
            var list = new List<AutoTrackingClientsEntity>();
            Console.WriteLine($"Get AutoTrackingClients_______________");
            Container container = _databaseSource.GetContainer("AutoTrackingClients");
            string query = "select * from c where c.PartitionKey = \"15133\" or c.PartitionKey = \"35705\" or c.PartitionKey = \"37152\" or c.PartitionKey = \"37339\" or c.PartitionKey = \"37357\" or c.PartitionKey = \"37819\" or c.PartitionKey = \"37845\"";
            
            var records = container.GetItemQueryIterator<AutoTrackingClientsEntity>(query);
            list.AddRange(await records.ReadNextAsync());

            while (records.HasMoreResults)
            {
                var response = await records.ReadNextAsync();
                list.AddRange(response);
            }

            Console.WriteLine($"   AutoTrackingClients Elements Count: {list.Count}");

            Container containerProd = await _databaseProd.CreateContainerIfNotExistsAsync("AutoTrackingClients", "/PartitionKey");
            for (int i = 0; i < list.Count; i = i + 1000)
            {
                var items = list.Skip(i).Take(1000);
                Console.WriteLine($"   __________Insert Elements Count: {items.Count()}");
                foreach (var item in items)
                {
                    try
                    {
                        var contcreated = await containerProd.CreateItemAsync<AutoTrackingClientsEntity>(item);
                    }
                    catch (Exception ex)
                    {
                        continue;
                    }
                }
                Console.WriteLine($"   __________Finish Insert Elements Count: {items.Count()}");
            }

            Console.WriteLine($"****AutoTrackingClients data was migrated succefull****");
        }

        static async Task GetAutoTrackingTransactionData()
        {
            var list = new List<AutoTrackingTransactionDataEntity>();
            Console.WriteLine($"Get AutoTrackingTransactionData_______________");
            Container container = _databaseSource.GetContainer("AutoTrackingTransactionData");
            string query = "select * from c where c.PartitionKey = \"15133\" or c.PartitionKey = \"35705\" or c.PartitionKey = \"37152\" or c.PartitionKey = \"37339\" or c.PartitionKey = \"37357\" or c.PartitionKey = \"37819\" or c.PartitionKey = \"37845\"";
            
            var records = container.GetItemQueryIterator<AutoTrackingTransactionDataEntity>(query);
            list.AddRange(await records.ReadNextAsync());

            while (records.HasMoreResults)
            {
                var response = await records.ReadNextAsync();
                list.AddRange(response);
            }

            Console.WriteLine($"   AutoTrackingTransactionData Elements Count: {list.Count}");

            Container containerProd = await _databaseProd.CreateContainerIfNotExistsAsync("AutoTrackingTransactionData", "/PartitionKey");
            for (int i = 0; i < list.Count; i = i + 1000)
            {
                var items = list.Skip(i).Take(1000);
                Console.WriteLine($"   __________Insert Elements Count: {items.Count()}");
                foreach (var item in items)
                {
                    try
                    {
                        var contcreated = await containerProd.CreateItemAsync<AutoTrackingTransactionDataEntity>(item);
                    }
                    catch (Exception ex)
                    {
                        continue;
                    }
                }
                Console.WriteLine($"   __________Finish Insert Elements Count: {items.Count()}");
            }

            Console.WriteLine($"****AutoTrackingTransactionData data was migrated succefull****");
        }
    }
}