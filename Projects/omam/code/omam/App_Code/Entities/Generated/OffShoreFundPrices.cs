/*
===============================================================================
                     EntitySpaces(TM) by EntitySpaces, LLC
                 A New 2.0 Architecture for the .NET Framework
                          http://www.entityspaces.net
===============================================================================
                       EntitySpaces Version # 2007.1.1210.0
                       MyGeneration Version # 1.3.0.3
                           22/02/2008 21:42:23
-------------------------------------------------------------------------------
*/


using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.ComponentModel;
using System.Xml.Serialization;


using EntitySpaces.Interfaces;
using EntitySpaces.Core;



namespace BusinessObjects
{
	[Serializable]
	abstract public class esOffShoreFundPricesCollection : esEntityCollection
	{
		public esOffShoreFundPricesCollection()
		{

		}	
		
		protected override string GetCollectionName()
		{
			return "OffShoreFundPricesCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esOffShoreFundPricesQuery query)
		{
			query.OnLoadEvent += new esDynamicQuery.QueryLoadedDelegate(OnQueryLoaded);
			query.es.Connection = ((IEntityCollection)this).Connection;
		}

		protected bool OnQueryLoaded(DataTable table)
		{
			this.PopulateCollection(table);
			return (this.RowCount > 0) ? true : false;
		}
		
        protected override void HookupQuery(esDynamicQuery query)
        {
            this.InitQuery(query as esOffShoreFundPricesQuery);
        }		
		#endregion
		
		virtual public OffShoreFundPrices DetachEntity(OffShoreFundPrices entity)
		{
			return base.DetachEntity(entity) as OffShoreFundPrices;
		}
		
		virtual public OffShoreFundPrices AttachEntity(OffShoreFundPrices entity)
		{
			return base.AttachEntity(entity) as OffShoreFundPrices;
		}
		
		virtual public void Combine(OffShoreFundPricesCollection collection)
		{
			base.Combine(collection);
		}
		
		new public OffShoreFundPrices this[int index]
		{
			get
			{
				return base[index] as OffShoreFundPrices;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(OffShoreFundPrices);
		}
	}
	


	[Serializable]
	abstract public class esOffShoreFundPrices : esEntity
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esOffShoreFundPricesQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esOffShoreFundPrices()
		{
	
		}
	
		public esOffShoreFundPrices(DataRow row)
			: base(row)
		{
	
		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int32 id)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int32 id)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(id);
			else
				return LoadByPrimaryKeyStoredProcedure(id);
		}
	
