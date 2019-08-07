using Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess
{
    public class KeepNoteContext
    {
        MongoClient mongoClient;
        IMongoDatabase database;

        public KeepNoteContext(IConfiguration config)
        {
            mongoClient = new MongoClient(config.GetSection("MongoDB:server").Value);
            database = mongoClient.GetDatabase(config.GetSection("MongoDB:database").Value);
        }

        public IMongoCollection<Note> Notes => database.GetCollection<Note>("Notes");
    }
}
