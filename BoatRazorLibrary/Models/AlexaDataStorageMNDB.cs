using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace BoatRazorLibrary.Models;

public class MNdata
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
}
public class AlexaDataStorageMNDB
{
    private readonly IMongoDatabase _mongoDatabase;
    private readonly IMongoCollection<BsonDocument> _mongoCollection;
    public IMongoCollection<MNdata> BoatUsersAToken { get { return _mongoDatabase.GetCollection<MNdata>("BUATRecord"); } }
    public AlexaDataStorageMNDB()
    {
        var thisClient = "mongodb+srv://busersblazer:busers123456@clusterbu.rmtc0xg.mongodb.net/bublazerdb?retryWrites=true&w=majority";
        var client = new MongoClient(thisClient);
        _mongoDatabase = client.GetDatabase("bublazerdb");
        _mongoCollection = _mongoDatabase.GetCollection<BsonDocument>("bublazerstorage");
    }
    public async Task InsertDocument()
    {
        var document = new BsonDocument
        {
            { "alexaBUToken", "thisToken" }
            //{ "info", new BsonDocument
            //    {
            //        { "x", 203 },
            //        { "y", 102 }
            //}}
        };
        try
        {
            await _mongoCollection.InsertOneAsync(document);
        }
        catch (Exception er)
        {

            Console.WriteLine(er.Message);
        }

    } //put get
}
