using System;

namespace HBOS.FS.AMP.UPD.Types.AssetFunds
{
	/// <summary>
	/// Use this class for currency objects
	/// </summary>
	public class Currency
	{
		/// <summary>
		/// Default constructor
		/// </summary>
		/// <param name="currencyCode">The currency code</param>
        public Currency(string currencyCode)
		{
            m_currencyCode = currencyCode;        
        }

        private string m_currencyCode;

        /// <summary>
        /// The currency code
        /// </summary>
        public string CurrencyCode
        {
            get
            {
                return m_currencyCode;
            }
            set
            {
                m_currencyCode = value;
            }
        }

		/// <summary>
		/// Overridden to return equality of two currency objects based on the CurrencyCode
		/// </summary>
		/// <param name="obj">Obj.</param>
		/// <returns></returns>
		public override bool Equals(object obj)
		{
			if (!(obj is Currency))
			{
				return false;
			}
			else
			{
				Currency check = (Currency)obj;
				if (check.CurrencyCode == this.CurrencyCode)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
		}

		/// <summary>
		/// Gets the hash code. Overridden to return the has code of the CurrencyCode
		/// </summary>
		/// <returns></returns>
        public override int GetHashCode()
        {
            return m_currencyCode.GetHashCode();
        }


	}
}