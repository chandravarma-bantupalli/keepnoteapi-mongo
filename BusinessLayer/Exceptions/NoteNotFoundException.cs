using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Exceptions
{
    public class NoteNotFoundException: ApplicationException
    {
        public NoteNotFoundException() { }
        public NoteNotFoundException(string message): base(message) { }
    }
}
