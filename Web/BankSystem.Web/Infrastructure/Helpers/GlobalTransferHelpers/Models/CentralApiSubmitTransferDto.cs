namespace BankSystem.Web.Infrastructure.Helpers.GlobalTransferHelpers.Models
{
    using Common.AutoMapping.Interfaces;
    using System.ComponentModel.DataAnnotations;

    public class CentralApiSubmitTransferDto : IMapWith<GlobalTransferDto>
    {
        public string Description { get; set; }

        [Range(typeof(decimal), "0,01", "10000,00")]
        public decimal Amount { get; set; }

        public string SenderName { get; set; }

        public string RecipientName { get; set; }

        public string SenderAccountUniqueId { get; set; }

        public string DestinationBankSwiftCode { get; set; }

        public string DestinationBankName { get; set; }

        public string DestinationBankCountry { get; set; }

        public string DestinationBankAccountUniqueId { get; set; }

        public string ReferenceNumber { get; set; } = ReferenceNumberGenerator.GenerateReferenceNumber();
    }
}
