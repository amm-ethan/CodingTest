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
                .ForMember(c => c.Id,
                opt => opt.MapFrom(x => x.TransactionId))
                .ForMember(c => c.Payment,
                opt => opt.MapFrom(x => string.Join(' ', x.Amount, x.CurrencyCode)))
                .ForMember(c => c.Status,
                opt => opt.MapFrom(x => x.Status == Status.Approved ? "A"
                : x.Status == Status.Failed || x.Status == Status.Rejected ? "R" : "D"
                ));
        }
    }
}
