using System;
using HBOS.FS.AMP.Entities;

namespace HBOS.FS.AMP.UPD.Types
{
	/// <summary>
	/// 
	/// </summary>
	[Serializable]
	public class EntityBase : IEntityBase
	{
		/// <summary>
		/// Dirt flag
		/// </summary>
		protected bool m_isDirty;
		/// <summary>
		/// New Flag
		/// </summary>
		protected bool m_isNew = true; //existing UPD 2.0 always initially sets to true, so set to false in ctor used by persister
		/// <summary>
		/// 
		/// </summary>
		protected bool m_isDeleted;
		/// <summary>
		/// 
		/// </summary>
		protected byte[] m_timestamp;

		/// <summary>
		/// Set the modified flag to be true
		/// </summary>
		protected void SetDirtyFlag()
		{
			//
			// TODO: Possibly check for initial loading and populating code here
			//
			this.m_isDirty = true;
		}

		/// <summary>
		/// Returns true if the object needs to be updated in the database.
		/// Otherwise false.
		/// </summary>
		public virtual bool IsDirty
		{
			get
			{
				return m_isDirty;
			}
			set
			{
				m_isDirty = value;
			}
		}

		/// <summary>
		/// Returns true if the object is a newly created object being added to the database.
		/// </summary>
		public bool IsNew
		{
			get
			{
				return m_isNew;
			}

			set
			{
				m_isNew = value;
			}
		}

		/// <summary>
		/// Returns true if the object has been flagged as deleted.  Set to false if the
		/// data has been un-deleted
		/// </summary>
		public bool IsDeleted
		{
			get 
			{
				return m_isDeleted;
			} 

			set 
			{
				m_isDeleted = value;
			}
		}

		/// <summary>
		/// Returns the time stamp of the data 
		/// </summary>
		public byte[] TimeStamp
		{
			get
			{
				// make copy of the time stamp byte array
				return m_timestamp;     //m_timeStamp.Clone()
			}

			set
			{
				m_timestamp = value;
			}
		}
	}
}