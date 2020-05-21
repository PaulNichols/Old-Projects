using Discovery.ComponentServices.ExceptionHandling;

namespace Discovery.Integration
{
    public class SequenceNumberException : DiscoveryException
    {
        public SequenceNumberException(string opCoCode,string message) : base(message)
        {
            Properties["OpCoCode"] = opCoCode;
        }
    }
}