namespace RailWorkshop.Db.Utils
{
    public class RepeatingIdException : Exception
    {
        public RepeatingIdException() : base()
        {
        }

        public RepeatingIdException(string? message) : base(message)
        {
        }

        public RepeatingIdException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}