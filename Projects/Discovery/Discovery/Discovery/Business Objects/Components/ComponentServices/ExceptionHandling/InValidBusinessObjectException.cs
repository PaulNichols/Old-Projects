namespace Discovery.ComponentServices.ExceptionHandling
{
    /// <summary>
    /// A class to handle business object exception
    /// </summary>
    public class InValidBusinessObjectException : DiscoveryException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:InValidBusinessObjectException"/> class.
        /// </summary>
        /// <param name="validatableObject">The validatable object.</param>
     public InValidBusinessObjectException(ValidationFramework.ValidatableBase validatableObject)
            : base("")
        {
        }
    }
}