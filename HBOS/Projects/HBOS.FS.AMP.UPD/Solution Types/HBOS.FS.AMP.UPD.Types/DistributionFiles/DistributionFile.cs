using System.Xml.Xsl;

namespace HBOS.FS.AMP.UPD.Types.DistributionFiles
{
	/// <summary>
	/// Summary description for DistributionFile.
	/// </summary>
	public class DistributionFile : EntityBase
	{
		/// <summary>
		/// Represents a physical file that the system will distribute.
		/// </summary>

		#region Locals
		private string m_ManipulationClassToInvoke = null;

		private int m_fileID = 0;
		private string m_fileDescription = string.Empty;
		private string m_fileName = string.Empty;
		private string m_filePath = string.Empty;
		private string m_archiveFolder = string.Empty;
		private string m_companyCode = string.Empty;

		private DistributionFileStatuses m_status = DistributionFileStatuses.Unavailable;
		private readonly int m_distributedCount;
		private readonly int m_distributableCount;
		private readonly int m_totalFundCount;
		private IXsltLoader m_xsltLoader = null;

		private bool m_fundGroupNumberRequired = false;
		private bool m_decimalPlacesRequired = false;
		private bool m_significantDecimalPlacesRequired = false;
		private bool m_majorDenominationRequired = false;

		#endregion

		#region Constructors

		/// <summary>
		/// Create a new Distribution File
		/// </summary>
		public DistributionFile()
			: this(0, string.Empty, string.Empty, string.Empty, DistributionFileStatuses.Unavailable,
			       null, false, false, false, false, new byte[1])
		{
			m_archiveFolder = string.Empty;
			m_companyCode = string.Empty;

			//Set up IEntityBase members
			m_isNew = true;
			m_isDeleted = false;
			m_isDirty = true;
		}

		/// <summary>
		/// Creates a new <see cref="DistributionFile"/> instance for static data maintenance
		/// </summary>
		/// <param name="fileID">File ID.</param>
		/// <param name="fileDescription">File description.</param>
		/// <param name="fileName">Name of the file.</param>
		/// <param name="filePath">File path.</param>
		/// <param name="status">Status.</param>
		/// <param name="manipulationClassToInvoke">Manipulation class to invoke.</param>
		/// <param name="fundGroupNumberRequired">Fund group number required.</param>
		/// <param name="decimalPlacesRequired">Decimal places required.</param>
		/// <param name="significantDecimalPlacesRequired">Significant decimal places required.</param>
		/// <param name="majorDenominationRequired">Major denomination required.</param>
		/// <param name="timeStamp">Time stamp.</param>
		public DistributionFile(int fileID, string fileDescription,
		                        string fileName, string filePath, DistributionFileStatuses status,
		                        string manipulationClassToInvoke,
		                        bool fundGroupNumberRequired, bool decimalPlacesRequired, bool significantDecimalPlacesRequired, bool majorDenominationRequired,
		                        byte[] timeStamp)
			: this(fileID, fileDescription, fileName, filePath, string.Empty, string.Empty, manipulationClassToInvoke, status, 0, 0, 0, null, timeStamp)
		{
			m_fundGroupNumberRequired = fundGroupNumberRequired;
			m_decimalPlacesRequired = decimalPlacesRequired;
			m_significantDecimalPlacesRequired = significantDecimalPlacesRequired;
			m_majorDenominationRequired = majorDenominationRequired;
		}

