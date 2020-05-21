using System;

namespace HBOS.FS.AMP.UPD.Types.Companies
{
	/// <summary>
	/// The HBOS company with which users and funds are associated.
	/// </summary>
	public class Company : EntityBase
	{
        #region Private variables
        private string m_companyCode = string.Empty;
		private string m_companyName = string.Empty;
		private DateTime m_ValuationDate;
		private DateTime m_NextValuationDate;
		private DateTime m_PreviousValuationDate;

		// Flags for IEntityBase implementation

		#endregion

        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public Company()
        {
            m_companyCode = string.Empty;
            m_companyName = string.Empty;

            // Set up the IEntityBase members.
            m_isDirty = false;
            m_isNew = true;
            m_isDeleted = false;
        	m_timestamp = new byte[1];
        }

        /// <summary>
        /// Initialise the CompanyCode as this is a read-only property.
        /// </summary>
        /// <param name="companyCode">The instance company code.</param>
        public Company(string companyCode)
        {
            m_companyCode = companyCode;
            m_companyName = string.Empty;

            m_isDirty = false;
            m_isNew = false;
            m_isDeleted = false;
        	m_timestamp = new byte[1];
        }

        /// <summary>
        /// Overloaded constructor
        /// </summary>
        /// <param name="companyCode">The company code for the company</param>
        /// <param name="companyName">The company name for the company</param>
        public Company(string companyCode, string companyName)
        {
            m_companyCode = companyCode;
            m_companyName = companyName;
			

            m_isDirty = false;
            m_isNew = false;
            m_isDeleted = false;
        	m_timestamp = new byte[1];
        }

        /// <summary>
        /// Overloaded constructor
        /// </summary>
        /// <param name="companyCode">The company code for the company</param>
        /// <param name="companyName">The company name for the company</param>
        /// <param name="timeStamp">Modification time stamp</param>
        /// <param name="valuationPointTime">The company valuation point time part</param>
        /// <param name="valuationDay">The companies valuation day</param>
        public Company(string companyCode, string companyName ,DateTime valuationDay,DateTime valuationPointTime,byte[] timeStamp)
        {
            m_companyCode = companyCode;
            m_companyName = companyName;
        	m_ValuationDate=new DateTime(valuationDay.Year ,valuationDay.Month ,valuationDay.Day, 
					valuationPointTime.Hour,valuationPointTime.Minute, 0);
			
            m_isDirty = false;
            m_isNew = false;
            m_isDeleted = false;
        	m_timestamp = timeStamp;
        }

		/// <summary>
		/// Overloaded constructor
		/// </summary>
		/// <param name="companyCode">The company code for the company</param>
		/// <param name="companyName">The company name for the company</param>
		/// <param name="timeStamp">Modification time stamp</param>
		/// <param name="valuationPointTime">The company valuation point time part</param>
		/// <param name="valuationDay">The companies valuation day</param>
		/// <param name="nextvaluationPointTime"></param>
		/// <param name="previousvaluationPointTime"></param>
		public Company(string companyCode, string companyName ,DateTime valuationDay,DateTime valuationPointTime,DateTime nextvaluationPointTime,
					   byte[] timeStamp, DateTime previousvaluationPointTime )
		{
			m_companyCode = companyCode;
			m_companyName = companyName;
			m_ValuationDate=new DateTime(valuationDay.Year ,valuationDay.Month ,valuationDay.Day, 
				valuationPointTime.Hour,valuationPointTime.Minute, 0);

			m_NextValuationDate=new DateTime(nextvaluationPointTime.Year ,nextvaluationPointTime.Month ,nextvaluationPointTime.Day, 
				valuationPointTime.Hour,valuationPointTime.Minute, 0);

			m_PreviousValuationDate=new DateTime(previousvaluationPointTime.Year ,previousvaluationPointTime.Month ,previousvaluationPointTime.Day, 
				valuationPointTime.Hour,valuationPointTime.Minute, 0);
			
			m_isDirty = false;
			m_isNew = false;
			m_isDeleted = false;
			m_timestamp = timeStamp;
		}

		/// <summary>
		/// Overloaded constructor
		/// </summary>
		/// <param name="companyCode">The company code for the company</param>
		/// <param name="companyName">The company name for the company</param>
		/// <param name="timeStamp">Modification time stamp</param>
		public Company(string companyCode, string companyName ,byte[] timeStamp)
		{
			m_companyCode = companyCode;
			m_companyName = companyName;

			m_isDirty = false;
			m_isNew = false;
			m_isDeleted = false;
			m_timestamp = timeStamp;
		}

        #endregion

        #region Methods

		#endregion

        #region Members
		/// <summary>
		/// The unique identifier for the company.
		/// </summary>
		public string CompanyCode
		{
			get
			{
				return this.m_companyCode;
			}

            
            set
            {
                this.m_companyCode = value;
            }
		}

		/// <summary>
		/// The company name.
		/// </summary>
		public string CompanyName
		{
			set
			{
                m_companyName = value;
                SetDirtyFlag();
			}

			get
			{
				return m_companyName;
			}
		}

		/// <summary>
		/// Gets or sets the valuation point time.
		/// </summary>
		/// <value></value>
		public DateTime ValuationDate
		{
			get {return m_ValuationDate; }
			set {m_ValuationDate=value;}
		}

		/// <summary>
		/// Gets the next valuation point time.
		/// </summary>
		/// <value></value>
		public DateTime NextValuationDate
		{
			get { return m_NextValuationDate; }
		}

		/// <summary>
		/// Gets the previous valuation point time.
		/// </summary>
		/// <value></value>
		public DateTime PreviousValuationDate
		{
			get { return m_PreviousValuationDate; }
		}

		#endregion

    }
}
