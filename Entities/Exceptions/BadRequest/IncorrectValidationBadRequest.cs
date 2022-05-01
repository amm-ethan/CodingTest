namespace Entities.Exceptions.BadRequest
{
    public sealed class IncorrectValidationBadRequest : BadRequestException
    {
        public IncorrectValidationBadRequest(string error)
        : base($"The Upload File Contain One or More Validation Error. \n" +
              $"{error}.")
        {
        }
    }
}
