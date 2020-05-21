using HBOS.FS.AMP.UPD.Types.BenchMark;
using HBOS.FS.AMP.UPD.Types.Funds;

namespace HBOS.FS.AMP.UPD.Types.AssetFunds
{
	/// <summary>
	/// Summary description for AssetFundConstituent.
	/// </summary>
	public class AssetMovementConstituent: EntityBase
	{
		private  AssetFund parent;
		private  decimal proportion;
		private readonly IBenchMark benchMark;


		
		/// <summary>
		/// Gets the parent asset fund.
		/// </summary>
		/// <value></value>
		public AssetFund ParentAssetFund
		{
			get { return parent; }
			set {  parent=value; }
		}

		/// <summary>
		/// Gets the proportion this benchmark is of the asset fund split.
		/// </summary>
		/// <value></value>
		public decimal Proportion
		{
			get
			{return proportion;}
			set
			{
				if (value<0 || value>1)
				{
					throw new HBOS.FS.AMP.UPD.Exceptions.ConstraintViolationException();
				}
				proportion=value;
				this.IsDirty=true;
			}
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
		/// Calculates the  effect (movement*proportion).
		/// </summary>
		/// <returns></returns>
		public decimal CalculateEffect()
		{
			return CalculateMovement()*Proportion;
		}

		/// <summary>
		/// Calculates the movement.
		/// </summary>
		/// <returns></returns>
		public decimal CalculateMovement()
		{
			return TotalMovement(CurrencyMovement());
		}

		/// <summary>
		/// Totals the movement.
		/// </summary>
		/// <param name="currencyMovement">Currency movement.</param>
		/// <returns></returns>
		public decimal TotalMovement(decimal currencyMovement)
		{
			return  BenchMark.Movement+ currencyMovement;
		}

		/// <summary>
		/// Works out the movement.
		/// </summary>
		/// <returns></returns>
		public decimal CurrencyMovement()
		{
			decimal currencyMovement=0;
			if (ParentAssetFund!=null) currencyMovement=Currency.Currency.CalculateMovement(BenchMark.Currency,ParentAssetFund.Currency);
			return currencyMovement;
		}
	
		/// <summary>
		/// The previous conversion rate.
		/// </summary>
		/// <returns></returns>
		public decimal PreviousConversionRate()
		{
			decimal previousConversionRate=0;
			if (ParentAssetFund!=null) previousConversionRate=Currency.Currency.PreviousConversionRate(BenchMark.Currency,ParentAssetFund.Currency);
			return previousConversionRate;

		}

		/// <summary>
		/// The current conversion rate.
		/// </summary>
		/// <returns></returns>
		public decimal CurrentConversionRate()
		{
			decimal currentConversionRate=0;
			if (ParentAssetFund!=null) currentConversionRate=Currency.Currency.TodaysConversionRate(BenchMark.Currency,ParentAssetFund.Currency);
			return currentConversionRate;
		}

		/// <summary>
		/// Creates a new <see cref="AssetMovementConstituent"/> instance.
		/// </summary>
		/// <param name="proportion">Proportion.</param>
		/// <param name="benchMark">Bench mark.</param>
		public AssetMovementConstituent(decimal proportion, IBenchMark benchMark)
		{
			this.parent = null;
			this.proportion = proportion;
			this.benchMark = benchMark;

			IsDirty   = false;
			IsDeleted = false;
			IsNew     = false;
		}


		/// <summary>
		/// Override the Equals as we are only worried if the indexname is the same and not the actual objects
		/// </summary>
		/// <param name="obj"></param>
		/// <returns>true is they have the same index name</returns>
		public override bool Equals(object obj)
		{
			if (obj is AssetMovementConstituent)
			{
				AssetMovementConstituent constitute =  obj as AssetMovementConstituent;
				return (this==constitute);
			}
			else
			{
				return base.Equals(obj);
			}
		}

		/// <summary>
		/// Overloaded equality operator
		/// </summary>
		/// <param name="lhs">First Object to compare</param>
		/// <param name="rhs">Second Object to compare</param>
		/// <returns></returns>
		public static bool operator==(AssetMovementConstituent  lhs,AssetMovementConstituent  rhs)
		{
			bool returnValue=false;
			if ((object)lhs !=null && (object)rhs!=null  && (object)lhs.BenchMark !=null && (object)rhs.BenchMark!=null )
			{
				if (lhs.BenchMark is Fund && rhs.BenchMark is Fund)
				{
					returnValue= ((Fund)lhs.BenchMark==(Fund)rhs.BenchMark);
				}
				else if (lhs.BenchMark is StockMarketIndex.StockMarketIndex && rhs.BenchMark is StockMarketIndex.StockMarketIndex)
				{
					returnValue= ((StockMarketIndex.StockMarketIndex)lhs.BenchMark==(StockMarketIndex.StockMarketIndex)rhs.BenchMark);
				}
			}
			else
			{
				returnValue= (object)lhs==(object)rhs;
			}
			return returnValue;
		}

		/// <summary>
		/// Overloaded inequality operator
		/// </summary>
		/// <param name="lhs">First Object to compare</param>
		/// <param name="rhs">Second Object to compare</param>
		/// <returns></returns>
		public static bool operator!=(AssetMovementConstituent lhs,AssetMovementConstituent  rhs)
		{
			return !(lhs==rhs);
		}


		/// <summary>
		/// Gets the hash code. Overridden to call the base implementation (????)
		/// </summary>
		/// <returns></returns>
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
}