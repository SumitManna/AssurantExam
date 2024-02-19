namespace AssurantTest.Application.Exception
{
    [Serializable]
    public class ApiException : System.Exception
    {
        public int StateCode { get; set; }
        public string ErrorMessage { get; set; }
        public ApiException(int statusCode, string message) : base(message)
        {
            StateCode = statusCode;
            ErrorMessage = message;
        }
    }
}
