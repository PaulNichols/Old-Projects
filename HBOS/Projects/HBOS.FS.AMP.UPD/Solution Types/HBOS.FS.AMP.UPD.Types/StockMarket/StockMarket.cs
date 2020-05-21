namespace HBOS.FS.AMP.UPD.Types.AssetFunds
{
	/// <summary>
	/// Summary description for MarketIndex.
	/// </summary>
	public class StockMarket : EntityBase
	{
		#region Private variable declaration

		//
		// Data fields
		//
		private string m_countryCode;
		private string m_indexName;
		private int m_marketIndexID;
		private bool m_global;

		#endregion

		#region Stock Market constructor

		/// <summary>
		/// Default constructor
		/// </summary>
		public StockMarket()
		{
			m_countryCode = "";
			m_indexName = "";
			m_marketIndexID = 0;
			m_global=false;
			m_isDirty = false;
			m_isNew = true;
			m_isDeleted = false;

			m_timestamp = new byte[1];
		}

		/// <summary>
		/// Overrloaded constructor, used to populate from the DB
		/// </summary>
		public StockMarket(string countryCode, string indexName,int marketIndexID,bool global,byte[ ] ts)
		{
			m_countryCode = countryCode;
			m_indexName = indexName;
			m_marketIndexID = marketIndexID;
			this.m_global = global;

			m_isDirty = false;
			m_isNew = false;
			m_isDeleted = false;

			m_timestamp = ts;

		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Override the Equals as we are only worried if the indexname is the same and not the actual objects
		/// </summary>
		/// <param name="obj"></param>
		/// <returns>true is they have the same index name</returns>
		public override bool Equals(object obj)
		{
			if (obj is StockMarket)
			{
				StockMarket stockMarket =  obj as StockMarket;
				return (this==stockMarket);
			}
			else
			{
				return base.Equals(obj);
			}
		}

		/// <summary>
		/// Overloaded equality operator
		/// </summary>
		/// <param name="lhs">First Object to compare</param>
		/// <param name="rhs">Second Object to compare</param>
		/// <returns></returns>
		public static bool operator==(StockMarket lhs,StockMarket rhs)
		{
			if ((object)lhs !=null && (object)rhs!=null )
			{
				return (lhs.IndexName==rhs.IndexName);
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
		public static bool operator!=(StockMarket lhs,StockMarket rhs)
		{
			return !(lhs==rhs);
		}


		/// <summary>
		/// Gets the hash code. Overridden to call the base implementation (????)
		/// </summary>
		/// <returns></returns>
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		#endregion

		#region MarketIndex properties
		
		/// <summary>
		/// Gets or sets the global flag.
		/// </summary>
		/// <value></value>
		public bool Global
		{
			get { return m_global; }
			set
			{
				m_global = value;
				SetDirtyFlag();
			}
		}

		/// <summary>
		/// Country code for the Market index
		/// </summary>
		public string CountryCode
		{
			get { return m_countryCode; }
			set
			{
				m_countryCode = value;
				SetDirtyFlag();
			}
		}

		/// <summary>
		/// Index name for the asset fund Market index
		/// </summary>
		public string IndexName
		{
			get { return m_indexName; }
			set
			{
				m_indexName = value;
				SetDirtyFlag();
			}
		}

		/// <summary>
		/// Proprtion figure for the asset fund Market index
		/// </summary>
		public int MarketIndexID
		{
			get { return m_marketIndexID; }
			set
			{
				m_marketIndexID = value;
				SetDirtyFlag();
			}
		}

		#endregion
	}
}