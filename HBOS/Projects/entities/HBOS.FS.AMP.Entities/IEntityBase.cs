using System;

namespace HBOS.FS.AMP.Entities
{
    /// <summary>
    /// The base object for all persistable objects.
    /// </summary>
    public interface IEntityBase
    {
        /// <summary>
        /// Flag to show whether the object's data has been changed or not.  If
        /// true then the data has changed since it was last persisted.
        /// </summary>
        bool IsDirty
        {
            get;
            set;
        }

        /// <summary>
        /// Flag to show whether this object is a new or existing object.  Used to call the correct
        /// method on persistence.
        /// </summary>
        bool IsNew
        {
            get;
            set;
        }

        /// <summary>
        /// Flag to show if the objects data has been deleted.  Used to show whether the data
        /// has had its deleted status undon.
        /// </summary>
        bool IsDeleted
        {
            get;
            set;
        }

        /// <summary>
        /// The date-time stamp of the last update to the object's source data. Note that this
        /// isn't the date-time stamp of the last IN MEMORY change of the object's data, and
        /// will not indicate, for instance, the time of the last call to the .IsDirty set.
        /// It is used to ensure that any database underlying the object has not had its data changed
        /// since the one in memory was loaded.
        /// </summary>
        byte[] TimeStamp
        {
            get;
            set;
        }
    }
}
