using System;

namespace HBOS.FS.AMP.UPD.Types.Users
{
	/// <summary>
	/// This class holds the prermissions for a particular user/company
	/// </summary>
	public class UserPermissions : EntityBase, ICloneable
	{
		#region Object Specific Private Variables

		// Define variables to hold object specific data
		private string m_lastChangedBy = string.Empty;
		private DateTime m_lastChangedDate = DateTime.Now;
		private string m_companyCode = string.Empty;
		private bool administrator = false;
		private bool canImportCompositePrices;

		// Imports and Exports
		private bool canImportLinkedPrices;
		private bool importExchangeRates = false;
		private bool m_importMarketIndices = false;
		//private bool m_importHI3Prices = false; 
		private bool m_importOverseasFundWeightings = false;
		private bool m_exportOEICSPrices = false;
		private bool m_reExportPrices = false;
		private bool m_importCompositeSplits = false;
		private bool canImportOEICPrices;

		// Authorisation        
		private bool m_authorisePrices = false;
		private bool m_releasePrices = false;
		private bool m_distributePrices = false;

		// Maintenance
		private bool m_maintainFundGroups = false;
		private bool m_maintainAssetFunds = false;
		private bool m_maintainFundMappings = false;
		private bool m_maintainDistributionSubscriptions = false;
		private bool m_maintainDistributionMethods = false;
		private bool m_maintainDistributionSubscribers = false;
		private bool m_maintainUserAccess = false;
		private bool m_maintainValidationTolerances = false;
		private bool m_maintainCalculationIndices = false;
		private bool m_maintainCalculationFactors = false;

		#endregion

		#region Constructors

		/// <summary>
		/// Creates a new <see cref="UserPermissions"/> instance from the database
		/// </summary>
		/// <param name="administrator">Administrator.</param>
		/// <param name="importExchangeRates">Import exchange rates.</param>
		/// <param name="importMarketIndices">Import market indices.</param>
		/// <param name="importCompositePrices"></param>
		/// <param name="importLinkedPrices"></param>
		/// <param name="importOEICPrices"></param>
		/// <param name="importOverSeasFundWeightings">Import over seas fund weightings.</param>
		/// <param name="exportOEICSPrices">Export OEICS prices.</param>
		/// <param name="authorisePrices">Second level authorise.</param>
		/// <param name="releasePrices">Release prices.</param>
		/// <param name="distributePrices">Distribute prices.</param>
		/// <param name="maintainFundGroups">Maintain fund groups.</param>
		/// <param name="maintainAssetFunds">Maintain asset funds</param>
		/// <param name="maintainFundMappings">Maintain fund mappings.</param>
		/// <param name="maintainDistributionSubscriptions">Maintain distribution subscriptions.</param>
		/// <param name="maintainDistributionMethods">Maintain distribution methods.</param>
		/// <param name="maintainDistributionSubscribers">Maintain distribution subscribers.</param>
		/// <param name="maintainUserAccess">Maintain user access.</param>
		/// <param name="maintainValidationTolerances">Maintain validation tolerances.</param>
		/// <param name="maintainCalculationIndices">Maintain calculation indices.</param>
		/// <param name="maintainCalculationFactors">Maintain calculation factors.</param>
		/// <param name="importCompositeSplits">Import composite splits.</param>
		/// <param name="reExportPrices">Re export prices.</param>
		/// <param name="companyCode">Company code.</param>
		/// <param name="lastChangedBy">Last changed by.</param>
		/// <param name="lastChangedDate">Last changed date.</param>
		/// <param name="timeStamp">Time stamp.</param>
		public UserPermissions(bool administrator, bool importExchangeRates,
		                       bool importMarketIndices, bool importLinkedPrices, bool importCompositePrices,
		                       bool importOEICPrices, bool importOverSeasFundWeightings, bool exportOEICSPrices,
		                       bool authorisePrices, bool releasePrices, bool distributePrices,
		                       bool maintainFundGroups, bool maintainAssetFunds, bool maintainFundMappings,
		                       bool maintainDistributionSubscriptions, bool maintainDistributionMethods,
		                       bool maintainDistributionSubscribers, bool maintainUserAccess,
		                       bool maintainValidationTolerances, bool maintainCalculationIndices,
		                       bool maintainCalculationFactors, bool importCompositeSplits, bool reExportPrices,
		                       string companyCode, string lastChangedBy, DateTime lastChangedDate, byte[] timeStamp)
		{
			this.administrator = administrator;
			this.importExchangeRates = importExchangeRates;
			this.m_importMarketIndices = importMarketIndices;
			this.canImportOEICPrices = importOEICPrices;
			this.m_importOverseasFundWeightings = importOverSeasFundWeightings;
			this.m_exportOEICSPrices = exportOEICSPrices;
			this.m_authorisePrices = authorisePrices;
			this.m_releasePrices = releasePrices;
			this.m_distributePrices = distributePrices;
			this.m_maintainFundGroups = maintainFundGroups;
			this.m_maintainAssetFunds = maintainAssetFunds;
			this.m_maintainFundMappings = maintainFundMappings;
			this.m_maintainDistributionSubscriptions = maintainDistributionSubscriptions;
			this.m_maintainDistributionMethods = maintainDistributionMethods;
			this.m_maintainDistributionSubscribers = maintainDistributionSubscribers;
			this.m_maintainUserAccess = maintainUserAccess;
			this.m_maintainValidationTolerances = maintainValidationTolerances;
			this.m_maintainCalculationIndices = maintainCalculationIndices;
			this.m_maintainCalculationFactors = maintainCalculationFactors;
			this.m_importCompositeSplits = importCompositeSplits;
			this.m_reExportPrices = reExportPrices;
			this.m_companyCode = companyCode;
			this.m_lastChangedBy = lastChangedBy;
			this.m_lastChangedDate = lastChangedDate;
			this.m_timestamp = timeStamp;
			this.canImportCompositePrices = importCompositePrices;
			this.CanImportLinkedPrices = importLinkedPrices;
		}

