namespace HBOS.FS.AMP.UPD.Types.Funds
{
	/// <summary>
	/// Summary description for OEICFund.
	/// </summary>
	public class OEICFund : Fund
	{
		#region Local variables

		private bool m_isExDividend;
		private byte[] m_oeicTimestamp;

		#endregion

		#region Constructors

		/// <summary>
		/// Default constructor creating an object with all properties initialised to the default values.
		/// </summary>
		public OEICFund() : base()
		{
			this.m_isExDividend = false;
			this.m_oeicTimestamp = new byte[1];
		}

		/// <summary>
		/// Creates a new rehydratred <see cref="OEICFund"/> instance.
		/// </summary>
		public OEICFund(FundFactory.FundParameters fundParameters)
			: base(fundParameters)
		{
			this.m_isExDividend = fundParameters.IsExDividend;
			this.m_oeicTimestamp = fundParameters.Timestamp;
		}

		#endregion

		#region Properties

		/// <summary>
		/// The string representation of the type name used for display and reporting purposes
		/// </summary>
		public override string FundType
		{
			get { return "OEIC"; }
			//todo - do we need a set for the grid?
		}

		/// <summary>
		/// Flag to indicate whether the fund is within an ExDivdend period or not.
		/// </summary>
		public bool IsExDividend
		{
			get { return m_isExDividend; }

			set { m_isExDividend = value; }
		}

		/// <summary>
		/// The OEIC fund timestamp.
		/// </summary>
		public byte[] OeicTimestamp
		{
			get { return this.m_oeicTimestamp; }

			set { this.m_oeicTimestamp = value; }
		}

		#endregion

		#region Valuation Basis Factor aggregated properties

		/// <summary>
		/// The valuation basis effect value
		/// </summary>
		public override decimal ValuationBasisEffect
		{
			get { return 0.0m; }
		}

		//
		// TODO: remove during next refactor phase as this is no longer required
		//
		/// <summary>
		/// The ID for the Valuation Basis effect used in the price calculation.
		/// </summary>
		public override int ValuationBasisID
		{
			get { return 0; }
		}

		//
		// TODO: remove during next refactor phase as this is no longer required
		//
		/// <summary>
		/// Flag indicating whether the valuation baisis factor ID holds a valid value or not.
		/// </summary>
		public override bool ValuationBasisIDSet
		{
			get { return false; }
		}

		/// <summary>
		/// returns whether or not current value is valid
		/// </summary>
		/// <returns></returns>
		public bool ValuationBasisValid()
		{
			return this.m_factors.ValuationBasisFctr.IsValid();
		}

		#endregion
	}
}