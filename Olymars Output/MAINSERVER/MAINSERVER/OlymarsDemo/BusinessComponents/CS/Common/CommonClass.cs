/*
	This C# source code was automatically generated using:

		SQL Server Centric .NET Code Generator
			v 1.0.1697.21165

			Generation Date: 28/12/2004 11:52:11
			Generator name: MAINSERVER\Administrator
			Template last update: 27/12/2004 16:52:27
			Template revision: 1247

			SQL Server version: 08.00.0760
			Server: MAINSERVER\MAINSERVER
			Database: [OlymarsDemo]

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

namespace OlymarsDemo.BusinessComponents {

	/// <summary>
	/// [To be supplied.]
	/// </summary>
	public interface IBusinessComponentRecord {

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		bool Refresh();
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		void Update();
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		string ToString();
	}

	/// <summary>
	/// [To be supplied.]
	/// </summary>
	public interface IBusinessComponentCollection {

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		string ConnectionString { get; }
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		IBusinessComponentRecord Add(IBusinessComponentRecord record); 
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		void Remove(IBusinessComponentRecord record);
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		void Refresh();
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		IBusinessComponentRecord Parent { get; }
	}
}
