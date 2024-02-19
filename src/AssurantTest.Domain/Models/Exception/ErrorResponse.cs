namespace AssurantTest.Domain.Models.Exception
{
    public record ErrorResponse(string Message, IEnumerable<string> ErrorList)
    {
        public ErrorResponse(string Message) : this(Message, new List<string>()) { }
    }
}
