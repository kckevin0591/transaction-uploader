using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Xml.Serialization;
using TransactionUploader.Common.Exceptions;

namespace TransactionUploader.Common.Extractors.XML
{
    public class XmlExtractor : IExtractor
    {
        private static List<string> validStatusList = new List<string>() {"Approved", "Rejected", "Done"};

        public IEnumerable<Transaction> Extract(string data)
        {
            if (string.IsNullOrEmpty(data))
                throw new InvalidFileContentException("File is empty");

            var xmlTxns = ExtractXmlList(data);

            var txns = new List<Transaction>();
            var errors = new List<string>();
            for (var i = 0; i < xmlTxns.Count; i++)
            {
                DateTime date = DateTime.Now;
                var xmlTxn = xmlTxns[i];
                var hasError = false;
                if (xmlTxn.PaymentDetails?.Amount == null)
                {
                    errors.Add($"item {i + 1}: invalid amount value");
                    hasError = true;
                }
                if (string.IsNullOrEmpty(xmlTxn.PaymentDetails?.CurrencyCode))
                {
                    errors.Add($"item {i + 1}: invalid currency code");
                    hasError = true;
                }

                if (string.IsNullOrEmpty(xmlTxn.id))
                {
                    errors.Add($"item {i + 1}: missing id");
                    hasError = true;
                }
                else if (xmlTxn.id.Length > 50)
                {
                    errors.Add($"item {i + 1}: id length exceeds 50 characters");
                    hasError = true;
                }

                if (string.IsNullOrEmpty(xmlTxn.TransactionDate))
                {
                    errors.Add($"item {i + 1}: missing transaction date");
                    hasError = true;
                }
                else
                {
                    var isOk = DateTime.TryParseExact(xmlTxn.TransactionDate, "s", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, out var dateTime);
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

                if (!validStatusList.Contains(xmlTxn.Status))
                {
                    errors.Add($"item {i + 1}: invalid status");
                    hasError = true;
                }

                if (!hasError)
                {
                    var txn = new Transaction()
                    {
                        Amount = xmlTxn.PaymentDetails.Amount.Value,
                        CurrencyCode = xmlTxn.PaymentDetails.CurrencyCode,
                        Status = MapStatus(xmlTxn.Status),
                       TransactionId = xmlTxn.id,
                        TransactionDate = date
                    };
                    txns.Add(txn);
                }
            }

            if (errors.Any())
            {
                var errorSummary = string.Join(',', errors);
                throw new InvalidFileContentException($"Found error(s) in the file: {errorSummary}");
            }

            return txns;
        }

        private string MapStatus(string xmlStatus)
        {
            switch (xmlStatus)
            {
                case "Approved":
                    return "A";
                case "Rejected":
                    return "R";
                case "Done":
                    return "D";
            }

            return "X";
        }

        private List<XmlTransaction> ExtractXmlList(string data)
        {
            try
            {
                var serializer = new XmlSerializer(typeof(XmlTransactionList));
                using (var reader = new StringReader(data))
                {
                    var xmlList = (XmlTransactionList)serializer.Deserialize(reader);
                    return xmlList.XmlTransactions;
                }
            }
            catch (InvalidOperationException e)
            {
                throw new InvalidFileContentException("Failed to parse the file", e);
            }

        }
    }
}