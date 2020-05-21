namespace HBOS.FS.AMP.UPD.Types.BenchMark
{
	/// <summary>
	/// Enumeration of the state of avalibility of this enum, 
	/// this (for example) will depend on Authorisation state if the benchmark is a fund.
	/// </summary>
	public enum BenchMarkAvailabilityState :int
	{
		/// <summary>
		/// </summary>
		Available = 0,
		/// <summary>
		/// </summary>
		AvailableWithWarnings = 1,
		/// <summary>
		/// </summary>
		Unavailable = 2,
	}

	/// <summary>
	/// An interface to be implemented by all type which Asset Funds can be Benchmarked against.
	/// </summary>
	public interface IBenchMark
	{
		/// <summary>
		/// Gets the Bench Mark movement.
		/// </summary>
		/// <value></value>
		decimal Movement { get; }

		/// <summary>
		/// Gets the Bench Mark currency.
		/// </summary>
		/// <value></value>
		Currency.Currency Currency { get; }

		/// <summary>
		/// Gets the state of availability for the Bench Mark.
		/// </summary>
		/// <value></value>
		BenchMarkAvailabilityState Availability { get; }

		/// <summary>
		/// Gets the bench mark type.
		/// </summary>
		/// <value></value>
		string  BenchMarkType{ get; }

		/// <summary>
		/// Gets the bench mark sub type.
		/// </summary>
		/// <value></value>
		string  BenchMarkSubType{ get; }

		/// <summary>
		/// Gets the previous benchmark value.
		/// </summary>
		/// <value></value>
		decimal PreviousBenchmarkValue { get; }

		/// <summary>
		/// Gets the current benchmark value.
		/// </summary>
		/// <value></value>
		decimal CurrentBenchmarkValue { get; }
	}
}