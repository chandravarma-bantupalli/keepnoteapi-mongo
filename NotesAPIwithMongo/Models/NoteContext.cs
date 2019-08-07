using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace NotesAPIwithMongo.Models
{
    public class NoteContext
    {
        MongoClient mongoClient;
        IMongoDatabase database;

        public NoteContext(IConfiguration config)
        {
            mongoClient = new MongoClient(config.GetSection("MongoDB:server").Value);
            database = mongoClient.GetDatabase(config.GetSection("MongoDB:database").Value);
        }

        public IMongoCollection<Note> Notes => database.GetCollection<Note>("Notes");
    }
}
