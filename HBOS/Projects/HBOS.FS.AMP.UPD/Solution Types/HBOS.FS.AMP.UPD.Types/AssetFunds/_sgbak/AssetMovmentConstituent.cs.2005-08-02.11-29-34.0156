using System;
using HBOS.FS.AMP.UPD.Types.BenchMark;

namespace HBOS.FS.AMP.UPD.Types.AssetFunds
{
	/// <summary>
	/// Summary description for AssetFundConstituent.
	/// </summary>
	public class AssetMovmentConstituent
	{
		private readonly AssetFund parent;
		private readonly decimal proportion;
		private readonly IBenchMark benchMark;


		/// <summary>
		/// Gets the parent asset fund.
		/// </summary>
		/// <value></value>
		public AssetFund ParentAssetFund
		{
			get { return parent; }
		}

		/// <summary>
		/// Gets the proportion this benchmark is of the asset fund split.
		/// </summary>
		/// <value></value>
		public decimal Proportion
		{
			get { return proportion; }
		}

		/// <summary>
		/// Gets the bench mark.
		/// </summary>
		/// <value></value>
		public IBenchMark BenchMark
		{
			get { return benchMark; }
		}


		/// <summary>
		/// Calculates the movement.
		/// </summary>
		/// <returns></returns>
		public decimal CalculateMovement()
		{
			throw new NotImplementedException();
			// Switch the sign of the currency rate movement to reflect the move from
			// the perspective of the local currency.
			//return  (m_proportion * m_marketMovement) + (m_proportion * (-1 * m_currencyMovement));
		}

		/// <summary>
		/// Creates a new <see cref="AssetMovmentConstituent"/> instance.
		/// </summary>
		/// <param name="parent">Parent.</param>
		/// <param name="proportion">Proportion.</param>
		/// <param name="benchMark">Bench mark.</param>
		public AssetMovmentConstituent(AssetFund parent, decimal proportion, IBenchMark benchMark)
		{
			this.parent = parent;
			this.proportion = proportion;
			this.benchMark = benchMark;
		}
	}
}