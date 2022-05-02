namespace Entities.Exceptions.BadRequest
{
    public class FileTypeBadRequestException : BadRequestException
    {
        public FileTypeBadRequestException()
        : base($"Can upload only csv and xml files.")
        {
        }
    }
}