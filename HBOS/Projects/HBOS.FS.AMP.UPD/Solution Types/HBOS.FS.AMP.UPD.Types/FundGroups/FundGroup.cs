using System;
using HBOS.FS.AMP.UPD.Types.DistributionFiles;

namespace HBOS.FS.AMP.UPD.Types.FundGroups
{
	/// <summary>
	/// An entity that represents a group of either Asset Funds or Funds
	/// </summary>
	[Serializable]
	public abstract class FundGroup : EntityBase
	{
		#region Private variable declaration

		// private variable declaration
		private string m_fundGroupName;
		private string m_fundGroupShortName;
		private int m_fundGroupID;
		private string m_companyCode;
		private bool m_forRelease;
		private bool m_hasAssociatedFunds;
		private bool m_allowSelectAllAuthorisation = false;

		//is initialised to null rather than creating an empty collection, as most commonly 
		//this will be loaded and set at load time - saves creating then destroying an object
		private DistributionFileCollection m_distributionFiles = null;

		//fund group numbers are 01-10 & 30. Therefore 0 can represent null in this case.
//		private int m_fundGroupNumber = 0;
//		private bool m_UseMajorDenomination=false;

		#endregion

		#region Constructors

		/// <summary>
		/// Used to create a new fund group from the GUI
		/// </summary>
		protected FundGroup()
		{
			//set default variables
			m_fundGroupName = "";
			m_fundGroupShortName = "";
			m_fundGroupID = 0;
			m_companyCode = "";
			m_forRelease = false;
			m_hasAssociatedFunds = false;
			m_allowSelectAllAuthorisation = false;

			//Set up IEntityBase members
			m_isNew = true;
			m_isDeleted = false;
			m_timestamp = new byte[1];
			m_isDirty = true;
		}

		/// <summary>
		/// Create an existing fund group
		/// </summary>
		protected FundGroup(int id, string companyCode, string fullName,
		                    string shortName, byte[] timeStamp, bool forRelease, bool hasAssociatedFunds,
		                    bool allowSelectAllAuthorisation)
		{
			//set passed variables
			m_fundGroupName = fullName;
			m_fundGroupShortName = shortName;
			m_fundGroupID = id;
			m_companyCode = companyCode;
			m_forRelease = forRelease;
			m_hasAssociatedFunds = hasAssociatedFunds;
			m_allowSelectAllAuthorisation = allowSelectAllAuthorisation;

			//Set up IEntityBase members
			m_isNew = false;
			m_isDeleted = false;
			m_timestamp = timeStamp;
			m_isDirty = false;

		}

		#endregion

		#region Methods

		/// <summary>
		/// Gets the hash code.
		/// </summary>
		/// <returns></returns>
		public override int GetHashCode()
		{
			//TODO - is this always going to generate a unique hash code? what if there are > 1 new items?
			//Does it matter if not unique?
			return this.ID;
		}

		/// <summary>
		/// Overridden to check equality based on FileId
		/// </summary>
		/// <param name="obj">Obj.</param>
		/// <returns></returns>
		public override bool Equals(object obj)
		{
			if (obj is FundGroup)
			{
				FundGroup fg = (FundGroup) obj;
				return (this == fg);
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
				return base.Equals(obj);
			}
		}


		/// <summary>
		/// Overloaded equality operator
		/// </summary>
		/// <param name="lhs">First Object to compare</param>
		/// <param name="rhs">Second Object to compare</param>
		/// <returns></returns>
		public static bool operator ==(FundGroup lhs, FundGroup rhs)
		{
			if ((object) lhs != null && (object) rhs != null)
			{
				return (lhs.ID == rhs.ID);
			}
			else
			{
				return (object) lhs == (object) rhs;
			}
		}

		/// <summary>
		/// Overloaded inequality operator
		/// </summary>
		/// <param name="lhs">First Object to compare</param>
		/// <param name="rhs">Second Object to compare</param>
		/// <returns></returns>
		public static bool operator !=(FundGroup lhs, FundGroup rhs)
		{
			return !(lhs == rhs);
		}


		/// <summary>
		/// Override the ToString method to provide useful information.
		/// </summary>
		/// <returns>The FundGroupName for display purposes.</returns>
		public override string ToString()
		{
			return this.FullName;
		}

		#endregion Methods

		#region FundGroup properties

//		/// <summary>
//		/// Returns whether or not prices distributed that are linked to this Fund Group 
//		/// should be displayed using major or minor denomination.
//		/// </summary>
//		public bool UseMajorDenomination
//		{
//			get 
//			{
//				return m_UseMajorDenomination;
//			}
//			set
//			{
//				m_UseMajorDenomination=value;
//			}
//		}

		/// <summary>
		/// Returns whether or not this fund group allows the user to click select all on authorisation screen in order to reduce
		/// number of button clicks for its child funds to be authorised.
		/// </summary>
		public bool AllowSelectAllAuthorisation
		{
			get { return m_allowSelectAllAuthorisation; }
			set { m_allowSelectAllAuthorisation = value; }
		}

//		/// <summary>
//		/// Returns the integer representation of the fund group number
//		/// </summary>
//		public int FundGroupNumber
//		{
//			get
//			{
//				return m_fundGroupNumber;
//			}
//			set
//			{
//				m_fundGroupNumber = value;
//			}
//		}

//		/// <summary>
//		/// Returns whether or not this fund group has a fund group number attached.
//		/// </summary>
//		public bool FundGroupNumberSet
//		{
//			get
//			{
//				return m_fundGroupNumber > 0;
//			}
//		}

		/// <summary>
		/// Gets / Sets the collection of distribution files that are associated with this fund group
		/// </summary>
		public DistributionFileCollection DistributionFiles
		{
			get
			{
				if (m_distributionFiles == null)
				{
					//may not have been created on load from db (if eg no files currently associated)
					m_distributionFiles = new DistributionFileCollection();
				}
				return m_distributionFiles;
			}
			set { m_distributionFiles = value; }
		}

		/// <summary>
		/// The full name of the FundGroup
		/// </summary>
		public string FullName
		{
			get { return m_fundGroupName; }
			set
			{
				m_fundGroupName = value;
				SetDirtyFlag();
			}
		}

		/// <summary>
		/// The short name of the Fund Group
		/// </summary>
		public string ShortName
		{
			get { return m_fundGroupShortName; }
			set
			{
				m_fundGroupShortName = value;
				SetDirtyFlag();
			}
		}

		/// <summary>
		/// Read-only Database ID for the Fund Group
		/// </summary>
		public int ID
		{
			set { m_fundGroupID = value; }
			get { return m_fundGroupID; }
		}

		/// <summary>
		/// The code for the Company that the fund group is set up for
		/// </summary>
		public string CompanyCode
		{
			get { return m_companyCode; }
			set { m_companyCode = value; }
		}


		/// <summary>
		/// Gets or sets a value indicating whether this fund group is for release.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if [for release]; otherwise, <c>false</c>.
		/// </value>
		public bool ForRelease
		{
			get { return m_forRelease; }
			set
			{
				m_forRelease = value;
				SetDirtyFlag();
			}
		}

		/// <summary>
		/// Gets a value indicating whether this instance has associated funds.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance has associated funds; otherwise, <c>false</c>.
		/// </value>
		public bool HasAssociatedFunds
		{
			get { return m_hasAssociatedFunds; }
		}

		#endregion

		#region Display Properties

		/// <summary>
		/// Displays a 'Y' or 'N'
		/// </summary>
		public string ForReleaseDisplay
		{
			get { return ForRelease ? "Y" : "N"; }
		}

		#endregion
	}
}