

namespace Discovery.ComponentServices.ExceptionHandling
{
    /// <summary>
    /// A class to handle exception for mapping
    /// </summary>
    public class MappingException : DiscoveryException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:MappingException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public MappingException(string message)
            : base(message)
        {
        }
    }
}
