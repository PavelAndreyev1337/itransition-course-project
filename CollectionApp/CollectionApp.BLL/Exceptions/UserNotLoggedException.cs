using System;

namespace CollectionApp.BLL.Exceptions
{
    public class UserNotLoggedException : Exception
    {
        public UserNotLoggedException()
        {
        }

        public UserNotLoggedException(string message) : base(message)
        {
        }
    }
}
