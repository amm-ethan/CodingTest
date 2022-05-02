namespace Entities.Exceptions.BadRequest
{
    public class FileTypeBadRequestException : InvalidFileException
    {
        public FileTypeBadRequestException()
        : base($"Can upload only csv and xml files.")
        {
        }
    }
}