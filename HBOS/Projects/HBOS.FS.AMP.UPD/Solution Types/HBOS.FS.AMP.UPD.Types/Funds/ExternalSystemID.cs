namespace HBOS.FS.AMP.UPD.Types.Funds
{
	/// <summary>
	/// The class holding the various external system IDs for a fund.
	/// </summary>
	public class ExternalSystemID : EntityBase
	{
		#region Local variables

		// Property variables
		private int m_systemID;
		private string m_systemName;
		private string m_fundCode;
		private string m_hiPortCode;

		#endregion

		#region Constructors

		/// <summary>
		/// /// Constructor setting all properties to default values.
		/// </summary>
		public ExternalSystemID()
		{
			// Initialise all variables to default values.
			m_hiPortCode = string.Empty;
			m_systemID = 0;
			m_systemName = string.Empty;
			m_fundCode = string.Empty;

			m_isDirty = true;
			m_isNew = true;
			m_isDeleted = false;
			m_timestamp = null;
		}

		/// <summary>
		/// Constructor to create the object with its properties initialised.
		/// </summary>
		/// <param name="HiPortfolioCode"></param>
		/// <param name="systemID">The ID of the system for which this fund code is to be used.</param>
		/// <param name="systemName">The name of the system for which this fund code is to be used.</param>
		/// <param name="fundCode">The code to use to identify the associated fund.</param>
		/// <param name="timestamp">Database timestamp of the record.</param>
		public ExternalSystemID(string HiPortfolioCode, int systemID, string systemName, string fundCode, byte[] timestamp)
		{
			m_hiPortCode = HiPortfolioCode;
			m_systemID = systemID;
			m_systemName = systemName;
			m_fundCode = fundCode;

			m_isDirty = false;
			m_isNew = false;
			m_isDeleted = false;
			m_timestamp = timestamp;
		}

		#endregion

		#region Properties

		/// <summary>
		/// The system for which this fund code is to be used.
		/// </summary>
		public int SystemID
		{
			get
			{
				return this.m_systemID;
			}

			set
			{
				this.m_systemID = value;
				this.m_isDirty = true;
			}
		}

		/// <summary>
		/// The identifier of the fund to which this external system identifier belongs.
		/// </summary>
		public string HiPortfolioCode
		{
			get 
			{
				return m_hiPortCode;
			}
			set
			{
				m_hiPortCode = value;
			}
		}

		/// <summary>
		/// The name of the system that this code is for.
		/// </summary>
		public string SystemName
		{
			get
			{
				return this.m_systemName;
			}

			
			set
			{
				this.m_systemName = value;
			}
		}

		/// <summary>
		/// The code to use to identify the associated fund.
		/// </summary>
		public string FundCode
		{
			get
			{
				return this.m_fundCode;
			}

			set
			{
				this.m_fundCode = value;
				this.m_isDirty = true;
			}
		}

		#endregion

		#region Overrides

		/// <summary>
		/// Overloaded equality operator
		/// </summary>
		/// <param name="lhs">First Object to compare</param>
		/// <param name="rhs">Second Object to compare</param>
		/// <returns></returns>
		public static bool operator==(ExternalSystemID lhs,ExternalSystemID rhs)
		{
			if ((object)lhs !=null && (object)rhs!=null )
			{
				return (lhs.SystemID==rhs.SystemID);
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
		public static bool operator!=(ExternalSystemID lhs,ExternalSystemID rhs)
		{
			return !(lhs==rhs);
		}

		/// <summary>
		/// Override to test equality based on System Id
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public override bool Equals(object obj)
		{
			if (obj != null & obj is ExternalSystemID)
			{
				return (this == ((ExternalSystemID)obj));
			}
			else
			{
				return base.Equals(obj);
			}
		}

		/// <summary>
		/// Gets the hash code.
		/// </summary>
		/// <returns></returns>
		public override int GetHashCode()
		{
			return this.m_systemID;
		}



		#endregion
	}
}