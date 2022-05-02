namespace Entities.Exceptions
{
    public abstract class InvalidFileException : InvalidOperationException
    {
        protected InvalidFileException()
        : base("Input File is invalid or empty")
        { }
    }
}
