namespace Discovery.ComponentServices.ExceptionHandling
{
    /// <summary>
    /// A class to handle exception for Optrak Parsing
    /// </summary>
    public class OptrakParsingException : DiscoveryException
    {
        private readonly int lineNumber;
        private readonly string lineText;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:OptrakParsingException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="lineNumber">The line number.</param>
        /// <param name="lineText">The line text.</param>
        public OptrakParsingException(string message, int lineNumber, string lineText)
            : base(message)
        {
            this.lineNumber = lineNumber;
            this.lineText = lineText;
        }

        /// <summary>
        /// Gets the line number we errored on.
        /// </summary>
        /// <value>The current line number.</value>
        public int LineNumber
        {
            get { return lineNumber; }
        }

        /// <summary>
        /// Gets the raw line text.
        /// </summary>
        /// <value>The line text.</value>
        public string LineText
        {
            get { return lineText; }
        }
    }
}