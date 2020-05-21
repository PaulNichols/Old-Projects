using System;
using System.Collections;
using HBOS.FS.Support.Tex;

namespace HBOS.FS.AMP.UPD.WinUI.Classes
{
	/// <summary>
	/// Provides parameters required to do a Static Data Export
	/// </summary>
	public class StaticDataExportParameters
	{
		#region Constructor

		/// <summary>
		/// Creates a new <see cref="StaticDataExportParameters"/> instance.
		/// </summary>
		public StaticDataExportParameters()
		{
			T.E();
			T.X();
		}

		#endregion

		#region Private Members
	
		private IList collectionToExport;
		private StaticDataExportCollection exports = new StaticDataExportCollection();

		#endregion

		#region Properties

		/// <summary>
		/// Gets or sets the collection of entities to export.
		/// </summary>
		/// <value></value>
		public IList CollectionToExport
		{
			get {return collectionToExport;}
			set {collectionToExport = value;}
		}

		/// <summary>
		/// Gets the list of xslt and csv filenames to use in the export
		/// </summary>
		/// <value></value>
		public StaticDataExportCollection Exports
		{
			get {return exports;}
		}

		#endregion
	}

	/// <summary>
	/// Structure to hold the one-to-one relationship between xslt resource and csv export filename
	/// </summary>
	public struct StaticDataExport
	{
		/// <summary>
		/// Creates a new <see cref="StaticDataExport"/> instance.
		/// </summary>
		/// <param name="xsltResourceName">
		/// The resource name of the XSLT file used to format the export
		/// </param>
		/// <param name="csvFileDescription">
		/// The middle part of the CSV file name used to identify the export type, the CSVFilename 
		/// property is built using the description prefixed with the company and suffixed with the
		/// current date
		/// </param>
		public StaticDataExport(string xsltResourceName,string csvFileDescription)
		{
			this.xsltResourceName = xsltResourceName;
			this.csvFilename = string.Empty;
			this.csvFilename = makeCsvFilename(csvFileDescription);
		}
		
		private string xsltResourceName;

		/// <summary>
		/// Gets the XSLT resource name.
		/// </summary>
		/// <value></value>
		public string XsltResourceName
		{
			get {return xsltResourceName;}
		}

		private string csvFilename;

		/// <summary>
		/// Gets the CSV filename.
		/// </summary>
		/// <value></value>
		public string CsvFilename
		{
			get {return csvFilename;}
		}

		/// <summary>
		/// Makes the export filename in the form [companyCode]_[middlePartOfFilename param]_[ccyymmdd].csv
		/// </summary>
		/// <param name="middlePartOfFilename">Name of the middle part of file.</param>
		/// <returns>The complete filename</returns>
		private string makeCsvFilename(string middlePartOfFilename)
		{
			T.E();
			string result = string.Format("{0}_{1}_{2:yyyyMMdd}.csv", GlobalRegistry.CompanyCode.Trim(), middlePartOfFilename, DateTime.Now.Date);
			T.X();
			return result;
		}

	}

	/// <summary>
	/// Specialised collection of StaticDataExportFilenames
	/// </summary>
	public class StaticDataExportCollection: CollectionBase
	{
		/// <summary>
		/// Adds the specified export to the collection
		/// </summary>
		/// <param name="export">Export details</param>
		/// <returns></returns>
		public int Add(StaticDataExport export)
		{
			return List.Add(export);
		}

		/// <summary>
		/// Gets or sets the <see cref="StaticDataExport"/> at the specified index.
		/// </summary>
		/// <value></value>
		public StaticDataExport this[int index]
		{
			get {return (StaticDataExport)List[index];}
			set {List[index]=value;}
		}
	}
}
