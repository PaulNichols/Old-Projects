using System;
using System.Runtime.InteropServices;
using CustomToolGenerator;
using EnvDTE;
using VSLangProj;
using System.Diagnostics;

namespace DiscoCodeGeneratorEx
{
	/// <summary>
	/// Summary description for CodeGen.
	/// </summary>
	[Guid("ACD090C3-A949-429a-8889-33F0937B8F30")]
	public class CodeGen : BaseCodeGeneratorWithSite
	{
		public override byte[] GenerateCode(string inputFileName, string inputFileContent)
		{
			object serviceObject = GetService(typeof(ProjectItem));
			Debug.Assert(serviceObject != null, "Unable to get Project Item.");
			if (serviceObject == null) 
			{
				string errorMessage = "Unable to add DLL to project references.";
				GeneratorErrorCallback(false, 1, errorMessage, 0, 0);
				return null;
			}

			Project containingProject = ((ProjectItem) serviceObject).ContainingProject;
			Debug.Assert(containingProject != null, "GetService(typeof(Project)) return null.");
			if (containingProject == null) 
			{
				string errorMessage = "Unable to add DLL to project references.";
				GeneratorErrorCallback(false, 1, errorMessage, 0, 0);
				return null;
			}

			GetOutputWindow();
			if (newOutputWindow != null)
			{
				//start with a clean window
				newOutputWindow.Activate();
				LogMessages("Starting...");
			}

			CreateCode.CreateCode classCode = new CreateCode.CreateCode(inputFileContent, inputFileName, new CreateCode.Delegates.LogMessageDelegate(LogMessages));
			string code = classCode.GetCode;

			//string code = "";
			LogMessages("Finished...");
			newOutputWindow = null;
			classCode = null;
			return System.Text.Encoding.ASCII.GetBytes(code);
		}

		private EnvDTE.OutputWindowPane newOutputWindow = null;
		private void GetOutputWindow()
		{
			if (newOutputWindow == null)
			{
				object serviceObject = GetService(typeof(ProjectItem));
				Debug.Assert(serviceObject != null, "Unable to get Project Item.");
				if (serviceObject == null) 
				{
					string errorMessage = "Unable to add DLL to project references.";
					GeneratorErrorCallback(false, 1, errorMessage, 0, 0);
					//				return null;
				}
				else
				{
					// get the output window
					EnvDTE.Window win = ((ProjectItem) serviceObject).DTE.Windows.Item(EnvDTE.Constants.vsWindowKindOutput);
					win.Activate();
					//and the internal object (.nets window)
					EnvDTE.OutputWindow outputWindow = (EnvDTE.OutputWindow)win.Object;
					try
					{
						// have we a curent pane?
						newOutputWindow = outputWindow.OutputWindowPanes.Item("Proxy Builder");
					}
					catch 
					{
						// no, so create...strangely it errors, so it must be handled this way
						newOutputWindow = outputWindow.OutputWindowPanes.Add("Proxy Builder");
					}
				}
			}
		}

		public void LogMessages(string message)
		{
			GetOutputWindow();
			if (newOutputWindow != null && message != null && message.Length > 0)
			{
				// write the text and append a new line after
				newOutputWindow.OutputString(message + "\n");
				System.Windows.Forms.Application.DoEvents();
			}
		}
	}
}
