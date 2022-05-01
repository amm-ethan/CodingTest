using AutoMapper;
using Entities.Models;
using Shared.DataTransferObjects;

namespace Api
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Transaction, TransactionDto>()
                .ForCtorParam("Id",
                opt => opt.MapFrom(x => x.TransactionId))
                .ForCtorParam("Payment",
                opt => opt.MapFrom(x => string.Join(' ', x.Amount, x.CurrencyCode)))
                .ForCtorParam("Status",
                opt => opt.MapFrom(x => x.Status == Status.Approved ? "A"
                : x.Status == Status.Failed || x.Status == Status.Rejected ? "R" : "D"
                ));
        }
    }
}
