using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotesAPIwithMongo.Models
{
    public class Label
    {
        [BsonId]
        public int LabelId { get; set; }
        public string LabelName { get; set; }
        public int NoteId { get; set; }
        public virtual Note LabelNote { get; set; }
    }
}