		/// <summary>
		/// Creates a new <see cref="UserPermissions"/> instance in the client
		/// </summary>
		/// <param name="Administrator">Administrator.</param>
		/// <param name="importExchangeRates">Import exchange rates.</param>
		/// <param name="ImportMarketIndices">Import market indices.</param>
		/// <param name="ImportHI3Prices">Import H i3 prices.</param>
		/// <param name="ImportOverSeasFundWeightings">Import over seas fund weightings.</param>
		/// <param name="ExportOEICSPrices">Export OEICS prices.</param>
		/// <param name="AuthorisePrices">Second level authorise.</param>
		/// <param name="ReleasePrices">Release prices.</param>
		/// <param name="DistributePrices">Distribute prices.</param>
		/// <param name="MaintainFundGroups">Maintain fund groups.</param>
		/// <param name="MaintainAssetFunds">Maintain asset funds</param>
		/// <param name="maintainFundMappings">Maintain fund mappings.</param>
		/// <param name="maintainDistributionSubscriptions">Maintain distribution subscriptions.</param>
		/// <param name="MaintainDistributionMethods">Maintain distribution methods.</param>
		/// <param name="MaintainDistributionSubscribers">Maintain distribution subscribers.</param>
		/// <param name="MaintainUserAccess">Maintain user access.</param>
		/// <param name="MaintainValidationTolerances">Maintain validation tolerances.</param>
		/// <param name="MaintainCalculationIndices">Maintain calculation indices.</param>
		/// <param name="MaintainCalculationFactors">Maintain calculation factors.</param>
		/// <param name="importCompositeSplits">Import composite splits.</param>
		/// <param name="reExportPrices">Re export prices.</param>
		/// <param name="CompanyCode">Company code.</param>
		public UserPermissions(bool Administrator, bool importExchangeRates,
		                       bool ImportMarketIndices, bool ImportHI3Prices, bool ImportOverSeasFundWeightings,
		                       bool ExportOEICSPrices, bool AuthorisePrices, bool ReleasePrices,
		                       bool DistributePrices, bool MaintainFundGroups, bool MaintainAssetFunds,
		                       bool maintainFundMappings, bool maintainDistributionSubscriptions,
		                       bool MaintainDistributionMethods, bool MaintainDistributionSubscribers,
		                       bool MaintainUserAccess, bool MaintainValidationTolerances,
		                       bool MaintainCalculationIndices, bool MaintainCalculationFactors,
		                       bool importCompositeSplits, bool reExportPrices, string CompanyCode)
		{
			this.administrator = Administrator;
			this.importExchangeRates = importExchangeRates;
			this.m_importMarketIndices = ImportMarketIndices;
			this.canImportOEICPrices = ImportHI3Prices;
			this.m_importOverseasFundWeightings = ImportOverSeasFundWeightings;
			this.m_exportOEICSPrices = ExportOEICSPrices;
			this.m_authorisePrices = AuthorisePrices;
			this.m_releasePrices = ReleasePrices;
			this.m_distributePrices = DistributePrices;
			this.m_maintainFundGroups = MaintainFundGroups;
			this.m_maintainAssetFunds = MaintainAssetFunds;
			this.m_maintainFundMappings = maintainFundMappings;
			this.m_maintainDistributionSubscriptions = maintainDistributionSubscriptions;
			this.m_maintainDistributionMethods = MaintainDistributionMethods;
			this.m_maintainDistributionSubscribers = MaintainDistributionSubscribers;
			this.m_maintainUserAccess = MaintainUserAccess;
			this.m_maintainValidationTolerances = MaintainValidationTolerances;
			this.m_maintainCalculationIndices = MaintainCalculationIndices;
			this.m_maintainCalculationFactors = MaintainCalculationFactors;
			this.m_importCompositeSplits = importCompositeSplits;
			this.m_reExportPrices = reExportPrices;
			this.m_companyCode = CompanyCode;
			this.m_timestamp = new byte[1];
			this.m_isNew = true;
		}

