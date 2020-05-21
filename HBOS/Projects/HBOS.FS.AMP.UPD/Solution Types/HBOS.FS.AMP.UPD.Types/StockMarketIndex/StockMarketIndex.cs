using HBOS.FS.AMP.UPD.Types.BenchMark;

namespace HBOS.FS.AMP.UPD.Types.StockMarketIndex
{
	/// <summary>
	/// Summary description for StockMarketIndex.
	/// </summary>
	public class StockMarketIndex : EntityBase,IBenchMark
	{
		private string marketName;
		private readonly int marketIndexId;
		private readonly object currentValue;
		private readonly object previousValue;
		private readonly Currency.Currency m_Currency;
		private string m_CountryCode;

		/// <summary>
		/// Creates a new <see cref="StockMarketIndex"/> instance.
		/// </summary>
		public StockMarketIndex(int marketIndexId, string marketName, object currentValue,
			object previousValue,Currency.Currency currency)
		{
			this.marketName = marketName;
			this.marketIndexId = marketIndexId;
			this.currentValue = currentValue;
			this.previousValue = previousValue;
			this.m_Currency = currency;


			IsDeleted = false;
			IsNew     = false;
			IsDirty   = false;
		}

		/// <summary>
		/// Creates a new <see cref="StockMarketIndex"/> instance.
		/// </summary>
		public StockMarketIndex(int marketIndexId, string marketName,Currency.Currency currency,string countryCode)
		{
			this.marketName = marketName;
			this.marketIndexId = marketIndexId;
			this.m_Currency = currency;
			this.m_CountryCode=countryCode;

			IsDeleted = false;
			IsNew     = false;
			IsDirty   = false;
		}

		#region IBenchMark implementation

		/// <summary>
		/// Gets the Benchmark movement.
		/// </summary>
		/// <value></value>
		public decimal Movement
		{
			get
			{
				if (CurrentValue != 0m && PreviousValue != 0m)
				{
					return (CurrentValue - PreviousValue)/PreviousValue;
				}
				else
				{
					return 0m;
				}
			}	
		}

		/// <summary>
		/// Gets the Bench Mark currency.
		/// </summary>
		/// <value></value>
		public Currency.Currency Currency
		{
			get { return m_Currency; }
		}

		/// <summary>
		/// Gets the state of availability for the Bench Mark.
		/// </summary>
		/// <value></value>
		public BenchMarkAvailabilityState Availability
		{
			//based on having current and previous imported value
			get
			{	
				BenchMarkAvailabilityState returnValue=BenchMarkAvailabilityState.Unavailable;
				if (CurrentValue == 0m || PreviousValue == 0m) 
				{
					returnValue= BenchMarkAvailabilityState.AvailableWithWarnings;
				}
				else if (this.Currency!=null && (this.Currency.CurrentRate== 0m || this.Currency.PreviousRate == 0m)) 
				{
					returnValue=BenchMarkAvailabilityState.AvailableWithWarnings;
				}
				else
				{
					returnValue=BenchMarkAvailabilityState.Available;
				}
				return returnValue;
			}
		}

		/// <summary>
		/// Toes the string.
		/// </summary>
		/// <returns></returns>
		public string MarketName
		{
			get
			{
				return marketName;				
			}
		}

		/// <summary>
		/// Gets the bench mark type.
		/// </summary>
		/// <value></value>
		public string BenchMarkType
		{
			get { return "Stock Market Index"; }
		}

		/// <summary>
		/// Gets the bench mark sub type.
		/// </summary>
		/// <value></value>
		public string BenchMarkSubType
		{
			get { return null; }
		}

		/// <summary>
		/// Gets the previous benchmark value.
		/// </summary>
		/// <value></value>
		public decimal PreviousBenchmarkValue
		{
			get { return PreviousValue; }
		}

		/// <summary>
		/// Gets the current benchmark value.
		/// </summary>
		/// <value></value>
		public decimal CurrentBenchmarkValue
		{
			get { return CurrentValue; }
		}

		/// <summary>
		/// Gets the market index unique id.
		/// </summary>
		/// <value></value>
		public int MarketIndexId
		{
			get { return marketIndexId; }
		}

		#endregion


		/// <summary>
		/// Override the Equals as we are only worried if the indexname is the same and not the actual objects
		/// </summary>
		/// <param name="obj"></param>
		/// <returns>true is they have the same index name</returns>
		public override bool Equals(object obj)
		{
			if (obj is StockMarketIndex)
			{
				StockMarketIndex stockMarket =  obj as StockMarketIndex;
				return (this==stockMarket);
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
		public static bool operator==(StockMarketIndex  lhs,StockMarketIndex  rhs)
		{
			if ((object)lhs !=null && (object)rhs!=null )
			{
				return (lhs.MarketIndexId==rhs.MarketIndexId);
			}
			else
			{
				return (object)lhs==(object)rhs;
			}
		}

		/// <summary>
		/// Overloaded inequality operator
		/// </summary>
		/// <param name="lhs">First Object to compare</param>
		/// <param name="rhs">Second Object to compare</param>
		/// <returns></returns>
		public static bool operator!=(StockMarketIndex lhs,StockMarketIndex  rhs)
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

		/// <summary>
		/// Gets the current value of the market.
		/// </summary>
		/// <value></value>
		public decimal CurrentValue
		{
			get
			{
				return currentValue == null ? 0 : (decimal) currentValue;
				;
			}
		}

		/// <summary>
		/// Gets the previous value of the market.
		/// </summary>
		/// <value></value>
		public decimal PreviousValue
		{
			get
			{
				return previousValue == null ? 0 : (decimal) previousValue;
				;
			}
		}

		/// <summary>
		/// Gets the country code.
		/// </summary>
		/// <value></value>
		public string CountryCode
		{
			get { return m_CountryCode==null?"":m_CountryCode; }
		}
	}
}