using System;

using HBOS.FS.AMP.Entities;
using HBOS.FS.AMP.UPD.Types;
using System.ComponentModel;

namespace HBOS.FS.AMP.UPD.Types.WeightedIndices
{
	/// <summary>
	/// Object to hold the market value split items as an asset fund weighted index
	/// </summary>
	public class WeightedIndex : EntityBase, IWeightedMovement
	{
		#region Member variables
		//
		// Data fields
		//
		private int m_marketIndexID;
		private string m_assetFundCode;
		private string m_countryCode;
		private string m_indexName;
		private string m_currencyCode;
		private decimal m_proportion;

		private decimal m_marketMovement;
		private long m_importedIndexValueImportID; //note: long = Int64
		private decimal m_currencyMovement;
		private long m_currencyRatesImportID;
		private string m_country;
		private long m_importID; //note: long = Int64
		private bool m_fromAuthorisedAssetFundDetails;


		//Sets
		private bool m_marketMovementSet;
		private bool m_currencyMovementSet;
		private bool m_proportionSet;

		#endregion

		#region WeightedIndex constructor

		/// <summary>
		/// Default constructor, used for creating a 'new' WeightedIndex()
		/// </summary>
		public WeightedIndex()
		{
			// Set attributes to empty
			m_marketIndexID = 0;
			m_indexName = string.Empty;
			m_importedIndexValueImportID = 0;
			m_countryCode = string.Empty;
			m_currencyCode = string.Empty;
			m_proportion = 0;           
			m_proportionSet = false;
			m_marketMovement = 0;
			m_marketMovementSet = false;
			m_importedIndexValueImportID = 0;
			m_currencyMovement = 0;
			m_currencyMovementSet = false;
			m_currencyRatesImportID = 0;
			m_country = string.Empty;
			m_importID = 0;
			m_fromAuthorisedAssetFundDetails = false;

			// Set up the IEntityBase members.
			m_isDirty = false;
			m_isNew = true;
			m_isDeleted = false;
			byte[] m_timeStamp = new byte[1];
		}
        
		/// <summary>
		/// Overrloaded constructor, used for populating from the DB
		/// </summary>
		public WeightedIndex(int marketIndexID, string assetFundCode, string indexName, decimal marketMovement
			, bool marketMovementSet, long importedIndexValueImportID, decimal currencyMovement, bool currencyMovementSet, long currencyRatesImportID
			, string countryCode, string currencyCode, string country, decimal proportion, bool proportionSet
			, long importID, bool fromAuthorisedAssetFundDetails, byte[] timeStamp)

		{
			// Populate attributes with values
			this.m_assetFundCode = assetFundCode;
			this.m_marketIndexID = marketIndexID;
			this.m_indexName = indexName;
			this.m_marketMovement = marketMovement;
			this.m_marketMovementSet = false;
			this.m_importedIndexValueImportID = importedIndexValueImportID;
			this.m_currencyMovement = currencyMovement;
			this.m_currencyMovementSet = false;
			this.m_currencyRatesImportID = currencyRatesImportID;
			this.m_countryCode = countryCode;
			this.m_currencyCode = currencyCode;
			this.m_country = country;
			this.m_proportion = proportion;
			this.m_proportionSet = proportionSet;
			this.m_importID = importID;
			this.m_fromAuthorisedAssetFundDetails = fromAuthorisedAssetFundDetails;

			this.IsDirty = false;
			this.IsNew = false;
			this.IsDeleted = false;
			this.TimeStamp = timeStamp;
		}


		#endregion

		#region IWeightedMovement 
		/// <summary>
		/// Calculates the value of a weighted index in terms of market and currency movement (in relation to its proportion)
		/// </summary>
		/// <returns></returns>
		public decimal CalculateMovement()
		{
			// Switch the sign of the currency rate movement to reflect the move from
			// the perspective of the local currency.
			return  (m_proportion * m_marketMovement) + (m_proportion * (-1 * m_currencyMovement));
		}

		#endregion

		#region WeightedIndex properties

		/// <summary>
		/// Gets or sets the market index ID.
		/// </summary>
		/// <value>The market index ID</value>
		public int MarketIndexID
		{
			get
			{
				return m_marketIndexID;
			}
			set 
			{
				m_marketIndexID = value;
				SetDirtyFlag();
			}
		}

		/// <summary>
		/// Gets or sets the asset fund code.
		/// </summary>
		/// <value>The asset fund code.</value>
		public string AssetFundCode
		{
			get
			{
				return m_assetFundCode;
			}
			set
			{
				m_assetFundCode = value;
				SetDirtyFlag();
			}
		}

		/// <summary>
		/// Country code for the Asset Fund Weighted Index
		/// </summary>
		public string CountryCode
		{
			get { return m_countryCode; }
			set 
			{ 
				m_countryCode = value;
				SetDirtyFlag();
			}
		}

		/// <summary>
		/// Index name for the asset fund weighted index
		/// </summary>
		public string IndexName
		{
			get { return m_indexName; }
			set 
			{
				m_indexName = value;
				SetDirtyFlag();
			}
		}

		/// <summary>
		/// Currency code for the asset fund weighted index
		/// </summary>
		public string CurrencyCode
		{
			get { return m_currencyCode; }
			set 
			{
				m_currencyCode = value;
				SetDirtyFlag();
			}
		}

		/// <summary>
		/// Proportion figure as a percentage for the asset fund weighted index
		/// </summary>
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
		/// gets/sets the proportion as a percentile rather than a factor of 1
		/// </summary>
		public decimal ProportionPercentage
		{
			get
			{
				return m_proportion * 100;
			}
			set 
			{
				m_proportion = value / 100;
				SetDirtyFlag();
			}
		}
		/// <summary>
		/// Flag that indicates whether the proportion has been set
		/// </summary>
		public bool ProportionSet
		{
			get
			{
				return m_proportionSet;
			}
		}

