using System;
namespace RadioTaxisAPI.Exceptions
{
    public class RequestNotFoundException : Exception
    {
        public RequestNotFoundException(string message)
            :base(message)
        {
        }
    }
}
