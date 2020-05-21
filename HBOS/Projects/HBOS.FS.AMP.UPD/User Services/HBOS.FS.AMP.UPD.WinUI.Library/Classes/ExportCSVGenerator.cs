using System.Collections;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Xsl;
using HBOS.FS.AMP.Utilities;
using HBOS.FS.AMP.Xml.XPath;

namespace HBOS.FS.AMP.UPD.WinUI.Classes
{
	/// <summary>
	/// Summary description for ExportCSVGenerator.
	/// </summary>
	public class ExportCSVGenerator
	{
		private IList m_objectsToTransform;
		private string m_xslResourceName;
		private string m_destinationFilePath;

		/// <summary>
		/// Creates a new <see cref="ExportCSVGenerator"/> instance.
		/// </summary>
		/// <param name="customCollection">Custom collection.</param>
		/// <param name="xslResourceName">Name of the XSL resource.</param>
		/// <param name="destinationFilePath">Destination file path.</param>
		public ExportCSVGenerator(IList customCollection, string xslResourceName, string destinationFilePath)
		{
			m_objectsToTransform = customCollection;
			m_xslResourceName = xslResourceName;
			m_destinationFilePath = destinationFilePath;
		}

		/// <summary>
		/// XSLTs the custom collection to file.
		/// </summary>
		public void XsltCustomCollectionToFile()
		{
			Stream myXslStream = null;

			XslTransform myTransform = new XslTransform();

			// Load the XSL
			lock (this)
			{
				myXslStream = ResourceHelper.GetManifestResourceStream(m_xslResourceName);
				myXslStream.Position = 0;
			}

			// Do the transform
			XmlTextReader myXslXmlTextReader = new XmlTextReader(myXslStream, XmlNodeType.Document, null);
			myXslXmlTextReader.MoveToContent();
			myTransform.Load(myXslXmlTextReader, null, null);

			StreamWriter myOutputFile = new StreamWriter(m_destinationFilePath, false, Encoding.Default);
			myOutputFile.AutoFlush = true;

			myTransform.Transform(new ObjectXPathNavigator(m_objectsToTransform), null, myOutputFile, null);

			// Close readers
			myXslXmlTextReader.Close();
			myXslStream.Close();
			myOutputFile.Close();
		}
	}
}