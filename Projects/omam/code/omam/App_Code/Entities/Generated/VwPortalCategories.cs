/*
===============================================================================
                     EntitySpaces(TM) by EntitySpaces, LLC
                 A New 2.0 Architecture for the .NET Framework
                          http://www.entityspaces.net
===============================================================================
                       EntitySpaces Version # 2007.1.1210.0
                       MyGeneration Version # 1.3.0.3
                           02/03/2008 21:33:12
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
	abstract public class esVwPortalCategoriesCollection : esEntityCollection
	{
		public esVwPortalCategoriesCollection()
		{

		}	
		
		protected override string GetCollectionName()
		{
			return "VwPortalCategoriesCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esVwPortalCategoriesQuery query)
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
            this.InitQuery(query as esVwPortalCategoriesQuery);
        }		
		#endregion
		
		virtual public VwPortalCategories DetachEntity(VwPortalCategories entity)
		{
			return base.DetachEntity(entity) as VwPortalCategories;
		}
		
		virtual public VwPortalCategories AttachEntity(VwPortalCategories entity)
		{
			return base.AttachEntity(entity) as VwPortalCategories;
		}
		
		virtual public void Combine(VwPortalCategoriesCollection collection)
		{
			base.Combine(collection);
		}
		
		new public VwPortalCategories this[int index]
		{
			get
			{
				return base[index] as VwPortalCategories;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(VwPortalCategories);
		}
	}
	


	[Serializable]
	abstract public class esVwPortalCategories : esEntity
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esVwPortalCategoriesQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esVwPortalCategories()
		{
	
		}
	
		public esVwPortalCategories(DataRow row)
			: base(row)
		{
	
		}
		
		#region LoadByPrimaryKey
		
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
						case "Category": this.str.Category = (string)value; break;							
						case "PortalId": this.str.PortalId = (string)value; break;							
						case "Id": this.str.Id = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "PortalId":
						
							if (value == null || value.GetType().ToString() == "System.Int32")
								this.PortalId = (System.Int32?)value;
							break;
						
						case "Id":
						
							if (value == null || value.GetType().ToString() == "System.Int32")
								this.Id = (System.Int32?)value;
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
		/// Maps to vw_PortalCategories.Category
		/// </summary>
		virtual public System.String Category
		{
			get
			{
				return base.GetSystemString(VwPortalCategoriesMetadata.ColumnNames.Category);
			}
			
			set
			{
				if(base.SetSystemString(VwPortalCategoriesMetadata.ColumnNames.Category, value))
				{
					this.MarkFieldAsModified(VwPortalCategoriesMetadata.ColumnNames.Category);
				}
			}
		}
		
		/// <summary>
		/// Maps to vw_PortalCategories.PortalId
		/// </summary>
		virtual public System.Int32? PortalId
		{
			get
			{
				return base.GetSystemInt32(VwPortalCategoriesMetadata.ColumnNames.PortalId);
			}
			
			set
			{
				if(base.SetSystemInt32(VwPortalCategoriesMetadata.ColumnNames.PortalId, value))
				{
					this.MarkFieldAsModified(VwPortalCategoriesMetadata.ColumnNames.PortalId);
				}
			}
		}
		
		/// <summary>
		/// Maps to vw_PortalCategories.Id
		/// </summary>
		virtual public System.Int32? Id
		{
			get
			{
				return base.GetSystemInt32(VwPortalCategoriesMetadata.ColumnNames.Id);
			}
			
			set
			{
				if(base.SetSystemInt32(VwPortalCategoriesMetadata.ColumnNames.Id, value))
				{
					this.MarkFieldAsModified(VwPortalCategoriesMetadata.ColumnNames.Id);
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
			public esStrings(esVwPortalCategories entity)
			{
				this.entity = entity;
			}
			
	
			public System.String Category
			{
				get
				{
					System.String data = entity.Category;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Category = null;
					else entity.Category = Convert.ToString(value);
				}
			}
				
			public System.String PortalId
			{
				get
				{
					System.Int32? data = entity.PortalId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PortalId = null;
					else entity.PortalId = Convert.ToInt32(value);
				}
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
			

			private esVwPortalCategories entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esVwPortalCategoriesQuery query)
		{
			query.OnLoadEvent += new esDynamicQuery.QueryLoadedDelegate(OnQueryLoaded);
			query.es.Connection = ((IEntity)this).Connection;
		}

		protected bool OnQueryLoaded(DataTable table)
		{
			bool dataFound = this.PopulateEntity(table);

			if (this.RowCount > 1)
			{
				throw new Exception("esVwPortalCategories can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}



	[Serializable]
	abstract public class esVwPortalCategoriesQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return VwPortalCategoriesMetadata.Meta();
			}
		}	
		

		public esQueryItem Category
		{
			get
			{
				return new esQueryItem(this, VwPortalCategoriesMetadata.ColumnNames.Category, esSystemType.String);
			}
		} 
		
		public esQueryItem PortalId
		{
			get
			{
				return new esQueryItem(this, VwPortalCategoriesMetadata.ColumnNames.PortalId, esSystemType.Int32);
			}
		} 
		
		public esQueryItem Id
		{
			get
			{
				return new esQueryItem(this, VwPortalCategoriesMetadata.ColumnNames.Id, esSystemType.Int32);
			}
		} 
		
	}
	


	[Serializable]
	[XmlType("VwPortalCategoriesCollection")]
	public partial class VwPortalCategoriesCollection : esVwPortalCategoriesCollection, IEnumerable<VwPortalCategories>
	{
		public VwPortalCategoriesCollection()
		{

		}	
		
        public static implicit operator List<VwPortalCategories>(VwPortalCategoriesCollection coll)
        {
            List<VwPortalCategories> list = new List<VwPortalCategories>();

            foreach (VwPortalCategories emp in coll)
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
				return  VwPortalCategoriesMetadata.Meta();
			}
		}	

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new VwPortalCategoriesQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new VwPortalCategories(row);
		}

		override protected esEntity CreateEntity()
		{
			return new VwPortalCategories();
		}
		
		
		override public bool LoadAll()
		{
			return base.LoadAll(esSqlAccessType.DynamicSQL);
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
		public VwPortalCategoriesQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new VwPortalCategoriesQuery();
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
        public bool Load(VwPortalCategoriesQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }		

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
		public VwPortalCategories AddNew()
		{
			VwPortalCategories entity = base.AddNewEntity() as VwPortalCategories;
			
			return entity;		
		}


		#region IEnumerable<VwPortalCategories> Members

		IEnumerator<VwPortalCategories> IEnumerable<VwPortalCategories>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as VwPortalCategories;
			}
		}

		#endregion
		
		private VwPortalCategoriesQuery query;
	}
	

	
	/// <summary>
	/// Encapsulates the 'vw_PortalCategories' view
	/// </summary>

	[Serializable]
	public partial class VwPortalCategories : esVwPortalCategories
	{
		public VwPortalCategories()
		{

		}	
	
		public VwPortalCategories(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return VwPortalCategoriesMetadata.Meta();
			}
		}

		override protected esVwPortalCategoriesQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new VwPortalCategoriesQuery();
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
		public VwPortalCategoriesQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new VwPortalCategoriesQuery();
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
        public bool Load(VwPortalCategoriesQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }			

		private VwPortalCategoriesQuery query;
	}
	


	[Serializable]
	public partial class VwPortalCategoriesQuery : esVwPortalCategoriesQuery
	{
		public VwPortalCategoriesQuery()
		{

		}		
		
        public VwPortalCategoriesQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }		

	}
	


	[Serializable]
	public partial class VwPortalCategoriesMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected VwPortalCategoriesMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(VwPortalCategoriesMetadata.ColumnNames.Category, 0, typeof(System.String), esSystemType.String);	
			c.PropertyName = VwPortalCategoriesMetadata.PropertyNames.Category;
			c.CharacterMaxLength = 50;
			_columns.Add(c); 
				
			c = new esColumnMetadata(VwPortalCategoriesMetadata.ColumnNames.PortalId, 1, typeof(System.Int32), esSystemType.Int32);	
			c.PropertyName = VwPortalCategoriesMetadata.PropertyNames.PortalId;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(VwPortalCategoriesMetadata.ColumnNames.Id, 2, typeof(System.Int32), esSystemType.Int32);	
			c.PropertyName = VwPortalCategoriesMetadata.PropertyNames.Id;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
		}
		#endregion

		static public VwPortalCategoriesMetadata Meta()
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
			 public const string Category = "Category";
			 public const string PortalId = "PortalId";
			 public const string Id = "Id";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string Category = "Category";
			 public const string PortalId = "PortalId";
			 public const string Id = "Id";
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
            lock (typeof(VwPortalCategoriesMetadata))
            {
				if(VwPortalCategoriesMetadata.mapDelegates == null)
				{
					VwPortalCategoriesMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
			
                if (VwPortalCategoriesMetadata.meta == null)
                {
                    VwPortalCategoriesMetadata.meta = new VwPortalCategoriesMetadata();
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
				
				meta.AddTypeMap("Category", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("PortalId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("Id", new esTypeMap("int", "System.Int32"));
			
				
				
				
				meta.Source = "vw_PortalCategories";
				meta.Destination = "vw_PortalCategories";
				
				meta.spInsert = "proc_vw_PortalCategoriesInsert";				
				meta.spUpdate = "proc_vw_PortalCategoriesUpdate";		
				meta.spDelete = "proc_vw_PortalCategoriesDelete";
				meta.spLoadAll = "proc_vw_PortalCategoriesLoadAll";
				meta.spLoadByPrimaryKey = "proc_vw_PortalCategoriesLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private VwPortalCategoriesMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
