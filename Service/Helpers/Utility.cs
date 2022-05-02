﻿using Contracts.Services;
using Entities.CsvModel;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Shared.DataTransferObjects;
using System.Globalization;
using System.Text;

namespace Service.Helpers
{
    public static class Utility
    {
        public static async Task<string> ReadAsStringAsync(this IFormFile file)
        {
            var result = new StringBuilder();
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                while (reader.Peek() >= 0)
                    result.AppendLine(await reader.ReadLineAsync());
            }
            return result.ToString();
        }

        public static (bool canParse, List<TransactionSubError> errors) TryParseTranscationObject(this CsvTransaction record, int index, string filename, ILoggerManager logger)
        {
            var errorList = new List<TransactionSubError>();

            #region TransactionId
            if (string.IsNullOrEmpty(record.TransactionId!.Trim().Replace("\"", "")))
            {
                var error = new TransactionSubError() { Position = $"({index + 1},1)", Header = "Transaction Id", Value = record.TransactionId, Error = "Value is empty" };
                logger.LogWarn($"Validation Error in File {filename}. {JsonConvert.SerializeObject(error)}");
                errorList.Add(error);
            }
            else if (record.TransactionId!.Trim().Replace("\"", "").Length > 50)
            {
                var error = new TransactionSubError() { Position = $"({index + 1},1)", Header = "Transaction Id", Value = record.TransactionId, Error = "Value length is greather than 50" };
                logger.LogWarn($"Validation Error in File {filename}. {JsonConvert.SerializeObject(error)}");
                errorList.Add(error);
            }
            #endregion TransactionId

            #region Amount
            if (string.IsNullOrEmpty(record.Amount!.Trim().Replace("\"", "")))
            {
                var error = new TransactionSubError() { Position = $"({index + 1},2)", Header = "Amount", Value = record.TransactionId, Error = "Value is empty" };
                logger.LogWarn($"Validation Error in File {filename}. {JsonConvert.SerializeObject(error)}");
                errorList.Add(error);
            }
            else if (!decimal.TryParse(record.Amount!.Trim().Replace("\"", ""), out _))
            {
                var error = new TransactionSubError() { Position = $"({index + 1},2)", Header = "Amount", Value = record.TransactionId, Error = "Value can't convert to decimal" };
                logger.LogWarn($"Validation Error in File {filename}. {JsonConvert.SerializeObject(error)}");
                errorList.Add(error);
            }
            #endregion Amount

            #region CurrentCode
            if (string.IsNullOrEmpty(record.CurrencyCode!.Trim().Replace("\"", "")))
            {
                var error = new TransactionSubError() { Position = $"({index + 1},3)", Header = "Currency Code", Value = record.TransactionId, Error = "Value is empty" };
                logger.LogWarn($"Validation Error in File {filename}. {JsonConvert.SerializeObject(error)}");
                errorList.Add(error);
            }
            #endregion CurrencyCode

            #region Transcation Date
            if (string.IsNullOrEmpty(record.TransactionDate!.Trim().Replace("\"", "")))
            {
                var error = new TransactionSubError() { Position = $"({index + 1},4)", Header = "Transaction Date", Value = record.TransactionId, Error = "Value is empty" };
                logger.LogWarn($"Validation Error in File {filename}. {JsonConvert.SerializeObject(error)}");
                errorList.Add(error);
            }
            else if (!DateTime.TryParseExact(record.TransactionDate!.Trim().Replace("\"", ""), "dd/MM/yyyy hh:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
            {
                var error = new TransactionSubError() { Position = $"({index + 1},4)", Header = "Transaction Date", Value = record.TransactionId, Error = "Value can't convert to DateTime" };
                logger.LogWarn($"Validation Error in File {filename}. {JsonConvert.SerializeObject(error)}");
                errorList.Add(error);
            }
            #endregion Transcation Date

            #region Status
            if (string.IsNullOrEmpty(record.Status!.Trim().Replace("\"", "")))
            {
                var error = new TransactionSubError() { Position = $"({index + 1},5)", Header = "Status", Value = record.TransactionId, Error = "Value is empty" };
                logger.LogWarn($"Validation Error in File {filename}. {JsonConvert.SerializeObject(error)}");
                errorList.Add(error);
            }
            else if (!Enum.TryParse(typeof(Status), record.Status!.Trim().Replace("\"", ""), true, out _))
            {
                var error = new TransactionSubError() { Position = $"({index + 1},5)", Header = "Status", Value = record.TransactionId, Error = "Value can't convert to Status" };
                logger.LogWarn($"Validation Error in File {filename}. {JsonConvert.SerializeObject(error)}");
                errorList.Add(error);
            }
            #endregion Status

            if (errorList.Count > 0)
                return (false,errorList);
            else
                return (true,errorList);
        }
    }
}