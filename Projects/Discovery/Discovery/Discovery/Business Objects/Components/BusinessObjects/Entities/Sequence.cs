using System;
using System.Collections.Generic;
using System.Text;
using ValidationFramework;

//using Discovery.ComponentServices.Parsing;

namespace Discovery.BusinessObjects
{
 /*************************************************************************************************
  ** CLASS:	Sequence
  **
  ** OVERVIEW:
  ** 
  **
  ** MODIFICATION HISTORY:
  **
  ** Date:		Version:    Who:	Change:
  ** 19/7/06	    1.0	    LAS		Initial Version
  ************************************************************************************************/
    /// <summary>
    /// A Class 'Sequence' which is an entity with namespace Discovery.BusinessObjects
    /// It is inherited from PersistableBusinessObject
    /// The class holds the sequence's fields such as name, seed, increment and current value
    /// </summary>
    [Serializable]
    public class Sequence : PersistableBusinessObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Sequence"/> class.
        /// </summary>
        public Sequence()
            : base()
        {
        }

        #region Private Fields

        private string name;
        private int seed;
        private int increment;
        private int currentValue;

        #endregion

        #region Public Method(s)

        #endregion

        #region Protected Method(s)

        #endregion

        #region Private Method(s)

        #endregion

        #region Constructor(s)

        #endregion

        #region Public Properties
      
        /// <summary>
        /// Gets or sets the sequence name.
        /// </summary>
        /// <value>The sequence name.</value>
        [RequiredValidator("Name is required.", "*")]
        [LengthValidator(10, "The maximum length of a Name is 255 characters.", "*")]
        public string Name
        {
            get { return name; }
            set { name = value.ToUpper(); }
        }

        /// <summary>
        /// Gets or sets the sequence seed, eg 0 or 1.
        /// </summary>
        /// <value>The sequence seed.</value>
        [RequiredValidator("Seed is required.", "*")]
        public int Seed
        {
            get { return seed; }
            set { seed = value; }
        }

        /// <summary>
        /// Gets or sets the sequence increment, eg 1 or 10.
        /// </summary>
        /// <value>The sequence seed.</value>
        [RequiredValidator("Increment is required.", "*")]
        public int Increment
        {
            get { return increment; }
            set 
            {
                if (value <= 0)
                {
                    throw new Exception("The sequence increment must be a value greater than 0 (zero).");
                }
                increment = value; 
            }
        }

        /// <summary>
        /// Gets the current value of the sequence.
        /// </summary>
        /// <value>The current sequence value.</value>
        public int CurrentValue
        {
            get { return currentValue; }
            set { currentValue = value; }
        }

        #endregion
    }
}


