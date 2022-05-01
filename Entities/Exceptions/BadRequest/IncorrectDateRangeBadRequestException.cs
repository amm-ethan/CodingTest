namespace Entities.Exceptions.BadRequest
{
    public class IncorrectDateRangeBadRequestException : BadRequestException
    {
        public IncorrectDateRangeBadRequestException()
        : base($"FromDate must not be greater than ToDate and both FromDate and ToDate must not be greater than today date. ")
        {
        }
    }
}