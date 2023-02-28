namespace Sprout.Exam.Business.Domain
{
    public class CommandErrorResult
    {
        public CommandErrorResult(string error)
        {
            Error = error;
        }

        public string Error { get; }
    }
}
