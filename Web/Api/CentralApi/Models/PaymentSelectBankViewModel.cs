namespace CentralApi.Models
{
    using System.Collections.Generic;

    public class PaymentSelectBankViewModel
    {
        [System.ComponentModel.DataAnnotations.Range(typeof(decimal), "0,01", "10000,00")]
        public decimal Amount { get; set; }

        public string Description { get; set; }

        public IEnumerable<BankListingViewModel> Banks { get; set; }
    }
}