		/// <summary>
		/// Creates a new <see cref="DistributionFile"/> instance for distribution.
		/// </summary>
		/// <param name="fileID">File ID.</param>
		/// <param name="fileDescription">File description.</param>
		/// <param name="fileName">Name of the file.</param>
		/// <param name="filePath">File path.</param>
		/// <param name="archiveFolder">Archive folder.</param>
		/// <param name="companyCode">Company code.</param>
		/// <param name="manipulationClassToInvoke">Manipulation class to invoke.</param>
		/// <param name="status">Status.</param>
		/// <param name="distributedCount">Distributed count.</param>
		/// <param name="distributableCount">Distributable count.</param>
		/// <param name="totalFundCount">Total fund count.</param>
		/// <param name="xsltLoader">XSLT loader.</param>
		/// <param name="timeStamp">Time stamp.</param>
		public DistributionFile(int fileID, string fileDescription,
		                        string fileName, string filePath, string archiveFolder,
		                        string companyCode, string manipulationClassToInvoke, DistributionFileStatuses status,
		                        int distributedCount, int distributableCount, int totalFundCount,
		                        IXsltLoader xsltLoader, byte[] timeStamp)
		{
			m_fileID = fileID;
			m_fileDescription = fileDescription;
			m_fileName = fileName;
			m_filePath = filePath;
			m_status = status;
			m_distributedCount = distributedCount;
			m_distributableCount = distributableCount;
			m_totalFundCount = totalFundCount;
			m_xsltLoader = xsltLoader;
			m_archiveFolder = archiveFolder;
			m_companyCode = companyCode;

			//Set up IEntityBase members
			m_isNew = false;
			m_isDeleted = false;
			m_timestamp = timeStamp;
			m_isDirty = false;
			m_ManipulationClassToInvoke = manipulationClassToInvoke;
		}

		#endregion

		#region Methods

		/// <summary>
		/// Override the ToString method to provide useful information.
		/// </summary>
		/// <returns>The FileDescription</returns>
		public override string ToString()
		{
			return this.m_fileDescription;
		}

		/// <summary>
		/// Overridden to check equality based on FileId
		/// </summary>
		/// <param name="obj">Obj.</param>
		/// <returns></returns>
		public override bool Equals(object obj)
		{
			if (!(obj is DistributionFile))
			{
				return base.Equals(obj);
			}
			else
			{
				DistributionFile df = (DistributionFile) obj;
				return (this == df);
			}
		}

