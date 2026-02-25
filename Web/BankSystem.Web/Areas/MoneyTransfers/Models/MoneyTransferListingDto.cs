namespace BankSystem.Web.Areas.MoneyTransfers.Models
{
    using Common.AutoMapping.Interfaces;
    using Services.Models.MoneyTransfer;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class MoneyTransferListingDto : IMapWith<MoneyTransferListingServiceModel>
    {
        public string Description { get; set; }

        [Range(typeof(decimal), "0,01", "10000,00")]
        public decimal Amount { get; set; }

        public string SenderName { get; set; }

        public string RecipientName { get; set; }

        public DateTime MadeOn { get; set; }

        public string Source { get; set; }

        public string Destination { get; set; }

        public string ReferenceNumber { get; set; }
    }
}



