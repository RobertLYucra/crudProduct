using MongoDB.Driver;
using System.Security.Authentication;

namespace ProductCrud.Resource
{
    public class MongoConections
    {
        public MongoClient client;
        public IMongoDatabase database;

        public MongoConections()
        {
            const string connectionUri = "mongodb+srv://robertlyucra:nhulmgsQlLz46V1B@robertlyucra.godma4g.mongodb.net/";
            var settings = MongoClientSettings.FromConnectionString(connectionUri);
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            client = new MongoClient(settings);
            database = client.GetDatabase("Tienda");
        }
    }
}
