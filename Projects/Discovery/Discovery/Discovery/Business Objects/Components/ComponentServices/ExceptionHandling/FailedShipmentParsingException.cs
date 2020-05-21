
namespace Discovery.ComponentServices.ExceptionHandling
{
    public class FailedShipmentParsingException : DiscoveryException
    {
        public FailedShipmentParsingException(string opCoCode, string message)
            : base(message)
        {
            Properties["OpCoCode"] = opCoCode;
        }
    }
}