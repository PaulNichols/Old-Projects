/*
===============================================================================
                     EntitySpaces(TM) by EntitySpaces, LLC
                 A New 2.0 Architecture for the .NET Framework
                          http://www.entityspaces.net
===============================================================================
                       EntitySpaces Version # 2007.1.1210.0
                       MyGeneration Version # 1.3.0.3
                           22/02/2008 21:51:12
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
	abstract public class esVwLatestOffShoreFundPricesCollection : esEntityCollection
	{
		public esVwLatestOffShoreFundPricesCollection()
		{

		}	
		
		protected override string GetCollectionName()
		{
			return "VwLatestOffShoreFundPricesCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esVwLatestOffShoreFundPricesQuery query)
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
            this.InitQuery(query as esVwLatestOffShoreFundPricesQuery);
        }		
		#endregion
		
		virtual public VwLatestOffShoreFundPrices DetachEntity(VwLatestOffShoreFundPrices entity)
		{
			return base.DetachEntity(entity) as VwLatestOffShoreFundPrices;
		}
		
		virtual public VwLatestOffShoreFundPrices AttachEntity(VwLatestOffShoreFundPrices entity)
		{
			return base.AttachEntity(entity) as VwLatestOffShoreFundPrices;
		}
		
		virtual public void Combine(VwLatestOffShoreFundPricesCollection collection)
		{
			base.Combine(collection);
		}
		
		new public VwLatestOffShoreFundPrices this[int index]
		{
			get
			{
				return base[index] as VwLatestOffShoreFundPrices;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(VwLatestOffShoreFundPrices);
		}
	}
	


	[Serializable]
	abstract public class esVwLatestOffShoreFundPrices : esEntity
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esVwLatestOffShoreFundPricesQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esVwLatestOffShoreFundPrices()
		{
	
		}
	
		public esVwLatestOffShoreFundPrices(DataRow row)
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
						case "FundName": this.str.FundName = (string)value; break;							
						case "Price": this.str.Price = (string)value; break;							
						case "Currency": this.str.Currency = (string)value; break;							
						case "OMAMFundId": this.str.OMAMFundId = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "Price":
						
							if (value == null || value.GetType().ToString() == "System.Double")
								this.Price = (System.Double?)value;
							break;
						
						case "OMAMFundId":
						
							if (value == null || value.GetType().ToString() == "System.Int32")
								this.OMAMFundId = (System.Int32?)value;
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
		/// Maps to vw_LatestOffShoreFundPrices.FundName
		/// </summary>
		virtual public System.String FundName
		{
			get
			{
				return base.GetSystemString(VwLatestOffShoreFundPricesMetadata.ColumnNames.FundName);
			}
			
			set
			{
				if(base.SetSystemString(VwLatestOffShoreFundPricesMetadata.ColumnNames.FundName, value))
				{
					this.MarkFieldAsModified(VwLatestOffShoreFundPricesMetadata.ColumnNames.FundName);
				}
			}
		}
		
		/// <summary>
		/// Maps to vw_LatestOffShoreFundPrices.Price
		/// </summary>
		virtual public System.Double? Price
		{
			get
			{
				return base.GetSystemDouble(VwLatestOffShoreFundPricesMetadata.ColumnNames.Price);
			}
			
			set
			{
				if(base.SetSystemDouble(VwLatestOffShoreFundPricesMetadata.ColumnNames.Price, value))
				{
					this.MarkFieldAsModified(VwLatestOffShoreFundPricesMetadata.ColumnNames.Price);
				}
			}
		}
		
		/// <summary>
		/// Maps to vw_LatestOffShoreFundPrices.Currency
		/// </summary>
		virtual public System.String Currency
		{
			get
			{
				return base.GetSystemString(VwLatestOffShoreFundPricesMetadata.ColumnNames.Currency);
			}
			
			set
			{
				if(base.SetSystemString(VwLatestOffShoreFundPricesMetadata.ColumnNames.Currency, value))
				{
					this.MarkFieldAsModified(VwLatestOffShoreFundPricesMetadata.ColumnNames.Currency);
				}
			}
		}
		
		/// <summary>
		/// Maps to vw_LatestOffShoreFundPrices.OMAMFundId
		/// </summary>
		virtual public System.Int32? OMAMFundId
		{
			get
			{
				return base.GetSystemInt32(VwLatestOffShoreFundPricesMetadata.ColumnNames.OMAMFundId);
			}
			
			set
			{
				if(base.SetSystemInt32(VwLatestOffShoreFundPricesMetadata.ColumnNames.OMAMFundId, value))
				{
					this.MarkFieldAsModified(VwLatestOffShoreFundPricesMetadata.ColumnNames.OMAMFundId);
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
			public esStrings(esVwLatestOffShoreFundPrices entity)
			{
				this.entity = entity;
			}
			
	
			public System.String FundName
			{
				get
				{
					System.String data = entity.FundName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FundName = null;
					else entity.FundName = Convert.ToString(value);
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
				
			public System.String Currency
			{
				get
				{
					System.String data = entity.Currency;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Currency = null;
					else entity.Currency = Convert.ToString(value);
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
			

			private esVwLatestOffShoreFundPrices entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esVwLatestOffShoreFundPricesQuery query)
		{
			query.OnLoadEvent += new esDynamicQuery.QueryLoadedDelegate(OnQueryLoaded);
			query.es.Connection = ((IEntity)this).Connection;
		}

		protected bool OnQueryLoaded(DataTable table)
		{
			bool dataFound = this.PopulateEntity(table);

			if (this.RowCount > 1)
			{
				throw new Exception("esVwLatestOffShoreFundPrices can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}



	[Serializable]
	abstract public class esVwLatestOffShoreFundPricesQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return VwLatestOffShoreFundPricesMetadata.Meta();
			}
		}	
		

		public esQueryItem FundName
		{
			get
			{
				return new esQueryItem(this, VwLatestOffShoreFundPricesMetadata.ColumnNames.FundName, esSystemType.String);
			}
		} 
		
		public esQueryItem Price
		{
			get
			{
				return new esQueryItem(this, VwLatestOffShoreFundPricesMetadata.ColumnNames.Price, esSystemType.Double);
			}
		} 
		
		public esQueryItem Currency
		{
			get
			{
				return new esQueryItem(this, VwLatestOffShoreFundPricesMetadata.ColumnNames.Currency, esSystemType.String);
			}
		} 
		
		public esQueryItem OMAMFundId
		{
			get
			{
				return new esQueryItem(this, VwLatestOffShoreFundPricesMetadata.ColumnNames.OMAMFundId, esSystemType.Int32);
			}
		} 
		
	}
	


	[Serializable]
	[XmlType("VwLatestOffShoreFundPricesCollection")]
	public partial class VwLatestOffShoreFundPricesCollection : esVwLatestOffShoreFundPricesCollection, IEnumerable<VwLatestOffShoreFundPrices>
	{
		public VwLatestOffShoreFundPricesCollection()
		{

		}	
		
        public static implicit operator List<VwLatestOffShoreFundPrices>(VwLatestOffShoreFundPricesCollection coll)
        {
            List<VwLatestOffShoreFundPrices> list = new List<VwLatestOffShoreFundPrices>();

            foreach (VwLatestOffShoreFundPrices emp in coll)
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
				return  VwLatestOffShoreFundPricesMetadata.Meta();
			}
		}	

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new VwLatestOffShoreFundPricesQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new VwLatestOffShoreFundPrices(row);
		}

		override protected esEntity CreateEntity()
		{
			return new VwLatestOffShoreFundPrices();
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
		public VwLatestOffShoreFundPricesQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new VwLatestOffShoreFundPricesQuery();
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
        public bool Load(VwLatestOffShoreFundPricesQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }		

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
		public VwLatestOffShoreFundPrices AddNew()
		{
			VwLatestOffShoreFundPrices entity = base.AddNewEntity() as VwLatestOffShoreFundPrices;
			
			return entity;		
		}


		#region IEnumerable<VwLatestOffShoreFundPrices> Members

		IEnumerator<VwLatestOffShoreFundPrices> IEnumerable<VwLatestOffShoreFundPrices>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as VwLatestOffShoreFundPrices;
			}
		}

		#endregion
		
		private VwLatestOffShoreFundPricesQuery query;
	}
	

	
	/// <summary>
	/// Encapsulates the 'vw_LatestOffShoreFundPrices' view
	/// </summary>

	[Serializable]
	public partial class VwLatestOffShoreFundPrices : esVwLatestOffShoreFundPrices
	{
		public VwLatestOffShoreFundPrices()
		{

		}	
	
		public VwLatestOffShoreFundPrices(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return VwLatestOffShoreFundPricesMetadata.Meta();
			}
		}

		override protected esVwLatestOffShoreFundPricesQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new VwLatestOffShoreFundPricesQuery();
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
		public VwLatestOffShoreFundPricesQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new VwLatestOffShoreFundPricesQuery();
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
        public bool Load(VwLatestOffShoreFundPricesQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }			

		private VwLatestOffShoreFundPricesQuery query;
	}
	


	[Serializable]
	public partial class VwLatestOffShoreFundPricesQuery : esVwLatestOffShoreFundPricesQuery
	{
		public VwLatestOffShoreFundPricesQuery()
		{

		}		
		
        public VwLatestOffShoreFundPricesQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }		

	}
	


	[Serializable]
	public partial class VwLatestOffShoreFundPricesMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected VwLatestOffShoreFundPricesMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(VwLatestOffShoreFundPricesMetadata.ColumnNames.FundName, 0, typeof(System.String), esSystemType.String);	
			c.PropertyName = VwLatestOffShoreFundPricesMetadata.PropertyNames.FundName;
			c.CharacterMaxLength = 100;
			_columns.Add(c); 
				
			c = new esColumnMetadata(VwLatestOffShoreFundPricesMetadata.ColumnNames.Price, 1, typeof(System.Double), esSystemType.Double);	
			c.PropertyName = VwLatestOffShoreFundPricesMetadata.PropertyNames.Price;
			c.NumericPrecision = 15;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(VwLatestOffShoreFundPricesMetadata.ColumnNames.Currency, 2, typeof(System.String), esSystemType.String);	
			c.PropertyName = VwLatestOffShoreFundPricesMetadata.PropertyNames.Currency;
			c.CharacterMaxLength = 5;
			_columns.Add(c); 
				
			c = new esColumnMetadata(VwLatestOffShoreFundPricesMetadata.ColumnNames.OMAMFundId, 3, typeof(System.Int32), esSystemType.Int32);	
			c.PropertyName = VwLatestOffShoreFundPricesMetadata.PropertyNames.OMAMFundId;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
		}
		#endregion

		static public VwLatestOffShoreFundPricesMetadata Meta()
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
			 public const string FundName = "FundName";
			 public const string Price = "Price";
			 public const string Currency = "Currency";
			 public const string OMAMFundId = "OMAMFundId";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string FundName = "FundName";
			 public const string Price = "Price";
			 public const string Currency = "Currency";
			 public const string OMAMFundId = "OMAMFundId";
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
            lock (typeof(VwLatestOffShoreFundPricesMetadata))
            {
				if(VwLatestOffShoreFundPricesMetadata.mapDelegates == null)
				{
					VwLatestOffShoreFundPricesMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
			
                if (VwLatestOffShoreFundPricesMetadata.meta == null)
                {
                    VwLatestOffShoreFundPricesMetadata.meta = new VwLatestOffShoreFundPricesMetadata();
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
				
				meta.AddTypeMap("FundName", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("Price", new esTypeMap("float", "System.Double"));
				meta.AddTypeMap("Currency", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("OMAMFundId", new esTypeMap("int", "System.Int32"));
			
				
				
				
				meta.Source = "vw_LatestOffShoreFundPrices";
				meta.Destination = "vw_LatestOffShoreFundPrices";
				
				meta.spInsert = "proc_vw_LatestOffShoreFundPricesInsert";				
				meta.spUpdate = "proc_vw_LatestOffShoreFundPricesUpdate";		
				meta.spDelete = "proc_vw_LatestOffShoreFundPricesDelete";
				meta.spLoadAll = "proc_vw_LatestOffShoreFundPricesLoadAll";
				meta.spLoadByPrimaryKey = "proc_vw_LatestOffShoreFundPricesLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private VwLatestOffShoreFundPricesMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
