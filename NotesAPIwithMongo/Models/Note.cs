using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;

namespace NotesAPIwithMongo.Models
{
    public class Note
    {
        [BsonId]
        public int NoteId { get; set; }
        [BsonElement("title")]
        public string NoteTitle { get; set; }
        public string NoteDescription { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsCompleted { get; set; }

        public virtual List<Label> Labels { get; set; }
    }
}
