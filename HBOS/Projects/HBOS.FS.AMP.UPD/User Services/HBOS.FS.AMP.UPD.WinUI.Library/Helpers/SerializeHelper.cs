using System;
using System.IO;
using System.Security;
using System.Collections;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

using HBOS.FS.Support.Tex;

namespace HBOS.FS.AMP.UPD.WinUI.Helpers
{
	/// <summary>
	/// Object containing Serializing routines.
	/// </summary>
	public class SerializeHelper
	{
		
		/// <summary>
		/// Default constructor
		/// </summary>
		public SerializeHelper()
		{
		}

		/// <summary>
		/// Serialise the information to the local disk using the CLR binary formatter
		/// </summary>
		/// <param name="sourceHashtable">SOurce hashtable to be serialised</param>
		/// <param name="targetSerialiseFilename">Target filename including path</param>
		/// <exception cref="SecurityException">The user does not have the required permission to serialise the information.</exception>
		/// <exception cref="ArgumentNullException">The file stream is a null reference</exception>
		/// <exception cref="SerializationException">An error has occurred whilst writing the information to the file stream</exception>
		public void SaveHashtableToStream(Hashtable sourceHashtable, string targetSerialiseFilename)
		{
			T.E();

			// Get the serialise file location from the App Config
			// string targetSerialiseFilename = ConfigurationSettings.AppSettings["ExportSerialiseFileStream"];
			FileStream fileStream = null;
			try
			{
				// Create a file stream to create the file
				fileStream = new FileStream( targetSerialiseFilename, FileMode.Create, 
					FileAccess.Write, FileShare.None );

				if ( File.Exists( targetSerialiseFilename ) )
				{
					// Use the CLR binary formatter for fast local storage
					// Construct a BinaryFormatter and use it to serialize the data to the stream
					BinaryFormatter binaryFormatter = new BinaryFormatter();

					// Serialise file path information to disk
					// Note. Hashtables are marked as Serialisable
					binaryFormatter.Serialize( fileStream, sourceHashtable );
				}
			}
			catch(SecurityException ex)
			{
				T.DumpException(ex);
				throw new SecurityException("The user does not have the required permission to serialise the information.", ex);
			}
			catch(ArgumentNullException ex)
			{
				T.DumpException(ex);
				throw new ArgumentNullException("The file stream is a null reference");
			}
			catch(SerializationException ex)
			{
				T.DumpException( ex );
				throw new SerializationException("An error has occurred whilst writing the information to the file stream", ex);
			}
			finally
			{
				if ( fileStream != null )
				{
					fileStream.Close();
				}
				T.X();
			}
		}

		/// <summary>
		/// Extract the information from the serilaised file of the file hashtable
		/// </summary>
		/// <param name="sourceSerialiseFilename">Path and name of the source serialise file stream</param>
		/// <returns>Hashtable</returns>
		/// <exception cref="ArgumentNullException">serializationStream is a null reference</exception>
		/// <exception cref="SerializationException">The length of the file stream is 0 preventing support of Seek</exception>
		/// <exception cref="DirectoryNotFoundException">The specified directory path does not exists for the serialise file stream</exception>
		/// <exception cref="SecurityException">The caller does not have the required permission</exception>
		public Hashtable LoadHashtableFromStream(string sourceSerialiseFilename) 
		{
			T.E();

			// Get the serialise file location from the App Config
			//string sourceSerialiseFilename = ConfigurationSettings.AppSettings["ExportSerialiseFileStream"];
			FileStream fileStream = null;
			Hashtable targetHashtable = new Hashtable();

			try 
			{
				if (File.Exists(sourceSerialiseFilename))
				{
					// Open the file containing the data that we want to deserialize.
					fileStream = new FileStream(sourceSerialiseFilename, FileMode.Open, 
						FileAccess.Read, FileShare.Read);

					BinaryFormatter binaryFormatter = new BinaryFormatter();

					// Deserialize the hashtable from the file and 
					// assign the reference to the local variable.
					targetHashtable.Clear();
					targetHashtable = (Hashtable) binaryFormatter.Deserialize(fileStream);
				}
			}
			catch (ArgumentNullException ex)
			{
				T.DumpException(ex);
				T.DumpObjectDeep(fileStream);
				throw new ArgumentNullException(string.Format("The serializationStream is a null reference for {0}", sourceSerialiseFilename));
			}
			catch (SerializationException ex) 
			{
				//The serializationStream supports seeking, but its length is 0.
				T.DumpException (ex);
				throw new SerializationException("The length of the file stream is 0 preventing support of Seek.", ex);
			}
			catch ( DirectoryNotFoundException ex )
			{
				T.DumpException (ex);
				throw new DirectoryNotFoundException(string.Format( "The specified directory path does not exists for the serialise file stream {0}", sourceSerialiseFilename), ex);
			}
			catch (SecurityException ex)
			{
				//The caller does not have the required permission.
				T.DumpException(ex);
				throw new SecurityException(string.Format("The user does not have the correct permissions to access the file {0}", sourceSerialiseFilename), ex);
			}
			finally 
			{
				if (fileStream != null)
				{
					fileStream.Close();
				}
				T.X();
			}

			return targetHashtable;
		}
	}
}