		/// <summary>
		/// Overloaded equality operator
		/// </summary>
		/// <param name="lhs">First Object to compare</param>
		/// <param name="rhs">Second Object to compare</param>
		/// <returns></returns>
		public static bool operator ==(DistributionFile lhs, DistributionFile rhs)
		{
			if ((object) lhs != null && (object) rhs != null)
			{
				return (lhs.FileID == rhs.FileID);
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
		public static bool operator !=(DistributionFile lhs, DistributionFile rhs)
		{
			return !(lhs == rhs);
		}


		/// <summary>
		/// Gets the hash code.
		/// </summary>
		/// <returns></returns>
		public override int GetHashCode()
		{
			return this.FileID;
		}

		/// <summary>
		/// Loads the XSLT for this distribution.
		/// </summary>
		/// <returns></returns>
		public XslTransform LoadXslt()
		{
			if (m_xsltLoader == null)
			{
				return null;
			}
			else
			{
				return m_xsltLoader.Load();
			}
		}

		#endregion Methods

		#region Public Properties

		/// <summary>
		/// Gets the manipulation class to invoke.
		/// </summary>
		/// <value></value>
		public string ManipulationClassToInvoke
		{
			get { return m_ManipulationClassToInvoke; }
			set { m_ManipulationClassToInvoke = value; }
		}


		/// <summary>
		/// The database unique key for this file.
		/// </summary>
		public int FileID
		{
			set
			{
				m_fileID = value;
				SetDirtyFlag();
			}
			get { return m_fileID; }
		}

		/// <summary>
		/// The description of this file.
		/// </summary>
		public string FileDescription
		{
			set
			{
				m_fileDescription = value;
				SetDirtyFlag();
			}
			get { return m_fileDescription; }
		}

		/// <summary>
		/// The physical file name of this file.
		/// </summary>
		public string FileName
		{
			set
			{
				m_fileName = value;
				SetDirtyFlag();
			}
			get { return m_fileName; }
		}


		/// <summary>
		/// The physical file path of this file.
		/// </summary>
		public string FilePath
		{
			set
			{
				m_filePath = value;
				SetDirtyFlag();
			}
			get { return m_filePath; }
		}

		/// <summary>
		/// The status for this file
		/// </summary>
		public DistributionFileStatuses Status
		{
			get { return m_status; }
			set
			{
				m_status = value;
				SetDirtyFlag();
			}
		}

		/// <summary>
		/// The folder location to archive this file
		/// </summary>
		public string ArchiveFolder
		{
			get { return m_archiveFolder; }
			set
			{
				m_archiveFolder = value;
				SetDirtyFlag();
			}
		}

		/// <summary>
		/// The company code associated with this files data
		/// </summary>
		/// <remarks>This is used for archiving to attach the company code to the archive filename</remarks>
		public string CompanyCode
		{
			get { return m_companyCode; }
			set
			{
				m_companyCode = value;
				SetDirtyFlag();
			}
		}

		/// <summary>
		/// Gets the count of distributed funds for this file.
		/// </summary>
		/// <value></value>
		public int DistributedCount
		{
			get { return m_distributedCount; }
		}

		/// <summary>
		/// Gets the count of distributable funds for this file.
		/// </summary>
		/// <value></value>
		public int DistributableCount
		{
			get { return m_distributableCount; }
		}

		/// <summary>
		/// Gets the total count of funds expected on the output file.
		/// </summary>
		/// <value></value>
		public int TotalFundCount
		{
			get { return m_totalFundCount; }
		}

		/// <summary>
		/// Gets or sets a value indicating whether [fund group number required].
		/// </summary>
		/// <value>
		/// 	<c>true</c> if [fund group number required]; otherwise, <c>false</c>.
		/// </value>
		public bool FundGroupNumberRequired
		{
			get { return m_fundGroupNumberRequired; }
			set { m_fundGroupNumberRequired = value; }
		}

		/// <summary>
		/// Gets or sets a value indicating whether [decimal places required].
		/// </summary>
		/// <value>
		/// 	<c>true</c> if [decimal places required]; otherwise, <c>false</c>.
		/// </value>
		public bool DecimalPlacesRequired
		{
			get { return m_decimalPlacesRequired; }
			set { m_decimalPlacesRequired = value; }
		}

		/// <summary>
		/// Gets or sets a value indicating whether [significant decimal places required].
		/// </summary>
		/// <value>
		/// 	<c>true</c> if [significant decimal places required]; otherwise, <c>false</c>.
		/// </value>
		public bool SignificantDecimalPlacesRequired
		{
			get { return m_significantDecimalPlacesRequired; }
			set { m_significantDecimalPlacesRequired = value; }
		}

		/// <summary>
		/// Gets or sets a value indicating whether [major denimonation required].
		/// </summary>
		/// <value>
		/// 	<c>true</c> if [major denimonation required]; otherwise, <c>false</c>.
		/// </value>
		public bool MajorDenimonationRequired
		{
			get { return m_majorDenominationRequired; }
			set { m_majorDenominationRequired = value; }
		}

		/// <summary>
		/// Gets or sets the XSLT loader.
		/// </summary>
		/// <value></value>
		public IXsltLoader xsltLoader
		{
			get { return m_xsltLoader; }
			set { m_xsltLoader = value; }
		}

		#endregion
	}

	#region Public Enumarators

	/// <summary>
	/// Enumeration of the status for each distribution file
	/// </summary>
	public enum DistributionFileStatuses
	{
		/// <summary>
		/// Not all the funds in this distribution have been released
		/// </summary>
		Unavailable,

		/// <summary>
		/// Not all of the funds in this distribution have been released but partial distributions
		/// are allowed
		/// </summary>
		Partial,

//        /// <summary>
//        /// Not all of the funds in this distribution have been released but partial distributions
//        /// are allowed and a previous partial distrbution has been performed
//        /// </summary>
//        PartiallyDistributed,

		/// <summary>
		/// All funds in the distribution have been released and this distribution can be carried out
		/// </summary>
		Available,

		/// <summary>
		/// This distribution has already been distributed, it may be re-distributed
		/// </summary>
		Distributed
	}

	#endregion
}