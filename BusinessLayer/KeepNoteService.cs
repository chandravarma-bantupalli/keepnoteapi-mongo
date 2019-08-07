using System;
using System.Collections.Generic;
using System.Text;
using BusinessLayer.Exceptions;
using DataAccess;
using Entities;

namespace BusinessLayer
{
    public class KeepNoteService : IKeepNoteService
    {
        private readonly IKeepNoteRepository repository; 
        public KeepNoteService(IKeepNoteRepository keepNoteRepo)
        {
            repository = keepNoteRepo;
        }
        public void AddLabelToNotes(int noteId, Label label)
        {
            Label l = repository.GetNotesLabelById(noteId, label.LabelId);
            if(l == null)
            {
                repository.AddLabelToNotes(noteId, label);
            }
            else
            {
                throw new LabelAlreadyExistsException($"Label with id {label.LabelId} already exists");
            }
        }

        public void CreateNote(Note note)
        {
            Note n = repository.GetNoteById(note.NoteId);
            if(n == null)
            {
                repository.CreateNote(note);
            }
            else
            {
                throw new NoteAlreadyExistsException($"Note with id {note.NoteId} already exists");
            }
        }

        public bool DeleteLabelFromNotes(int noteId, int labelId)
        {
            Label l = repository.GetNotesLabelById(noteId, labelId);
            if(l != null)
            {
                return repository.DeleteLabelFromNotes(noteId, labelId);
            }
            else
            {
                throw new LabelNotFoundException($"Label with id {labelId} not found");
            }
        }

        public List<Note> GetAllNotes()
        {
            return repository.GetAllNotes();
        }

        public Note GetNoteById(int noteId)
        {
            return repository.GetNoteById(noteId);
        }

        public Label GetNotesLabelById(int noteId, int labelId)
        {
            return repository.GetNotesLabelById(noteId, labelId);
        }

        public List<Label> GetNotesLabels(int noteId)
        {
            return repository.GetNotesLabels(noteId);
        }

        public bool RemoveNote(int noteId)
        {
            Note n = repository.GetNoteById(noteId);
            if(n != null)
            {
                return repository.RemoveNote(noteId);
            }
            else
            {
                throw new NoteNotFoundException($"Notes with id {noteId} not exists");
            }
        }

        public bool UpdateLabelInNotes(int noteId, int labelId, Label label)
        {
            Label l = repository.GetNotesLabelById(noteId, labelId);
            if(l != null)
            {
                return repository.UpdateLabelInNotes(noteId, labelId, label);
            }
            else
            {
                throw new LabelNotFoundException($"Label with id {labelId} not exists");
            }
        }

        public bool UpdateNote(int noteId, Note note)
        {
            Note n = repository.GetNoteById(noteId);
            if(n != null)
            {
                return repository.UpdateNote(noteId, note);
            }
            else
            {
                throw new NoteNotFoundException($"Note with id {noteId} note exists");
            }
        }
    }
}
