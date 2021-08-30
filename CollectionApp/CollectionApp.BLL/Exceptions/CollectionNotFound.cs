using System;
using System.Collections.Generic;
using System.Text;

namespace CollectionApp.BLL.Exceptions
{
    class CollectionNotFound : Exception
    {
        public CollectionNotFound()
        {
        }

        public CollectionNotFound(string message) : base(message)
        {
        }
    }
}
