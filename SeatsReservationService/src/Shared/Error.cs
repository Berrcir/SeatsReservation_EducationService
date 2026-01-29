using System.Text.Json.Serialization;

namespace Shared
{
    public record class Error
    {
        public string Code { get; init; }

        public string Message { get; init; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ErrorType Type { get; init; }

        public string? InvalidField { get; init; }

        [JsonConstructor]
        public Error(string code, string message, ErrorType type, string? invalidField = default)
        {
            Code = code;
            Message = message;
            Type = type;
            InvalidField = invalidField;
        }

        public static Error NotFound(string? code, string message) =>
            new(code ?? "record.not.found", message, ErrorType.NotFound);

        public static Error Validation(string? code, string message, string invalidField) =>
            new(code ?? "value.is.invalid", message, ErrorType.Validation, invalidField);

        public static Error Conflict(string? code, string message) =>
            new(code ?? "value.is.conflict", message, ErrorType.Conflict);

        public static Error Failure(string? code, string message) =>
            new(code ?? "failure", message, ErrorType.Failure);
    }
}