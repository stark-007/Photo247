using System;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Security.Authentication;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver.Linq;

namespace Photo247
{
    public static class MongoService
    {
        static IMongoCollection<Tree> todoItemsCollection;
        readonly static string dbName = "247nosqlstore";
        readonly static string collectionName = "TreesCollection";
        static MongoClient client;

        static IMongoCollection<Tree> ToDoItemsCollection
        {
            get
            {
                if (client == null || todoItemsCollection == null)
                {
                    var conx = "mongodb://247training:7fNvgnRlGzSscLcpw3RhKN14DL57VQE1OkedPV39fdXm0gv3PH6F7Zb3rvlx0vTTmfYYZbRdAiYnut9EiAIv5A==@247training.documents.azure.com:10255/?ssl=true&replicaSet=globaldb";
                    MongoClientSettings settings = MongoClientSettings.FromUrl(
                        new MongoUrl(conx)
                    );

                    settings.SslSettings = new SslSettings { EnabledSslProtocols = SslProtocols.Tls12 };

                    client = new MongoClient(settings);
                    var db = client.GetDatabase(dbName);

                    var collectionSettings = new MongoCollectionSettings { ReadPreference = ReadPreference.Nearest };
                    todoItemsCollection = db.GetCollection<Tree>(collectionName, collectionSettings);
                }

                return todoItemsCollection;
            }
        }

        public async static Task<List<Tree>> GetAllItems()
        {
            var allItems = await ToDoItemsCollection
                .Find(new BsonDocument())
                .ToListAsync();

            return allItems;
        }

        public async static Task<List<Tree>> SearchByName(string name)
        {
            var results = await ToDoItemsCollection
                            .AsQueryable()
                            .Where(tdi => tdi.Name.Contains(name))
                            .Take(10)
                            .ToListAsync();

            return results;
        }

        public async static Task InsertItem(Tree item)
        {
            await ToDoItemsCollection.InsertOneAsync(item);
        }

        //public async static Task<bool> DeleteItem(Tree item)
        //{
        //    var result = await ToDoItemsCollection.DeleteOneAsync(tdi => tdi. == item.);

        //    return result.IsAcknowledged && result.DeletedCount == 1;
        //}
    }
}
