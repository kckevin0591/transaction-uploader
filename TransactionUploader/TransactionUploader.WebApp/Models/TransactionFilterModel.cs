﻿using System;

namespace TransactionUploader.WebApp.Models
{
    public class TransactionFilterModel
    {
        public string CurrencyCode { get; set; }
        public string Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}