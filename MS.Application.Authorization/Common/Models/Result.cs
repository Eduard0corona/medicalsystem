using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MS.Application.Authorization.Common.Models
{
    public class Result<T>
    {
        protected Result(bool isSuccess, T value, string error)
        {
            IsSuccess = isSuccess;
            Value = value;
            Error = error;
        }

        public bool IsSuccess { get; }
        public T? Value { get; }
        public string? Error { get; }

        public static Result<T> Success(T value) => new(true, value, null!);
        public static Result<T> Failure(string error) => new(false, default!, error);
    }
}
