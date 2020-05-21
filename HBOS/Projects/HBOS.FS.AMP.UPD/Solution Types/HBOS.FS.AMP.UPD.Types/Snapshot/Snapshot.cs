using System;

namespace HBOS.FS.AMP.UPD.Types.Snapshot
{
	/// <summary>
	/// Summary description for Snapshot.
	/// </summary>
	public class Snapshot: EntityBase
	{
		/// <summary>
		/// Creates a new <see cref="Snapshot"/> instance.
		/// </summary>
		public Snapshot(
			long id,
			string userId,
			DateTime snapshotDate,
			SnapshotProcess process,
			string importFilename,
			string companyCode,
			byte[] timestamp
			)
		{
			this.id = id;
			this.userId = userId;
			this.snapshotDate = snapshotDate;
			this.process = process;
			this.importFilename = importFilename;
			this.companyCode = companyCode;
			
			TimeStamp = timestamp;
			IsDirty = false;
			IsDeleted = false;
			IsNew = false;
		}

		private long id;
		private string userId;
		private DateTime snapshotDate;
		private SnapshotProcess process;
		private string importFilename;
		private string companyCode;

		/// <summary>
		/// Gets the id for the snapshot.
		/// </summary>
		/// <value></value>
		public long Id
		{
			get
			{
				return id;
			}
		}

		/// <summary>
		/// Gets the user id for the person who created the snapshot.
		/// </summary>
		/// <value></value>
		public string UserId
		{
			get
			{
				return userId;
			}
		}

		/// <summary>
		/// Gets the date and time the snapshot was created.
		/// </summary>
		/// <value></value>
		public DateTime SnapshotDate
		{
			get
			{
				return snapshotDate;
			}
		}

		/// <summary>
		/// Gets the process that created the snapshot.
		/// </summary>
		/// <value></value>
		public SnapshotProcess Process
		{
			get
			{
				return process;
			}
		}

		/// <summary>
		/// Gets the import filename for import snapshots only. Null otherwise.
		/// </summary>
		/// <value></value>
		public string ImportFilename
		{
			get
			{
				if (Process == SnapshotProcess.Import)
				{
					return importFilename;
				}
				else
				{
					return null;
				}
			}
		}

		/// <summary>
		/// Gets the company code for the snapshot.
		/// </summary>
		/// <value></value>
		public string CompanyCode
		{
			get
			{
				return companyCode;
			}
		}
	}

	/// <summary>
	/// Describes the Process used to create a snapshot
	/// </summary>
	public enum SnapshotProcess
	{
		/// <summary>
		/// Snapshot was created from a data Import
		/// </summary>
		Import,
		/// <summary>
		/// Snapshot was created from the static data screens
		/// </summary>
		StaticData,
		/// <summary>
		/// Reports that require imports such as the price comparision report
		/// </summary>
		Report,
		/// <summary>
		/// Snapshot was created from an unknown source
		/// </summary>
		Unknown
	}
}
