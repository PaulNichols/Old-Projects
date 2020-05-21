namespace Discovery.ComponentServices.ExceptionHandling
{
    /// <summary>
    /// A class to handle commander parsing exception with name spaces Discovery.ComponentServices.ExceptionHandling
    /// </summary>
    public class CommanderParsingException : DiscoveryException
    {
       /// <summary>
        /// Initializes a new instance of the <see cref="T:CommanderParsingException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public CommanderParsingException(string message)
            : base(message)
        {
        }
    }
}