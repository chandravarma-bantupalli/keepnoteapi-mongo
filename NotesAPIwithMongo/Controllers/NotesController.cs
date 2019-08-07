using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Entities;
using BusinessLayer;
using BusinessLayer.Exceptions;

namespace NotesAPIwithMongo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly IKeepNoteService service;
        public NotesController(IKeepNoteService keepNoteService)
        {
            service = keepNoteService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(service.GetAllNotes());
            }
            catch(NoteNotFoundException nExe)
            {
                return NotFound(nExe.Message);
            }
            
        }

        [HttpGet]
        [Route("{noteId}")]
        public IActionResult Get(int noteId)
        {
            try
            {
                return Ok(service.GetNoteById(noteId));
            }
            catch (NoteNotFoundException nExe)
            {
                return NotFound(nExe.Message);
            }
        }

        [HttpGet]
        [Route("{notesId}/labels")]
        public IActionResult GetLabels(int notesId)
        {
            try
            {
                return Ok(service.GetNotesLabels(notesId));
            }
            catch(LabelNotFoundException lExe)
            {
                return NotFound(lExe.Message);
            }
        }

        [HttpGet]
        [Route("{notesId}/labels/{labelId}")]
        public IActionResult GetLabelById(int notesId, int labelId)
        {
            try
            {
                return Ok(service.GetNotesLabelById(notesId, labelId));
            }
            catch(LabelNotFoundException lExe)
            {
                return NotFound(lExe.Message);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] Note note)
        {
            try
            {
                service.CreateNote(note);
                return Ok("Note Added");
            }
            catch(NoteAlreadyExistsException nExe)
            {
                return BadRequest(nExe.Message);
            }
        }

        [HttpPost]
        [Route("{notesId}/labels")]
        public IActionResult Post(int notesId, [FromBody] Label label)
        {
            try
            {
                service.AddLabelToNotes(notesId, label);
                return Ok("Added Label");
            }
            catch(LabelAlreadyExistsException lExe)
            {
                return BadRequest(lExe.Message);
            }

        }


        [HttpPut]
        [Route("{noteId}")]
        public IActionResult Put(int noteId, [FromBody] Note notes)
        {
            try
            {
                return Ok(service.UpdateNote(noteId, notes));
            }
            catch (NoteNotFoundException nExe)
            {
                return NotFound(nExe.Message);
            }
        }

        [HttpPut]
        [Route("{noteId}/labels/{labelId}")]
        public IActionResult PutLabel(int noteId, int labelId, [FromBody] Label label)
        {
            try
            {
                return Ok(service.UpdateLabelInNotes(noteId, labelId, label));
            }
            catch(LabelNotFoundException lExe)
            {
                return NotFound(lExe.Message);
            }
        }

        [HttpDelete]
        [Route("{noteId}")]
        public IActionResult Delete(int noteId)
        {
            try
            {
                return Ok(service.RemoveNote(noteId));
            }
            catch(NoteNotFoundException nExe)
            {
                return NotFound(nExe.Message);
            }
        }

        [HttpDelete]
        [Route("{noteId}/labels/{labelId}")]
        public IActionResult Delete(int noteId, int labelId)
        {
            try
            {
                return Ok(service.DeleteLabelFromNotes(noteId, labelId));
            }
            catch (LabelNotFoundException lExe)
            {
                return NotFound(lExe.Message);
            }
        }
    }
}