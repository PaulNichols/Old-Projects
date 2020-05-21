using System;
using System.Collections;

namespace HBOS.FS.AMP.UPD.Types.Factors
{
	/// <summary>
	/// Summary description for RevaluationFactor.
	/// </summary>
	public class RevaluationFactor : Factor
	{
		#region Member Variables

		private DateTime m_endDate = DateTime.MinValue;
		private Hashtable m_holidays = null;
		private DateTime m_currentValuationDate;

		#endregion

		#region Properties

		/// <summary>
		/// The base percent by which this factor affects the predicted price (in conjunction with any other variables).
		/// </summary>
		public DateTime EndDate
		{
			get { return this.m_endDate; }
			set
			{
				this.m_endDate = value;
				this.setDirtyFlag();

			}
		}

		/// <summary>
		/// Has the end date been set?
		/// </summary>
		public bool EndDateSet
		{
			get { return this.m_endDate != DateTime.MinValue && this.m_endDate != DateTime.MaxValue; }
		}

		/// <summary>
		/// Used in Fund status property Grid, maybe ToString 
		/// could have been overriden but it looked like that 
		/// was used elsewhere for a different reason?
		/// </summary>
		public override string DisplayName
		{
			get { return "Revaluation Factor"; }
		}

		/// <summary>
		/// the collection of holiday dates required for validation
		/// </summary>
		public Hashtable Holidays
		{
			get { return m_holidays; }
			set { m_holidays = value; }
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Creates a new factor and initialises factor name
		/// </summary>
		public RevaluationFactor() : base()
		{
		}

		/// <summary>
		/// Constructor that initialises member variables without setting dirty flag and also initialises factors
		/// </summary>
		/// <param name="ratioValue"></param>
		/// <param name="factorID"></param>
		/// <param name="effectiveDate"></param>
		/// <param name="endDate"></param>
		/// <param name="timestamp"></param>
		/// <param name="currentValuationDate"></param>
		public RevaluationFactor(decimal ratioValue, int factorID, DateTime effectiveDate, DateTime endDate, byte[] timestamp,DateTime currentValuationDate)
			: base(ratioValue, factorID, effectiveDate, timestamp)
		{
			m_endDate = endDate;
			m_currentValuationDate=currentValuationDate;
		}

		#endregion

		#region IFactor Members

		/// <summary>
		/// Calculates the effect of the Revaulation Factor on the unit price. Overrides default implementation
		/// </summary>
		/// <returns></returns>
		public override decimal CalculateEffect()
		{
			if (m_endDate == DateTime.MinValue || m_effectiveDate == DateTime.MinValue)
			{
				return 0M;
			}
			
			return effectToday;
		}

		/// <summary>
		/// Default validity check - ensures XFactor >= 0
		/// </summary>
		/// <returns></returns>
		public override bool IsValid()
		{
			if (m_holidays == null)
			{
				throw new ArgumentException("Static list of holidays required in order to be able to ascertain validity");
			}

			//must be at least tomorrow
			return base.IsValid() &&
				(m_ratioValue == 0 ||
					(m_endDate != DateTime.MinValue && m_effectiveDate != DateTime.MinValue &&
						//m_endDate.Date > m_effectiveDate.Date && m_endDate.Date > DateTime.Now.Date &&
						m_endDate.DayOfWeek != DayOfWeek.Saturday && m_endDate.DayOfWeek != DayOfWeek.Sunday &&
						(!m_holidays.ContainsKey(m_endDate.Date))
						)
					);
		}

		/// <summary>
		/// Indicates to caller that the property values constitute a valid factor,
		/// and that the factor hasn't just been created due to property access.
		/// i.e. this flag indicates whether any properties make it worth saving or not.
		/// </summary>
		/// <returns></returns>
		public override bool IsSet()
		{
			return base.IsSet() || m_endDate != DateTime.MinValue;
		}

		/// <summary>
		/// Gets the effect today.
		/// </summary>
		/// <value></value>
		public override decimal effectToday
		{
			get
			{
				TimeSpan elapsedDays = m_currentValuationDate.Subtract(m_effectiveDate.Date);
				TimeSpan totalDays = m_endDate.Date.Subtract(m_effectiveDate.Date);

				// Start day and end day count, so add 1 to get the number of elapsed and total days
				int numElapsedDays = elapsedDays.Days + 1;
				int numTotalDays = totalDays.Days + 1;

				return (numElapsedDays*this.RatioValue)/numTotalDays;
			}
		}

		#endregion
	}
}