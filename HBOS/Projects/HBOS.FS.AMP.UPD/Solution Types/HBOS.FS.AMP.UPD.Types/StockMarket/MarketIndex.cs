using System;

using HBOS.FS.AMP.Entities;
using HBOS.FS.AMP.UPD.Types;

namespace HBOS.FS.AMP.UPD.Types.AssetFunds
{
	/// <summary>
	/// Summary description for MarketIndex.
	/// </summary>
	public class MarketIndex : EntityBase
	{

        #region Private variable declaration
        //
        // Data fields
        //
        private string m_countryCode;
        private string m_indexName;
        private string m_currencyCode;
        private int m_marketIndexID;

        #endregion
        
        #region MarketIndex constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public MarketIndex()
		{
            m_countryCode = "";
            m_indexName = "";
            m_currencyCode = "";
            m_marketIndexID = 0;

            m_isDirty = false;
            m_isNew = true;
            m_isDeleted = false;

        	m_timestamp = new byte[1];
        }

        /// <summary>
        /// Overrloaded constructor, used to populate from the DB
        /// </summary>
        public MarketIndex(string countryCode, string indexName, string currencyCode, int marketIndexID)
        {
            m_countryCode = countryCode;
            m_indexName = indexName;
            m_currencyCode = currencyCode;
            m_marketIndexID = marketIndexID;

            m_isDirty = false;
            m_isNew = false;
            m_isDeleted = false;

        	m_timestamp = new byte[1];

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
			if (!(obj is MarketIndex))
			{
				return false;
			}
			else
			{
				MarketIndex check = (MarketIndex)obj;
				if (check.IndexName == this.IndexName)
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
        /// Currency code for the asset fund Market index
        /// </summary>
        public string CurrencyCode
        {
            get { return m_currencyCode; }
            set 
            {
                m_currencyCode = value;
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
