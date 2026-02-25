namespace BankSystem.Services.Models.MoneyTransfer
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Common;

    public class ReceiveMoneyTransferServiceModel : MoneyTransferBaseServiceModel
    {
        public string Description { get; set; }

        [Required]
       
        [Range(typeof(decimal), "0,01", "10000,00")]
        public decimal Amount { get; set; }

        [Required]
        public string AccountId { get; set; }

        [Required]
        public DateTime MadeOn { get; set; } = DateTime.UtcNow;

        [Required]
        [MaxLength(ModelConstants.BankAccount.UniqueIdMaxLength)]
        public string Source => this.Destination;

        [Required]
        [MaxLength(ModelConstants.BankAccount.UniqueIdMaxLength)]
        public string Destination { get; set; }
    }
}

