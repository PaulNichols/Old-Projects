using System;

namespace HBOS.FS.AMP.UPD.Types.Lookups
{
    /// <summary>
    /// A simple key-value pair type to use with lookups to use with hash tables
    /// </summary>
    public class SimpleStringLookup
	{
        /// <summary>
        /// Creates a new <see cref="SimpleStringLookup"/> instance.
        /// </summary>
        /// <param name="key">The key for the lookup</param>
        /// <param name="displayValue">Value.</param>
        public SimpleStringLookup(string key, string displayValue)
        {
            this.key = key;
            this.displayValue = displayValue;
        }

        private string key;
        /// <summary>
        /// Gets the key value.
        /// </summary>
        /// <value></value>
        public string Key
        {
            get {return key;}
        }

        private string displayValue;
        /// <summary>
        /// Gets or sets the display value.
        /// </summary>
        /// <value></value>
        public string DisplayValue
        {
            get {return displayValue;}
			set {displayValue = value;}
        }

        /// <summary>
        /// Overridden to return the DisplayValue
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return DisplayValue;
        }	

		/// <summary>
		/// Overridden to determine equality based on Key
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public override bool Equals(object obj)
		{
			if (obj != null && obj is SimpleStringLookup)
			{
				return (this.Key == ((SimpleStringLookup)obj).Key);
			}
			else
			{
				return base.Equals (obj);
			}
		}

		/// <summary>
		/// Overridden to please the compiler.
		/// </summary>
		/// <returns></returns>
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

    }
}
