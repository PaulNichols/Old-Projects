/*************************************************************************************************
 ** FILE:	Mapping.cs
 ** DATE:	30/05/2006
 ** AUTHOR:	Pail Nichols
 **
 **
 ** OVERVIEW:
 **
 ** MODIFICATION HISTORY:
 **
 ** Date:		Version:	Who:	Change:
 ** 30/5/06		1.0		    PJN	    Initial Version
 ************************************************************************************************/
using System;
using ValidationFramework;

namespace Discovery.BusinessObjects
{
    /// <summary>
    /// A Class 'Mapping' which is an entity with namespace Discovery.BusinessObjects
    /// It is inherited from PersistableBusinessObject
    /// The class holds the mapping details from the source to the detination
    /// </summary>
    [Serializable]
    public class Mapping : PersistableBusinessObject
    {
        #region Private Fields

        private string sourceValue;
        private string destinationValue;
        private int mappingPropertyAssociationId;
        private MappingPropertyAssociation mappingPropertyAssociation;
        private int destinationSystemId;
        private int sourceSystemId;
        private MappingSystem destinationSystem;
        private MappingSystem sourceSystem;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the source value. The value to map from, in the case of Shipment Mapping this would be an OpCo value.
        /// </summary>
        /// <value>The source value.</value>
        [RequiredValidator("Source Value is required.", "*")]
        [LengthValidator(256, "The maximum length of a Source Value is 256 characters.", "*")]
        public string SourceValue
        {
            get { return sourceValue; }
            set { sourceValue = value; }
        }

        /// <summary>
        /// Gets or sets the destination value. The value to map to, in the case of Shipment Mapping this would be a TDC value.
        /// </summary>
        /// <value>The destination value.</value>
        [RequiredValidator("Destination Value is required.", "*")]
        [LengthValidator(256, "The maximum length of a Destination Value is 256 characters.", "*")]
        public string DestinationValue
        {
            get { return destinationValue; }
            set { destinationValue = value; }
        }


       
        /// <summary>
        /// Gets or sets the mapping property association id.
        /// </summary>
        /// <value>The mapping property association id.</value>
        public int MappingPropertyAssociationId
        {
            get { return mappingPropertyAssociationId; }
            set { mappingPropertyAssociationId = value; }
        }

        /// <summary>
        /// Gets or sets the mapping property association.
        /// </summary>
        /// <value>The mapping property association.</value>
        public MappingPropertyAssociation MappingPropertyAssociation
        {
            get { return mappingPropertyAssociation; }
            set { mappingPropertyAssociation = value; }
        }

        /// <summary>
        /// Gets or sets the destination system id.
        /// </summary>
        /// <value>The destination system id.</value>
        [RequiredValidator("Destination System is required.", "*")]
        [CompareValidator(-1, ValidationCompareOperator.NotEqual, "Destination System is required.", "*")]
        public int DestinationSystemId
        {
            get { return destinationSystemId; }
            set { destinationSystemId = value; }
        }

        /// <summary>
        /// Gets or sets the source system id.
        /// </summary>
        /// <value>The source system id.</value>
        [RequiredValidator("Source System is required.", "*")]
        [CompareValidator(-1, ValidationCompareOperator.NotEqual, "Source System is required.", "*")]
        public int SourceSystemId
        {
            get { return sourceSystemId; }
            set { sourceSystemId = value; }
        }

        /// <summary>
        /// Gets or sets the destination system.
        /// </summary>
        /// <value>The destination system.</value>
        public MappingSystem DestinationSystem
        {
            get { return destinationSystem; }
            set { destinationSystem = value; }
        }

        /// <summary>
        /// Gets or sets the source system.
        /// </summary>
        /// <value>The source system.</value>
        public MappingSystem SourceSystem
        {
            get { return sourceSystem; }
            set { sourceSystem = value; }
        }

        #endregion

        #region Public Method(s)

        #endregion

        #region Protected Method(s)

        #endregion

        #region Private Method(s)

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Mapping"/> class.
        /// </summary>
        public Mapping()
        {
            mappingPropertyAssociation=new MappingPropertyAssociation();
            destinationSystem=new MappingSystem();
            sourceSystem=new MappingSystem();
                    }

        #endregion
    }

    /// <summary>
    /// A Class 'MappingSystem' which is an entity with namespace Discovery.BusinessObjects
    /// It is inherited from PersistableBusinessObject
    /// </summary>
    [Serializable]
    public class MappingSystem : PersistableBusinessObject
    {
        #region Private Fields

        private string name;
        private bool isSource;
        private bool isDestination;

        #endregion

