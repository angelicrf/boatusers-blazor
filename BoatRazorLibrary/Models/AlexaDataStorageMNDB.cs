using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace BoatRazorLibrary.Models;

public class MNdata
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("LastUpdated")]
    public DateTime LastUpdated { get; set; }

    [BsonElement("alexaBUName")]
    public string AlexaBUName { get; set; }

    [BsonElement("alexaBUToken")]
    public string AlexaBUToken { get; set; }
}
public class AlexaDataStorageMNDB
{
    private readonly IMongoDatabase _mongoDatabase;
    private readonly IMongoCollection<BsonDocument> _mongoCollection;
    public bool OneTimeRun { get; set; } = false;
    public IMongoCollection<MNdata> BoatUsersAToken { get { return _mongoDatabase.GetCollection<MNdata>("BUATRecord"); } }
    public AlexaDataStorageMNDB()
    {
        var thisClient = "mongodb+srv://busersblazer:busers123456@clusterbu.rmtc0xg.mongodb.net/bublazerdb?retryWrites=true&w=majority";
        var client = new MongoClient(thisClient);
        _mongoDatabase = client.GetDatabase("bublazerdb");
        _mongoCollection = _mongoDatabase.GetCollection<BsonDocument>("bublazerstorage");
    }
    public async Task<bool> InsertDocument(string thisAToken)
    {
        var document = new BsonDocument
        {
            { "alexaBUName", "BoatUsersMNApi" },
            { "alexaBUToken", $"{thisAToken}" },
            { "LastUpdated", DateTime.Now }
        };
        try
        {
            _mongoCollection.InsertOneAsync(document).GetAwaiter();
            OneTimeRun = true;
            return OneTimeRun;
        }
        catch (Exception er)
        {

            Console.WriteLine(er.Message);
            return false;
        }

    } // get
    public async Task<MNdata> GetMNDocument()
    {
        DateTime now = DateTime.Now;
        DateTime yesterday = now.AddDays(-1);

        //var filter = Builders<BsonDocument>.Filter.Eq("alexaBUName", "BoatUsersMNApi");
        //Builders<BsonDocument>.Filter.("alexaBUName", "BoatUsersMNApi");
        //var getData = _mongoCollection.Find(filter).FirstOrDefault();

        var resultData = from buData in _mongoDatabase.GetCollection<MNdata>("bublazerstorage").AsQueryable()
                         where buData.LastUpdated > yesterday && buData.LastUpdated <= now
                         select buData;

        if (resultData.Any())
        {
            foreach (var item in resultData)
            {
                return new MNdata
                {
                    Id = item.Id,
                    AlexaBUName = item.AlexaBUName,
                    AlexaBUToken = item.AlexaBUToken,
                    LastUpdated = item.LastUpdated
                };
            }
        }
        return new MNdata();

    }
}
