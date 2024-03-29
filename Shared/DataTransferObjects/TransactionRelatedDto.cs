﻿using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Shared.DataTransferObjects
{
    public record TransactionDto
    {
        public string? Id { get; init; }
        public string? Payment { get; init; }
        public string? Status { get; init; }
    }


    public class TransactionErrorDto
    {
        public string? Title { get; set; } = "One or more validation errors occured";
        public List<TransactionSubError>? Details { get; set; }
        public override string ToString() => JsonConvert.SerializeObject(this);
    }

    public class TransactionSubError
    {
        public string? Position { get; set; }
        public string? Header { get; set; }
        public string? Value { get; set; }
        public string? Error { get; set; }

        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}