        #region Public Properties



        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [RequiredValidator("Source System is required.", "*")]
        [LengthValidator(50, "The maximum length of a Source System is 50 characters.", "*")]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is source.
        /// </summary>
        /// <value><c>true</c> if this instance is source; otherwise, <c>false</c>.</value>
        public bool IsSource
        {
            get { return isSource; }
            set { isSource = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is destination.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is destination; otherwise, <c>false</c>.
        /// </value>
        public bool IsDestination
        {
            get { return isDestination; }
            set { isDestination = value; }
        }

        #endregion

        #region Public Method(s)

        #endregion

        #region Protected Method(s)

        #endregion

        #region Private Method(s)

        #endregion

        #region Constructor(s)

        #endregion
    }

    /// <summary>
    /// A Class 'MappingPropertyAssociation' which is an entity with namespace Discovery.BusinessObjects
    /// It is inherited from PersistableBusinessObject
    /// </summary>
    [Serializable]
    public class MappingPropertyAssociation : PersistableBusinessObject
    {
        #region Private Fields

        private string lookupTableName;
        private string lookUpTableDisplayColumn;
        private string sourceProperty;
        private string destinationProperty;
        private int mappingClassAssociationId;
        private MappingClassAssociation mappingClassAssociation;
        
        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the name of the lookup table.
        /// </summary>
        /// <value>The name of the lookup table.</value>
        public string LookupTableName
        {
            get { return lookupTableName; }
            set { lookupTableName = value; }
        }

        /// <summary>
        /// Gets or sets the look up table display column.
        /// </summary>
        /// <value>The look up table display column.</value>
        public string LookUpTableDisplayColumn
        {
            get { return lookUpTableDisplayColumn; }
            set { lookUpTableDisplayColumn = value; }
        }

        /// <summary>
        /// Gets or sets the source property. The name of the property on the SourceType to be mapped, this will be used during the reflection in the mapping method of the Mapping Controller.
        /// </summary>
        /// <value>The source property.</value>
        [RequiredValidator("Source Property is required.", "*")]
        [LengthValidator(100, "The maximum length of a Source Property is 100 characters.", "*")]
        public string SourceProperty
        {
            get { return sourceProperty; }
            set { sourceProperty = value; }
        }

        /// <summary>
        /// Gets or sets the destination property. The name of the property on the DestinationType to be mapped, this will be used during the reflection in the mapping method of the Mapping Controller.
        /// </summary>
        /// <value>The destination property.</value>
        [RequiredValidator("Destination Property is required.", "*")]
        [LengthValidator(100, "The maximum length of a Destination Property is 100 characters.", "*")]
        public string DestinationProperty
        {
            get { return destinationProperty; }
            set { destinationProperty = value; }
        }



        /// <summary>
        /// Gets or sets the mapping class association id.
        /// </summary>
        /// <value>The mapping class association id.</value>
        public int MappingClassAssociationId
        {
            get { return mappingClassAssociationId; }
            set
            {
                mappingClassAssociationId = value;
            }
        }

        /// <summary>
        /// Gets or sets the mapping class association.
        /// </summary>
        /// <value>The mapping class association.</value>
        public MappingClassAssociation MappingClassAssociation
        {
            get { return mappingClassAssociation; }
            set { mappingClassAssociation = value; }
        }

        #endregion

        #region Public Method(s)

        #endregion

        #region Protected Method(s)

        #endregion

        #region Private Method(s)

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Initializes a new instance of the <see cref="T:MappingPropertyAssociation"/> class.
        /// </summary>
        public MappingPropertyAssociation()
        {
            mappingClassAssociation=new MappingClassAssociation();
        }

        #endregion
    }

    /// <summary>
    /// A Class 'MappingClassAssociation' which is an entity with namespace Discovery.BusinessObjects
    /// It is inherited from PersistableBusinessObject
    /// </summary>
    [Serializable]
    public class MappingClassAssociation : PersistableBusinessObject
    {
        #region Private Fields

        private string sourceType;
        private string destinationType;
        private string sourceTypeFullName;
        private string destinationTypeFullName;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the type of the source. The fully qualified .Net type to map from, this will be used during reflection in the mapping method of the Mapping Controller.
        /// </summary>
        /// <value>The type of the source.</value>
        [RequiredValidator("Source Type is required.", "*")]
        [LengthValidator(256, "The maximum length of a Source Type is 256 characters.", "*")]
        public string SourceType
        {
            get { return sourceType; }
            set { sourceType = value; }
        }

        /// <summary>
        /// Gets or sets the type of the destination. The fully qualified .Net type to map to, this will be used during reflection in the mapping method of the Mapping Controller.
        /// </summary>
        /// <value>The type of the destination.</value>
        [RequiredValidator("Destination Type is required.", "*")]
        [LengthValidator(256, "The maximum length of a Destination Type is 256 characters.", "*")]
        public string DestinationType
        {
            get { return destinationType; }
            set { destinationType = value; }
        }

        /// <summary>
        /// Gets or sets the type of the source. The fully qualified .Net type to map from, this will be used during reflection in the mapping method of the Mapping Controller.
        /// </summary>
        /// <value>The type of the source.</value>
        [RequiredValidator("Source Type is required.", "*")]
        [LengthValidator(256, "The maximum length of a Source Type is 256 characters.", "*")]
        public string SourceTypeFullName
        {
            get { return sourceTypeFullName; }
            set { sourceTypeFullName = value; }
        }

        /// <summary>
        /// Gets or sets the type of the destination. The fully qualified .Net type to map to, this will be used during reflection in the mapping method of the Mapping Controller.
        /// </summary>
        /// <value>The type of the destination.</value>
        [RequiredValidator("Destination Type is required.", "*")]
        [LengthValidator(256, "The maximum length of a Destination Type is 256 characters.", "*")]
        public string DestinationTypeFullName
        {
            get { return destinationTypeFullName; }
            set { destinationTypeFullName = value; }
        }

       
        #endregion

        #region Public Method(s)

        #endregion

        #region Protected Method(s)

        #endregion

        #region Private Method(s)

        #endregion

        #region Constructor(s)

        #endregion
    }
}