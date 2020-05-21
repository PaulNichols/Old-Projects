using System;

namespace HBOS.FS.AMP.UPD.Types.Factors
{
	/// <summary>
	/// The interface to be implemented by a factor in order for its effect to be calculated generically
	/// </summary>
	public interface IFactor
	{
		/// <summary>
		/// Calculates the effect of this factor on the predicted price
		/// using properties that have been set through the concrete class interface
		/// </summary>
		/// <returns></returns>
		decimal CalculateEffect();

		/// <summary>
		/// Indicates that the factor data is valid and ok to save (or use in calculations)
		/// </summary>
		/// <returns></returns>
		bool IsValid();

		/// <summary>
		/// Indicates to calling collection that this factor
		/// is used in SumFactors, i.e. is in the factor summation
		/// part of the movement calculation formula
		/// </summary>
		/// <returns></returns>
		bool IsIncludedInSummation();

		/// <summary>
		/// Indicates to caller that the property values constitute a valid factor,
		/// and that the factor hasn't just been created due to property access.
		/// i.e. this flag indicates whether any properties make it worth saving or not.
		/// </summary>
		/// <returns></returns>
		bool IsSet();

		
		/// <summary>
		/// Gets the effect today.
		/// </summary>
		/// <value></value>
		decimal effectToday{get;}

	}
}
