/*
===============================================================================
                     EntitySpaces(TM) by EntitySpaces, LLC
                 A New 2.0 Architecture for the .NET Framework
                          http://www.entityspaces.net
===============================================================================
                       EntitySpaces Version # 2007.1.1210.0
                       MyGeneration Version # 1.3.0.3
                           17/02/2008 20:43:30
-------------------------------------------------------------------------------
*/


using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Xml.Serialization;
using EntitySpaces.Core;
using EntitySpaces.Interfaces;

namespace BusinessObjects
{
    [Serializable]
    public abstract class esOMAMFundPricesCollection : esEntityCollection
    {
        public esOMAMFundPricesCollection()
        {
        }

        protected override string GetCollectionName()
        {
            return "OMAMFundPricesCollection";
        }

        #region Query Logic

        protected void InitQuery(esOMAMFundPricesQuery query)
        {
            query.OnLoadEvent += new esDynamicQuery.QueryLoadedDelegate(OnQueryLoaded);
            query.es.Connection = ((IEntityCollection) this).Connection;
        }

        protected bool OnQueryLoaded(DataTable table)
        {
            PopulateCollection(table);
            return (RowCount > 0) ? true : false;
        }

        protected override void HookupQuery(esDynamicQuery query)
        {
            InitQuery(query as esOMAMFundPricesQuery);
        }

        #endregion

        public virtual OMAMFundPrices DetachEntity(OMAMFundPrices entity)
        {
            return base.DetachEntity(entity) as OMAMFundPrices;
        }

        public virtual OMAMFundPrices AttachEntity(OMAMFundPrices entity)
        {
            return base.AttachEntity(entity) as OMAMFundPrices;
        }

        public virtual void Combine(OMAMFundPricesCollection collection)
        {
            base.Combine(collection);
        }

        public new OMAMFundPrices this[int index]
        {
            get { return base[index] as OMAMFundPrices; }
        }

        public override Type GetEntityType()
        {
            return typeof (OMAMFundPrices);
        }
    }


    [Serializable]
    public abstract class esOMAMFundPrices : esEntity
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        protected virtual esOMAMFundPricesQuery GetDynamicQuery()
        {
            return null;
        }

        public esOMAMFundPrices()
        {
        }

        public esOMAMFundPrices(DataRow row)
            : base(row)
        {
        }

        #region LoadByPrimaryKey

