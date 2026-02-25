namespace BankSystem.Services.Models.MoneyTransfer
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class MoneyTransferListingServiceModel : MoneyTransferBaseServiceModel
    {
        public string Id { get; set; }

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

