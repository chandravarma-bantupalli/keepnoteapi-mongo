using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace NotesAPIwithMongo.Models
{
    public class NotesRepository : INoteRepo
    {
        private readonly NoteContext context;
        public NotesRepository(NoteContext notesContext)
        {
            context = notesContext;
        }
        public void CreateNote(Note note)
        {
            context.Notes.InsertOne(note);
        }

        public void AddLabelToNotes(int noteId, Label label)
        {
            var filter = Builders<Note>.Filter.Where(n => n.NoteId == noteId);
            var update = Builders<Note>.Update.Push(n => n.Labels, label);
            context.Notes.FindOneAndUpdate(filter, update);

        }

        public List<Note> GetAllNotes()
        {
            return context.Notes.Find(_ => true).ToList();
        }

        public Note GetNoteById(int noteId)
        {
            return context.Notes.Find(n => n.NoteId == noteId).FirstOrDefault();
        }

        public List<Label> GetNotesLabels(int notesId)
        {
            var result = context.Notes.Find(n => n.NoteId == notesId).FirstOrDefault();
            return result.Labels;

        }

        public Label GetNotesLabelById(int notesId, int labelId)
        {
            var labels = context.Notes.Find(n => n.NoteId == notesId).FirstOrDefault();
            var result = labels.Labels.Find(l => l.LabelId == labelId);
            return result;
        }

        public bool RemoveNote(int noteId)
        {
            var rNote = context.Notes.DeleteOne(n => n.NoteId == noteId);
            return rNote.IsAcknowledged && (rNote.DeletedCount > 0);
        }

        public bool DeleteLabelFromNotes(int noteId, int labelId)
        {
            Note note = context.Notes.Find(n => n.NoteId == noteId).First();
            Label labelToDelete = note.Labels.First(l => l.LabelId == labelId);
            note.Labels.Remove(labelToDelete);

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

        public bool UpdateLabelInNotes(int noteId, int labelId, Label label)
        {
            Note note = context.Notes.Find(n => n.NoteId == noteId).First();
            Label labelToUpdate = note.Labels.First(l => l.LabelId == labelId);
            labelToUpdate.LabelName = label.LabelName;
            labelToUpdate.LabelNote = label.LabelNote;

            var result = context.Notes.ReplaceOne(n => n.NoteId == note.NoteId, note);
            return result.IsAcknowledged && (result.ModifiedCount > 0);
        }
    }
}