		/// <summary>
		/// Creates a new <see cref="UserPermissions"/> instance.
		/// </summary>
		public UserPermissions()
		{
			this.m_timestamp = new byte[1];
			this.m_isNew = true;
		}

		/// <summary>
		/// Creates a new <see cref="UserPermissions"/> instance.
		/// </summary>
		public UserPermissions(string companyCode)
		{
			this.m_companyCode = companyCode;
			this.m_timestamp = new byte[1];
			this.m_isNew = true;
		}

		#endregion

		#region Object Specific Properties

		/// <summary>
		/// The company code for the permissions
		/// </summary>
		public string CompanyCode
		{
			// This field cannot be changed so no dirty flag is needed
			get { return this.m_companyCode; }
		}

		/// <summary>
		/// Can the user administrate?
		/// </summary>
		public bool Administrator
		{
			get { return this.administrator; }
			set
			{
				if (this.administrator != value)
				{
					m_isDirty = true;
					this.administrator = value;
				}
			}
		}

		/// <summary>
		/// Can the user import exchange rates?
		/// </summary>
		public bool ImportExchangeRates
		{
			get { return this.importExchangeRates; }
			set
			{
				if (this.importExchangeRates != value)
				{
					m_isDirty = true;
					this.importExchangeRates = value;
				}
			}
		}

		/// <summary>
		/// Gets a value indicating whether this user can import linked prices.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this user can import linked prices; otherwise, <c>false</c>.
		/// </value>
		public bool CanImportLinkedPrices
		{
			get { return canImportLinkedPrices; }
			set
			{
				m_isDirty = true;
				canImportLinkedPrices = value;
			}
		}


		/// <summary>
		/// Gets a value indicating whether this user can import OEIC prices.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this user can import OEIC prices; otherwise, <c>false</c>.
		/// </value>
		public bool CanImportOEICPrices
		{
			get { return canImportOEICPrices; }
			set
			{
				m_isDirty = true;
				canImportOEICPrices = value;
			}
		}

		/// <summary>
		/// Can the user import market indices?
		/// </summary>
		public bool ImportMarketIndices
		{
			get { return this.m_importMarketIndices; }
			set
			{
				if (this.m_importMarketIndices != value)
				{
					m_isDirty = true;
					this.m_importMarketIndices = value;
				}
			}
		}

//		/// <summary>
//		/// Can the user import HI3 prices?
//		/// </summary>
//		public bool ImportHI3Prices 
//		{
//			get { return this.m_importHI3Prices ; }
//			set 
//			{
//				if (this.m_importHI3Prices != value)
//				{
//					m_isDirty = true; 
//					this.m_importHI3Prices  = value; 
//				}
//			}
//		}

		/// <summary>
		/// Can the user import overseas fund weightings?
		/// </summary>
		public bool ImportOverSeasFundWeightings
		{
			get { return this.m_importOverseasFundWeightings; }
			set
			{
				if (this.m_importOverseasFundWeightings != value)
				{
					m_isDirty = true;
					this.m_importOverseasFundWeightings = value;
				}
			}
		}

