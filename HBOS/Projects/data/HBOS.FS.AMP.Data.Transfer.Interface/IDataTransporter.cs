using System;

namespace HBOS.FS.AMP.Data.Transfer.Interface
{
	/// <summary>
	/// Data Transporter contains the functionality to transfer data from a data source to a data sink.
	/// </summary>
	public interface IDataTransporter
	{
		/// <summary>
		/// Run the Data Transfer
		/// </summary>
		void RunDataTransfer();
	}
}