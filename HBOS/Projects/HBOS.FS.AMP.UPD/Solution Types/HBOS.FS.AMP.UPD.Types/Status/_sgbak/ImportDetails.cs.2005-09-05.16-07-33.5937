using System;
using System.ComponentModel;

namespace HBOS.FS.AMP.UPD.Types.Status
{
	/// <summary>
	/// Summary description for ImportDetails.
	/// </summary>
    public class ImportDetails
    {
        string importSource;
        string importedByUserName;
        string importedByUser;
        DateTime importedOn;

        /// <summary>
        /// Create an existing Import details object
        /// </summary>
        /// <param name="importSourceFile"></param>
        /// <param name="importedByUserName"></param>
        /// <param name="importedByLogin"></param>
        /// <param name="importedOn"></param>
		public ImportDetails(
            string importSourceFile,
            string importedByUserName,
            string importedByLogin,
            DateTime importedOn)
        {
            this.importSource = importSourceFile;
            this.importedByUserName = importedByUserName;
            this.importedByUser = importedByLogin;
            this.importedOn = importedOn;
        }

		/// <summary>
		/// Gets the name of the import file.
		/// </summary>
		/// <value></value>
        public string ImportFileName
        {
            get {return this.importSource;}
        }

		/// <summary>
		/// Gets the name of the user who imported the file.
		/// </summary>
		/// <value></value>
        public string ImportedByName
        {
            get {return this.importedByUser;}
        }

		/// <summary>
		/// Gets the user name who imported the file.
		/// </summary>
		/// <value></value>
        public string ImportedByAccount
        {
            get {return this.importedByUserName;}
        }

		/// <summary>
		/// Gets the import date time.
		/// </summary>
		/// <value></value>
        public DateTime ImportDateTime
        {
            get {return this.importedOn;}
        }
//
//		/// <summary>
//		/// Overridden to return the <see cref="FileValuationPoint"/> as a string
//		/// </summary>
//		/// <returns></returns>
//        public override string ToString()
//        {
//            return this.valuationPoint.ToShortDateString();
//		}
		#region IEntityBase Members

		/// <summary>
		/// 
		/// </summary>
		public  static bool IsDeleted
		{
			get
			{
				// TODO:  Add ImportDetails.IsDeleted getter implementation
				return false;
			}
			set
			{
				// TODO:  Add ImportDetails.IsDeleted setter implementation
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public static bool IsNew
		{
			get
			{
				// TODO:  Add ImportDetails.IsNew getter implementation
				return false;
			}
			set
			{
				// TODO:  Add ImportDetails.IsNew setter implementation
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public  static byte[] TimeStamp
		{
			get
			{
				// TODO:  Add ImportDetails.TimeStamp getter implementation
				return null;
			}
			set
			{
				// TODO:  Add ImportDetails.TimeStamp setter implementation
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public static bool IsDirty
		{
			get
			{
				// TODO:  Add ImportDetails.IsDirty getter implementation
				return false;
			}
			set
			{
				// TODO:  Add ImportDetails.IsDirty setter implementation
			}
		}

		#endregion
	}
}
