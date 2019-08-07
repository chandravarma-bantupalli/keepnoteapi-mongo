using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Exceptions
{
    public class LabelAlreadyExistsException: ApplicationException
    {
        public LabelAlreadyExistsException() { }
        public LabelAlreadyExistsException(string message): base(message) { }
    }
}
