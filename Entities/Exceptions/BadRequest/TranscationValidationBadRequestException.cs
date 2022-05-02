using Newtonsoft.Json;
using Shared.DataTransferObjects;

namespace Entities.Exceptions.BadRequest
{
    public sealed class TranscationValidationBadRequestException : BadRequestException
    {
        public TranscationValidationBadRequestException(TransactionErrorDto transactionError)
        : base(JsonConvert.SerializeObject(transactionError))
        {
        }
    }
}