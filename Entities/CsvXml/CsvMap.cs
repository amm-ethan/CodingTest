using CsvHelper.Configuration;

namespace Entities.CsvXml
{
    public sealed class CsvMap : ClassMap<CsvTransaction>
    {
        public CsvMap()
        {
            Map(f => f.TransactionId).Index(0);
            Map(f => f.Amount).Index(1);
            Map(f => f.CurrencyCode).Index(2);
            Map(f => f.TransactionDate).Index(3);
            Map(f => f.Status).Index(4);
        }
    }
}