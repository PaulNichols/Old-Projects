using System;

namespace HBOS.FS.AMP.UPD.Types.Factors
{
	/// <summary>
	/// Summary description for ScalingFactor.
	/// This class currently just uses all defaults and facilities provided by the base class,
	/// but is used to distinguish as a type.
	/// </summary>
	public class ScalingFactor : Factor
	{
		/// <summary>
		/// Default constructor
		/// </summary>
		public ScalingFactor() : base()
		{
		}

		/// <summary>
		/// Constructor that initialises member variables without setting dirty flag and also initialises factors
		/// </summary>
		/// <param name="ratioValue"></param>
		/// <param name="factorID"></param>
		/// <param name="effectiveDate"></param>
		/// <param name="timestamp"></param>
		public ScalingFactor (decimal ratioValue, int factorID, DateTime effectiveDate, byte[] timestamp)
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

		/// <summary>
		/// Used in Fund status property Grid, maybe ToString 
		/// could have been overriden but it looked like that 
		/// was used elsewhere for a different reason?
		/// </summary>
		public override string DisplayName 
		{
			get
			{
				return "Scaling Factor";
			}
		}	

	

	}
}
