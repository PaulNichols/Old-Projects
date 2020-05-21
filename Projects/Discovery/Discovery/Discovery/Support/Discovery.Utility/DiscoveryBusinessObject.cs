using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using ValidationFramework;

/// <summary>
/// This is a base object that all Discovery business objects ultimately derive from. Currently this type will allow be used to identify properties that are our own type when performing reflection operations. This will be useful in the CBO. Also we may be able to strongly type collections and the like more so than by simply specifying arguments of Object.
/// </summary>
namespace Discovery.BusinessObjects
{
    [Serializable]
    public abstract class DiscoveryBusinessObject : ValidatableBase
    {
        #region Private Fields

        #endregion

        #region Public Properties

        #endregion

        #region Public Method(s)

        #endregion

        #region Protected Method(s)

        /// <summary>
        /// Create a "deep" clone of 
        /// an object. That is, copy not only
        /// the object and its pointers
        /// to other objects, but create 
        /// copies of all the subsidiary 
        /// objects as well. This code even 
        /// handles recursive relationships.
        /// </summary>
        /// <returns></returns>
        public T DeepClone<T>() where T : DiscoveryBusinessObject
        {

            object objResult = null;
            if (this != null)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    bf.Serialize(ms, this);

                    // Rewind back to the beginning 
                    // of the memory stream. 
                    // Deserialize the data, then
                    // Close the memory stream.
                    ms.Position = 0;
                    objResult = bf.Deserialize(ms);
                }
            }
            return (T)objResult;
        }

        #endregion

        #region Private Method(s)

        #endregion

        #region Constructor(s)

        #endregion
    }

    /// <summary>
    /// All business objects that will be persisted to the database should inherit from this class.
    /// </summary>
    [Serializable]
    public abstract class PersistableBusinessObject : DiscoveryBusinessObject, IEquatable<PersistableBusinessObject>
    {
        #region Private Fields

        private int checkSum;
        private string updatedBy;
        private DateTime updatedDate;
        private bool archived;
        private int id;

        #endregion

        #region Public Properties

        /// <summary>
        ///The Id of the User that added or updated the corresponding record in the database.
        /// </summary>
        /// <value>A User Id.</value>
        public virtual string UpdatedBy
        {
            get { return updatedBy; }
            set { updatedBy = value; }
        }

        /// <summary>
        /// Gets or Sets the date and time a User added or updated the corresponding record in the database.
        /// </summary>
        /// <value>The updated date.</value>
        public virtual DateTime UpdatedDate
        {
            get { return updatedDate; }
            set { updatedDate = value; }
        }

        /// <summary>
        /// Gets or sets The unique identifier for this data, this corresponds with the primary key field in the database. 
        ///If this value is null this will signify that the data, when saved, will trigger an insert into the database rather than an update.</summary>
        /// <value>The primary ID.</value>
        public virtual int Id
        {
            get { return id; }
            set { id = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is archived.
        /// When this indicator is set it means that the archive process will pick it up when it next runs and achieve the record.</summary>
        /// <value>
        /// 	<c>true</c> if this instance is archived; otherwise, <c>false</c>.
        /// </value>
        public virtual bool IsArchived
        {
            get { return archived; }
            set { archived = value; }
        }

        /// <summary>
        /// Gets or sets the check sum. This binary checksum value has been created from the state of all fields within the corresponding database record at the time of retrieving the data. This value can be used to highlight concurrency issues when data is saved back to the database and potentially when data is deleted also.</summary>
        /// <value>The check sum.</value>
        public int CheckSum
        {
            get { return checkSum; }
            set { checkSum = value; }
        }

        /// <summary>
        /// Gets a value indicating whether this instance can update. This read-only property must be overridden by inheritors, if no custom check is applicable to the specific business object then true should be returned otherwise a check should be performed, in the example of a Shipment the status could be checked allowing only Shipments at the correct status to be updated.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance can update; otherwise, <c>false</c>.
        /// </value>
        public  virtual bool CanUpdate
        {
            get
            {
                return true;
            }
        }

        #endregion

        #region Public Method(s)


        /// <summary>
        /// Serves as a hash function for a particular type. <see cref="M:System.Object.GetHashCode"></see> is suitable for use in hashing algorithms and data structures like a hash table.
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="T:System.Object"></see>.
        /// </returns>
        public override int GetHashCode()
        {
            return Id;
        }

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object"></see> is equal to the current <see cref="T:System.Object"></see>.
        /// </summary>
        /// <param name="obj">The <see cref="T:System.Object"></see> to compare with the current <see cref="T:System.Object"></see>.</param>
        /// <returns>
        /// true if the specified <see cref="T:System.Object"></see> is equal to the current <see cref="T:System.Object"></see>; otherwise, false.
        /// </returns>
        public override bool Equals(object obj)
        {
            return (Equals(obj as PersistableBusinessObject));
        }

        ///<summary>
        ///Indicates whether the current object is equal to another object of the same type.
        ///</summary>
        ///
        ///<returns>
        ///true if the current object is equal to the other parameter; otherwise, false.
        ///</returns>
        ///
        ///<param name="other">An object to compare with this object.</param>
        public bool Equals(PersistableBusinessObject other)
        {
            if (this == other) return true;
            if (other == null) return false;
            if (Id != other.Id) return false;
            return true;
        }

        #endregion

        #region Protected Method(s)

        #endregion

        #region Private Method(s)

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Initializes a new instance of the <see cref="T:PersistableBusinessObject"/> class.
        /// </summary>
        public PersistableBusinessObject()
            : base()
        {
            Id = -1;
        }

        #endregion
    }
}