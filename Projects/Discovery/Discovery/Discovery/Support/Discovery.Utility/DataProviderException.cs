
namespace Discovery.ComponentServices.ExceptionHandling
{
    /// <summary>
    /// A class to handle exception for data provider
    /// </summary>
    public class DataProviderException : DiscoveryException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:DataProviderException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public DataProviderException(string message):base(message)
            
        {
            }
    }
}
