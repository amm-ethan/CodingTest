namespace Entities.Exceptions.BadRequest
{
    public class SizeBadRequestException : InvalidFileException
    {
        public SizeBadRequestException()
        : base($"File Size must be less than 1MB. ")
        {
        }
    }
}