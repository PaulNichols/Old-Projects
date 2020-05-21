using System;
using System.IO;
using System.Data;
using System.Xml;
using System.Xml.Xsl;

using HBOS.FS.AMP.Data.Persister.Interface;
using HBOS.FS.AMP.Utilities;

namespace HBOS.FS.AMP.Data.Persister
{
	/// <summary>
	/// DataSetXSLTFilePersister - Used to persist a Dataset to a file using an XSLT transform.
	/// </summary>
	public class DataSetXSLTFilePersister : BaseXSLFilePersister , IPersistDataSet 
	{
		#region Constructor

		/// <summary>
		/// Constructor which takes the file name to write to and the XSLT transform to perform.
		/// </summary>
		/// <param name="fileName">File to persist the dataset to.</param>
		/// <param name="xslFileResourceName">XSLT resource to use to transform the data before persisting.</param>
		public DataSetXSLTFilePersister( string fileName , string xslFileResourceName ) : base( fileName , xslFileResourceName )
		{
		}

		#endregion

		#region ITransferDataSet

		/// <summary>
		/// Persist the dataset to a file using an XSLT transform.
		/// </summary>
		/// <param name="sourceDataSet">The dataset to persist.</param>
		/// <example>
		///		<code lang="C#">
		///		((IPersistDataSet)m_dataSetPersisters[i]).PersistDataSet( m_sourceDataSet );
		///		</code>
		/// </example>
		public void PersistDataSet( DataSet sourceDataSet )
		{
			// Set up the XmlDocument
			XmlDataDocument m_xmlDocument = new XmlDataDocument( sourceDataSet );
			XslTransform myTransform = new XslTransform();

			Stream myXslStream = ResourceHelper.GetManifestResourceStream( base.XSLTResourceName );
			XmlTextReader myXslXmlTextReader = new XmlTextReader( myXslStream , XmlNodeType.Document , null );

			myXslXmlTextReader.MoveToContent();
			myTransform.Load( myXslXmlTextReader, null , null );
			myTransform.Transform( m_xmlDocument , null , base.FileStream , null );
			
			myXslXmlTextReader.Close();
			myXslStream.Close();
		}

		#endregion

	}
}
