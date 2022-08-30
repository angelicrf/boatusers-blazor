namespace BoatRazorLibrary.Models;
using Azure.Data.Tables;

public class AzureBUDB
{
    private TableServiceClient tableServiceClient = new TableServiceClient("DefaultEndpointsProtocol=https;AccountName=cosmodbbusers;AccountKey=4h5eS8y7XkUYgXRxGw4ASdFED3hWP50ewRXVwD0o9uJZuuBTwrdKuqyBrFy78dVxGgPtRvQIVRbaa6Ht8IQ1PA==;TableEndpoint=https://cosmodbbusers.table.cosmos.azure.com:443/;");


    private TableClient initializeClient(string thisTableName)
    {
        TableClient tableClient = tableServiceClient.GetTableClient(tableName: thisTableName);
        return tableClient;
    }
    public Task BUCreateTable(string thisTableName)
    {
        var getClient = initializeClient(thisTableName);
        if (getClient != null)
        {
            try
            {
                getClient.CreateIfNotExistsAsync().Wait();
                return Task.CompletedTask;
            }
            catch (Exception en)
            {

                Console.WriteLine("Error Create Table: " + en.Message);
            }
        }
        return Task.CompletedTask;
    }
    public Task BUInsertData(string thisTableName, string thisRowKey, string thisPartKey, string thisName, string thisPassword, bool thisBool)
    {
        var getClient = initializeClient(thisTableName);
        if (getClient != null)
        {
            var buLogin = new BULoginModel()
            {
                RowKey = thisRowKey,
                PartitionKey = thisPartKey,
                UserName = thisName,
                Password = thisPassword,
                IsLogedIn = thisBool
            };
            try
            {
                getClient.AddEntityAsync<BULoginModel>(buLogin).Wait();
                return Task.CompletedTask;
            }
            catch (Exception e)
            {

                Console.WriteLine("Error Insert Data: " + e.Message); ;
            }

        }
        return Task.CompletedTask;
    }
    public Task<string> BUGetDataById(string thisTableName, string thisRowKey, string thisPartKey)
    {
        var resValue = string.Empty;
        var getClient = initializeClient(thisTableName);
        if (getClient != null)
        {
            try
            {
                var product = getClient.GetEntityAsync<BULoginModel>(
                              rowKey: thisRowKey,
                              partitionKey: thisPartKey
                              );
                resValue = product.Result.Value.UserName;
                if (!string.IsNullOrEmpty(resValue))
                {
                    return Task.FromResult(resValue);
                }

            }
            catch (Exception e)
            {

                Console.WriteLine("Error Get Data ByID: " + e.Message);
                return Task.FromResult(e.Message);
            }
        }
        return Task.FromResult(resValue);
    }
    public Task<BULoginModel> BUGetDataByQuery(string thisTableName, string thisUserName, string thisPassword)
    {

        var getClient = initializeClient(thisTableName);
        List<BULoginModel> thisClient = new List<BULoginModel>();

        if (getClient != null)
        {
            //IQueryable<BULoginModel> linqQuery = table.CreateQuery<BULoginModel>()
            //  .Where(x => x.PartitionKey == "4")
            //  .Select(x => new CustomerEntity() { PartitionKey = x.PartitionKey, RowKey = x.RowKey, Password = x.Password });
            try
            {
                var searchClient = getClient.Query<BULoginModel>(x => x.Password == thisPassword && x.UserName == thisUserName);

                foreach (var item in searchClient)
                {
                    thisClient.Add(item);
                }
                if (thisClient.Count > 0)
                {
                    return Task.FromResult(thisClient[0]);
                }
            }
            catch (Exception e)
            {

                Console.WriteLine("Error Get DataByQuery: " + e.Message); ;
            }

        }
        return null;

    }
    public Task BUDeleteData(string thisTableName, string thisPartitionkey, string thisRowKey)
    {
        var getClient = initializeClient(thisTableName);
        if (getClient != null)
        {
            try
            {
                getClient.DeleteEntity(thisPartitionkey, thisRowKey);
                return Task.CompletedTask;
            }
            catch (Exception e)
            {

                Console.WriteLine("Error Delete Data: " + e.Message);
                return Task.CompletedTask;
            }

        }
        return Task.CompletedTask;
    }
    public Task BUDeleteTable(string thisTableName)
    {
        try
        {
            var serviceClient = new TableServiceClient(
               new Uri("https://cosmodbbusers.table.cosmos.azure.com:443/"),
               new TableSharedKeyCredential("cosmodbbusers", "4h5eS8y7XkUYgXRxGw4ASdFED3hWP50ewRXVwD0o9uJZuuBTwrdKuqyBrFy78dVxGgPtRvQIVRbaa6Ht8IQ1PA=="));
            serviceClient.DeleteTable(thisTableName);
            return Task.CompletedTask;
        }
        catch (Exception e)
        {

            Console.WriteLine("Error Delete Table: " + e.Message);
            return Task.CompletedTask;
        }

    }
}
