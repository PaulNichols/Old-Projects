using System;

namespace HBOS.FS.AMP.ExceptionManagement
{
	#if (DEBUG)
	/// <summary>
	/// <para><see cref="HBOS.FS.AMP.ExceptionManagement"/> provides some generic AMP Exception Management functionality.</para>
	/// <para>The main functionality at present is <see cref="ErrorDialog"/> which displays a dialog box with Exception Information.</para>
	/// <para>The difference from a normal MessageBox is that it allows display of next exceptions along with additional information.</para>
	/// <para>A summary of the information displayed by ErrorDialog is:</para>
	/// <list type="bullet">
	///		<item>
	///			<term>The Exception</term>
	///			<description>Allows display of nested exception information.</description>
	///		</item>
	///		<item>
	///			<term>Stack Trace</term>
	///			<description>The application stack trace that caused the Exception.</description>
	///		</item>
	///		<item>
	///			<term>Inner Exception Trace</term>
	///			<description>Any inner Exception stack trace information.</description>
	///		</item>
	///		<item>
	///			<term>Other Information</term>
	///			<description>Any other exception information that exist as properties of the exception.</description>
	///		</item>
	/// </list>
	/// </summary>
	public class NamespaceDoc
	{
		/// <summary>
		/// This class is a dummy class used by NDoc to provide a namespace summary.
		/// </summary>
		public NamespaceDoc()
		{
		}
	}
#endif
}
