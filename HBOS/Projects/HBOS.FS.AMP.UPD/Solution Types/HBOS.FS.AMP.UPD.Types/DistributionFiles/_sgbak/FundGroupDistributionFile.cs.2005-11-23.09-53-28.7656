using HBOS.FS.AMP.UPD.Exceptions;

namespace HBOS.FS.AMP.UPD.Types.DistributionFiles
{
	/// <summary>
	/// Summary description for FundGroupDistributionFile.
	/// </summary>
	public class FundGroupDistributionFile : DistributionFile
	{
		#region Private members
		
		private bool m_majorDenomination = false;
		private object m_fundGroupNumber =null;
		private int m_decimalPlaces = 1;
		private int m_significantDecimalPlaces = 1;

		#endregion Private members

		#region Constructors
		/// <summary>
		/// Creates a new <see cref="FundGroupDistributionFile"/> instance.
		/// </summary>
		public FundGroupDistributionFile() : base()
		{
		}
//
//		/// <summary>
//		/// Creates a new <see cref="FundGroupDistributionFile"/> instance.
//		/// </summary>
//		/// <param name="fileID">File ID.</param>
//		/// <param name="fileDescription">File description.</param>
//		/// <param name="fileName">Name of the file.</param>
//		/// <param name="filePath">File path.</param>
//		/// <param name="status">Status.</param>
//		/// <param name="manipulationClassToInvoke">Manipulation class to invoke.</param>
//		/// <param name="fundGroupNumber">Fund group number.</param>
//		/// <param name="majorDenimonation">Major denimonation.</param>
//		/// <param name="decimalPlaces">Decimal places.</param>
//		/// <param name="significantDecimalPlaces">Significant decimal places.</param>
//		/// <param name="timeStamp">Time stamp.</param>
//		public FundGroupDistributionFile(int fileID, string fileDescription, string fileName, 
//					string filePath, DistributionFileStatuses status, string manipulationClassToInvoke, 
//					int fundGroupNumber, bool majorDenimonation, int decimalPlaces, int significantDecimalPlaces,
//					byte[] timeStamp) 
//			
//			: this(fileID, fileDescription, fileName, filePath, string.Empty, string.Empty, 
//					manipulationClassToInvoke, status, 0, 0, 0, null, 
//					fundGroupNumber, majorDenimonation, decimalPlaces, significantDecimalPlaces,
//					timeStamp)
//		{
//		}

		/// <summary>
		/// Creates a new <see cref="FundGroupDistributionFile"/> instance for static data maintenance
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
		/// <param name="fundGroupNumber">Fund group number.</param>
		/// <param name="majorDenimonation">Major denimonation.</param>
		/// <param name="decimalPlaces">Decimal places.</param>
		/// <param name="significantDecimalPlaces">Significant decimal places.</param>
		/// <param name="timeStamp">Time stamp.</param>
		public FundGroupDistributionFile(int fileID, string fileDescription,
			string fileName, string filePath, DistributionFileStatuses status,
			string manipulationClassToInvoke,
			bool fundGroupNumberRequired, bool decimalPlacesRequired, bool significantDecimalPlacesRequired, bool majorDenominationRequired,
			object fundGroupNumber, bool majorDenimonation, int decimalPlaces, int significantDecimalPlaces,
			byte[] timeStamp) 
						
			: base(fileID,  fileDescription,fileName, filePath, status,
			manipulationClassToInvoke,fundGroupNumberRequired, decimalPlacesRequired, significantDecimalPlacesRequired, majorDenominationRequired,
			timeStamp)

		{
			m_fundGroupNumber = fundGroupNumber;
			m_majorDenomination = majorDenimonation;
			m_decimalPlaces = decimalPlaces;
			m_significantDecimalPlaces = significantDecimalPlaces;
		}

		#endregion Constructors

		#region Public properties
		
		/// <summary>
		/// Gets or sets a value indicating whether to use [major denomination].
		/// </summary>
		/// <value>
		/// 	<c>true</c> if [major denomination]; otherwise, <c>false</c>.
		/// </value>
		public bool MajorDenomination
		{
			get { return m_majorDenomination; }
			set { m_majorDenomination = value; }
		}

		/// <summary>
		/// Gets or sets the fund group number.
		/// </summary>
		/// <value></value>
		public object FundGroupNumber
		{
			get { return m_fundGroupNumber; }
			set
			{
				try
				{
					if (value!=null)
					{
						int fundGroupNumber=int.Parse(value.ToString());
						if (fundGroupNumber> 0 && (fundGroupNumber< 11 || fundGroupNumber== 30) )
						{
							m_fundGroupNumber = value;
						}
						else
						{
							throw new InvalidFundGroupNumber("",null);
						}
					}					
					else
					{
						m_fundGroupNumber = value;
					}
				}
				catch
				{
					throw;
				}
			}
		}

		/// <summary>
		/// Gets or sets the number of decimal places.
		/// </summary>
		/// <value></value>
		public int NumberOfDecimalPlaces
		{
			get { return m_decimalPlaces; }
			set { m_decimalPlaces = value; }
		}

		/// <summary>
		/// Gets or sets the number of significant decimal places.
		/// </summary>
		/// <value></value>
		public int NumberOfSignificantDecimalPlaces
		{
			get { return m_significantDecimalPlaces; }
			set { m_significantDecimalPlaces = value; }
		}
		#endregion Public properties
	}
}
