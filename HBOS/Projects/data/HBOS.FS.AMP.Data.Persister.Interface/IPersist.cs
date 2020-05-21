using System;

namespace HBOS.FS.AMP.Data.Persister.Interface
{
	/// <summary>
	/// Base Interface definition for persisting data to various data sinks
	/// </summary>
	public interface IPersist
	{
		/// <summary>
		/// Initialise the transfer
		/// </summary>
		void InitialiseTransfer();

		/// <summary>
		/// Complete the transfer
		/// </summary>
		void CompleteTransfer();

		/// <summary>
		/// Cancel the transfer
		/// </summary>
		void CancelTransfer();

		/// <summary>
		/// Is there a transfer in progress
		/// </summary>
		bool IsTransferInProgress{get;}
	}
}
