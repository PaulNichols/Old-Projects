using System;

namespace HBOS.FS.AMP.UPD.Types.Countries
{
	/// <summary>
	/// Class representing data for a country
	/// </summary>
	 [Serializable]
	public class Country : EntityBase
	{
		#region Private variable declaration

		//
		// Data fields
		//
		private string m_countryCode;
		private string m_countryName;
		private string m_currencyCode="";
		private string m_currentCountryCode;

		#endregion

		#region Country constructor

		/// <summary>
		/// Default constructor
		/// </summary>
		public Country()
		{
			this.IsNew = true;
			this.IsDeleted = false;
			this.IsDirty = false;
		}

		/// <summary>
		/// Default constructor
		/// </summary>
		/// <param name="CountryCode">The Country code</param>
		/// 
		public Country(string CountryCode)
		{
			m_countryCode = CountryCode;
			m_currentCountryCode = m_countryCode;

			this.IsNew = false;
			this.IsDeleted = false;
			this.IsDirty = false;
		}

		/// <summary>
		/// Default constructor
		/// </summary>
		/// <param name="CountryCode">The Country code</param>
		/// <param name="countryName"></param>
		/// 
		public Country(string CountryCode,string countryName)
		{
			m_countryCode = CountryCode;
			m_countryName=countryName;

			this.IsNew = false;
			this.IsDeleted = false;
			this.IsDirty = false;
		}

		/// <summary>
		/// Creates a new <see cref="Country"/> instance.
		/// </summary>
		/// <param name="CountryCode">Country code.</param>
		/// <param name="CountryName">Name of the Country.</param>
		/// <param name="currencyCode">The Code of the Counties default currency</param>
		/// <param name="timestamp">Timestamp.</param>
		public Country(string CountryCode, string CountryName, string currencyCode,byte[] timestamp) : this(CountryCode)
		{
			m_countryName = CountryName;
			this.m_currencyCode = currencyCode;
			m_timestamp = timestamp;
		}

		#endregion

		#region Country properties

		/// <summary>
		/// Country code for the country
		/// </summary>
		public string CountryCode
		{
			get { return m_countryCode; }


			set { m_countryCode = value; }
		}

		/// <summary>
		/// Gets or sets the current country code (used for updating).
		/// </summary>
		/// <value></value>
		public string CurrentCountryCode
		{
			get { return m_currentCountryCode; }


			set { m_currentCountryCode = value; }
		}


		/// <summary>
		/// Name of the country
		/// </summary>
		public string CountryName
		{
			get { return m_countryName; }
			set { m_countryName = value; }
		}

		/// <summary>
		/// Gets or sets the currency.
		/// </summary>
		/// <value></value>
		public string CurrencyCode
		{
			get { return m_currencyCode; }
			set { m_currencyCode = value; }
		}

		#endregion Country properties

		/// <summary>
		/// Overridden to return equality of two Country objects based on the CountryCode
		/// </summary>
		/// <param name="obj">Obj.</param>
		/// <returns></returns>
		public override bool Equals(object obj)
		{
			if (!(obj is Country))
			{
				return base.Equals(obj);
			}
			else
			{
				Country check = (Country) obj;
				return (check == this);
			}
		}


		 /// <summary>
		 /// Overloaded equality operator
		 /// </summary>
		 /// <param name="lhs">First Object to compare</param>
		 /// <param name="rhs">Second Object to compare</param>
		 /// <returns></returns>
		 public static bool operator==(Country lhs,Country rhs)
		 {
			 if ((object)lhs !=null && (object)rhs!=null )
			 {
				 return (lhs.CountryCode==rhs.CountryCode);
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
		 public static bool operator!=(Country lhs,Country rhs)
		 {
			 return !(lhs==rhs);
		 }


		/// <summary>
		/// Gets the hash code.
		/// </summary>
		/// <returns></returns>
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}


	}
}