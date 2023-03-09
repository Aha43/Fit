using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fit.Exceptions
{
    public class DuplicateActorException : Exception
    {
        public DuplicateActorException(string name) : base(name) { }
    }
}
