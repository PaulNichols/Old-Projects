namespace HBOS.FS.AMP.UPD.Types.Funds
{
	/// <summary>
	/// A LinkedFund. Is a class in its own right in order to be able to distinguish by type.
	/// </summary>
	public class LinkedFund : NonOEIC
	{
		#region Constructors

		/// <summary>
		/// Default constructor for the linked fund
		/// </summary>
		public LinkedFund() : base()
		{
		}


		/// <summary>
		/// Creates a new linked fund
		/// </summary>
		/// <param name="fundParameters">Object containing a necessary fund details</param>
		public LinkedFund(FundFactory.FundParameters fundParameters)
			: base(fundParameters)
		{
		}

		#endregion

		/// <summary>
		/// The string representation of the type name used for display and reporting purposes
		/// </summary>
		public override string FundType
		{
			get { return "Linked"; }
			//todo - do we need a set for the grid?
		}

	}
}