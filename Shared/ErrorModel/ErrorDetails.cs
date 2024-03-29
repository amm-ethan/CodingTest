﻿using System.Text.Json;

namespace Shared.ErrorModel
{
    public class ErrorDetails
    {
        public int StatusCode { get; set; }
        public Object? Message { get; set; }

        public override string ToString() => JsonSerializer.Serialize(this);
    }
}
