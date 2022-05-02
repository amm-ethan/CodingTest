using AutoMapper;
using Entities.CsvModel;
using Entities.Models;
using Entities.XmlModel;
using Shared.DataTransferObjects;
using System.Globalization;

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

            CreateMap<CsvTransaction, Transaction>()
                .ForMember(c => c.TransactionId,
                opt => opt.MapFrom(x => x.TransactionId!.Trim().Replace("\"","")))
                .ForMember(c => c.Amount,
                opt => opt.MapFrom(x => decimal.Parse(x.Amount!.Trim().Replace("\"", "").Replace(",", ""))))
                 .ForMember(c => c.CurrencyCode,
                opt => opt.MapFrom(x => x.CurrencyCode!.Trim().Replace("\"", "")))
                  .ForMember(c => c.TransactionDate,
                opt => opt.MapFrom(x => DateTime.ParseExact(x.TransactionDate!.Trim().Replace("\"", ""), "dd/MM/yyyy hh:mm:ss", CultureInfo.InvariantCulture)))
                   .ForMember(c => c.Status,
                opt => opt.MapFrom(x => (Status)Enum.Parse(typeof(Status), x.Status!.Trim().Replace("\"", ""))));

            CreateMap<TransactionXml, CsvTransaction>()
                .ForMember(c => c.TransactionId,
                opt => opt.MapFrom(x => x.Id!.Trim().Replace("\"", "")))
                .ForMember(c => c.Amount,
                opt => opt.MapFrom(x => x.PaymentDetails!.Amount!.Trim().Replace("\"", "")))
                .ForMember(c => c.CurrencyCode,
                opt => opt.MapFrom(x => x.PaymentDetails!.CurrencyCode!.Trim().Replace("\"", "")))
                .ForMember(c => c.TransactionDate,
                opt => opt.MapFrom(x => x.TransactionDate!.Trim().Replace("\"", "")))
                .ForMember(c => c.Status,
                opt => opt.MapFrom(x => x.Status!.Trim().Replace("\"", ""))).ReverseMap();
        }
    }
}
