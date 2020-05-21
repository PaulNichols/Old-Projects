using System;

namespace HBOS.FS.AMP.UPD.Types.DistributionFiles
{
	/// <summary>
	/// Interface to be implemented by classes that load xslt
	/// </summary>
	public interface IXsltLoader
	{
		/// <summary>
		/// Loads this XSLT from the source
		/// </summary>
		/// <returns></returns>
		System.Xml.Xsl.XslTransform	Load();
	}
}