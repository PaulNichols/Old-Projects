using System;
using System.IO;
using System.Text;
using HBOS.FS.Support.Tex;

namespace HBOS.FS.AMP.UPD.Controllers
{
	/// <summary>
	/// Summary description for UPDIO.
	/// </summary>
	public abstract class UPDIO
	{
		/// <summary>
		/// Creates a new <see cref="UPDIO"/> instance.
		/// </summary>
		/// <remarks>Made private to prevent creation of object</remarks>
		private UPDIO()
		{
		}

		#region File methods

		/// <summary>
		/// Test to see if a specified file exists in the designated path
		/// </summary>
		/// <param name="fileName">Filename and path</param>
		/// <returns>Exists flag</returns>
		public static bool FileExists(string fileName)
		{
			bool exists = false;
			T.E();
			try
			{
				if (File.Exists(fileName))
				{
					exists = true;
				}
				else
				{
					exists = false;
				}
			}
			catch (DirectoryNotFoundException ex)
			{
				T.DumpException(ex);
				throw new DirectoryNotFoundException(string.Format("The specified directory path does not exists for file: {0}", fileName), ex);
			}
			finally
			{
				T.X();
			}

			return (exists);
		}

		/// <summary>
		/// Copy a file from one location to another
		/// </summary>
		/// <param name="sourceFileName">Source file name and path</param>
		/// <param name="destinationFileName">Destination file name and path</param>
		/// <returns>Success flag</returns>
		public static bool MoveFile(string sourceFileName, string destinationFileName)
		{
			T.E();
			bool successFlag = false;
			try
			{
				// Rename and move the destination file if it exists
				if (FileExists(destinationFileName))
				{
					string newDestinationFile = Path.GetDirectoryName(destinationFileName) + @"\"
						+ Path.GetFileNameWithoutExtension(sourceFileName)
						+ @"_" + formatDateToSring(DateTime.Now)
						+ Path.GetExtension(destinationFileName);

					MoveFile(destinationFileName, newDestinationFile);
				}

				// Move file to destination
				File.Move(sourceFileName, destinationFileName);

				// Check the file has been moved
				successFlag = FileExists(destinationFileName);
			}
			catch (PathTooLongException ex)
			{
				T.DumpException(ex);
				throw new PathTooLongException(string.Format("The path and/or filename are too long for the operating system for the specified file: {0}", sourceFileName), ex);
			}
			catch (DirectoryNotFoundException ex)
			{
				T.DumpException(ex);
				throw new DirectoryNotFoundException(string.Format("The specified directory path does not exists for file: {0}", sourceFileName), ex);
			}
			catch (Exception ex)
			{
				T.DumpException(ex);
				throw new Exception(string.Format("There was a problem trying to move the specified file: {0}", sourceFileName), ex);
			}
			finally
			{
				T.X();
			}

			return successFlag;
		}

		/// <summary>
		/// Create a new file name for the specified file.  If the file already
		/// exists then apply the file creation date to the file name
		/// </summary>
		/// <param name="fileName">Existing file</param>
		/// <param name="prefix">Any prefix to be applied to the filename</param>
		/// <returns>New file name</returns>
		public static string LoadNewFileNameWithDate(string fileName, string prefix)
		{
			T.E();
			string fileCreateDate = string.Empty;
			string newFileName = string.Empty;

			try
			{
				// When the file already exists use existing attributes
				if (FileExists(fileName))
				{
					// Extract the file creation date
					fileCreateDate = formatDateToSring(File.GetCreationTime(fileName));

					// Apply the current name, date, and extension
					newFileName = prefix.Trim() + Path.GetFileNameWithoutExtension(fileName) + "_" +
						fileCreateDate + Path.GetExtension(fileName);
				}
				else
				{
					// Pick up the current date and time
					fileCreateDate = formatDateToSring(DateTime.Today);
					newFileName = Path.GetFileNameWithoutExtension(fileName) + "_" +
						fileCreateDate + Path.GetExtension(fileName);
				}
			}
			catch (Exception ex)
			{
				T.DumpException(ex);
				throw new Exception(string.Format("There was a problem generating a new file name for: {0}", fileName), ex);
			}
			finally
			{
				T.X();
			}

			return newFileName;
		}

