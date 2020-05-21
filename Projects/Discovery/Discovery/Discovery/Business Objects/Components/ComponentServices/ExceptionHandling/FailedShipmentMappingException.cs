
namespace Discovery.ComponentServices.ExceptionHandling
{
    public class FailedShipmentMappingException : DiscoveryException
    {
        public FailedShipmentMappingException(string opCoCode, object operatorEmail, string message)
            : base(message)
        {
            Properties["OpCoCode"] = opCoCode;
            Properties["OperatorEmail"] = operatorEmail;
        }
    }
}