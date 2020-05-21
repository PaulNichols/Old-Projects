using System;
using System.Collections;

namespace HBOS.FS.AMP.UPD.Types.Funds
{
	/// <summary>
	/// This class holds the commonanality between non OEIC funds
	/// </summary>
	public abstract class NonOEIC : Fund
	{
		/// <summary>
		/// Default constructor for the linked fund
		/// </summary>
		public NonOEIC() : base()
		{
		}

		/// <summary>
		/// Creates a new linked fund
		/// </summary>
		/// <param name="fundParameters">Object containing a necessary fund details</param>
		public NonOEIC(FundFactory.FundParameters fundParameters)
			: base(fundParameters)
		{
			this.m_useMidPriceAsBidPrice = fundParameters.UseMidPriceAsBidPrice;
			this.m_isDualPrice = fundParameters.IsDualPrice;
			this.m_isLife = fundParameters.IsLife;

		}


		private bool m_useMidPriceAsBidPrice = false;
		private bool m_isDualPrice = false;
		private bool m_isLife = false;


		/// <summary>
		/// The price type (eg. single (S) or dual (D)).
		/// </summary>
		public bool IsDualPrice
		{
			get { return this.m_isDualPrice; }

			set
			{
				this.m_isDualPrice = value;
				this.setDirtyFlag();
			}
		}

		/// <summary>
		/// Flag indicating whether the mid price should be used as the bid price or not.
		/// </summary>
		public bool UseMidPriceAsBidPrice
		{
			get { return this.m_useMidPriceAsBidPrice; }

			set
			{
				this.m_useMidPriceAsBidPrice = value;
				this.setDirtyFlag();
			}
		}


		/// <summary>
		/// Flag indicating whether the fund can be used for life polices.
		/// </summary>
		public bool IsLife
		{
			get { return this.m_isLife; }

			set
			{
				this.m_isLife = value;
				this.setDirtyFlag();
			}
		}

		/// <summary>
		/// Gets the unique db id for the revaluation factor
		/// </summary>
		public int RevaluationFactorID
		{
			get { return m_factors.RevaluationFctr.FactorID; }

			set { m_factors.RevaluationFctr.FactorID = value; }

		}

		/// <summary>
		/// Flag to indicate whether the revaluation change has been set because the default may be a
		/// valid value.
		/// </summary>
		public bool RevaluationFactorIDSet
		{
			get { return m_factors.RevaluationFctr.FactorIDSet; }
		}

		/// <summary>
		/// The start date of the revaluation period.
		/// </summary>
		public DateTime RevaluationEffectiveDate
		{
			get { return m_factors.RevaluationFctr.EffectiveDate; }

			set
			{
				m_factors.RevaluationFctr.EffectiveDate = value;
				this.setDirtyFlag();
			}
		}

		/// <summary>
		/// Flag to indicate whether the revaluation effective date has been set because the default
		/// may be a valid value.
		/// </summary>
		public bool RevaluationEffectiveDateSet
		{
			get { return m_factors.RevaluationFctr.EffectiveDateSet; }
		}

		/// <summary>
		/// The number of days that the revaluation period runs for.
		/// </summary>
		public DateTime RevaluationEndDate
		{
			get { return m_factors.RevaluationFctr.EndDate; }

			set
			{
				m_factors.RevaluationFctr.EndDate = value;
				this.setDirtyFlag();
			}
		}

		/// <summary>
		/// Flag to indicate whether the revaluation working days have been set because the default
		/// may be a valid value.
		/// </summary>
		public bool RevaluationEndDateSet
		{
			get { return m_factors.RevaluationFctr.EndDateSet; }
		}

		/// <summary>
		/// The calculated revaluation factor used in price prediction.
		/// </summary>
		public decimal RevalFactor
		{
			get { return this.m_factors.RevaluationFctr.RatioValue; }

			set
			{
				//todo - does grid cause this to be set & therefore dirty flag to be set unnecessarily
				this.m_factors.RevaluationFctr.RatioValue = value;
				this.setDirtyFlag();
			}
		}

		/// <summary>
		/// The revaluation factor as a percentage, rather than a ratio(factor) of 1
		/// </summary>
		public decimal RevalFactorPercent
		{
			get { return this.m_factors.RevaluationFctr.PercentValue; }

			set
			{
				this.m_factors.RevaluationFctr.PercentValue = value;
				this.setDirtyFlag();
			}
		}

		/// <summary>
		/// returns whether or not current reval factor is valid
		/// </summary>
		/// <returns></returns>
		public bool RevaluationFactorValid()
		{
			return this.m_factors.RevaluationFctr.IsValid();
		}

