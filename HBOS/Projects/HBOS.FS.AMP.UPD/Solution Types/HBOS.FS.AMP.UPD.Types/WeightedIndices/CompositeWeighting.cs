using System;
using HBOS.FS.AMP.Entities;
using HBOS.FS.AMP.UPD.Types;
using System.ComponentModel;

namespace HBOS.FS.AMP.UPD.Types.WeightedIndices
{
	/// <summary>
	/// Summary description for CompositeWeighting.
	/// </summary>
	public class CompositeWeighting : EntityBase, IWeightedMovement
	{
		#region Constructors

		/// <summary>
		/// Constructs a composite weighting object
		/// </summary>
		public CompositeWeighting()
		{
			m_linkedFundCode = String.Empty;
			m_linkedFundMovement = 0M;
			m_proportion = 0M;
			m_isAuthorised = false;

			// Set up the IEntityBase members.
			m_isDirty = false;
			m_isNew = true; //default to true - most likely used via UI
			m_isDeleted = false;
			byte[] m_timeStamp = new byte[1];

		}

		/// <summary>
		/// Constructs a composite weighting object with params supplied
		/// </summary>
		/// <param name="linkedFundName"></param>
		/// <param name="linkedFundCode">The fund code of the underlying linked fund</param>
		/// <param name="linkedFundMovement">The movement of the linked fund as a percentage</param>
		/// <param name="proportion">The proportion that this linked fund is in relation to the other linked funds for the parent
		/// <param name="isAuthorised">Indicates if the price is authorised</param>
		/// composite fund, expressed as a ratio 0 - 1
		/// </param>
		public CompositeWeighting(string linkedFundName,string linkedFundCode, decimal linkedFundMovement, decimal proportion, bool isAuthorised)
		{
			m_linkedFundCode = linkedFundCode;
			m_linkedFundName = linkedFundName;
			m_linkedFundMovement = LinkedFundMovement;
			m_proportion = proportion;
			m_isAuthorised = isAuthorised;

			// Set up the IEntityBase members.
			m_isDirty = false;
			m_isNew = false; //default to false - most likely used via dB load
			m_isDeleted = false;
			byte[] m_timeStamp = new byte[1];

		}

		/// <summary>
		/// Constructs a composite weighting object with params supplied
		/// </summary>
		/// <param name="linkedFundCode">The fund code of the underlying linked fund</param>
		/// <param name="linkedFundMovement">The movement of the linked fund as a percentage</param>
		/// <param name="proportion">The proportion that this linked fund is in relation to the other linked funds for the parent
		/// <param name="isAuthorised">Indicates if the price is authorised</param>
		/// composite fund, expressed as a ratio 0 - 1
		/// </param>
		public CompositeWeighting(string linkedFundCode, decimal linkedFundMovement, decimal proportion, bool isAuthorised):this("",linkedFundCode,linkedFundMovement,proportion,isAuthorised)
		{
		}

		#endregion

		#region Member Variables

		private string m_linkedFundCode;
		private string m_linkedFundName;
		private decimal m_linkedFundMovement;
		private decimal m_proportion;
		private bool m_isAuthorised;


		#endregion

		#region CompositeWeighting properties

		/// <summary>
		/// Gets or sets the code for the underlying linked fund.
		/// </summary>
		/// <value>The linked fund code</value>
		public string LinkedFundCode
		{
			get
			{
				return m_linkedFundCode;
			}
			set 
			{
				m_linkedFundCode = value;
				SetDirtyFlag();
			}
		}

		/// <summary>
		/// Gets or sets the movement for the underlying linked fund in percentage terms.
		/// </summary>
		/// <value>The linked fund movement percentage</value>
		public decimal LinkedFundMovement
		{
			get
			{
				return m_linkedFundMovement;
			}
			set 
			{
				m_linkedFundMovement = value;
				SetDirtyFlag();
			}
		}

		/// <summary>
		/// Gets or sets the proportion for the underlying linked fund in respect to other linked funds for the parent Composite Asset Fund
		/// (as a ration between 0 and 1).
		/// </summary>
		/// <value>The proportion as a ratio 0 - 1</value>
		public decimal Proportion
		{
			get
			{
				return m_proportion;
			}
			set 
			{
				m_proportion = value;
				SetDirtyFlag();
			}
		}

		/// <summary>
		/// Gets a value indicating whether this price has been authorised.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is authorised; otherwise, <c>false</c>.
		/// </value>
		public bool IsAuthorised
		{
			get {return m_isAuthorised;}
		}

		#endregion

		#region IWeightedMovement 
		/// <summary>
		/// Calculates the value of a composite weighting in terms of underlying linked fund movement (in relation to its proportion)
		/// </summary>
		/// <returns></returns>
		public decimal CalculateMovement()
		{
			return  m_proportion * m_linkedFundMovement;
		}

		#endregion

		#region Display Methods

				
		/// <summary>
		/// Gets or sets the market movement.
		/// </summary>
		/// <value>The market movement.</value>
		public string MarketMovementDisplay
		{
			get { return this.LinkedFundMovement.ToString("p2"); }
		}

		/// <summary>
		/// This is a display property for the properties grid in Asset Fund Status
		/// </summary>
		public string MovementEffectDisplay
		{
			get { return CalculateMovement().ToString("p2"); }
		}

		/// <summary>
		/// This is a display property for the properties grid in Asset Fund Status
		/// </summary>
		public string DisplayName
		{
			get { return this.m_linkedFundName; }
		}

		/// <summary>
		/// returns the proportion in a format displayable by UI (and displays 'Unavailable' if not set
		/// </summary>
		public string ProportionDisplay
		{
			get
			{
				return DisplayFormat.Percent (m_proportion, m_proportion > 0);
			}
		}

		#endregion

	}
}