		private bool LoadByPrimaryKeyDynamic(System.Int32 id)
		{
			esOffShoreFundPricesQuery query = this.GetDynamicQuery();
			query.Where(query.Id == id);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(System.Int32 id)
		{
			esParameters parms = new esParameters();
			parms.Add("Id",id);
			return this.Load(esQueryType.StoredProcedure, this.es.spLoadByPrimaryKey, parms);
		}
		#endregion
				
		
		#region Properties
		
		
		public override void SetProperties(IDictionary values)
		{
			foreach (string propertyName in values.Keys)
			{
				this.SetProperty(propertyName, values[propertyName]);
			}
		}

		public override void SetProperty(string name, object value)
		{
			if(this.Row == null) this.AddNew();
			
			esColumnMetadata col = this.Meta.Columns.FindByPropertyName(name);
			if (col != null)
			{
				if(value == null || value.GetType().ToString() == "System.String")
				{				
					// Use the strongly typed property
					switch (name)
					{							
						case "Id": this.str.Id = (string)value; break;							
						case "OMAMFundId": this.str.OMAMFundId = (string)value; break;							
						case "Price": this.str.Price = (string)value; break;							
						case "CreatedDate": this.str.CreatedDate = (string)value; break;							
						case "CreatedBy": this.str.CreatedBy = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "Id":
						
							if (value == null || value.GetType().ToString() == "System.Int32")
								this.Id = (System.Int32?)value;
							break;
						
						case "OMAMFundId":
						
							if (value == null || value.GetType().ToString() == "System.Int32")
								this.OMAMFundId = (System.Int32?)value;
							break;
						
						case "Price":
						
							if (value == null || value.GetType().ToString() == "System.Double")
								this.Price = (System.Double?)value;
							break;
						
						case "CreatedDate":
						
							if (value == null || value.GetType().ToString() == "System.DateTime")
								this.CreatedDate = (System.DateTime?)value;
							break;
						
						case "CreatedBy":
						
							if (value == null || value.GetType().ToString() == "System.Int32")
								this.CreatedBy = (System.Int32?)value;
							break;
						
					
						default:
							break;
					}
				}
			}
			else if(this.Row.Table.Columns.Contains(name))
			{
				this.Row[name] = value;
			}
			else
			{
				throw new Exception("SetProperty Error: '" + name + "' not found");
			}
		}
		
		
		/// <summary>
		/// Maps to OffShoreFundPrices.Id
		/// </summary>
		virtual public System.Int32? Id
		{
			get
			{
				return base.GetSystemInt32(OffShoreFundPricesMetadata.ColumnNames.Id);
			}
			
			set
			{
				if(base.SetSystemInt32(OffShoreFundPricesMetadata.ColumnNames.Id, value))
				{
					this.MarkFieldAsModified(OffShoreFundPricesMetadata.ColumnNames.Id);
				}
			}
		}
		
		/// <summary>
		/// Maps to OffShoreFundPrices.OMAMFundId
		/// </summary>
		virtual public System.Int32? OMAMFundId
		{
			get
			{
				return base.GetSystemInt32(OffShoreFundPricesMetadata.ColumnNames.OMAMFundId);
			}
			
			set
			{
				if(base.SetSystemInt32(OffShoreFundPricesMetadata.ColumnNames.OMAMFundId, value))
				{
					this.MarkFieldAsModified(OffShoreFundPricesMetadata.ColumnNames.OMAMFundId);
				}
			}
		}
		
		/// <summary>
		/// Maps to OffShoreFundPrices.Price
		/// </summary>
		virtual public System.Double? Price
		{
			get
			{
				return base.GetSystemDouble(OffShoreFundPricesMetadata.ColumnNames.Price);
			}
			
			set
			{
				if(base.SetSystemDouble(OffShoreFundPricesMetadata.ColumnNames.Price, value))
				{
					this.MarkFieldAsModified(OffShoreFundPricesMetadata.ColumnNames.Price);
				}
			}
		}
		
		/// <summary>
		/// Maps to OffShoreFundPrices.CreatedDate
		/// </summary>
		virtual public System.DateTime? CreatedDate
		{
			get
			{
				return base.GetSystemDateTime(OffShoreFundPricesMetadata.ColumnNames.CreatedDate);
			}
			
			set
			{
				if(base.SetSystemDateTime(OffShoreFundPricesMetadata.ColumnNames.CreatedDate, value))
				{
					this.MarkFieldAsModified(OffShoreFundPricesMetadata.ColumnNames.CreatedDate);
				}
			}
		}
		
		/// <summary>
		/// Maps to OffShoreFundPrices.CreatedBy
		/// </summary>
		virtual public System.Int32? CreatedBy
		{
			get
			{
				return base.GetSystemInt32(OffShoreFundPricesMetadata.ColumnNames.CreatedBy);
			}
			
			set
			{
				if(base.SetSystemInt32(OffShoreFundPricesMetadata.ColumnNames.CreatedBy, value))
				{
					this.MarkFieldAsModified(OffShoreFundPricesMetadata.ColumnNames.CreatedBy);
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

		[BrowsableAttribute( false )]		
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
		sealed public class esStrings
		{
			public esStrings(esOffShoreFundPrices entity)
			{
				this.entity = entity;
			}
			
	
			public System.String Id
			{
				get
				{
					System.Int32? data = entity.Id;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Id = null;
					else entity.Id = Convert.ToInt32(value);
				}
			}
				
			public System.String OMAMFundId
			{
				get
				{
					System.Int32? data = entity.OMAMFundId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OMAMFundId = null;
					else entity.OMAMFundId = Convert.ToInt32(value);
				}
			}
				
			public System.String Price
			{
				get
				{
					System.Double? data = entity.Price;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Price = null;
					else entity.Price = Convert.ToDouble(value);
				}
			}
				
			public System.String CreatedDate
			{
				get
				{
					System.DateTime? data = entity.CreatedDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CreatedDate = null;
					else entity.CreatedDate = Convert.ToDateTime(value);
				}
			}
				
			public System.String CreatedBy
			{
				get
				{
					System.Int32? data = entity.CreatedBy;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CreatedBy = null;
					else entity.CreatedBy = Convert.ToInt32(value);
				}
			}
			

			private esOffShoreFundPrices entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esOffShoreFundPricesQuery query)
		{
			query.OnLoadEvent += new esDynamicQuery.QueryLoadedDelegate(OnQueryLoaded);
			query.es.Connection = ((IEntity)this).Connection;
		}

		protected bool OnQueryLoaded(DataTable table)
		{
			bool dataFound = this.PopulateEntity(table);

			if (this.RowCount > 1)
			{
				throw new Exception("esOffShoreFundPrices can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	/// <summary>
	/// Hierarchical for the 'OffShoreFundPrices' table
	/// </summary>
	public partial class OffShoreFundPrices : esOffShoreFundPrices
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
	abstract public class esOffShoreFundPricesQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return OffShoreFundPricesMetadata.Meta();
			}
		}	
		

		public esQueryItem Id
		{
			get
			{
				return new esQueryItem(this, OffShoreFundPricesMetadata.ColumnNames.Id, esSystemType.Int32);
			}
		} 
		
		public esQueryItem OMAMFundId
		{
			get
			{
				return new esQueryItem(this, OffShoreFundPricesMetadata.ColumnNames.OMAMFundId, esSystemType.Int32);
			}
		} 
		
		public esQueryItem Price
		{
			get
			{
				return new esQueryItem(this, OffShoreFundPricesMetadata.ColumnNames.Price, esSystemType.Double);
			}
		} 
		
		public esQueryItem CreatedDate
		{
			get
			{
				return new esQueryItem(this, OffShoreFundPricesMetadata.ColumnNames.CreatedDate, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem CreatedBy
		{
			get
			{
				return new esQueryItem(this, OffShoreFundPricesMetadata.ColumnNames.CreatedBy, esSystemType.Int32);
			}
		} 
		
	}
	


	[Serializable]
	[XmlType("OffShoreFundPricesCollection")]
	public partial class OffShoreFundPricesCollection : esOffShoreFundPricesCollection, IEnumerable<OffShoreFundPrices>
	{
		public OffShoreFundPricesCollection()
		{

		}	
		
        public static implicit operator List<OffShoreFundPrices>(OffShoreFundPricesCollection coll)
        {
            List<OffShoreFundPrices> list = new List<OffShoreFundPrices>();

            foreach (OffShoreFundPrices emp in coll)
            {
                list.Add(emp);
            }

            return list;
        }		
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return  OffShoreFundPricesMetadata.Meta();
			}
		}	

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new OffShoreFundPricesQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new OffShoreFundPrices(row);
		}

		override protected esEntity CreateEntity()
		{
			return new OffShoreFundPrices();
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

		[BrowsableAttribute( false )]
		public OffShoreFundPricesQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new OffShoreFundPricesQuery();
					base.InitQuery(this.query);
				}

				return this.query;
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
			this.query = null;
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
        public bool Load(OffShoreFundPricesQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }		

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
		public OffShoreFundPrices AddNew()
		{
			OffShoreFundPrices entity = base.AddNewEntity() as OffShoreFundPrices;
			
			return entity;		
		}

		public OffShoreFundPrices FindByPrimaryKey(System.Int32 id)
		{
			return base.FindByPrimaryKey(id) as OffShoreFundPrices;
		}


		#region IEnumerable<OffShoreFundPrices> Members

		IEnumerator<OffShoreFundPrices> IEnumerable<OffShoreFundPrices>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as OffShoreFundPrices;
			}
		}

		#endregion
		
		private OffShoreFundPricesQuery query;
	}
	

	
	/// <summary>
	/// Encapsulates the 'OffShoreFundPrices' table
	/// </summary>

	[Serializable]
	public partial class OffShoreFundPrices : esOffShoreFundPrices
	{
		public OffShoreFundPrices()
		{

		}	
	
		public OffShoreFundPrices(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return OffShoreFundPricesMetadata.Meta();
			}
		}

		override protected esOffShoreFundPricesQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new OffShoreFundPricesQuery();
				this.InitQuery(query);
			}
			return this.query;
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

		[BrowsableAttribute( false )]
		public OffShoreFundPricesQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new OffShoreFundPricesQuery();
					base.InitQuery(this.query);
				}

