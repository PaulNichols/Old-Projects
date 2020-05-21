using System;

namespace HBOS.FS.AMP.UPD.Types.AssetFunds
{
	/// <summary>
	/// Class representing data for a PriceFile
	/// </summary>
	 [Serializable]
	public class PriceFile : EntityBase
	{
		#region Private variable declaration

		//
		// Data fields
		//
		private string m_FileDescription;
		private string m_FileName;
		private string m_Extension;
		private int m_FileId;
		private string m_CompanyCode;

		#endregion

		#region PriceFile constructor

		/// <summary>
		/// Default constructor
		/// </summary>
		public PriceFile()
		{
			this.IsNew = true;
			this.IsDeleted = false;
			this.IsDirty = false;
		}

		/// <summary>
		/// Default constructor
		/// </summary>
		/// <param name="fileName">The Price File name</param>
		/// <param name="fileDescription">The Price File description</param>
		/// <param name="extension">File extension</param>
		/// /// <param name="companyCode">The code of the company this file relates to </param>
		/// <param name="fileId">Unique Id</param>
		/// 
		public PriceFile(int fileId,string fileName,string fileDescription,string extension,string companyCode)
		{
			m_FileName = fileName;
			m_FileDescription= fileDescription;
			m_Extension=extension;
			m_FileId=fileId;
			m_CompanyCode=companyCode;

			this.IsNew = false;
			this.IsDeleted = false;
			this.IsDirty = false;
		}


		/// <summary>
		/// Creates a new <see cref="PriceFile"/> instance.
		/// </summary>
		 /// <param name="fileName">The Price File name</param>
		 /// <param name="fileDescription">The Price File description</param>
		 /// <param name="extension">File extension</param>
		 /// <param name="fileId">Unique Id</param>
		 /// <param name="companyCode">The code of the company this file relates to </param>
		/// <param name="timestamp">Timestamp.</param>
		public PriceFile(string fileName, string fileDescription,string extension,int fileId,string companyCode,byte[] timestamp) : this(fileId,fileName,fileDescription,extension,companyCode)
		{
			m_timestamp = timestamp;
		}

		#endregion

		#region PriceFile properties

		 /// <summary>
		 /// Gets or sets the file id.
		 /// </summary>
		 /// <value></value>
		 public int FileId
		 {
			 get { return m_FileId; }
			 set { m_FileId=value; }
		 }


		 /// <summary>
		 /// Gets or sets the description of the file.
		 /// </summary>
		 /// <value></value>
		 public string FileDescription
		 {
			 get { return m_FileDescription; }
			 set { m_FileDescription=value; }
		 }

		 /// <summary>
		 /// Gets or sets the name of the file.
		 /// </summary>
		 /// <value></value>
		 public string FileName
		 {
			 get { return m_FileName; }
			 set { m_FileName=value; }
		 }

		 /// <summary>
		 /// Gets or sets the extension of the file.
		 /// </summary>
		 /// <value></value>
		 public string Extension
		 {
			 get { return m_Extension; }
			 set { m_Extension=value; }
		 }

		 /// <summary>
		 /// Gets or sets the company code.
		 /// </summary>
		 /// <value></value>
		 public string CompanyCode
		 {
			 get { return m_CompanyCode; }
			 set { m_CompanyCode=value; }
		 }



		#endregion PriceFile properties

		/// <summary>
		/// Overridden to return equality of two PriceFile objects based on the PriceFileCode
		/// </summary>
		/// <param name="obj">Obj.</param>
		/// <returns></returns>
		public override bool Equals(object obj)
		{
			if (!(obj is PriceFile))
			{
				return base.Equals(obj);
			}
			else
			{
				PriceFile check = (PriceFile) obj;
				return (check == this);
			}
		}

		 /// <summary>
		 /// Overloaded equality operator
		 /// </summary>
		 /// <param name="lhs">First Object to compare</param>
		 /// <param name="rhs">Second Object to compare</param>
		 /// <returns></returns>
		 public static bool operator==(PriceFile lhs,PriceFile rhs)
		 {
			 if ((object)lhs !=null && (object)rhs!=null )
			 {
				 return (lhs.FileId==rhs.FileId);
			 }
			 else
			 {
				 return (object)lhs==(object)rhs;
			 }
		 }

		 /// <summary>
		 /// Overloaded inequality operator
		 /// </summary>
		 /// <param name="lhs">First Object to compare</param>
		 /// <param name="rhs">Second Object to compare</param>
		 /// <returns></returns>
		 public static bool operator!=(PriceFile lhs,PriceFile rhs)
		 {
			 return !(lhs==rhs);
		 }

		/// <summary>
		/// Gets the hash code.
		/// </summary>
		/// <returns></returns>
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}



	}
}