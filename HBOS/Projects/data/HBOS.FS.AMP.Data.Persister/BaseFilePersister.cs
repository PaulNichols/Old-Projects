using System;
using System.IO;
using System.Data;
using System.Security;
using System.Security.Permissions;
using System.Text;

namespace HBOS.FS.AMP.Data.Persister
{
	/// <summary>
	/// BaseFilePersister - Base functionality for persisting files
	/// </summary>
	/// <remarks>Remembers the file name we are writing to.</remarks>
	public abstract class BaseFilePersister 
	{
		#region Variables

		private StreamWriter m_fileStream = null;
		private bool m_transferInProgress = false;
		private string m_fileName = String.Empty;

		#endregion

		#region Constructor

		/// <summary>
		/// Constructor which takes the file name we are persisting to
		/// </summary>
		/// <param name="fileName">File to persist the data to.</param>
		public BaseFilePersister( string fileName )
		{
			m_fileName = fileName;
		}

		#endregion

		#region ITransfer Members

		/// <summary>
		/// Initiate the transfer which creates the file.
		/// </summary>
		/// <exception cref="SecurityException"></exception>
		public void InitialiseTransfer()
		{
			FileIOPermission writeFilePermissions = new FileIOPermission(FileIOPermissionAccess.AllAccess, this.m_fileName);
			try
			{
				writeFilePermissions.Demand();
			}
			catch (SecurityException se)
			{
				throw se;
			}

			m_fileStream = new StreamWriter( m_fileName , false , Encoding.Default );
			m_fileStream.AutoFlush = true;

			m_transferInProgress = true;
		}

		/// <summary>
		/// Is there a transfer in progress
		/// </summary>
		public bool IsTransferInProgress
		{
			get 
			{ 
				return m_transferInProgress; 
			}
		}

		/// <summary>
		/// Cancel the transfer and delete the file.
		/// </summary>
		public void CancelTransfer()
		{
			m_fileStream.Close();
			File.Delete( m_fileName );
			m_transferInProgress = false;
		}

		/// <summary>
		/// Complete the transfer and close the file.
		/// </summary>
		public void CompleteTransfer()
		{
			m_fileStream.Flush();
			m_fileStream.Close();

			m_transferInProgress = false;
		}

		#endregion

		#region Properties

		/// <summary>
		/// Inheritance access to the file stream
		/// </summary>
		protected StreamWriter FileStream
		{
			get
			{
				return m_fileStream;
			}
		}

		#endregion
	}
}
