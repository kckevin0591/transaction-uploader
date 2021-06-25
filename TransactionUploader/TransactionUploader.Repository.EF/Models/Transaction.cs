using System;
using System.Collections.Generic;

#nullable disable

namespace TransactionUploader.Repository.EF.Models
{
    public partial class Transaction
    {
        public long TransId { get; set; }
        public string Id { get; set; }
        public double Amount { get; set; }
        public string CurrencyCode { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Status { get; set; }
    }
}
