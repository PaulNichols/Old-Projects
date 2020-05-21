/*
	This C# source code was automatically generated using:

		SQL Server Centric .NET Code Generator
			v 1.0.1697.21165

			Generation Date: 15/01/2005 18:39:10
			Generator name: MAINSERVER\Administrator
			Template last update: 13/10/2003 04:51:40
			Template revision: 56177501

			SQL Server version: 08.00.0760
			Server: MAINSERVER\MAINSERVER
			Database: [Bob]

	WARNING: This source is provided "AS IS" without warranty of any kind.
	The author disclaims all warranties, either express or implied, including
	the warranties of merchantability and fitness for a particular purpose.
	In no event shall the author or its suppliers be liable for any damages
	whatsoever including direct, indirect, incidental, consequential, loss of
	business profits or special damages, even if the author or its suppliers
	have been advised of the possibility of such damages.

	    More information: http://www.microsoft.com/france/msdn/olymars
	Latest interim build: http://www.olymars.net/latest.zip
	       Author's blog: http://blogs.msdn.com/olymars
*/

namespace Bob.DataClasses {

	/// <summary>
	/// Represents a custom exception. This exception can be thrown by any class of the Bob namespace.
	/// </summary>
	public class CustomException : System.Exception	{

		private IParameter parameter;
		private string className;
		private string methodName;
		
		/// <summary>
		/// Initializes a new instance of the CustomException class.
		/// </summary>
		/// <param name="parameter">IParameter object involved when the exception has been thrown.</param>
		/// <param name="className">Name of the class where the exception has been thrown.</param>
		/// <param name="methodName">Name of the method where the exception has been thrown.</param>
		public CustomException(IParameter parameter, string className, string methodName) {

			this.parameter = parameter;
			this.className = className;
			this.methodName = methodName;
		}
		
		/// <summary>
		/// IParameter object involved when the exception has been thrown.
		/// </summary>
		public IParameter Parameter {

			get {

				return(this.parameter);
			}
		}

		/// <summary>
		/// Name of the class where the exception has been thrown.
		/// </summary>
		public string ClassName {

			get {

				return(this.className);
			}
		}

		/// <summary>
		/// Name of the method where the exception has been thrown.
		/// </summary>
		public string MethodName {

			get {

				return(this.methodName);
			}
		}
	}
}