        public virtual bool LoadByPrimaryKey(Int32 id)
        {
            if (es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(id);
            else
                return LoadByPrimaryKeyStoredProcedure(id);
        }

        /// <summary>
        /// Loads an entity by primary key
        /// </summary>
        /// <remarks>
        /// EntitySpaces requires primary keys be defined on all tables.
        /// If a table does not have a primary key set,
        /// this method will not compile.
        /// </remarks>
        /// <param name="sqlAccessType">Either esSqlAccessType StoredProcedure or DynamicSQL</param>
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 id)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(id);
            else
                return LoadByPrimaryKeyStoredProcedure(id);
        }

        private bool LoadByPrimaryKeyDynamic(Int32 id)
        {
            esOMAMFundPricesQuery query = GetDynamicQuery();
            query.Where(query.Id == id);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(Int32 id)
        {
            esParameters parms = new esParameters();
            parms.Add("Id", id);
            return Load(esQueryType.StoredProcedure, es.spLoadByPrimaryKey, parms);
        }

        #endregion

        #region Properties

        public override void SetProperties(IDictionary values)
        {
            foreach (string propertyName in values.Keys)
            {
                SetProperty(propertyName, values[propertyName]);
            }
        }

        public override void SetProperty(string name, object value)
        {
            if (Row == null) AddNew();

            esColumnMetadata col = Meta.Columns.FindByPropertyName(name);
            if (col != null)
            {
                if (value == null || value.GetType().ToString() == "System.String")
                {
                    // Use the strongly typed property
                    switch (name)
                    {
                        case "Id":
                            str.Id = (string) value;
                            break;
                        case "FundCode":
                            str.FundCode = (string) value;
                            break;
                        case "Description":
                            str.Description = (string) value;
                            break;
                        case "PriceType":
                            str.PriceType = (string) value;
                            break;
                        case "BidPrice":
                            str.BidPrice = (string) value;
                            break;
                        case "OfferPrice":
                            str.OfferPrice = (string) value;
                            break;
                        case "Yield":
                            str.Yield = (string) value;
                            break;
                        case "UploadDate":
                            str.UploadDate = (string) value;
                            break;
                        case "UploadedBy":
                            str.UploadedBy = (string) value;
                            break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "Id":

                            if (value == null || value.GetType().ToString() == "System.Int32")
                                Id = (Int32?) value;
                            break;

                        case "BidPrice":

                            if (value == null || value.GetType().ToString() == "System.Double")
                                BidPrice = (Double?) value;
                            break;

                        case "OfferPrice":

                            if (value == null || value.GetType().ToString() == "System.Double")
                                OfferPrice = (Double?) value;
                            break;

                        case "Yield":

                            if (value == null || value.GetType().ToString() == "System.Double")
                                Yield = (Double?) value;
                            break;

                        case "UploadDate":

                            if (value == null || value.GetType().ToString() == "System.DateTime")
                                UploadDate = (DateTime?) value;
                            break;

                        case "UploadedBy":

                            if (value == null || value.GetType().ToString() == "System.Int32")
                                UploadedBy = (Int32?) value;
                            break;


                        default:
                            break;
                    }
                }
            }
            else if (Row.Table.Columns.Contains(name))
            {
                Row[name] = value;
            }
            else
            {
                throw new Exception("SetProperty Error: '" + name + "' not found");
            }
        }


        /// <summary>
        /// Maps to OMAMFundPrices.Id
        /// </summary>
        public virtual Int32? Id
        {
            get { return base.GetSystemInt32(OMAMFundPricesMetadata.ColumnNames.Id); }

            set
            {
                if (base.SetSystemInt32(OMAMFundPricesMetadata.ColumnNames.Id, value))
                {
                    MarkFieldAsModified(OMAMFundPricesMetadata.ColumnNames.Id);
                }
            }
        }

        /// <summary>
        /// Maps to OMAMFundPrices.FundCode
        /// </summary>
        public virtual String FundCode
        {
            get { return base.GetSystemString(OMAMFundPricesMetadata.ColumnNames.FundCode); }

            set
            {
                if (base.SetSystemString(OMAMFundPricesMetadata.ColumnNames.FundCode, value))
                {
                    MarkFieldAsModified(OMAMFundPricesMetadata.ColumnNames.FundCode);
                }
            }
        }

        /// <summary>
        /// Maps to OMAMFundPrices.Description
        /// </summary>
        public virtual String Description
        {
            get { return base.GetSystemString(OMAMFundPricesMetadata.ColumnNames.Description); }

            set
            {
                if (base.SetSystemString(OMAMFundPricesMetadata.ColumnNames.Description, value))
                {
                    MarkFieldAsModified(OMAMFundPricesMetadata.ColumnNames.Description);
                }
            }
        }

        /// <summary>
        /// Maps to OMAMFundPrices.PriceType
        /// </summary>
        public virtual String PriceType
        {
            get { return base.GetSystemString(OMAMFundPricesMetadata.ColumnNames.PriceType); }

            set
            {
                if (base.SetSystemString(OMAMFundPricesMetadata.ColumnNames.PriceType, value))
                {
                    MarkFieldAsModified(OMAMFundPricesMetadata.ColumnNames.PriceType);
                }
            }
        }

        /// <summary>
        /// Maps to OMAMFundPrices.BidPrice
        /// </summary>
        public virtual Double? BidPrice
        {
            get { return base.GetSystemDouble(OMAMFundPricesMetadata.ColumnNames.BidPrice); }

            set
            {
                if (base.SetSystemDouble(OMAMFundPricesMetadata.ColumnNames.BidPrice, value))
                {
                    MarkFieldAsModified(OMAMFundPricesMetadata.ColumnNames.BidPrice);
                }
            }
        }

        /// <summary>
        /// Maps to OMAMFundPrices.OfferPrice
        /// </summary>
        public virtual Double? OfferPrice
        {
            get { return base.GetSystemDouble(OMAMFundPricesMetadata.ColumnNames.OfferPrice); }

            set
            {
                if (base.SetSystemDouble(OMAMFundPricesMetadata.ColumnNames.OfferPrice, value))
                {
                    MarkFieldAsModified(OMAMFundPricesMetadata.ColumnNames.OfferPrice);
                }
            }
        }

        /// <summary>
        /// Maps to OMAMFundPrices.Yield
        /// </summary>
        public virtual Double? Yield
        {
            get { return base.GetSystemDouble(OMAMFundPricesMetadata.ColumnNames.Yield); }

            set
            {
                if (base.SetSystemDouble(OMAMFundPricesMetadata.ColumnNames.Yield, value))
                {
                    MarkFieldAsModified(OMAMFundPricesMetadata.ColumnNames.Yield);
                }
            }
        }

        /// <summary>
        /// Maps to OMAMFundPrices.UploadDate
        /// </summary>
        public virtual DateTime? UploadDate
        {
            get { return base.GetSystemDateTime(OMAMFundPricesMetadata.ColumnNames.UploadDate); }

            set
            {
                if (base.SetSystemDateTime(OMAMFundPricesMetadata.ColumnNames.UploadDate, value))
                {
                    MarkFieldAsModified(OMAMFundPricesMetadata.ColumnNames.UploadDate);
                }
            }
        }

        /// <summary>
        /// Maps to OMAMFundPrices.UploadedBy
        /// </summary>
        public virtual Int32? UploadedBy
        {
            get { return base.GetSystemInt32(OMAMFundPricesMetadata.ColumnNames.UploadedBy); }

            set
            {
                if (base.SetSystemInt32(OMAMFundPricesMetadata.ColumnNames.UploadedBy, value))
                {
                    MarkFieldAsModified(OMAMFundPricesMetadata.ColumnNames.UploadedBy);
                }
            }
        }

        #endregion

        #region String Properties

        /// <summary>
        /// Converts an entity's properties to
        /// and from strings.
        /// </summary>
        /// <remarks>
        /// The str properties Get and Set provide easy conversion
        /// between a string and a property's data type. Not all
        /// data types will get a str property.
        /// </remarks>
        /// <example>
        /// Set a datetime from a string.
        /// <code>
        /// Employees entity = new Employees();
        /// entity.LoadByPrimaryKey(10);
        /// entity.str.HireDate = "2007-01-01 00:00:00";
        /// entity.Save();
        /// </code>
        /// Get a datetime as a string.
        /// <code>
        /// Employees entity = new Employees();
        /// entity.LoadByPrimaryKey(10);
        /// string theDate = entity.str.HireDate;
        /// </code>
        /// </example>
        [Browsable(false)]
        public esStrings str
        {
            get
            {
                if (esstrings == null)
                {
                    esstrings = new esStrings(this);
                }
                return esstrings;
            }
        }


        [Serializable]
        public sealed class esStrings
        {
            public esStrings(esOMAMFundPrices entity)
            {
                this.entity = entity;
            }


            public String Id
            {
                get
                {
                    Int32? data = entity.Id;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Id = null;
                    else entity.Id = Convert.ToInt32(value);
                }
            }

            public String FundCode
            {
                get
                {
                    String data = entity.FundCode;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.FundCode = null;
                    else entity.FundCode = Convert.ToString(value);
                }
            }

            public String Description
            {
                get
                {
                    String data = entity.Description;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Description = null;
                    else entity.Description = Convert.ToString(value);
                }
            }

            public String PriceType
            {
                get
                {
                    String data = entity.PriceType;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PriceType = null;
                    else entity.PriceType = Convert.ToString(value);
                }
            }

            public String BidPrice
            {
                get
                {
                    Double? data = entity.BidPrice;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.BidPrice = null;
                    else entity.BidPrice = Convert.ToDouble(value);
                }
            }

            public String OfferPrice
            {
                get
                {
                    Double? data = entity.OfferPrice;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.OfferPrice = null;
                    else entity.OfferPrice = Convert.ToDouble(value);
                }
            }

            public String Yield
            {
                get
                {
                    Double? data = entity.Yield;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Yield = null;
                    else entity.Yield = Convert.ToDouble(value);
                }
            }

            public String UploadDate
            {
                get
                {
                    DateTime? data = entity.UploadDate;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.UploadDate = null;
                    else entity.UploadDate = Convert.ToDateTime(value);
                }
            }

            public String UploadedBy
            {
                get
                {
                    Int32? data = entity.UploadedBy;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.UploadedBy = null;
                    else entity.UploadedBy = Convert.ToInt32(value);
                }
            }


            private esOMAMFundPrices entity;
        }

        #endregion

        #region Query Logic

        protected void InitQuery(esOMAMFundPricesQuery query)
        {
            query.OnLoadEvent += new esDynamicQuery.QueryLoadedDelegate(OnQueryLoaded);
            query.es.Connection = ((IEntity) this).Connection;
        }

        protected bool OnQueryLoaded(DataTable table)
        {
            bool dataFound = PopulateEntity(table);

            if (RowCount > 1)
            {
                throw new Exception("esOMAMFundPrices can only hold one record of data");
            }

            return dataFound;
        }

        #endregion

        [NonSerialized] private esStrings esstrings;
    }


    /// <summary>
    /// Hierarchical for the 'OMAMFundPrices' table
    /// </summary>
    public partial class OMAMFundPrices : esOMAMFundPrices
    {
        /// <summary>
        /// Used internally by the entity's hierarchical properties.
        /// </summary>
        protected override List<esPropertyDescriptor> GetHierarchicalProperties()
        {
            List<esPropertyDescriptor> props = new List<esPropertyDescriptor>();


            return props;
        }

        /// <summary>
        /// Used internally for retrieving AutoIncrementing keys
        /// during hierarchical PreSave.
        /// </summary>
        protected override void ApplyPreSaveKeys()
        {
        }

        /// <summary>
        /// Used internally for retrieving AutoIncrementing keys
        /// during hierarchical PostSave.
        /// </summary>
        protected override void ApplyPostSaveKeys()
        {
        }

        /// <summary>
        /// Used internally for retrieving AutoIncrementing keys
        /// during hierarchical PostOneToOneSave.
        /// </summary>
        protected override void ApplyPostOneSaveKeys()
        {
        }
    }


    [Serializable]
    public abstract class esOMAMFundPricesQuery : esDynamicQuery
    {
        protected override IMetadata Meta
        {
            get { return OMAMFundPricesMetadata.Meta(); }
        }


        public esQueryItem Id
        {
            get { return new esQueryItem(this, OMAMFundPricesMetadata.ColumnNames.Id, esSystemType.Int32); }
        }

        public esQueryItem FundCode
        {
            get { return new esQueryItem(this, OMAMFundPricesMetadata.ColumnNames.FundCode, esSystemType.String); }
        }

        public esQueryItem Description
        {
            get { return new esQueryItem(this, OMAMFundPricesMetadata.ColumnNames.Description, esSystemType.String); }
        }

        public esQueryItem PriceType
        {
            get { return new esQueryItem(this, OMAMFundPricesMetadata.ColumnNames.PriceType, esSystemType.String); }
        }

        public esQueryItem BidPrice
        {
            get { return new esQueryItem(this, OMAMFundPricesMetadata.ColumnNames.BidPrice, esSystemType.Double); }
        }

        public esQueryItem OfferPrice
        {
            get { return new esQueryItem(this, OMAMFundPricesMetadata.ColumnNames.OfferPrice, esSystemType.Double); }
        }

        public esQueryItem Yield
        {
            get { return new esQueryItem(this, OMAMFundPricesMetadata.ColumnNames.Yield, esSystemType.Double); }
        }

        public esQueryItem UploadDate
        {
            get { return new esQueryItem(this, OMAMFundPricesMetadata.ColumnNames.UploadDate, esSystemType.DateTime); }
        }

        public esQueryItem UploadedBy
        {
            get { return new esQueryItem(this, OMAMFundPricesMetadata.ColumnNames.UploadedBy, esSystemType.Int32); }
        }
    }


    [Serializable]
    [XmlType("OMAMFundPricesCollection")]
    public partial class OMAMFundPricesCollection : esOMAMFundPricesCollection, IEnumerable<OMAMFundPrices>
    {
        public OMAMFundPricesCollection()
        {
        }

        public static implicit operator List<OMAMFundPrices>(OMAMFundPricesCollection coll)
        {
            List<OMAMFundPrices> list = new List<OMAMFundPrices>();

            foreach (OMAMFundPrices emp in coll)
            {
                list.Add(emp);
            }

            return list;
        }

        #region Housekeeping methods

        protected override IMetadata Meta
        {
            get { return OMAMFundPricesMetadata.Meta(); }
        }

        protected override esDynamicQuery GetDynamicQuery()
        {
            if (query == null)
            {
                query = new OMAMFundPricesQuery();
                InitQuery(query);
            }
            return query;
        }

        protected override esEntity CreateEntityForCollection(DataRow row)
        {
            return new OMAMFundPrices(row);
        }

        protected override esEntity CreateEntity()
        {
            return new OMAMFundPrices();
        }

        #endregion

        /// <summary>
        /// This represents the Query on the class.
        /// </summary>
        /// <remarks>
        /// Extensive documentation and examples on the
        /// Query API can be found at the
        /// <a target="_blank" href="http://www.entityspaces.net">EntitySpaces site</a>.
        /// </remarks>
        [BrowsableAttribute(false)]
        public OMAMFundPricesQuery Query
        {
            get
            {
                if (query == null)
                {
                    query = new OMAMFundPricesQuery();
                    base.InitQuery(query);
                }

                return query;
            }
        }

        /// <summary>
        /// Useful for building up conditional queries.
        /// In most cases, before loading an entity or collection,
        /// you should instantiate a new one. This method was added
        /// to handle specialized circumstances, and should not be
        /// used as a substitute for that.
        /// </summary>
        /// <remarks>
        /// This just sets obj.Query to null/Nothing.
        /// In most cases, you will 'new' your object before
        /// loading it, rather than calling this method.
        /// It only affects obj.Query.Load(), so is not useful
        /// when Joins are involved, or for many other situations.
        /// Because it clears out any obj.Query.Where clauses,
        /// it can be useful for building conditional queries on the fly.
        /// <code>
        /// public bool ReQuery(string lastName, string firstName)
        /// {
        ///     this.QueryReset();
        ///     
        ///     if(!String.IsNullOrEmpty(lastName))
        ///     {
        ///         this.Query.Where(
        ///             this.Query.LastName == lastName);
        ///     }
        ///     if(!String.IsNullOrEmpty(firstName))
        ///     {
        ///         this.Query.Where(
        ///             this.Query.FirstName == firstName);
        ///     }
        ///     
        ///     return this.Query.Load();
        /// }
        /// </code>
        /// <code lang="vbnet">
        /// Public Function ReQuery(ByVal lastName As String, _
        ///     ByVal firstName As String) As Boolean
        /// 
        ///     Me.QueryReset()
        /// 
        ///     If Not [String].IsNullOrEmpty(lastName) Then
        ///         Me.Query.Where(Me.Query.LastName = lastName)
        ///     End If
        ///     If Not [String].IsNullOrEmpty(firstName) Then
        ///         Me.Query.Where(Me.Query.FirstName = firstName)
        ///     End If
        /// 
        ///     Return Me.Query.Load()
        /// End Function
        /// </code>
        /// </remarks>
        public void QueryReset()
        {
            query = null;
        }

        /// <summary>
        /// Used to custom load a Join query.
        /// Returns true if at least one record was loaded.
        /// </summary>
        /// <remarks>
        /// Provides support for InnerJoin, LeftJoin,
        /// RightJoin, and FullJoin. You must provide an alias
        /// for each query when instantiating them.
        /// <code>
        /// EmployeeCollection collection = new EmployeeCollection();
        /// 
        /// EmployeeQuery emp = new EmployeeQuery("eq");
        /// CustomerQuery cust = new CustomerQuery("cq");
        /// 
        /// emp.Select(emp.EmployeeID, emp.LastName, cust.CustomerName);
        /// emp.LeftJoin(cust).On(emp.EmployeeID == cust.StaffAssigned);
        /// 
        /// collection.Load(emp);
        /// </code>
        /// <code lang="vbnet">
        /// Dim collection As New EmployeeCollection()
        /// 
        /// Dim emp As New EmployeeQuery("eq")
        /// Dim cust As New CustomerQuery("cq")
        /// 
        /// emp.Select(emp.EmployeeID, emp.LastName, cust.CustomerName)
        /// emp.LeftJoin(cust).On(emp.EmployeeID = cust.StaffAssigned)
        /// 
        /// collection.Load(emp)
        /// </code>
        /// </remarks>
        /// <param name="query">The query object instance name.</param>
        /// <returns>True if at least one record was loaded.</returns>
        public bool Load(OMAMFundPricesQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public OMAMFundPrices AddNew()
        {
            OMAMFundPrices entity = base.AddNewEntity() as OMAMFundPrices;

            return entity;
        }

        public OMAMFundPrices FindByPrimaryKey(Int32 id)
        {
            return base.FindByPrimaryKey(id) as OMAMFundPrices;
        }

        #region IEnumerable<OMAMFundPrices> Members

        IEnumerator<OMAMFundPrices> IEnumerable<OMAMFundPrices>.GetEnumerator()
        {
            IEnumerable enumer = this as IEnumerable;
            IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as OMAMFundPrices;
            }
        }

        #endregion

        private OMAMFundPricesQuery query;
    }


    /// <summary>
    /// Encapsulates the 'OMAMFundPrices' table
    /// </summary>
    [Serializable]
    public partial class OMAMFundPrices : esOMAMFundPrices
    {
        public OMAMFundPrices()
        {
        }

        public OMAMFundPrices(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods

        protected override IMetadata Meta
        {
            get { return OMAMFundPricesMetadata.Meta(); }
        }

        protected override esOMAMFundPricesQuery GetDynamicQuery()
        {
            if (query == null)
            {
                query = new OMAMFundPricesQuery();
                InitQuery(query);
            }
            return query;
        }

        #endregion

        /// <summary>
        /// This represents the Query on the class.
        /// </summary>
        /// <remarks>
        /// Extensive documentation and examples on the
        /// Query API can be found at the
        /// <a target="_blank" href="http://www.entityspaces.net">EntitySpaces site</a>.
        /// </remarks>
        [BrowsableAttribute(false)]
        public OMAMFundPricesQuery Query
        {
            get
            {
                if (query == null)
                {
                    query = new OMAMFundPricesQuery();
                    base.InitQuery(query);
                }

                return query;
            }
        }

        /// <summary>
        /// Useful for building up conditional queries.
        /// In most cases, before loading an entity or collection,
        /// you should instantiate a new one. This method was added
        /// to handle specialized circumstances, and should not be
        /// used as a substitute for that.
        /// </summary>
        /// <remarks>
        /// This just sets obj.Query to null/Nothing.
        /// In most cases, you will 'new' your object before
        /// loading it, rather than calling this method.
        /// It only affects obj.Query.Load(), so is not useful
        /// when Joins are involved, or for many other situations.
        /// Because it clears out any obj.Query.Where clauses,
        /// it can be useful for building conditional queries on the fly.
        /// <code>
        /// public bool ReQuery(string lastName, string firstName)
        /// {
        ///     this.QueryReset();
        ///     
        ///     if(!String.IsNullOrEmpty(lastName))
        ///     {
        ///         this.Query.Where(
        ///             this.Query.LastName == lastName);
        ///     }
        ///     if(!String.IsNullOrEmpty(firstName))
        ///     {
        ///         this.Query.Where(
        ///             this.Query.FirstName == firstName);
        ///     }
        ///     
        ///     return this.Query.Load();
        /// }
        /// </code>
        /// <code lang="vbnet">
        /// Public Function ReQuery(ByVal lastName As String, _
        ///     ByVal firstName As String) As Boolean
        /// 
        ///     Me.QueryReset()
        /// 
        ///     If Not [String].IsNullOrEmpty(lastName) Then
        ///         Me.Query.Where(Me.Query.LastName = lastName)
        ///     End If
        ///     If Not [String].IsNullOrEmpty(firstName) Then
        ///         Me.Query.Where(Me.Query.FirstName = firstName)
        ///     End If
        /// 
        ///     Return Me.Query.Load()
        /// End Function
        /// </code>
        /// </remarks>
        public void QueryReset()
        {
            query = null;
        }

        /// <summary>
        /// Used to custom load a Join query.
        /// Returns true if at least one row is loaded.
        /// For an entity, an exception will be thrown
        /// if more than one row is loaded.
        /// </summary>
        /// <remarks>
        /// Provides support for InnerJoin, LeftJoin,
        /// RightJoin, and FullJoin. You must provide an alias
        /// for each query when instantiating them.
        /// <code>
        /// EmployeeCollection collection = new EmployeeCollection();
        /// 
        /// EmployeeQuery emp = new EmployeeQuery("eq");
        /// CustomerQuery cust = new CustomerQuery("cq");
        /// 
        /// emp.Select(emp.EmployeeID, emp.LastName, cust.CustomerName);
        /// emp.LeftJoin(cust).On(emp.EmployeeID == cust.StaffAssigned);
        /// 
        /// collection.Load(emp);
        /// </code>
        /// <code lang="vbnet">
        /// Dim collection As New EmployeeCollection()
        /// 
        /// Dim emp As New EmployeeQuery("eq")
        /// Dim cust As New CustomerQuery("cq")
        /// 
        /// emp.Select(emp.EmployeeID, emp.LastName, cust.CustomerName)
        /// emp.LeftJoin(cust).On(emp.EmployeeID = cust.StaffAssigned)
        /// 
        /// collection.Load(emp)
        /// </code>
        /// </remarks>
        /// <param name="query">The query object instance name.</param>
        /// <returns>True if at least one record was loaded.</returns>
        public bool Load(OMAMFundPricesQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return Query.Load();
        }

        private OMAMFundPricesQuery query;
    }


    [Serializable]
    public partial class OMAMFundPricesQuery : esOMAMFundPricesQuery
    {
        public OMAMFundPricesQuery()
        {
        }

        public OMAMFundPricesQuery(string joinAlias)
        {
            es.JoinAlias = joinAlias;
        }
    }


    [Serializable]
    public partial class OMAMFundPricesMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor

        protected OMAMFundPricesMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(ColumnNames.Id, 0, typeof (Int32), esSystemType.Int32);
            c.PropertyName = PropertyNames.Id;
            c.IsInPrimaryKey = true;
            c.IsAutoIncrement = true;
            c.NumericPrecision = 10;
            _columns.Add(c);

            c = new esColumnMetadata(ColumnNames.FundCode, 1, typeof (String), esSystemType.String);
            c.PropertyName = PropertyNames.FundCode;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(ColumnNames.Description, 2, typeof (String), esSystemType.String);
            c.PropertyName = PropertyNames.Description;
            c.CharacterMaxLength = 100;
            _columns.Add(c);

            c = new esColumnMetadata(ColumnNames.PriceType, 3, typeof (String), esSystemType.String);
            c.PropertyName = PropertyNames.PriceType;
            c.CharacterMaxLength = 5;
            _columns.Add(c);

            c = new esColumnMetadata(ColumnNames.BidPrice, 4, typeof (Double), esSystemType.Double);
            c.PropertyName = PropertyNames.BidPrice;
            c.NumericPrecision = 15;
            _columns.Add(c);

            c = new esColumnMetadata(ColumnNames.OfferPrice, 5, typeof (Double), esSystemType.Double);
            c.PropertyName = PropertyNames.OfferPrice;
            c.NumericPrecision = 15;
            _columns.Add(c);

            c = new esColumnMetadata(ColumnNames.Yield, 6, typeof (Double), esSystemType.Double);
            c.PropertyName = PropertyNames.Yield;
            c.NumericPrecision = 15;
            _columns.Add(c);

            c = new esColumnMetadata(ColumnNames.UploadDate, 7, typeof (DateTime), esSystemType.DateTime);
            c.PropertyName = PropertyNames.UploadDate;
            _columns.Add(c);

            c = new esColumnMetadata(ColumnNames.UploadedBy, 8, typeof (Int32), esSystemType.Int32);
            c.PropertyName = PropertyNames.UploadedBy;
            c.NumericPrecision = 10;
            _columns.Add(c);
        }

        #endregion

        public static OMAMFundPricesMetadata Meta()
        {
            return meta;
        }

        public Guid DataID
        {
            get { return base._dataID; }
        }

        public bool MultiProviderMode
        {
            get { return false; }
        }

        public esColumnMetadataCollection Columns
        {
            get { return base._columns; }
        }

        #region ColumnNames

        public class ColumnNames
        {
            public const string Id = "Id";
            public const string FundCode = "FundCode";
            public const string Description = "Description";
            public const string PriceType = "PriceType";
            public const string BidPrice = "BidPrice";
            public const string OfferPrice = "OfferPrice";
            public const string Yield = "Yield";
            public const string UploadDate = "UploadDate";
            public const string UploadedBy = "UploadedBy";
        }

        #endregion

        #region PropertyNames

        public class PropertyNames
        {
            public const string Id = "Id";
            public const string FundCode = "FundCode";
            public const string Description = "Description";
            public const string PriceType = "PriceType";
            public const string BidPrice = "BidPrice";
            public const string OfferPrice = "OfferPrice";
            public const string Yield = "Yield";
            public const string UploadDate = "UploadDate";
            public const string UploadedBy = "UploadedBy";
        }

        #endregion

        public esProviderSpecificMetadata GetProviderMetadata(string mapName)
        {
            MapToMeta mapMethod = mapDelegates[mapName];

            if (mapMethod != null)
                return mapMethod(mapName);
            else
                return null;
        }

        #region MAP esDefault

        private static int RegisterDelegateesDefault()
        {
            // This is only executed once per the life of the application
            lock (typeof (OMAMFundPricesMetadata))
            {
                if (mapDelegates == null)
                {
                    mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (meta == null)
                {
                    meta = new OMAMFundPricesMetadata();
                }

                MapToMeta mapMethod = new MapToMeta(meta.esDefault);
                mapDelegates.Add("esDefault", mapMethod);
                mapMethod("esDefault");
            }
            return 0;
        }

        private esProviderSpecificMetadata esDefault(string mapName)
        {
            if (!_providerMetadataMaps.ContainsKey(mapName))
            {
                esProviderSpecificMetadata meta = new esProviderSpecificMetadata();

                meta.AddTypeMap("Id", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("FundCode", new esTypeMap("nvarchar", "System.String"));
                meta.AddTypeMap("Description", new esTypeMap("nvarchar", "System.String"));
                meta.AddTypeMap("PriceType", new esTypeMap("nvarchar", "System.String"));
                meta.AddTypeMap("BidPrice", new esTypeMap("float", "System.Double"));
                meta.AddTypeMap("OfferPrice", new esTypeMap("float", "System.Double"));
                meta.AddTypeMap("Yield", new esTypeMap("float", "System.Double"));
                meta.AddTypeMap("UploadDate", new esTypeMap("smalldatetime", "System.DateTime"));
                meta.AddTypeMap("UploadedBy", new esTypeMap("int", "System.Int32"));


                meta.Source = "OMAMFundPrices";
                meta.Destination = "OMAMFundPrices";

                meta.spInsert = "proc_OMAMFundPricesInsert";
                meta.spUpdate = "proc_OMAMFundPricesUpdate";
                meta.spDelete = "proc_OMAMFundPricesDelete";
                meta.spLoadAll = "proc_OMAMFundPricesLoadAll";
                meta.spLoadByPrimaryKey = "proc_OMAMFundPricesLoadByPrimaryKey";

                _providerMetadataMaps["esDefault"] = meta;
            }

            return _providerMetadataMaps["esDefault"];
        }

        #endregion

        private static OMAMFundPricesMetadata meta;
        protected static Dictionary<string, MapToMeta> mapDelegates;
        private static int _esDefault = RegisterDelegateesDefault();
    }
}