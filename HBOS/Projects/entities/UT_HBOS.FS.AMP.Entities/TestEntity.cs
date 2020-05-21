using System;

using HBOS.FS.AMP.Entities;

namespace UT_HBOS.FS.AMP.Entities
{
	/// <summary>
	/// Summary description for TestEntity.
	/// </summary>
	public class TestEntity : IEntityBase
	{
        private string m_name;
        private string m_fullName;

        // Flags for IEntityBase implementation
        private bool m_isDirty;
        private bool m_isNew;
        private bool m_isDeleted;
        private byte[] m_timeStamp;

        
        public TestEntity()
		{
            m_name = "";
            m_fullName = "";

            //Set up IEntityBase members
            m_isNew = true;
            m_isDeleted = false;
            m_timeStamp = new byte[1];
            m_isDirty = true;

        }

        [NotNull]
        public string Name
        {
            get
            {
                return m_name;
            }
            set
            {
                m_name = value;
            }
        }

        [MaxLength(10)]
        [MinLength(15)]
        public string FullName
        {
            get
            {
                return m_fullName;
            }
            set
            {
                m_fullName = value;
            }
        }

        #region IEntityBase Members

        /// <summary>
        /// Flag indicating that values have been modified
        /// </summary>
        public bool IsDirty
        {
            get { return this.m_isDirty; }
            set { this.m_isDirty = value; }
        }

        /// <summary>
        /// Returns true if the object is a newly created object being added to the database.
        /// </summary>
        public bool IsNew
        {
            get	{ return m_isNew; }
            set	{ m_isNew = value; }
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
        /// Returns the timestamp of the data 
        /// </summary>
        public byte[] TimeStamp
        {
            get
            {
                // make copy of the time stamp byte array
                return m_timeStamp;     //m_timeStamp.Clone();
            }

            set
            {
                m_timeStamp = value;
            }
        }

        #endregion
    }
}
