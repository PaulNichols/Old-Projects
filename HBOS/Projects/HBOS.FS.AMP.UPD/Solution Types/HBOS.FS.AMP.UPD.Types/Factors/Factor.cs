using System;

namespace HBOS.FS.AMP.UPD.Types.Factors
{
	/// <summary>
	/// The base class for the various factors that can affect the predicted price.
	/// </summary>
	public abstract class Factor : EntityBase, IFactor
	{
		#region Member Variables
		
		/// <summary>
		/// The base percent by which this factor affects the predicted price (in conjunction with any other variables).
		/// Is protected so that sub class can use directly in calculations for the Factor Effect
		/// </summary>
		protected decimal m_ratioValue = 0M;

		/// <summary>
		/// The base percent by which this factor affects the predicted price (in conjunction with any other variables),
		/// once it has been authorised
		/// Is protected so that sub class can use directly in calculations for the Factor Effect
		/// </summary>
		protected decimal m_authorisedRatioValue = 0M;
				
		/// <summary>
		/// The date that the factor becomes effective. Is DateTime.MinValue if not set (00:00:00.0000000, January 1, 0001)
		/// Is protected so that sub class can use directly in calculations for the Factor Effect
		/// </summary>
		protected DateTime m_effectiveDate = DateTime.MinValue;

		/// <summary>
		/// The unique id set by the database
		/// Is protected so that sub class can set it directly (and without setting dirty flag if necessary)
		/// </summary>
		protected int m_factorID = 0;


		#endregion

		#region Constructors

		/// <summary>
		/// Default Constructor
		/// </summary>
		protected Factor()
		{
		}

		/// <summary>
		/// Base constructor to be called from sub class
		/// </summary>
		/// <param name="ratioValue"></param>
		/// <param name="factorID"></param>
		/// <param name="effectiveDate"></param>
		/// <param name="timestamp"></param>
		protected Factor (decimal ratioValue, int factorID, DateTime effectiveDate, byte[] timestamp)
		{
			m_factorID = factorID;
			m_effectiveDate = effectiveDate;
			m_ratioValue = ratioValue;
			m_timestamp = timestamp;
		}

        /// <summary>
        /// Base constructor to be called from sub class, for read only factor types
        /// </summary>
        /// <param name="ratioValue"></param>
        protected Factor (decimal ratioValue) : base()
        {
            m_ratioValue = ratioValue;
        }

		#endregion Constructors

		#region Properties

		/// <summary>
		/// The ID for the  factor used in the price calculation.
		/// </summary>
		public int FactorID
		{
			get
			{
				return this.m_factorID;
			}

			
			set
			{
				this.m_factorID = value;
			}
		}

		/// <summary>
		/// Flag indicating whether the  factor ID contains a valid value.
		/// </summary>
		public bool FactorIDSet
		{
			get
			{
				return this.m_factorID > 0;
			}
		}


		/// <summary>
		/// The base percent by which this factor affects the predicted price (in conjunction with any other variables).
		/// Is as a ratio relative to 1.
		/// </summary>
		public decimal RatioValue
		{
			get
			{
				return this.m_ratioValue;
			}
			set
			{
				this.m_ratioValue = value;
				this.setDirtyFlag();
			}
		}

		/// <summary>
		/// The base percent by which this factor affects the predicted price (in conjunction with any other variables).
		/// </summary>
		public decimal PercentValue
		{
			get
			{
				return this.m_ratioValue * 100;
			}
			set
			{
				this.m_ratioValue = value / 100;
				this.setDirtyFlag();
			}
		}

		/// <summary>
		/// The base percent by which this factor affects the predicted price (in conjunction with any other variables),
		/// once it has been authorised
		/// </summary>
		public decimal AuthorisedRatioValue
		{
			get
			{
				return this.m_authorisedRatioValue;
			}
			set
			{
				this.m_authorisedRatioValue = value;
				this.setDirtyFlag();
			}
		}

		/// <summary>
		/// The base percent by which this factor affects the predicted price (in conjunction with any other variables),
		/// once it has been authorised
		/// </summary>
		public decimal AuthorisedPercentValue
		{
			get
			{
				return this.m_authorisedRatioValue * 100;
			}
			set
			{
				this.m_authorisedRatioValue = value / 100;
				this.setDirtyFlag();
			}
		}

		/// <summary>
		/// The date that the factor becomes effective. Is DateTime.MinValue if not set (00:00:00.0000000, January 1, 0001)
		/// </summary>
		public DateTime EffectiveDate
		{
			get
			{
				return this.m_effectiveDate;
			}
			set
			{
				this.m_effectiveDate = value;
				this.setDirtyFlag();
			}
		}

		/// <summary>
		/// Flag indicating whether the  factor ID contains a valid value.
		/// </summary>
		public bool EffectiveDateSet
		{
			get
			{
				return this.m_effectiveDate != DateTime.MinValue && m_effectiveDate != DateTime.MaxValue;
			}
		}
		
		/// <summary>
		/// Used in Fund status property Grid, maybe ToString 
		/// could have been overriden but it looked like that 
		/// was used elsewhere for a different reason?
		/// </summary>
		public abstract string DisplayName {get;}	


		#endregion

		#region Private Methods

		/// <summary>
		/// Set the modified flag to be true
		/// </summary>
		protected void setDirtyFlag()
		{
			this.m_isDirty = true;
		}

		#endregion

		#region IFactor Members


		/// <summary>
		/// Is the default calculation
		/// </summary>
		/// <returns></returns>
		public virtual decimal CalculateEffect()
		{			
			return m_ratioValue;
		}

		/// <summary>
		/// Default validity check
		/// </summary>
		/// <returns></returns>
		public virtual bool IsValid()
		{
			//as discussed with Kevin 0 - 100% either way is reasonable validation here
			//percentage must be to 2dp, so means ratio max 4dp
			//after discussion with the business -ve factors disallowed

            /* 
             * UA246 05/10/2005 MISDC Finance MAW
             * Removed the validation that prevents negative factors as pre request by Alan Sutton
             * Change is to be applied to ALL factors
             * return m_ratioValue >= 0 && m_ratioValue <= 1 && m_ratioValue == decimal.Round(m_ratioValue,4);
            */
			return  m_ratioValue >= -1 && m_ratioValue <= 1 && m_ratioValue == decimal.Round(m_ratioValue,6);
		}

		/// <summary>
		/// Indicates to calling collection that this factor
		/// is used in SumFactors, i.e. is in the factor summation
		/// part of the movement calculation formula. Default is to include.
		/// </summary>
		/// <returns></returns>
		public virtual bool IsIncludedInSummation()
		{
			return true;
		}

		/// <summary>
		/// Indicates to caller that the property values constitute a valid factor,
		/// and that the factor hasn't just been created due to property access.
		/// i.e. this flag indicates whether any properties make it worth saving or not.
		/// </summary>
		/// <returns></returns>
		public virtual bool IsSet()
		{
			return m_ratioValue > 0 || m_effectiveDate != DateTime.MinValue;
		}

		/// <summary>
		/// Gets the effect today.
		/// </summary>
		/// <value></value>
		public virtual decimal effectToday
		{
			get { return m_ratioValue ;}
		}

		#endregion

	}
}
