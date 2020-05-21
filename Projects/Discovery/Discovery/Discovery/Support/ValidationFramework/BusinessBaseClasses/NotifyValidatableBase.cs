#region Using Directives

using System;
using System.ComponentModel;

#endregion

namespace ValidationFramework
{
    /// <summary>
    /// Provides base class that developer can (optionally) inherit from to provide
    /// INotifyPropertyChanged and IDataErrorInfo functionality for all sub-classes.
    /// This ideal for windows forms applications to get immediate validation feedback
    /// on databound controls.
    /// 
    /// An example sub-class property would look like this:
    /// <example>
    /// [RequiredValidator("Last name is required.")]
    /// public string LastName
    /// {
    ///     get { return this.lastName; }
    ///     set
    ///     {
    ///         if (lastName != value)
    ///         {
    ///             this.lastName = value;
    ///             this.NotifyAndValidate(LastNameMember);
    ///         }
    ///     }
    /// }
    /// </example>
    /// </summary>
    public abstract class NotifyValidatableBase : ValidatableBase, IDataErrorInfo, INotifyPropertyChanged
    {
        #region Properties

        /// <summary>
        /// Property can be overridden when a parent object needs to validate children.
        /// </summary>
        /// <value></value>
        public override bool IsValid
        {
            get
            {
                //Override this property to JUST return bool value because all
                //validations have already been done for each property
                return Validator.IsValid;
            }
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Perform validation and INotifyPropertyChanged functionality for 
        /// specified property.
        /// </summary>
        /// <param name="propertyName"></param>
        protected void NotifyAndValidate(string propertyName)
        {
            Validator.Validate(propertyName);
            NotifyPropertyChanged(propertyName);
        }

        /// <summary>
        /// Performs INotifyPropertyChanged functionality for specified property.
        /// </summary>
        /// <param name="propertyName"></param>
        protected void NotifyPropertyChanged(string propertyName)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Perform Validation for specified property.
        /// </summary>
        /// <param name="propertyName"></param>
        protected void ValidateProperty(string propertyName)
        {
            Validator.Validate(propertyName);
        }

        #endregion

        #region IDataErrorInfo Members

        string IDataErrorInfo.this[string columnName]
        {
            get { return Validator.GetErrorMessages(columnName); }
        }

        /// <summary>
        /// Gets the <see cref="T:String"/> with the specified column name.
        /// </summary>
        /// <value></value>
        protected string this[string columnName]
        {
            get { return Validator.GetErrorMessages(columnName); }
        }

        string IDataErrorInfo.Error
        {
            get { return GetAllValidationMessages(Environment.NewLine); }
        }

        /// <summary>
        /// Gets an error message indicating what is wrong with this object.
        /// </summary>
        /// <value></value>
        /// <returns>An error message indicating what is wrong with this object. The default is an empty string ("").</returns>
        protected string Error
        {
            get { return GetAllValidationMessages(Environment.NewLine); }
        }

        #endregion

        #region INotifyPropertyChanged Members

        private event PropertyChangedEventHandler propertyChanged;

        /// <summary>
        /// Manipulate the property change value
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged
        {
            add { propertyChanged += value; }
            remove { propertyChanged -= value; }
        }

        /// <summary>
        /// Raises the <see cref="E:PropertyChanged"/> event.
        /// </summary>
        /// <param name="e">The <see cref="T:System.ComponentModel.PropertyChangedEventArgs"/> instance containing the event data.</param>
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (propertyChanged != null)
            {
                propertyChanged(this, e);
            }
        }

        #endregion
    } //end class
} //end namespace