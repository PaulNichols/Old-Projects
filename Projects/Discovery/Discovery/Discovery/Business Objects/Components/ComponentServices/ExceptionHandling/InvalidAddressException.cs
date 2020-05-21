
namespace Discovery.ComponentServices.ExceptionHandling
{
    public class InvalidAddressException : DiscoveryException
    {
        public InvalidAddressException(string opCoCode,  object operatorEmail,string message)
            : base(message)
        {
            Properties["OpCoCode"] = opCoCode;
            Properties["OperatorEmail"] = operatorEmail;
        }
    }
}