		/// <summary>
		/// gets / sets the list of bank holidays, which is required for validation purposes.
		/// Only sets them on reval factor at present as others have no need.
		/// </summary>
		public Hashtable Holidays
		{
			get { return this.m_factors.RevaluationFctr.Holidays; }
			set { m_factors.RevaluationFctr.Holidays = value; }
		}

		/// <summary>
		/// The scaling factor used for this fund in price prediction calculations.
		/// </summary>
		public decimal ScaleFactor
		{
			get { return this.m_factors.ScalingFctr.RatioValue; }

			set
			{
				this.m_factors.ScalingFctr.RatioValue = value;
				this.setDirtyFlag();
			}
		}

		/// <summary>
		/// The scaling factor as a percentage, rather than a ratio(factor) of 1
		/// </summary>
		public decimal ScaleFactorPercent
		{
			get { return this.m_factors.ScalingFctr.PercentValue; }

			set
			{
				this.m_factors.ScalingFctr.PercentValue = value;
				this.setDirtyFlag();
			}
		}

		/// <summary>
		/// The ID for the scaling factor used in the price calculation.
		/// </summary>
		public int ScalingFactorID
		{
			get { return this.m_factors.ScalingFctr.FactorID; }


			set { this.m_factors.ScalingFctr.FactorID = value; }
		}

		/// <summary>
		/// Flag to indicate whether the scaling factor ID is a valid value or not.
		/// </summary>
		public bool ScalingFactorIDSet
		{
			get { return this.m_factors.ScalingFctr.FactorIDSet; }

		}

		/// <summary>
		/// returns whether or not current scaling factor is valid
		/// </summary>
		/// <returns></returns>
		public bool ScalingFactorValid()
		{
			return this.m_factors.ScalingFctr.IsValid();
		}

		/// <summary>
		/// The tax provision estimate
		/// </summary>
		public decimal TPE
		{
			get { return this.m_factors.TPE.RatioValue; }

			set
			{
				this.m_factors.TPE.RatioValue = value;
				this.setDirtyFlag();
			}
		}

		/// <summary>
		/// The tax provision estimate as a percentage, rather than a ratio(factor) of 1
		/// </summary>
		public decimal TPEPercent
		{
			get { return this.m_factors.TPE.PercentValue; }

			set
			{
				this.m_factors.TPE.PercentValue = value;
				this.setDirtyFlag();
			}
		}

		/// <summary>
		/// The ID for the tax provision estimate used in the price calculation.
		/// </summary>
		public int TaxProvisionFactorID
		{
			get { return this.m_factors.TPE.FactorID; }


			set { this.m_factors.TPE.FactorID = value; }
		}

		/// <summary>
		/// Flag indicating whether the tax provision estimate ID holds a valid value or not.
		/// </summary>
		private bool TaxProvisionFactorIDSet
		{
			get { return this.m_factors.TPE.FactorIDSet; }

		}

		/// <summary>
		/// returns whether or not current tax provision estimate is valid
		/// </summary>
		/// <returns></returns>
		public bool TPEValid()
		{
			return this.m_factors.TPE.IsValid();
		}

		/// <summary>
		/// The formatted field used to display the Tax Provision Estimate.
		/// </summary>
		public string TPEDisplay
		{
			get { return DisplayFormat.Percent(this.TPE, (this.TaxProvisionFactorIDSet)); }
		}

		/// <summary>
		/// Displays a Y or N depending on whether the fund is a life fund
		/// </summary>
		public string IsLifeDisplay
		{
			get { return this.IsLife ? "Y" : "N"; }
		}


		/// <summary>
		/// Calculates the predicted price (is a derived property)
		/// </summary>
		public override decimal PredictedPrice
		{
			get
			{
				//TPE effect is the only thing that differs between Composite pred price
				//and base predicted price. 
				//This is catered for in predictedMovemnt, therefore
				//we can just call the base here.

				//Predicted Price Today = 
				//	Previous day's autorised price * (1 + predictedMovement)
				//		where predictedMovement = (Asset Movement + Sum of Factors (excluding TPE)) * (1 - TPE)	

				return base.PredictedPrice;
			}


			set
			{
				//do nothing - it's a derived value. This is left in for the grid
			}
		}

		/// <summary>
		/// calculates the predicted movement to be applied to the price in order to calculate predicted price
		/// </summary>
		protected override decimal predictedMovement
		{
			get { return base.predictedMovement*m_factors.TPE.CalculateEffect(); }
		}
	}
}