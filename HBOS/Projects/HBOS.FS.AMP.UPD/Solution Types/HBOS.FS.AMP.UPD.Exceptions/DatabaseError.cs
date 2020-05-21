using System;

namespace HBOS.FS.AMP.UPD.Exceptions
{
	/// <summary>
	/// Database Errors (from sysmessages)
	/// </summary>
	public enum DatabaseError : int
	{
		/// <summary>
		/// Sql Server does not exist (17)
		/// </summary>
		None = 0,

		/// <summary>
		/// Sql Server does not exist (17)
		/// </summary>
		SQLServerDoesNotExist = 17,

		/// <summary>
		/// Null Parameter (515)
		/// </summary>
		NullParameter = 515,

		/// <summary>
		/// Constraint Violation (547)
		/// </summary>
		ConstraintViolation = 547,

		/// <summary>
		/// Constraint Violation Duplicate Key (2627)
		/// </summary>
		ConstraintViolationDuplicateKey = 2627,

		/// <summary>
		/// No Database Access (4060)
		/// </summary>
		NoDatabaseAccess = 4060,

		/// <summary>
		/// Login Failed not associated with trusted connection (18452)
		/// </summary>
		LoginFailedNotAssociatedWithTrustedConnection = 18452,

		/// <summary>
		/// Login Failed (18456)
		/// </summary>
		LoginFailed = 18456,

        /// <summary>
        /// Custom error was raised which is not in sysmessages (50000)
        /// </summary>
        CustomError = 50000
	}
}
