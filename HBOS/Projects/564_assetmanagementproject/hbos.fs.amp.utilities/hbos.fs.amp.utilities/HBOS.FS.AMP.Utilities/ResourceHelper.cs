using System;
using System.IO;
using System.Reflection;
using System.Diagnostics;

namespace HBOS.FS.AMP.Utilities
{
	/// <summary>
	/// <p>Resource Helper - Gets embedded resources out of assemblies.</p>
	/// <p>Generally, the resource will be in the calling assembly, but there are times when this may not be the case. In order to
	/// make the process of retrieving resource as easy as possible, <see cref="ResourceHelper"/> does a stack walk of the loaded assemblies
	/// to look for the specific resource name.</p>
	/// <p>The class contains 1 static method called <see cref="GetManifestResourceStream">GetManifestResourceStream()</see> which does the work of doing the stack walk to retrieve the resource.</p>
	/// </summary>
	/// <exception cref="ApplicationException">Application Exception</exception>
	/// <example>
	///		<p>An example of using GetManifestResourceStream is:</p>
	///		<code escaped="true" lang="C#">
	///		Stream myXslStream = ResourceHelper.GetManifestResourceStream( "HBOS.FS.AMP.Data.Transfer.ValidationErrors.xslt" );
	///		</code>
	/// </example>
	/// <remarks>Does a stack walk to look for an embedded resource.
	/// <note>The resource name should be fully qualified with the namespace. If the project contains a folder structure, 
	/// the folder name must be added to the Namespace. e.g. if the Namespace is HBOS.FS.AMP.RAW.Windows.Controls and the resource is 
	/// stored in a folder called Buttons and the resource name is "BigRedButton.xslt", then the fully qualified resource name is
	/// HBOS.FS.AMP.Windows.Controls.Buttons.BigRedButton.xslt. </note>
	/// </remarks>
	public class ResourceHelper
	{
		#region Public Static Methods

		/// <summary>
		/// Get an embedded resource out of an assembly as a stream. Looks in all the assemblies in the Stack to find the resource.
		/// </summary>
		/// <param name="resourceName">Resource Name to look for in the Stack</param>
		/// <returns>The matching embedded resource as a stream</returns>
		/// <exception cref="ApplicationException">Application Exception</exception>
		/// <remarks>Does a stack walk to find the resource.
		/// <note>The resource name should be fully qualified with the namespace. If the project contains a folder structure, 
		/// the folder name must be added to the Namespace. e.g. if the Namespace is HBOS.FS.AMP.RAW.Windows.Controls and the resource is 
		/// stored in a folder called Buttons and the resource name is "BigRedButton.xslt", then the fully qualified resource name is
		/// HBOS.FS.AMP.Windows.Controls.Buttons.BigRedButton.xslt. </note>
		/// </remarks>
		/// <example>
		///		<p>An example of using GetManifestResourceStream is:</p>
		///		<code escaped="true" lang="C#">
		///		Stream myXslStream = ResourceHelper.GetManifestResourceStream( "HBOS.FS.AMP.Data.Transfer.ValidationErrors.xslt" );
		///		</code>
		/// </example>
		public static Stream GetManifestResourceStream( string resourceName )
		{
			bool foundResource = false;

			Stream myXslStream = null;
			
			StackTrace myTrace = new StackTrace();

			// Check all resources in all frames for this xslt
			for( int i = 0 ; i < myTrace.FrameCount ; i++ )
			{
				StackFrame myFrame = myTrace.GetFrame( i );
				string[] myResources = myFrame.GetMethod().ReflectedType.Assembly.GetManifestResourceNames();

				foreach( string myResource in myResources )
				{
					if ( myResource == resourceName )
					{
						lock( typeof(ResourceHelper) )
						{
							myXslStream = myFrame.GetMethod().ReflectedType.Assembly.GetManifestResourceStream( resourceName );
							myXslStream.Position = 0;
						}
						foundResource = true;
						break;
					}
				}	
			
				// If we found the resource in this frame, stop processing
				if ( foundResource )
				{
					break;
				}
			}

			// Did we find the resource
			if ( foundResource == false )
			{
				throw new ApplicationException( String.Format( "Failed to find the following resource {0}. Make sure its an embedded resource!" , resourceName ) );
			}
			else
			{
				return myXslStream;
			}
		}
		#endregion

	}
}
