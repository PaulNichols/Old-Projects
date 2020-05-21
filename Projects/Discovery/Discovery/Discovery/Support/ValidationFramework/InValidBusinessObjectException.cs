using ValidationFramework;

/// <summary>
/// A class to handle invalid business object exception
/// </summary>
public class InValidBusinessObjectException : System.Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="T:InValidBusinessObjectException"/> class.
    /// </summary>
    /// <param name="validatableObject">The validatable object.</param>
    public InValidBusinessObjectException(ValidatableBase validatableObject): base("Object is invalid")
    {
        this.validatableObject = validatableObject;
    }

    private ValidatableBase validatableObject;

    /// <summary>
    /// Gets the validatable object.
    /// </summary>
    /// <value>The validatable object.</value>
    public ValidatableBase ValidatableObject
    {
        get { return validatableObject; }
    }

    public override string Message
    {
        get
        {
            // See if we have some validation messages
            if (null != validatableObject && null != validatableObject.ValidationMessages)
            {
                // Return the validation messages
                return string.Join(" ", validatableObject.ValidationMessages.ToArray());
            }
            else
            {
                return "";
            }
        }
    }
}