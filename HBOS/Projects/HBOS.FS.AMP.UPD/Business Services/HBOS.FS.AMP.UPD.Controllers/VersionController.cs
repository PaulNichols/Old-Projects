using System;
using HBOS.FS.AMP.UPD.Persistence;
using HBOS.FS.Support.Tex;

namespace HBOS.FS.AMP.UPD.Controllers
{
	/// <summary>
	/// Provides Business Logic for controlling versioning
	/// </summary>
	public abstract /*static*/ class VersionController
	{
		/// <summary>
		/// Creates a new <see cref="VersionController"/> instance.
		/// Private constructor to prevent construction of a static class
		/// </summary>
		private VersionController()
		{
		}

		/// <summary>
		/// Verifies the version number
		/// </summary>
		/// <param name="connectionString">String to use for database connection</param>
		/// <param name="version">Version to verify.</param>
		/// <returns></returns>
		public static bool VerifyVersion(string connectionString,Version version)
		{
			T.E();
			VersionPersister persister = new VersionPersister(connectionString);
			bool result = persister.VerifyVersion(version);
			T.X();
			return result;
		}
	}
}