		/// <summary>
		/// Can the user export OEIC prices
		/// </summary>
		public bool ExportOEICSPrices
		{
			get { return this.m_exportOEICSPrices; }
			set
			{
				if (this.m_exportOEICSPrices != value)
				{
					m_isDirty = true;
					this.m_exportOEICSPrices = value;
				}
			}
		}

		/// <summary>
		/// Can the user perform second level authorisation?
		/// </summary>
		public bool AuthorisePrices
		{
			get { return this.m_authorisePrices; }
			set
			{
				if (this.m_authorisePrices != value)
				{
					m_isDirty = true;
					this.m_authorisePrices = value;
				}
			}
		}

		/// <summary>
		/// Can the user release prices?
		/// </summary>
		public bool ReleasePrices
		{
			get { return this.m_releasePrices; }
			set
			{
				if (this.m_releasePrices != value)
				{
					m_isDirty = true;
					this.m_releasePrices = value;
				}
			}
		}

		/// <summary>
		/// Can the user distribute prices?
		/// </summary>
		public bool DistributePrices
		{
			get { return this.m_distributePrices; }
			set
			{
				if (this.m_distributePrices != value)
				{
					m_isDirty = true;
					this.m_distributePrices = value;
				}
			}
		}


		/// <summary>
		/// Can the user maintain fund groups?
		/// </summary>
		public bool MaintainFundGroups
		{
			get { return this.m_maintainFundGroups; }
			set
			{
				if (this.m_maintainFundGroups != value)
				{
					m_isDirty = true;
					this.m_maintainFundGroups = value;
				}
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether a user has access to maintain asset funds.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if [maintain asset funds]; otherwise, <c>false</c>.
		/// </value>
		public bool MaintainAssetFunds
		{
			get { return this.m_maintainAssetFunds; }

			set
			{
				if (this.m_maintainAssetFunds != value)
				{
					this.m_maintainAssetFunds = value;
					this.m_isDirty = true;
				}
			}
		}

		/// <summary>
		/// Can the user maintain fund mappings?
		/// </summary>
		public bool MaintainFundMappings
		{
			get { return this.m_maintainFundMappings; }
			set
			{
				if (this.m_maintainFundMappings != value)
				{
					m_isDirty = true;
					this.m_maintainFundMappings = value;
				}
			}
		}

		/// <summary>
		/// Can the user maintain distribution subscriptions?
		/// </summary>
		public bool MaintainDistributionSubscriptions
		{
			get { return this.m_maintainDistributionSubscriptions; }
			set
			{
				if (this.m_maintainDistributionSubscriptions != value)
				{
					m_isDirty = true;
					this.m_maintainDistributionSubscriptions = value;
				}
			}
		}

		/// <summary>
		/// Can the user maintain distribution methods?
		/// </summary>
		public bool MaintainDistributionMethods
		{
			get { return this.m_maintainDistributionMethods; }
			set
			{
				if (this.m_maintainDistributionMethods != value)
				{
					m_isDirty = true;
					this.m_maintainDistributionMethods = value;
				}
			}
		}

		/// <summary>
		/// Can the user maintain distribution subscribers
		/// </summary>
		public bool MaintainDistributionSubscribers
		{
			get { return this.m_maintainDistributionSubscribers; }
			set
			{
				if (this.m_maintainDistributionSubscribers != value)
				{
					m_isDirty = true;
					this.m_maintainDistributionSubscribers = value;
				}
			}
		}

		/// <summary>
		/// Can the user maintain user access?
		/// </summary>
		public bool MaintainUserAccess
		{
			get { return this.m_maintainUserAccess; }
			set
			{
				if (this.m_maintainUserAccess != value)
				{
					m_isDirty = true;
					this.m_maintainUserAccess = value;
				}
			}
		}

		/// <summary>
		/// Can the user maintain validation tolerances?
		/// </summary>
		public bool MaintainValidationTolerances
		{
			get { return this.m_maintainValidationTolerances; }
			set
			{
				if (this.m_maintainValidationTolerances != value)
				{
					m_isDirty = true;
					this.m_maintainValidationTolerances = value;
				}
			}
		}

		/// <summary>
		/// Can the user maintain indices?
		/// </summary>
		public bool MaintainCalculationIndices
		{
			get { return this.m_maintainCalculationIndices; }
			set
			{
				if (this.m_maintainCalculationIndices != value)
				{
					m_isDirty = true;
					this.m_maintainCalculationIndices = value;
				}
			}
		}

		/// <summary>
		/// Can the user maintain calculation factors?
		/// </summary>
		public bool MaintainCalculationFactors
		{
			get { return this.m_maintainCalculationFactors; }
			set
			{
				if (this.m_maintainCalculationFactors != value)
				{
					m_isDirty = true;
					this.m_maintainCalculationFactors = value;
				}
			}
		}

		/// <summary>
		/// Can the user import composite splits
		/// </summary>
		public bool ImportCompositeSplits
		{
			get { return this.m_importCompositeSplits; }
			set
			{
				if (this.m_importCompositeSplits != value)
				{
					m_isDirty = true;
					this.m_importCompositeSplits = value;
				}
			}
		}

		/// <summary>
		/// Can the user re-export prices?
		/// </summary>
		public bool ReExportPrices
		{
			get { return this.m_reExportPrices; }
			set
			{
				if (this.m_reExportPrices != value)
				{
					m_isDirty = true;
					this.m_reExportPrices = value;
				}
			}
		}

		/// <summary>
		/// The login id of the person who last changed this record (read only)
		/// </summary>
		public string LastChangedBy
		{
			get { return this.m_lastChangedBy; }
		}

		/// <summary>
		/// The date this record was last changed (read only)
		/// </summary>
		public DateTime LastChangedDate
		{
			get { return this.m_lastChangedDate; }
		}

		/// <summary>
		/// Gets or sets a value indicating whether this user can import composite prices.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this user can import composite prices; otherwise, <c>false</c>.
		/// </value>
		public bool CanImportCompositePrices
		{
			get { return canImportCompositePrices; }
			set
			{
				m_isDirty = true;
				canImportCompositePrices = value;
			}
		}

		#endregion

		#region ICloneable Members

		/// <summary>
		/// Provides a deep copy of the current user permissions object data
		/// </summary>
		/// <returns>The deep copy of the current user permissions data</returns>
		public object Clone()
		{
			UserPermissions clonedPermissions = new UserPermissions();

			clonedPermissions.m_isDeleted = this.m_isDeleted;
			clonedPermissions.m_isDirty = this.m_isDirty;
			clonedPermissions.m_isNew = this.m_isNew;
			clonedPermissions.m_lastChangedBy = this.m_lastChangedBy;
			clonedPermissions.m_lastChangedDate = this.m_lastChangedDate;
			clonedPermissions.m_timestamp = this.m_timestamp;

			clonedPermissions.administrator = this.administrator;
			clonedPermissions.m_companyCode = this.m_companyCode;
			clonedPermissions.m_distributePrices = this.m_distributePrices;
			clonedPermissions.m_exportOEICSPrices = this.m_exportOEICSPrices;
			clonedPermissions.importExchangeRates = this.importExchangeRates;
			clonedPermissions.canImportOEICPrices = this.canImportOEICPrices;
			clonedPermissions.m_importMarketIndices = this.m_importMarketIndices;
			clonedPermissions.m_importOverseasFundWeightings = this.m_importOverseasFundWeightings;
			clonedPermissions.m_maintainCalculationFactors = this.m_maintainCalculationFactors;
			clonedPermissions.m_maintainCalculationIndices = this.m_maintainCalculationIndices;
			clonedPermissions.m_maintainDistributionMethods = this.m_maintainDistributionMethods;
			clonedPermissions.m_maintainDistributionSubscribers = this.m_maintainDistributionSubscribers;
			clonedPermissions.m_maintainDistributionSubscriptions = this.m_maintainDistributionSubscriptions;
			clonedPermissions.m_maintainFundGroups = this.m_maintainFundGroups;
			clonedPermissions.m_maintainFundMappings = this.m_maintainFundMappings;
			clonedPermissions.m_maintainUserAccess = this.m_maintainUserAccess;
			clonedPermissions.m_maintainValidationTolerances = this.m_maintainValidationTolerances;
			clonedPermissions.m_releasePrices = this.m_releasePrices;
			clonedPermissions.m_authorisePrices = this.m_authorisePrices;
			clonedPermissions.m_importCompositeSplits = this.m_importCompositeSplits;
			clonedPermissions.m_reExportPrices = this.m_reExportPrices;

			return clonedPermissions;
		}

		#endregion
	}
}