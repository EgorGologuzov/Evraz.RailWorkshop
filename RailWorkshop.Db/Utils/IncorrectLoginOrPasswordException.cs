namespace RailWorkshop.Db.Utils
{
    public class IncorrectLoginOrPasswordException : Exception
    {
        public IncorrectLoginOrPasswordException() : base()
        {
        }

        public IncorrectLoginOrPasswordException(string? message) : base(message)
        {
        }

        public IncorrectLoginOrPasswordException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}