		/// <summary>
		/// Gets or sets the market movement.
		/// </summary>
		/// <value>The market movement.</value>
		public decimal MarketMovement
		{
			get { return this.m_marketMovement; }
			set 
			{
				SetDirtyFlag();
				this.m_marketMovement = value; }
		}

		/// <summary>
		/// Flag that indicates whether the Stock Market Movement has been set
		/// </summary>
		public bool MarketMovementSet
		{
			get
			{
				return m_marketMovementSet;
			}
		}

		/// <summary>
		/// Gets or sets the imported index value import ID.
		/// </summary>
		/// <value>The imported index value import ID.</value>
		public long ImportedIndexValueImportID
		{
			get { return this.m_importedIndexValueImportID; }
			set 
			{
				SetDirtyFlag(); 
				this.m_importedIndexValueImportID = value; }
		}

		/// <summary>
		/// Gets or sets the currency movement.
		/// </summary>
		/// <value>The currency movement.</value>
		public decimal CurrencyMovement
		{
			get { return this.m_currencyMovement; }
			set 
			{
				SetDirtyFlag(); 
				this.m_currencyMovement = value; }
		}

		/// <summary>
		/// Flag that indicates whether the currency (exchange rate) movement has been set
		/// </summary>
		public bool CurrencyMovementSet
		{
			get
			{
				return m_currencyMovementSet;
			}
		}
  
		/// <summary>
		/// Gets or sets the currency rates import ID.
		/// </summary>
		/// <value>The currency rates import ID.</value>
		public long CurrencyRatesImportID
		{
			get { return this.m_currencyRatesImportID; }
			set 
			{
				SetDirtyFlag(); 
				this.m_currencyRatesImportID = value; }
		}

		/// <summary>
		/// Gets or sets the country.
		/// </summary>
		/// <value>the country</value>
		public string Country
		{
			get { return this.m_country; }
			set 
			{
				SetDirtyFlag(); 
				this.m_country = value; }
		}
        
		/// <summary>
		/// Gets or sets the import ID.
		/// </summary>
		/// <value>the import ID</value>
		public long ImportID
		{
			get { return this.m_importID; }
			set 
			{
				SetDirtyFlag();
				this.m_importID = value; }
		}

		/// <summary>
		/// Gets or sets a value indicating whether this is from an authorised asset fund.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if [from authorised asset fund details]; otherwise, <c>false</c>.
		/// </value>
		public bool FromAuthorisedAssetFundDetails
		{
			get { return this.m_fromAuthorisedAssetFundDetails; }
			set 
			{
				SetDirtyFlag();
				this.m_fromAuthorisedAssetFundDetails = value; }
		}

		#endregion

		#region Display Properties (Required for grid. TODO: see if we can remove this using property formatter - requires grid refactor)

		/// <summary>
		/// Displays the percent in standard formatted way (or 'Unavailable')
		/// </summary>
		public string ProportionDisplay
		{
			get
			{
				return DisplayFormat.Percent (m_proportion, m_proportionSet);
			}
		}

		/// <summary>
		/// Displays the percent to 6dp min 2 showing
		/// </summary>
		public string ProportionPercentageDisplay
		{
			get
			{
				//db is limited to 6dp which is 4dp as a percentage
				return String.Format ("{0:##0.00##}", ProportionPercentage);
			}
			set //this is required for the list to list control to be ably to display then set back through ui
			{
				try
				{
					ProportionPercentage = decimal.Parse (value);
				}
				catch
				{
				}
			}
		}

		/// <summary>
		/// Gets or sets the market movement.
		/// </summary>
		/// <value>The market movement.</value>
		public string MovementDisplay
		{
			// Switch the sign of the currency rate movement to reflect the move from
			// the perspective of the local currency.
			get {return (m_marketMovement + (-1 * m_currencyMovement)).ToString("p4"); }
		}

		/// <summary>
		/// This is a display property for the properties grid in Asset Fund Status
		/// </summary>
		public string MovementEffectDisplay
		{
			get { return CalculateMovement().ToString("p4"); }
		}

		/// <summary>
		/// This is a display property for the properties grid in Asset Fund Status
		/// </summary>
		public string DisplayName
		{
			get 
			{ 	
				return m_indexName;
			}
		}

		#endregion

		#region ListToList required methods

		/// <summary>
		/// Gets the hash code.
		/// </summary>
		/// <returns></returns>
		public override int GetHashCode()
		{
			//TODO - is this always going to generate a unique hash code? what if there are > 1 new items?
			//Does it matter if not unique?
			return this.MarketIndexID;
		}

		/// <summary>
		/// Overridden to check equality based on unique id. Required in order for list to list to work
		/// </summary>
		/// <param name="obj">Obj.</param>
		/// <returns></returns>
		public override bool Equals(object obj)
		{
			if (obj is WeightedIndex)
			{
				WeightedIndex wi = (WeightedIndex)obj;
				return (this.MarketIndexID == wi.MarketIndexID);
			}
				/*
				 * TODO - once (if) list to list control can cope with a lookup on one side
				 * and a specific object collection on the other, re-introduce this.
				 * 
				else if (obj is SimpleLookup)
				{
					SimpleLookup fgLookup = (SimpleStringLookup) obj;
					return this.ID == fgLookup.Key; 
				}
				*/
			else
			{
				return base.Equals (obj);
			}
		}

		#endregion


	}
}
