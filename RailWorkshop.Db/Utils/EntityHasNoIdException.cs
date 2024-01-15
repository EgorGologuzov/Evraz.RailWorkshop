using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailWorkshop.Db.Utils
{
    public class EntityHasNoIdException : Exception
    {
        public EntityHasNoIdException() : base()
        {
        }

        public EntityHasNoIdException(string? message) : base(message)
        {
        }

        public EntityHasNoIdException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}