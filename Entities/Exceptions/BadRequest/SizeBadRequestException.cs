namespace Entities.Exceptions.BadRequest
{
    public class SizeBadRequestException : BadRequestException
    {
        public SizeBadRequestException()
        : base($"File Size must be less than 1MB.")
        {
        }
    }
}