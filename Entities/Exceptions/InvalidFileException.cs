namespace Entities.Exceptions
{
    public class InvalidFileException : InvalidOperationException
    {
        public InvalidFileException()
        : base("Input File is invalid or empty")
        { }
    }
}