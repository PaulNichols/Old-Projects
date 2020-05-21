using System;

namespace HBOS.FS.AMP.UPD.Types.WeightedIndices
{
	/// <summary>
	/// Used to calculate the movement in a generic fashion for a 
	/// WeightedIndex (for OEIC or Linked Asset Funds) or for a CompositeWeightedIndex
	/// (for CompositeAssetFunds). Or of course for any future weightings that would have movement
	/// </summary>
	public interface IWeightedMovement
	{
		/// <summary>
		/// Implement this for custom calculation of movement of a weighting as a percentile
		/// factoring in its proportion
		/// </summary>
		/// <returns>The movement of a weighting in percentage terms, with proportion factored in</returns>
		decimal CalculateMovement();
	}
}
