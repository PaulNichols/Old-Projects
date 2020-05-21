using System;

namespace HBOS.FS.AMP.UPD.Types.Factors
{
	/// <summary>
	/// Summary description for TaxProvisionEstimate.
	/// This class just uses all defaults and facilities provided by the base class,
	/// but is used to distinguish as a type.
	/// </summary>
	public class TaxProvisionEstimate : Factor, IFactor	
	{
		#region Constructors

		/// <summary>
		/// Default constructor
		/// </summary>
		public TaxProvisionEstimate() : base()
		{
		}
		/// <summary>
		/// Constructor that initialises member variables without setting dirty flag and also initialises factors
		/// </summary>
		/// <param name="ratioValue"></param>
		/// <param name="factorID"></param>
		/// <param name="effectiveDate"></param>
		/// <param name="timestamp"></param>
		public TaxProvisionEstimate (decimal ratioValue, int factorID, DateTime effectiveDate, byte[] timestamp)
			: base (ratioValue, factorID, effectiveDate, timestamp)
		{
		}

		/// <summary>
		/// Default validity check - ensures XFactor >= 0
		/// </summary>
		/// <returns></returns>
		public override bool IsValid()
		{
			//todo - are -ve figures allowed?
			return base.IsValid();
		}

		#endregion

		#region Overrides

		/// <summary>
		/// Used in Fund status property Grid, maybe ToString 
		/// could have been overriden but it looked like that 
		/// was used elsewhere for a different reason?
		/// </summary>
		public override string DisplayName 
		{
			get
			{
				return "Tax Provision Estimate";
			}		
		}

		#region IFactor

		/// <summary>
		/// Calculates the effect of the TPE
		/// </summary>
		/// <returns></returns>
		public override decimal CalculateEffect()
		{
			return 1 - m_ratioValue;
		}

		/// <summary>
		/// Indicates to calling collection that this factor
		/// is used in SumFactors, i.e. is in the factor summation
		/// part of the movement calculation formula. 
		/// Overriden here as TPE is not in this part of the formula
		/// </summary>
		/// <returns></returns>
		public override bool IsIncludedInSummation()
		{
			return false;
		}

		

		#endregion

		#endregion	
	}
}