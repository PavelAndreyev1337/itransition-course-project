using System;

namespace CollectionApp.BLL.Exceptions
{
    class UserNoRightsException : Exception
    {
        public UserNoRightsException()
        {
        }

        public UserNoRightsException(string message) : base(message)
        {
        }
    }
}
