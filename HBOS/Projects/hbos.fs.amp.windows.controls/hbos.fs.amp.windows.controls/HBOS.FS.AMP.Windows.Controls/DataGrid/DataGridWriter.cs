using System;
using System.IO;
using System.Data;
using System.Xml;
using System.Xml.Xsl;
using System.Text;
using System.Reflection;
using System.Diagnostics;

using HBOS.FS.AMP.Utilities;

namespace HBOS.FS.AMP.Windows.Controls
{
	/// <summary>
	/// DataGridWriter - Provides the functionality to writes the contents of a dataview / data grid to a file using an XSLT transform.
	/// </summary>
	internal class DataGridWriter
	{
		#region Variables

		private string m_outputFileName;
		private string m_xslResourceName;
		private DataTable m_dataTable = null;

		#endregion

		#region Constructor

		/// <summary>
		/// Constructor for writing a DataGrid / DataView's contents.
		/// </summary>
		/// <param name="outputFileName">File to write the grid contents to.</param>
		/// <param name="xslResourceName">XSLT resoure to use to transform the grid contents.</param>
		/// <param name="tableToWrite">DataTable containing the data to write.</param>
		/// <remarks>The root name will be "DataGrid" with each row being the DataTable.TableName.</remarks>
		public DataGridWriter( string outputFileName ,  string xslResourceName , DataTable tableToWrite )
		{
			m_outputFileName = outputFileName;
			m_xslResourceName = xslResourceName;
			m_dataTable = tableToWrite;
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Write the contents as per the transform. The root name matched against will be "DataGrid" with each row being tagged with the DataTable.TableName.
		/// </summary>
		public void Write()
		{
			DataSet myDataSet = new DataSet( "DataGrid" );
			Stream myXslStream = null;


			myDataSet.Tables.Add( m_dataTable );

			// Load the XML
			XmlDataDocument m_xmlDocument = new XmlDataDocument( myDataSet );
			XslTransform myTransform = new XslTransform();

			// Load the XSL
			lock(this)
			{
				myXslStream = ResourceHelper.GetManifestResourceStream( m_xslResourceName );
				myXslStream.Position = 0;
			}

			// Do the transform
			XmlTextReader myXslXmlTextReader = new XmlTextReader( myXslStream , XmlNodeType.Document , null );
			myXslXmlTextReader.MoveToContent();
			myTransform.Load( myXslXmlTextReader, null , null );
			
			StreamWriter myOutputFile = new StreamWriter( m_outputFileName , false , Encoding.Default );
			myOutputFile.AutoFlush = true;
			myTransform.Transform( m_xmlDocument , null , myOutputFile , null );

			// Close readers
			myXslXmlTextReader.Close();
			myXslStream.Close();
			myOutputFile.Close();
		}

		#endregion
	}
}
