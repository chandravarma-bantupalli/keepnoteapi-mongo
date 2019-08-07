using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
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
