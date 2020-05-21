using System;

namespace HBOS.FS.AMP.UPD.Types.Factors
{
	/// <summary>
	/// Summary description for XFactor.
	/// </summary>
	public class XFactor : Factor
	{
		#region Member Variables

		/// <summary>
		/// The xfactor description, describing what this percentage is that affects the price
		/// </summary>
		private string m_description;		

		#endregion

		#region Properties

		/// <summary>
		/// The xfactor description, describing what this percentage is that affects the price
		/// </summary>
		public string Description
		{
			get
			{
				return this.m_description;
			}
			set
			{
				this.m_description = value;
				this.setDirtyFlag();
			}
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
				return "X Factor";
			}
		}	
		#endregion

		#region Constructors

		/// <summary>
		/// Default constructor
		/// </summary>
		public XFactor() : base()
		{
		}

		/// <summary>
		/// Constructor that initialises member variables without setting dirty flag and also initialises factors
		/// </summary>
		/// <param name="ratioValue"></param>
		/// <param name="factorID"></param>
		/// <param name="effectiveDate"></param>
		/// <param name="description"></param>
		/// <param name="timestamp"></param>
		public XFactor (decimal ratioValue, int factorID, DateTime effectiveDate, string description, byte[] timestamp)
			: base (ratioValue, factorID, effectiveDate, timestamp)
		{
			m_description = description;
		}

		#endregion

		#region IFactor

		/// <summary>
		/// Default validity check - ensures XFactor >= 0
		/// </summary>
		/// <returns></returns>
		public override bool IsValid()
		{
			return base.IsValid() && (m_description == null || m_description.Length <= 1000) ;
		}

		/// <summary>
		/// Indicates to caller that the property values constitute a valid factor,
		/// and that the factor hasn't just been created due to property access.
		/// i.e. this flag indicates whether any properties make it worth saving or not.
		/// </summary>
		/// <returns></returns>
		public override bool IsSet()
		{
			return base.IsSet() || (m_description != null && m_description.Length > 0);
		}

	

		#endregion

	}
}