				return this.query;
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
			this.query = null;
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
        public bool Load(OffShoreFundPricesQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }			

		private OffShoreFundPricesQuery query;
	}
	


	[Serializable]
	public partial class OffShoreFundPricesQuery : esOffShoreFundPricesQuery
	{
		public OffShoreFundPricesQuery()
		{

		}		
		
        public OffShoreFundPricesQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }		

	}
	


	[Serializable]
	public partial class OffShoreFundPricesMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected OffShoreFundPricesMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(OffShoreFundPricesMetadata.ColumnNames.Id, 0, typeof(System.Int32), esSystemType.Int32);	
			c.PropertyName = OffShoreFundPricesMetadata.PropertyNames.Id;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(OffShoreFundPricesMetadata.ColumnNames.OMAMFundId, 1, typeof(System.Int32), esSystemType.Int32);	
			c.PropertyName = OffShoreFundPricesMetadata.PropertyNames.OMAMFundId;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(OffShoreFundPricesMetadata.ColumnNames.Price, 2, typeof(System.Double), esSystemType.Double);	
			c.PropertyName = OffShoreFundPricesMetadata.PropertyNames.Price;
			c.NumericPrecision = 15;
			_columns.Add(c); 
				
			c = new esColumnMetadata(OffShoreFundPricesMetadata.ColumnNames.CreatedDate, 3, typeof(System.DateTime), esSystemType.DateTime);	
			c.PropertyName = OffShoreFundPricesMetadata.PropertyNames.CreatedDate;
			_columns.Add(c); 
				
			c = new esColumnMetadata(OffShoreFundPricesMetadata.ColumnNames.CreatedBy, 4, typeof(System.Int32), esSystemType.Int32);	
			c.PropertyName = OffShoreFundPricesMetadata.PropertyNames.CreatedBy;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
		}
		#endregion

		static public OffShoreFundPricesMetadata Meta()
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
			get	{ return base._columns; }
		}
		
		#region ColumnNames
		public class ColumnNames
		{ 
			 public const string Id = "Id";
			 public const string OMAMFundId = "OMAMFundId";
			 public const string Price = "Price";
			 public const string CreatedDate = "CreatedDate";
			 public const string CreatedBy = "CreatedBy";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string Id = "Id";
			 public const string OMAMFundId = "OMAMFundId";
			 public const string Price = "Price";
			 public const string CreatedDate = "CreatedDate";
			 public const string CreatedBy = "CreatedBy";
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
		
		static private int RegisterDelegateesDefault()
		{
            // This is only executed once per the life of the application
            lock (typeof(OffShoreFundPricesMetadata))
            {
				if(OffShoreFundPricesMetadata.mapDelegates == null)
				{
					OffShoreFundPricesMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
			
                if (OffShoreFundPricesMetadata.meta == null)
                {
                    OffShoreFundPricesMetadata.meta = new OffShoreFundPricesMetadata();
                }

                MapToMeta mapMethod = new MapToMeta(meta.esDefault);
                mapDelegates.Add("esDefault", mapMethod);
                mapMethod("esDefault");
            }
			return 0;			
		}			

		private esProviderSpecificMetadata esDefault(string mapName)
		{
			if(!_providerMetadataMaps.ContainsKey(mapName))
			{
				esProviderSpecificMetadata meta = new esProviderSpecificMetadata();
				
				meta.AddTypeMap("Id", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("OMAMFundId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("Price", new esTypeMap("float", "System.Double"));
				meta.AddTypeMap("CreatedDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("CreatedBy", new esTypeMap("int", "System.Int32"));
			
				
				
				
				meta.Source = "OffShoreFundPrices";
				meta.Destination = "OffShoreFundPrices";
				
				meta.spInsert = "proc_OffShoreFundPricesInsert";				
				meta.spUpdate = "proc_OffShoreFundPricesUpdate";		
				meta.spDelete = "proc_OffShoreFundPricesDelete";
				meta.spLoadAll = "proc_OffShoreFundPricesLoadAll";
				meta.spLoadByPrimaryKey = "proc_OffShoreFundPricesLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private OffShoreFundPricesMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
