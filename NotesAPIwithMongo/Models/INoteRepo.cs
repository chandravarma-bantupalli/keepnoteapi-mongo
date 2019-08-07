using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotesAPIwithMongo.Models
{
    public interface INoteRepo
    {
        Note GetNoteById(int noteId);
        List<Note> GetAllNotes();
        void CreateNote(Note note);
        bool UpdateNote(int noteId, Note note);
        bool RemoveNote(int noteId);
        List<Label> GetNotesLabels(int noteId);
        Label GetNotesLabelById(int noteId, int labelId);
        void AddLabelToNotes(int noteId, Label label);
        bool UpdateLabelInNotes(int noteId, int labelId, Label label);
        bool DeleteLabelFromNotes(int noteId, int labelId);
    }
}
