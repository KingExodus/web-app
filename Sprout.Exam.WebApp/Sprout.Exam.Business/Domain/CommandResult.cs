using Sprout.Exam.Common.Enums;

namespace Sprout.Exam.Business.Domain
{
    public class CommandResult<T> : CommandResult
    {
        public CommandResult(T value) : this(OperationResult.Success, value, null)
        {
        }

        public CommandResult(string error) : this(OperationResult.Error, default(T), error)
        {
        }

        public CommandResult(OperationResult result, string error) : this(result, default(T), error)
        {
        }

        public CommandResult(OperationResult result, T value, string error) : base(result, error)
        {
            Value = value;
        }

        public T Value { get; set; }
    }

    public class CommandResult
    {
        public CommandResult()
            : this(OperationResult.Success, null)
        {
        }

        public CommandResult(string error)
            : this(OperationResult.Error, error)
        {
        }

        public CommandResult(OperationResult result, string error)
        {
            Result = result;
            Error = error;
        }

        public static CommandResult Success { get; } = new CommandResult();

        public OperationResult Result { get; }

        public string Error { get; }

        public CommandResult<T> To<T>()
        {
            return new CommandResult<T>(Result, Error);
        }
    }
}