		/// <summary>
		/// Create the specified file
		/// </summary>
		/// <param name="fileName"></param>
		/// <param name="addDummyLine"></param>
		/// <returns></returns>
		public static bool Createfile(string fileName, bool addDummyLine)
		{
			T.E();
			try
			{
				// Delete the file if it exists.  
				// It's up to the client to do anything with it!
				if (FileExists(fileName))
				{
					File.Delete(fileName);
				}

				// Create the file.
				using (FileStream fs = File.Create(fileName, 1024))
				{
					if (addDummyLine)
					{
						Byte[] info = new UTF8Encoding(true).GetBytes("This is some text in the file.");
						// Add some information to the file.
						fs.Write(info, 0, info.Length);
					}
				}
			}
			catch (PathTooLongException ex)
			{
				T.DumpException(ex);
				throw new PathTooLongException(string.Format("The path and/or filename is too long for the operating system for the specified file: {0}", fileName), ex);
			}
			catch (DirectoryNotFoundException ex)
			{
				T.DumpException(ex);
				throw new DirectoryNotFoundException(string.Format("The specified directory path does not exists for file: {0}", fileName), ex);
			}
			catch (Exception ex)
			{
				T.DumpException(ex);
				throw new Exception(string.Format("There was a problem trying to test if the specified file exists: {0}", fileName), ex);
			}
			finally
			{
				T.X();
			}

			return (FileExists(fileName));
		}

		/// <summary>
		/// Create a specified folder
		/// </summary>
		/// <param name="folderPath"></param>
		/// <returns>Success flag</returns>
		public static bool CreateFolder(string folderPath)
		{
			if (!Directory.Exists(folderPath))
			{
				try
				{
					// The one and only line of code doing something other than throwing an exception!
					Directory.CreateDirectory(folderPath);
				}
				catch (ArgumentNullException ex)
				{
					T.DumpException(ex);
					throw new ArgumentNullException("Target path is a null reference");
				}
				catch (PathTooLongException ex)
				{
					T.DumpException(ex);
					throw new PathTooLongException(string.Format("The path is too long for the operating system for the specified folder: {0}", folderPath), ex);
				}
				catch (DirectoryNotFoundException ex)
				{
					T.DumpException(ex);
					throw new DirectoryNotFoundException(string.Format("The specified path ({0}) is invalid, such as being on an unmapped drive", folderPath), ex);
				}
				catch (UnauthorizedAccessException ex)
				{
					T.DumpException(ex);
					throw new UnauthorizedAccessException(string.Format("You do not have the required permission to create the folder {0}", folderPath), ex);
				}
				catch (ArgumentException ex)
				{
					T.DumpException(ex);
					throw new ArgumentException("the target folder path is a zero-length string, contains only white space, or contains one or more invalid characters", ex);
				}
				catch (IOException ex)
				{
					T.DumpException(ex);
					throw new IOException(string.Format("{0} is read-only or is not empty", folderPath), ex);
				}
				catch (NotSupportedException ex)
				{
					T.DumpException(ex);
					throw new NotSupportedException("Creating a directory with only the colon (:) character was attempted", ex);
				}
				catch (Exception ex)
				{
					T.DumpException(ex);
					throw new Exception(string.Format("There was a problem trying to test if the specified file exists: {0}", folderPath), ex);
				}
				finally
				{
					T.X();
				}
			}

			return (Directory.Exists(folderPath));
		}

		/// <summary>
		/// Get the folder path from the file name and path
		/// </summary>
		/// <param name="fileName">File name and path</param>
		/// <returns>The folder path</returns>
		public static string LoadFilePath(string fileName)
		{
			T.E();
			string directoryName = string.Empty;
			try
			{
				// Extract the directory path from the filename
				directoryName = Path.GetDirectoryName(fileName);

				// Now test the director path exists
				if (!Directory.Exists(directoryName))
				{
					throw new DirectoryNotFoundException(string.Format("The specified directory path does not exists for file: {0}", fileName));
				}
			}
			catch (PathTooLongException ex)
			{
				T.DumpException(ex);
				throw new PathTooLongException(string.Format("The path and/or filename is too long for the operating system for the specified file: {0}", fileName), ex);
			}
			catch (DirectoryNotFoundException ex)
			{
				T.DumpException(ex);
				throw new DirectoryNotFoundException(string.Format("The specified directory path does not exists for file: {0}", fileName), ex);
			}
			finally
			{
				T.X();
			}

			return (directoryName);
		}


		/// <summary>
		/// Join a filename and pathname together
		/// </summary>
		/// <param name="pathname"></param>
		/// <param name="filename"></param>
		/// <returns></returns>
		public static string BuildFilePath(string pathname, string filename)
		{
			if (!pathname.EndsWith("\\") && !filename.StartsWith("\\"))
			{
				pathname += "\\";
			}

			return (pathname + filename);
		}

		#endregion

		#region Date methods

		/// <summary>
		/// Format the date time into a string to be used within a file name
		/// </summary>
		/// <param name="dateTime">DateTime object</param>
		/// <returns>A formatted string of the date time: yyyyMMddHHmmss</returns>
		private static string formatDateToSring(DateTime dateTime)
		{
			T.E();
			string dateFormat = string.Format("{0:yyyyMMddHHmmss}", dateTime);
			T.X();
			return (dateFormat);
		}

		#endregion

	}
}