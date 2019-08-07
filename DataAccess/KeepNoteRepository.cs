using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities;
using MongoDB.Driver;

namespace DataAccess
{
    public class KeepNoteRepository : IKeepNoteRepository
    {
        private readonly KeepNoteContext context;
        public KeepNoteRepository(KeepNoteContext keepNoteContext)
        {
            context = keepNoteContext;
        }
        public void AddLabelToNotes(int noteId, Label label)
        {
            var filter = Builders<Note>.Filter.Where(n => n.NoteId == noteId);
            var update = Builders<Note>.Update.Push(n => n.Labels, label);
            context.Notes.FindOneAndUpdate(filter, update);
        }

        public void CreateNote(Note note)
        {
            context.Notes.InsertOne(note);
        }

        public bool DeleteLabelFromNotes(int noteId, int labelId)
        {
            Note note = context.Notes.Find(n => n.NoteId == noteId).First();
            Label labelToDelete = note.Labels.First(l => l.LabelId == labelId);
            note.Labels.Remove(labelToDelete);

            var result = context.Notes.ReplaceOne(n => n.NoteId == note.NoteId, note);
            return result.IsAcknowledged && (result.ModifiedCount > 0);
        }

        public List<Note> GetAllNotes()
        {
            return context.Notes.Find(_ => true).ToList();
        }

        public Note GetNoteById(int noteId)
        {
            return context.Notes.Find(n => n.NoteId == noteId).FirstOrDefault();
        }

        public Label GetNotesLabelById(int noteId, int labelId)
        {
            var labels = context.Notes.Find(n => n.NoteId == noteId).FirstOrDefault();
            var result = labels.Labels.Find(l => l.LabelId == labelId);
            return result;
        }

        public List<Label> GetNotesLabels(int noteId)
        {
            var result = context.Notes.Find(n => n.NoteId == noteId).FirstOrDefault();
            return result.Labels;
        }

        public bool RemoveNote(int noteId)
        {
            var rNote = context.Notes.DeleteOne(n => n.NoteId == noteId);
            return rNote.IsAcknowledged && (rNote.DeletedCount > 0);
        }

        public bool UpdateLabelInNotes(int noteId, int labelId, Label label)
        {
            Note note = context.Notes.Find(n => n.NoteId == noteId).First();
            Label labelToUpdate = note.Labels.First(l => l.LabelId == labelId);
            labelToUpdate.LabelName = label.LabelName;
            labelToUpdate.LabelNote = label.LabelNote;

            var result = context.Notes.ReplaceOne(n => n.NoteId == note.NoteId, note);
            return result.IsAcknowledged && (result.ModifiedCount > 0);
        }

        public bool UpdateNote(int noteId, Note note)
        {
            var filter = Builders<Note>.Filter.Where(n => n.NoteId == noteId);
            if (filter != null)
            {
                var result = context.Notes.ReplaceOne(filter, note);
                return result.IsAcknowledged && (result.ModifiedCount > 0);
            }
            else
            {
                return false;
            }
        }
    }
}
