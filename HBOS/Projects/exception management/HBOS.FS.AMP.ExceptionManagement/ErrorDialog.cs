using System;

using HBOS.FS.Common.ExceptionManagement;

namespace HBOS.FS.AMP.ExceptionManagement
{
	/// <summary>
	/// ErrorDialog - Allows display of exception information. Includes functionality to display nested exceptions and stack traces.
	/// </summary>
	/// <remarks>
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
	/// <para>Once the Error Dialog is displayed to the user, they can "Copy" the Exception which puts the contents of the exception on the clipboard. The 
	/// main purpose of this is to facilitate the emailing of Exception information to support or developers for investigation.</para>
	/// </remarks>
	/// <example>
	///		<para>An example of using ErrorDialog is as follows:</para>
	///		<code lang="C#">
	///		catch( Exception Ex )
	///		{
	///			ErrorDialog.Show( Ex );
	///		}
	///		</code>
	/// </example>
	public class ErrorDialog
	{
		/// <summary>
		/// Static method to create and show error dialog box for a generic system exception
		/// </summary>
		/// <param name="exception">System.Exception</param>
		/// <example>
		///		<para>An example of using ErrorDialog is as follows:</para>
		///		<code lang="C#">
		///		catch( Exception Ex )
		///		{
		///			ErrorDialog.Show( Ex );
		///		}
		///		</code>
		/// </example>
		public static void Show(Exception exception)
		{
			new ErrorDialogForm(exception).ShowDialog();

		}

		/// <summary>
		/// Static method to create and show error dialog box for any HBOS.FS exception
		/// </summary>
		/// <param name="baseException">HBOS.FS.Common.BaseException or derived</param>
		/// <example>
		///		<para>An example of using ErrorDialog is as follows:</para>
		///		<code lang="C#">
		///		catch( BaseApplicationException Ex )
		///		{
		///			ErrorDialog.Show( Ex );
		///		}
		///		</code>
		/// </example>
		public static void Show(BaseApplicationException baseException)
		{
			new ErrorDialogForm(baseException).ShowDialog();
		}
	}
}
