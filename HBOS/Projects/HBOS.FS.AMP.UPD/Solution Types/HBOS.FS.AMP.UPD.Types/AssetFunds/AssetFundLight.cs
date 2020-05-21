using System;
using HBOS.FS.AMP.Entities;

namespace HBOS.FS.AMP.UPD.Types.AssetFunds
{
	/// <summary>
	/// Light weight asset fund object
	/// </summary>
	public class AssetFundLight : IEntityBase
	{
		#region Local variables

		private string m_assetFundCode;
		private string m_shortName;
		private string m_fullName;
		private string m_CompanyCode;
		private int m_fundGroupId;
		private bool m_fundGroupIDSet = false;
		private byte[] m_timeStamp;

		private bool m_isDirty;
		private bool m_isNew;
		private bool m_isDeleted;

		#endregion

		#region Constructor

		/// <summary>
		/// Asset Fund Light constructor
		/// </summary>
		/// <param name="assetFundCode"></param>
		/// <param name="shortName"></param>
		/// <param name="fullName"></param>
		/// <param name="companyCode"></param>
		/// <param name="fundGroupId"></param>
		/// <param name="fundGroupIDSet"></param>
		/// <param name="timestamp"></param>
		public AssetFundLight( string assetFundCode , string shortName , string fullName , string companyCode , int fundGroupId , bool fundGroupIDSet , byte[] timestamp)
		{
			m_assetFundCode = assetFundCode;
			m_shortName = shortName;
			m_fullName = fullName;
			m_CompanyCode = companyCode;
			m_fundGroupId = fundGroupId;
			m_timeStamp = timestamp;
			m_fundGroupIDSet = fundGroupIDSet;
		}

		/// <summary>
		/// Create a new empty light asset fund
		/// </summary>
        public AssetFundLight()
        {
            this.m_assetFundCode = "";
            this.m_shortName = "";
            this.m_fullName = "";
            this.m_CompanyCode = "";
            this.m_fundGroupId = 0;
			this.m_fundGroupIDSet = false;

            //Set up IEntityBase members
            m_isNew = true;
            m_isDeleted = false;
            m_timeStamp = new byte[1];
            m_isDirty = true;
        }


		#endregion

		#region Properties

		/// <summary>
		/// The unique identifier for the asset fund.
		/// </summary>
		public string AssetFundCode
		{
			get
			{ 
				return m_assetFundCode; 
			}
			set
			{
				m_assetFundCode = value;
			}
		}

		/// <summary>
		/// The short asset fund name.
		/// </summary>
		public string ShortName
		{
			get
			{ 
				return m_shortName; 
			}
			set
			{
				m_shortName = value;
				m_isDirty = true;
			}
		}

		/// <summary>
		/// The full asset fund name.
		/// </summary>
		public string FullName
		{
			get
			{ 
				return m_fullName; 
			}
			set
			{
				m_fullName = value;
				m_isDirty = true;
			}
		}

		/// <summary>
		/// A code that represents the Company that this Asset Fund belongs to
		/// </summary>
		public string CompanyCode
		{
			get
			{ 
				return m_CompanyCode;	
			}
			set
			{
				m_CompanyCode = value;
				m_isDirty = false;
			}
		}

		/// <summary>
		/// Fund Group Id
		/// </summary>
		public int FundGroupId
		{
			get
			{ 
				return m_fundGroupId;
			}
			set
			{
				m_fundGroupId = value;
				m_fundGroupIDSet = true;
			}
		}

		/// <summary>
		/// Dirty Flag
		/// </summary>
		public bool IsDirty
		{
			get
			{
				return m_isDirty;
			}
			set
			{
				m_isDirty = value;
			}
		}

		/// <summary>
		/// New Flag
		/// </summary>
		public bool IsNew
		{
			get
			{
				return m_isNew;
			}
			set
			{
				m_isNew = value;
			}
		}

		/// <summary>
		/// Deleted Flag
		/// </summary>
		public bool IsDeleted
		{
			get
			{
				return m_isDeleted;
			}
			set
			{
				m_isDeleted = value;
			}
		}

		/// <summary>
		/// Timestamp
		/// </summary>
		public byte[] TimeStamp
		{
			get
			{
				return m_timeStamp;
			}
			set
			{
				m_timeStamp = value;
			}
		}

		/// <summary>
		/// Flag to indicate whether the FundGroupID is valid or not because a fund may not have been
		/// associated with a fund group.
		/// </summary>
		public bool FundGroupIDSet
		{
			get
			{
				return this.m_fundGroupIDSet;
			}
			set
			{
				this.m_fundGroupIDSet = value;
			}
		}

		#endregion
	}
}
