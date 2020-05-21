/*
===============================================================================
                     EntitySpaces(TM) by EntitySpaces, LLC
                 A New 2.0 Architecture for the .NET Framework
                          http://www.entityspaces.net
===============================================================================
                       EntitySpaces Version # 2007.1.1210.0
                       MyGeneration Version # 1.3.0.3
                           20/02/2008 22:42:08
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
	abstract public class esVwFundPricesCollection : esEntityCollection
	{
		public esVwFundPricesCollection()
		{

		}	
		
		protected override string GetCollectionName()
		{
			return "VwFundPricesCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esVwFundPricesQuery query)
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
            this.InitQuery(query as esVwFundPricesQuery);
        }		
		#endregion
		
		virtual public VwFundPrices DetachEntity(VwFundPrices entity)
		{
			return base.DetachEntity(entity) as VwFundPrices;
		}
		
		virtual public VwFundPrices AttachEntity(VwFundPrices entity)
		{
			return base.AttachEntity(entity) as VwFundPrices;
		}
		
		virtual public void Combine(VwFundPricesCollection collection)
		{
			base.Combine(collection);
		}
		
		new public VwFundPrices this[int index]
		{
			get
			{
				return base[index] as VwFundPrices;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(VwFundPrices);
		}
	}
	


	[Serializable]
	abstract public class esVwFundPrices : esEntity
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esVwFundPricesQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esVwFundPrices()
		{
	
		}
	
		public esVwFundPrices(DataRow row)
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
						case "Description": this.str.Description = (string)value; break;							
						case "BidPriceLatest": this.str.BidPriceLatest = (string)value; break;							
						case "OfferPriceLatest": this.str.OfferPriceLatest = (string)value; break;							
						case "Yield": this.str.Yield = (string)value; break;							
						case "AsOf": this.str.AsOf = (string)value; break;							
						case "BidPricePrevious": this.str.BidPricePrevious = (string)value; break;							
						case "OfferPricePrevious": this.str.OfferPricePrevious = (string)value; break;							
						case "PriceChange": this.str.PriceChange = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "BidPriceLatest":
						
							if (value == null || value.GetType().ToString() == "System.Double")
								this.BidPriceLatest = (System.Double?)value;
							break;
						
						case "OfferPriceLatest":
						
							if (value == null || value.GetType().ToString() == "System.Double")
								this.OfferPriceLatest = (System.Double?)value;
							break;
						
						case "Yield":
						
							if (value == null || value.GetType().ToString() == "System.Double")
								this.Yield = (System.Double?)value;
							break;
						
						case "AsOf":
						
							if (value == null || value.GetType().ToString() == "System.DateTime")
								this.AsOf = (System.DateTime?)value;
							break;
						
						case "BidPricePrevious":
						
							if (value == null || value.GetType().ToString() == "System.Double")
								this.BidPricePrevious = (System.Double?)value;
							break;
						
						case "OfferPricePrevious":
						
							if (value == null || value.GetType().ToString() == "System.Double")
								this.OfferPricePrevious = (System.Double?)value;
							break;
						
						case "PriceChange":
						
							if (value == null || value.GetType().ToString() == "System.Double")
								this.PriceChange = (System.Double?)value;
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
		/// Maps to vw_FundPrices.Description
		/// </summary>
		virtual public System.String Description
		{
			get
			{
				return base.GetSystemString(VwFundPricesMetadata.ColumnNames.Description);
			}
			
			set
			{
				if(base.SetSystemString(VwFundPricesMetadata.ColumnNames.Description, value))
				{
					this.MarkFieldAsModified(VwFundPricesMetadata.ColumnNames.Description);
				}
			}
		}
		
		/// <summary>
		/// Maps to vw_FundPrices.BidPriceLatest
		/// </summary>
		virtual public System.Double? BidPriceLatest
		{
			get
			{
				return base.GetSystemDouble(VwFundPricesMetadata.ColumnNames.BidPriceLatest);
			}
			
			set
			{
				if(base.SetSystemDouble(VwFundPricesMetadata.ColumnNames.BidPriceLatest, value))
				{
					this.MarkFieldAsModified(VwFundPricesMetadata.ColumnNames.BidPriceLatest);
				}
			}
		}
		
		/// <summary>
		/// Maps to vw_FundPrices.OfferPriceLatest
		/// </summary>
		virtual public System.Double? OfferPriceLatest
		{
			get
			{
				return base.GetSystemDouble(VwFundPricesMetadata.ColumnNames.OfferPriceLatest);
			}
			
			set
			{
				if(base.SetSystemDouble(VwFundPricesMetadata.ColumnNames.OfferPriceLatest, value))
				{
					this.MarkFieldAsModified(VwFundPricesMetadata.ColumnNames.OfferPriceLatest);
				}
			}
		}
		
		/// <summary>
		/// Maps to vw_FundPrices.Yield
		/// </summary>
		virtual public System.Double? Yield
		{
			get
			{
				return base.GetSystemDouble(VwFundPricesMetadata.ColumnNames.Yield);
			}
			
			set
			{
				if(base.SetSystemDouble(VwFundPricesMetadata.ColumnNames.Yield, value))
				{
					this.MarkFieldAsModified(VwFundPricesMetadata.ColumnNames.Yield);
				}
			}
		}
		
		/// <summary>
		/// Maps to vw_FundPrices.AsOf
		/// </summary>
		virtual public System.DateTime? AsOf
		{
			get
			{
				return base.GetSystemDateTime(VwFundPricesMetadata.ColumnNames.AsOf);
			}
			
			set
			{
				if(base.SetSystemDateTime(VwFundPricesMetadata.ColumnNames.AsOf, value))
				{
					this.MarkFieldAsModified(VwFundPricesMetadata.ColumnNames.AsOf);
				}
			}
		}
		
		/// <summary>
		/// Maps to vw_FundPrices.BidPricePrevious
		/// </summary>
		virtual public System.Double? BidPricePrevious
		{
			get
			{
				return base.GetSystemDouble(VwFundPricesMetadata.ColumnNames.BidPricePrevious);
			}
			
			set
			{
				if(base.SetSystemDouble(VwFundPricesMetadata.ColumnNames.BidPricePrevious, value))
				{
					this.MarkFieldAsModified(VwFundPricesMetadata.ColumnNames.BidPricePrevious);
				}
			}
		}
		
		/// <summary>
		/// Maps to vw_FundPrices.OfferPricePrevious
		/// </summary>
		virtual public System.Double? OfferPricePrevious
		{
			get
			{
				return base.GetSystemDouble(VwFundPricesMetadata.ColumnNames.OfferPricePrevious);
			}
			
			set
			{
				if(base.SetSystemDouble(VwFundPricesMetadata.ColumnNames.OfferPricePrevious, value))
				{
					this.MarkFieldAsModified(VwFundPricesMetadata.ColumnNames.OfferPricePrevious);
				}
			}
		}
		
		/// <summary>
		/// Maps to vw_FundPrices.PriceChange
		/// </summary>
		virtual public System.Double? PriceChange
		{
			get
			{
				return base.GetSystemDouble(VwFundPricesMetadata.ColumnNames.PriceChange);
			}
			
			set
			{
				if(base.SetSystemDouble(VwFundPricesMetadata.ColumnNames.PriceChange, value))
				{
					this.MarkFieldAsModified(VwFundPricesMetadata.ColumnNames.PriceChange);
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
			public esStrings(esVwFundPrices entity)
			{
				this.entity = entity;
			}
			
	
			public System.String Description
			{
				get
				{
					System.String data = entity.Description;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Description = null;
					else entity.Description = Convert.ToString(value);
				}
			}
				
			public System.String BidPriceLatest
			{
				get
				{
					System.Double? data = entity.BidPriceLatest;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BidPriceLatest = null;
					else entity.BidPriceLatest = Convert.ToDouble(value);
				}
			}
				
			public System.String OfferPriceLatest
			{
				get
				{
					System.Double? data = entity.OfferPriceLatest;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OfferPriceLatest = null;
					else entity.OfferPriceLatest = Convert.ToDouble(value);
				}
			}
				
			public System.String Yield
			{
				get
				{
					System.Double? data = entity.Yield;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Yield = null;
					else entity.Yield = Convert.ToDouble(value);
				}
			}
				
			public System.String AsOf
			{
				get
				{
					System.DateTime? data = entity.AsOf;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AsOf = null;
					else entity.AsOf = Convert.ToDateTime(value);
				}
			}
				
			public System.String BidPricePrevious
			{
				get
				{
					System.Double? data = entity.BidPricePrevious;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BidPricePrevious = null;
					else entity.BidPricePrevious = Convert.ToDouble(value);
				}
			}
				
			public System.String OfferPricePrevious
			{
				get
				{
					System.Double? data = entity.OfferPricePrevious;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OfferPricePrevious = null;
					else entity.OfferPricePrevious = Convert.ToDouble(value);
				}
			}
				
			public System.String PriceChange
			{
				get
				{
					System.Double? data = entity.PriceChange;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PriceChange = null;
					else entity.PriceChange = Convert.ToDouble(value);
				}
			}
			

			private esVwFundPrices entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esVwFundPricesQuery query)
		{
			query.OnLoadEvent += new esDynamicQuery.QueryLoadedDelegate(OnQueryLoaded);
			query.es.Connection = ((IEntity)this).Connection;
		}

		protected bool OnQueryLoaded(DataTable table)
		{
			bool dataFound = this.PopulateEntity(table);

			if (this.RowCount > 1)
			{
				throw new Exception("esVwFundPrices can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}



	[Serializable]
	abstract public class esVwFundPricesQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return VwFundPricesMetadata.Meta();
			}
		}	
		

		public esQueryItem Description
		{
			get
			{
				return new esQueryItem(this, VwFundPricesMetadata.ColumnNames.Description, esSystemType.String);
			}
		} 
		
		public esQueryItem BidPriceLatest
		{
			get
			{
				return new esQueryItem(this, VwFundPricesMetadata.ColumnNames.BidPriceLatest, esSystemType.Double);
			}
		} 
		
		public esQueryItem OfferPriceLatest
		{
			get
			{
				return new esQueryItem(this, VwFundPricesMetadata.ColumnNames.OfferPriceLatest, esSystemType.Double);
			}
		} 
		
		public esQueryItem Yield
		{
			get
			{
				return new esQueryItem(this, VwFundPricesMetadata.ColumnNames.Yield, esSystemType.Double);
			}
		} 
		
		public esQueryItem AsOf
		{
			get
			{
				return new esQueryItem(this, VwFundPricesMetadata.ColumnNames.AsOf, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem BidPricePrevious
		{
			get
			{
				return new esQueryItem(this, VwFundPricesMetadata.ColumnNames.BidPricePrevious, esSystemType.Double);
			}
		} 
		
		public esQueryItem OfferPricePrevious
		{
			get
			{
				return new esQueryItem(this, VwFundPricesMetadata.ColumnNames.OfferPricePrevious, esSystemType.Double);
			}
		} 
		
		public esQueryItem PriceChange
		{
			get
			{
				return new esQueryItem(this, VwFundPricesMetadata.ColumnNames.PriceChange, esSystemType.Double);
			}
		} 
		
	}
	


	[Serializable]
	[XmlType("VwFundPricesCollection")]
	public partial class VwFundPricesCollection : esVwFundPricesCollection, IEnumerable<VwFundPrices>
	{
		public VwFundPricesCollection()
		{

		}	
		
        public static implicit operator List<VwFundPrices>(VwFundPricesCollection coll)
        {
            List<VwFundPrices> list = new List<VwFundPrices>();

            foreach (VwFundPrices emp in coll)
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
				return  VwFundPricesMetadata.Meta();
			}
		}	

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new VwFundPricesQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new VwFundPrices(row);
		}

		override protected esEntity CreateEntity()
		{
			return new VwFundPrices();
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
		public VwFundPricesQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new VwFundPricesQuery();
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
        public bool Load(VwFundPricesQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }		

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
		public VwFundPrices AddNew()
		{
			VwFundPrices entity = base.AddNewEntity() as VwFundPrices;
			
			return entity;		
		}


		#region IEnumerable<VwFundPrices> Members

		IEnumerator<VwFundPrices> IEnumerable<VwFundPrices>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as VwFundPrices;
			}
		}

		#endregion
		
		private VwFundPricesQuery query;
	}
	

	
	/// <summary>
	/// Encapsulates the 'vw_FundPrices' view
	/// </summary>

	[Serializable]
	public partial class VwFundPrices : esVwFundPrices
	{
		public VwFundPrices()
		{

		}	
	
		public VwFundPrices(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return VwFundPricesMetadata.Meta();
			}
		}

		override protected esVwFundPricesQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new VwFundPricesQuery();
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
		public VwFundPricesQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new VwFundPricesQuery();
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
        public bool Load(VwFundPricesQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }			

		private VwFundPricesQuery query;
	}
	


	[Serializable]
	public partial class VwFundPricesQuery : esVwFundPricesQuery
	{
		public VwFundPricesQuery()
		{

		}		
		
        public VwFundPricesQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }		

	}
	


	[Serializable]
	public partial class VwFundPricesMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected VwFundPricesMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(VwFundPricesMetadata.ColumnNames.Description, 0, typeof(System.String), esSystemType.String);	
			c.PropertyName = VwFundPricesMetadata.PropertyNames.Description;
			c.CharacterMaxLength = 100;
			_columns.Add(c); 
				
			c = new esColumnMetadata(VwFundPricesMetadata.ColumnNames.BidPriceLatest, 1, typeof(System.Double), esSystemType.Double);	
			c.PropertyName = VwFundPricesMetadata.PropertyNames.BidPriceLatest;
			c.NumericPrecision = 15;
			_columns.Add(c); 
				
			c = new esColumnMetadata(VwFundPricesMetadata.ColumnNames.OfferPriceLatest, 2, typeof(System.Double), esSystemType.Double);	
			c.PropertyName = VwFundPricesMetadata.PropertyNames.OfferPriceLatest;
			c.NumericPrecision = 15;
			_columns.Add(c); 
				
			c = new esColumnMetadata(VwFundPricesMetadata.ColumnNames.Yield, 3, typeof(System.Double), esSystemType.Double);	
			c.PropertyName = VwFundPricesMetadata.PropertyNames.Yield;
			c.NumericPrecision = 15;
			_columns.Add(c); 
				
			c = new esColumnMetadata(VwFundPricesMetadata.ColumnNames.AsOf, 4, typeof(System.DateTime), esSystemType.DateTime);	
			c.PropertyName = VwFundPricesMetadata.PropertyNames.AsOf;
			_columns.Add(c); 
				
			c = new esColumnMetadata(VwFundPricesMetadata.ColumnNames.BidPricePrevious, 5, typeof(System.Double), esSystemType.Double);	
			c.PropertyName = VwFundPricesMetadata.PropertyNames.BidPricePrevious;
			c.NumericPrecision = 15;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(VwFundPricesMetadata.ColumnNames.OfferPricePrevious, 6, typeof(System.Double), esSystemType.Double);	
			c.PropertyName = VwFundPricesMetadata.PropertyNames.OfferPricePrevious;
			c.NumericPrecision = 15;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(VwFundPricesMetadata.ColumnNames.PriceChange, 7, typeof(System.Double), esSystemType.Double);	
			c.PropertyName = VwFundPricesMetadata.PropertyNames.PriceChange;
			c.NumericPrecision = 15;
			c.IsNullable = true;
			_columns.Add(c); 
				
		}
		#endregion

		static public VwFundPricesMetadata Meta()
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
			 public const string Description = "Description";
			 public const string BidPriceLatest = "BidPriceLatest";
			 public const string OfferPriceLatest = "OfferPriceLatest";
			 public const string Yield = "Yield";
			 public const string AsOf = "AsOf";
			 public const string BidPricePrevious = "BidPricePrevious";
			 public const string OfferPricePrevious = "OfferPricePrevious";
			 public const string PriceChange = "PriceChange";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string Description = "Description";
			 public const string BidPriceLatest = "BidPriceLatest";
			 public const string OfferPriceLatest = "OfferPriceLatest";
			 public const string Yield = "Yield";
			 public const string AsOf = "AsOf";
			 public const string BidPricePrevious = "BidPricePrevious";
			 public const string OfferPricePrevious = "OfferPricePrevious";
			 public const string PriceChange = "PriceChange";
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
            lock (typeof(VwFundPricesMetadata))
            {
				if(VwFundPricesMetadata.mapDelegates == null)
				{
					VwFundPricesMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
			
                if (VwFundPricesMetadata.meta == null)
                {
                    VwFundPricesMetadata.meta = new VwFundPricesMetadata();
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
				
				meta.AddTypeMap("Description", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("BidPriceLatest", new esTypeMap("float", "System.Double"));
				meta.AddTypeMap("OfferPriceLatest", new esTypeMap("float", "System.Double"));
				meta.AddTypeMap("Yield", new esTypeMap("float", "System.Double"));
				meta.AddTypeMap("AsOf", new esTypeMap("smalldatetime", "System.DateTime"));
				meta.AddTypeMap("BidPricePrevious", new esTypeMap("float", "System.Double"));
				meta.AddTypeMap("OfferPricePrevious", new esTypeMap("float", "System.Double"));
				meta.AddTypeMap("PriceChange", new esTypeMap("float", "System.Double"));
			
				
				
				
				meta.Source = "vw_FundPrices";
				meta.Destination = "vw_FundPrices";
				
				meta.spInsert = "proc_vw_FundPricesInsert";				
				meta.spUpdate = "proc_vw_FundPricesUpdate";		
				meta.spDelete = "proc_vw_FundPricesDelete";
				meta.spLoadAll = "proc_vw_FundPricesLoadAll";
				meta.spLoadByPrimaryKey = "proc_vw_FundPricesLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private VwFundPricesMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
