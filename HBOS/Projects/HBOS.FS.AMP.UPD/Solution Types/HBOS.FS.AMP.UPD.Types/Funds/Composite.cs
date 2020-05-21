using HBOS.FS.AMP.UPD.Types.BenchMark;

namespace HBOS.FS.AMP.UPD.Types.Funds
{
	/// <summary>
	/// Summary description for Composite.
	/// </summary>
	public class Composite : NonOEIC
	{
		#region Constructors

		/// <summary>
		/// Default constructor
		/// </summary>
		public Composite() : base()
		{
		}

		/// <summary>
		/// Creates a new Composite 
		/// </summary>
		public Composite(FundFactory.FundParameters fundParameters)
			: base(fundParameters)
		{
		}

		#endregion

		#region IBenchMark implementation

		/// <summary>
		/// Gets the state of availability for the Bench Mark.
		/// </summary>
		/// <value></value>
		public override BenchMarkAvailabilityState Availability
		{
			get { return BenchMarkAvailabilityState.Unavailable; }
		}

		#endregion

		#region Composite specific properties

		/// <summary>
		/// The string representation of the type name used for display and reporting purposes
		/// </summary>
		public override string FundType
		{
			get { return "Composite"; }
		}

		#endregion
	}
}