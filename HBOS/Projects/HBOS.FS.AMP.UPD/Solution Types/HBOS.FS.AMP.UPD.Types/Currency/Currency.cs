using System;

namespace HBOS.FS.AMP.UPD.Types.Currency
{
	/// <summary>
	/// Use this class for currency objects
	/// </summary>
	[Serializable]
	public class Currency : EntityBase	
	{
		private string m_currencyCode;
		private string m_currencyName;
		private string m_currentCurrencyCode;
		private decimal m_currentRate;
		private decimal m_previousRate;
		private static Currency m_globalMarket;
		private static Currency m_gbPound;

		/// <summary>
		/// Default constructor
		/// </summary>
		public Currency()
		{
			this.IsNew = true;
			this.IsDeleted = false;
			this.IsDirty = false;
		}

		/// <summary>
		/// Default constructor
		/// </summary>
		/// <param name="currencyCode">The currency code</param>
		/// 
		public Currency(string currencyCode)
		{
			m_currencyCode = currencyCode;
			m_currentCurrencyCode = m_currencyCode;

			this.IsNew = false;
			this.IsDeleted = false;
			this.IsDirty = false;
		}

		/// <summary>
		/// Creates a new <see cref="Currency"/> instance.
		/// </summary>
		/// <param name="currencyCode">Currency code.</param>
		/// <param name="currentRate">Current rate.</param>
		/// <param name="previousRate">Previous rate.</param>
		public Currency(string currencyCode, decimal currentRate, decimal previousRate) : this(currencyCode)
		{
			this.m_currentRate = currentRate;
			this.m_previousRate = previousRate;
		}

		/// <summary>
		/// Creates a new <see cref="Currency"/> instance.
		/// </summary>
		/// <param name="currencyCode">Currency code.</param>
		/// <param name="currencyName">Name of the currency.</param>
		/// <param name="timestamp">Timestamp.</param>
		public Currency(string currencyCode, string currencyName, byte[] timestamp) : this(currencyCode)
		{
			m_currencyName = currencyName;
			m_timestamp = timestamp;
		}

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
				this.IsDirty = true;
			}
		}

		/// <summary>
		/// Gets or sets the name of the currency.
		/// </summary>
		/// <value></value>
		public string CurrencyName
		{
			get
			{
				return m_currencyName;
			}
			set
			{
				m_currencyName = value;
				this.IsDirty = true;
			}
		}

		/// <summary>
		/// Gets or sets the current currency code.
		/// </summary>
		/// <value></value>
		public string CurrentCurrencyCode
		{
			get
			{
				return m_currentCurrencyCode;
			}
			set
			{
				m_currentCurrencyCode = value;
			}
		}

		/// <summary>
		/// Gets the current rate.
		/// </summary>
		/// <value></value>
		public decimal CurrentRate
		{
			get { return m_currentRate; }
		}

		/// <summary>
		/// Gets the previous rate.
		/// </summary>
		/// <value></value>
		public decimal PreviousRate
		{
			get { return m_previousRate; }
		}

		/// <summary>
		/// Gets the gb pound.
		/// </summary>
		/// <value></value>
		public static Currency GBPound
		{
			get
			{
				if (m_gbPound == null)
				{
					m_gbPound = new Currency("GBP", 1, 1);
				}
				return m_gbPound;
			}
		}

		/// <summary>
		/// Gets the global market which can be compared against to 
		/// determine if an object has no Currency.
		/// </summary>
		/// <value></value>
		public static Currency GlobalMarket
		{
			get
			{
				if (m_globalMarket == null)
				{
					m_globalMarket = new Currency("", 0, 0);
				}
				return m_globalMarket;
			}
		}

		/// <summary>
		/// Calculates the movement based on todays conversion rate - the last conversion rate all divided by the last conversion rate.
		/// </summary>
		/// <param name="currencyFrom">Currency from.</param>
		/// <param name="currencyTo">Currency to.</param>
		/// <returns></returns>
		public static decimal CalculateMovement(Currency currencyFrom, Currency currencyTo)
		{
			decimal movement=0;
			decimal previousConversionRate = PreviousConversionRate(currencyFrom, currencyTo);
			decimal todaysConversionRate = TodaysConversionRate(currencyFrom, currencyTo);
			
            /* UA240 MISDC Finance MAW - amended line below to include the check on todaysConversionRate */
            if (previousConversionRate > 0 && todaysConversionRate > 0)
			{
				movement= (todaysConversionRate - previousConversionRate)/previousConversionRate;
			}
				return movement*-1;
		}

		/// <summary>
		/// Previouses the conversion rate.
		/// </summary>
		/// <param name="from">From.</param>
		/// <param name="to">To.</param>
		/// <returns></returns>
		public static decimal PreviousConversionRate(Currency from, Currency to)
		{
			try
			{
				return from.PreviousRate/to.PreviousRate;
			}
			catch //(System.DivideByZeroException e)
			{
				return 0m;
			}
		}

		/// <summary>
		/// Todayses the conversion rate.
		/// </summary>
		/// <param name="from">From.</param>
		/// <param name="to">To.</param>
		/// <returns></returns>
		public static decimal TodaysConversionRate(Currency from, Currency to)
		{
			try
			{
				return from.CurrentRate/to.CurrentRate;
			}
			catch// (System.DivideByZeroException e)
			{
				return 0m;
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
				return base.Equals(obj);
			}
			else
			{
				Currency check = (Currency) obj;
				return (check == this);
			}
		}

		/// <summary>
		/// Overloaded equality operator
		/// </summary>
		/// <param name="lhs">First Object to compare</param>
		/// <param name="rhs">Second Object to compare</param>
		/// <returns></returns>
		public static bool operator==(Currency lhs,Currency rhs)
		{
			if ((object)lhs !=null && (object)rhs!=null )
			{
				return (lhs.CurrencyCode==rhs.CurrencyCode);
			}
			else
			{
				return (object)lhs==(object)rhs;
			}
		}

		/// <summary>
		/// Overloaded inequality operator
		/// </summary>
		/// <param name="lhs">First Object to compare</param>
		/// <param name="rhs">Second Object to compare</param>
		/// <returns></returns>
		public static bool operator!=(Currency lhs,Currency rhs)
		{
			return !(lhs==rhs);
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