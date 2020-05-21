using Discovery.ComponentServices.ExceptionHandling;

namespace Discovery.Integration
{
    public class MissingFileException : DiscoveryException
    {
        public MissingFileException(string opCoCode,string message) : base(message)
        {
            Properties["OpCoCode"] = opCoCode;
        }
    }
}