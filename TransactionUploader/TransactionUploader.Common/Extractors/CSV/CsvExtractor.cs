using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using TinyCsvParser;
using TransactionUploader.Common.Exceptions;

namespace TransactionUploader.Common.Extractors.CSV
{
    public class CsvExtractor : IExtractor
    {
        private static List<string> validStatusList = new List<string>() { "Approved", "Failed", "Finished" };
        public IEnumerable<Transaction> Extract(string data)
        {
            CsvParserOptions csvParserOptions = new CsvParserOptions(false, ',');
            var csvParser = new CsvParser<CsvTransaction>(csvParserOptions, new CsvTransactionMapping());
            var records = csvParser.ReadFromString(
                new CsvReaderOptions(new string[] { "\n", "\r\n" }), data).ToList();

            var txns = new List<Transaction>();
            var errors = new List<string>();
            for (int i = 0; i < records.Count; i++)
            {
                var csvTxn = records[i].Result;
                var hasError = false;
                decimal amount = 0;
                var date = DateTime.Now;
                if (string.IsNullOrEmpty(csvTxn.Amount))
                {
                    errors.Add($"item {i + 1}: invalid amount value");
                    hasError = true;
                }
                else
                {
                    var isAmountOk = decimal.TryParse(csvTxn.Amount,
                        NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands,
                        new CultureInfo("en-US"),
                        out var resultAmount);

                    if (!isAmountOk)
                    {
                        errors.Add($"item {i + 1}: invalid amount value");
                        hasError = true;
                    }
                    else
                    {
                        amount = resultAmount;
                    }
                }
                if (string.IsNullOrEmpty(csvTxn.CurrencyCode))
                {
                    errors.Add($"item {i + 1}: invalid currency code");
                    hasError = true;
                }

                if (string.IsNullOrEmpty(csvTxn.TransactionId))
                {
                    errors.Add($"item {i + 1}: missing id");
                    hasError = true;
                }
                else if (csvTxn.TransactionId.Length > 50)
                {
                    errors.Add($"item {i + 1}: id length exceeds 50 characters");
                    hasError = true;
                }

                if (string.IsNullOrEmpty(csvTxn.TransactionDate))
                {
                    errors.Add($"item {i + 1}: missing transaction date");
                    hasError = true;
                }
                else
                {
                    var isOk = DateTime.TryParseExact(csvTxn.TransactionDate,
                        "dd/MM/yyyy hh:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out var dateTime);
                    if (!isOk)
                    {
                        errors.Add($"item {i + 1}: invalid date time format");
                        hasError = true;
                    }
                    else
                    {
                        date = dateTime;
                    }
                }

                if (!validStatusList.Contains(csvTxn.Status))
                {
                    errors.Add($"item {i + 1}: invalid status");
                    hasError = true;
                }

                if (!hasError)
                {
                    var txn = new Transaction()
                    {
                        Amount = amount,
                        CurrencyCode = csvTxn.CurrencyCode,
                        Status = MapStatus(csvTxn.Status),
                        TransactionId = csvTxn.TransactionId,
                        TransactionDate = date
                    };
                    txns.Add(txn);
                }

            }

            if (errors.Any())
            {
                throw new InvalidFileContentException($"Found error(s) in the file.", errors);
            }

            return txns;
        }

        private string MapStatus(string xmlStatus)
        {
            switch (xmlStatus)
            {
                case "Approved":
                    return "A";
                case "Failed":
                    return "R";
                case "Finished":
                    return "D";
            }

            return "X";
        }
    }
}