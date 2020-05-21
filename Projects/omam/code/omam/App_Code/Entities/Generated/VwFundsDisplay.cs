/*
===============================================================================
                     EntitySpaces(TM) by EntitySpaces, LLC
                 A New 2.0 Architecture for the .NET Framework
                          http://www.entityspaces.net
===============================================================================
                       EntitySpaces Version # 2007.1.1210.0
                       MyGeneration Version # 1.3.0.3
                           02/03/2008 23:53:08
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
	abstract public class esVwFundsDisplayCollection : esEntityCollection
	{
		public esVwFundsDisplayCollection()
		{

		}	
		
		protected override string GetCollectionName()
		{
			return "VwFundsDisplayCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esVwFundsDisplayQuery query)
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
            this.InitQuery(query as esVwFundsDisplayQuery);
        }		
		#endregion
		
		virtual public VwFundsDisplay DetachEntity(VwFundsDisplay entity)
		{
			return base.DetachEntity(entity) as VwFundsDisplay;
		}
		
		virtual public VwFundsDisplay AttachEntity(VwFundsDisplay entity)
		{
			return base.AttachEntity(entity) as VwFundsDisplay;
		}
		
		virtual public void Combine(VwFundsDisplayCollection collection)
		{
			base.Combine(collection);
		}
		
		new public VwFundsDisplay this[int index]
		{
			get
			{
				return base[index] as VwFundsDisplay;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(VwFundsDisplay);
		}
	}
	


	[Serializable]
	abstract public class esVwFundsDisplay : esEntity
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esVwFundsDisplayQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esVwFundsDisplay()
		{
	
		}
	
		public esVwFundsDisplay(DataRow row)
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
						case "FundId": this.str.FundId = (string)value; break;							
						case "FactsheetFile": this.str.FactsheetFile = (string)value; break;							
						case "FactsheetURL": this.str.FactsheetURL = (string)value; break;							
						case "BidPrice": this.str.BidPrice = (string)value; break;							
						case "OfferPrice": this.str.OfferPrice = (string)value; break;							
						case "Omamtv": this.str.Omamtv = (string)value; break;							
						case "CityWireRatingURL": this.str.CityWireRatingURL = (string)value; break;							
						case "CityWitreRatingCopy": this.str.CityWitreRatingCopy = (string)value; break;							
						case "OBSRRatingURL": this.str.OBSRRatingURL = (string)value; break;							
						case "OBSRRatingCopy": this.str.OBSRRatingCopy = (string)value; break;							
						case "SPRatingCopy": this.str.SPRatingCopy = (string)value; break;							
						case "SPRatingURL": this.str.SPRatingURL = (string)value; break;							
						case "CategoryId": this.str.CategoryId = (string)value; break;							
						case "PortalId": this.str.PortalId = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "FundId":
						
							if (value == null || value.GetType().ToString() == "System.Int32")
								this.FundId = (System.Int32?)value;
							break;
						
						case "BidPrice":
						
							if (value == null || value.GetType().ToString() == "System.Double")
								this.BidPrice = (System.Double?)value;
							break;
						
						case "OfferPrice":
						
							if (value == null || value.GetType().ToString() == "System.Double")
								this.OfferPrice = (System.Double?)value;
							break;
						
						case "CategoryId":
						
							if (value == null || value.GetType().ToString() == "System.Int32")
								this.CategoryId = (System.Int32?)value;
							break;
						
						case "PortalId":
						
							if (value == null || value.GetType().ToString() == "System.Int32")
								this.PortalId = (System.Int32?)value;
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
		/// Maps to vw_FundsDisplay.FundName
		/// </summary>
		virtual public System.String FundName
		{
			get
			{
				return base.GetSystemString(VwFundsDisplayMetadata.ColumnNames.FundName);
			}
			
			set
			{
				if(base.SetSystemString(VwFundsDisplayMetadata.ColumnNames.FundName, value))
				{
					this.MarkFieldAsModified(VwFundsDisplayMetadata.ColumnNames.FundName);
				}
			}
		}
		
		/// <summary>
		/// Maps to vw_FundsDisplay.FundId
		/// </summary>
		virtual public System.Int32? FundId
		{
			get
			{
				return base.GetSystemInt32(VwFundsDisplayMetadata.ColumnNames.FundId);
			}
			
			set
			{
				if(base.SetSystemInt32(VwFundsDisplayMetadata.ColumnNames.FundId, value))
				{
					this.MarkFieldAsModified(VwFundsDisplayMetadata.ColumnNames.FundId);
				}
			}
		}
		
		/// <summary>
		/// Maps to vw_FundsDisplay.FactsheetFile
		/// </summary>
		virtual public System.String FactsheetFile
		{
			get
			{
				return base.GetSystemString(VwFundsDisplayMetadata.ColumnNames.FactsheetFile);
			}
			
			set
			{
				if(base.SetSystemString(VwFundsDisplayMetadata.ColumnNames.FactsheetFile, value))
				{
					this.MarkFieldAsModified(VwFundsDisplayMetadata.ColumnNames.FactsheetFile);
				}
			}
		}
		
		/// <summary>
		/// Maps to vw_FundsDisplay.FactsheetURL
		/// </summary>
		virtual public System.String FactsheetURL
		{
			get
			{
				return base.GetSystemString(VwFundsDisplayMetadata.ColumnNames.FactsheetURL);
			}
			
			set
			{
				if(base.SetSystemString(VwFundsDisplayMetadata.ColumnNames.FactsheetURL, value))
				{
					this.MarkFieldAsModified(VwFundsDisplayMetadata.ColumnNames.FactsheetURL);
				}
			}
		}
		
		/// <summary>
		/// Maps to vw_FundsDisplay.BidPrice
		/// </summary>
		virtual public System.Double? BidPrice
		{
			get
			{
				return base.GetSystemDouble(VwFundsDisplayMetadata.ColumnNames.BidPrice);
			}
			
			set
			{
				if(base.SetSystemDouble(VwFundsDisplayMetadata.ColumnNames.BidPrice, value))
				{
					this.MarkFieldAsModified(VwFundsDisplayMetadata.ColumnNames.BidPrice);
				}
			}
		}
		
		/// <summary>
		/// Maps to vw_FundsDisplay.OfferPrice
		/// </summary>
		virtual public System.Double? OfferPrice
		{
			get
			{
				return base.GetSystemDouble(VwFundsDisplayMetadata.ColumnNames.OfferPrice);
			}
			
			set
			{
				if(base.SetSystemDouble(VwFundsDisplayMetadata.ColumnNames.OfferPrice, value))
				{
					this.MarkFieldAsModified(VwFundsDisplayMetadata.ColumnNames.OfferPrice);
				}
			}
		}
		
		/// <summary>
		/// Maps to vw_FundsDisplay.OMAMTV
		/// </summary>
		virtual public System.String Omamtv
		{
			get
			{
				return base.GetSystemString(VwFundsDisplayMetadata.ColumnNames.Omamtv);
			}
			
			set
			{
				if(base.SetSystemString(VwFundsDisplayMetadata.ColumnNames.Omamtv, value))
				{
					this.MarkFieldAsModified(VwFundsDisplayMetadata.ColumnNames.Omamtv);
				}
			}
		}
		
		/// <summary>
		/// Maps to vw_FundsDisplay.CityWireRatingURL
		/// </summary>
		virtual public System.String CityWireRatingURL
		{
			get
			{
				return base.GetSystemString(VwFundsDisplayMetadata.ColumnNames.CityWireRatingURL);
			}
			
			set
			{
				if(base.SetSystemString(VwFundsDisplayMetadata.ColumnNames.CityWireRatingURL, value))
				{
					this.MarkFieldAsModified(VwFundsDisplayMetadata.ColumnNames.CityWireRatingURL);
				}
			}
		}
		
		/// <summary>
		/// Maps to vw_FundsDisplay.CityWitreRatingCopy
		/// </summary>
		virtual public System.String CityWitreRatingCopy
		{
			get
			{
				return base.GetSystemString(VwFundsDisplayMetadata.ColumnNames.CityWitreRatingCopy);
			}
			
			set
			{
				if(base.SetSystemString(VwFundsDisplayMetadata.ColumnNames.CityWitreRatingCopy, value))
				{
					this.MarkFieldAsModified(VwFundsDisplayMetadata.ColumnNames.CityWitreRatingCopy);
				}
			}
		}
		
		/// <summary>
		/// Maps to vw_FundsDisplay.OBSRRatingURL
		/// </summary>
		virtual public System.String OBSRRatingURL
		{
			get
			{
				return base.GetSystemString(VwFundsDisplayMetadata.ColumnNames.OBSRRatingURL);
			}
			
			set
			{
				if(base.SetSystemString(VwFundsDisplayMetadata.ColumnNames.OBSRRatingURL, value))
				{
					this.MarkFieldAsModified(VwFundsDisplayMetadata.ColumnNames.OBSRRatingURL);
				}
			}
		}
		
		/// <summary>
		/// Maps to vw_FundsDisplay.OBSRRatingCopy
		/// </summary>
		virtual public System.String OBSRRatingCopy
		{
			get
			{
				return base.GetSystemString(VwFundsDisplayMetadata.ColumnNames.OBSRRatingCopy);
			}
			
			set
			{
				if(base.SetSystemString(VwFundsDisplayMetadata.ColumnNames.OBSRRatingCopy, value))
				{
					this.MarkFieldAsModified(VwFundsDisplayMetadata.ColumnNames.OBSRRatingCopy);
				}
			}
		}
		
		/// <summary>
		/// Maps to vw_FundsDisplay.SPRatingCopy
		/// </summary>
		virtual public System.String SPRatingCopy
		{
			get
			{
				return base.GetSystemString(VwFundsDisplayMetadata.ColumnNames.SPRatingCopy);
			}
			
			set
			{
				if(base.SetSystemString(VwFundsDisplayMetadata.ColumnNames.SPRatingCopy, value))
				{
					this.MarkFieldAsModified(VwFundsDisplayMetadata.ColumnNames.SPRatingCopy);
				}
			}
		}
		
		/// <summary>
		/// Maps to vw_FundsDisplay.SPRatingURL
		/// </summary>
		virtual public System.String SPRatingURL
		{
			get
			{
				return base.GetSystemString(VwFundsDisplayMetadata.ColumnNames.SPRatingURL);
			}
			
			set
			{
				if(base.SetSystemString(VwFundsDisplayMetadata.ColumnNames.SPRatingURL, value))
				{
					this.MarkFieldAsModified(VwFundsDisplayMetadata.ColumnNames.SPRatingURL);
				}
			}
		}
		
		/// <summary>
		/// Maps to vw_FundsDisplay.CategoryId
		/// </summary>
		virtual public System.Int32? CategoryId
		{
			get
			{
				return base.GetSystemInt32(VwFundsDisplayMetadata.ColumnNames.CategoryId);
			}
			
			set
			{
				if(base.SetSystemInt32(VwFundsDisplayMetadata.ColumnNames.CategoryId, value))
				{
					this.MarkFieldAsModified(VwFundsDisplayMetadata.ColumnNames.CategoryId);
				}
			}
		}
		
		/// <summary>
		/// Maps to vw_FundsDisplay.PortalId
		/// </summary>
		virtual public System.Int32? PortalId
		{
			get
			{
				return base.GetSystemInt32(VwFundsDisplayMetadata.ColumnNames.PortalId);
			}
			
			set
			{
				if(base.SetSystemInt32(VwFundsDisplayMetadata.ColumnNames.PortalId, value))
				{
					this.MarkFieldAsModified(VwFundsDisplayMetadata.ColumnNames.PortalId);
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
			public esStrings(esVwFundsDisplay entity)
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
				
			public System.String FundId
			{
				get
				{
					System.Int32? data = entity.FundId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FundId = null;
					else entity.FundId = Convert.ToInt32(value);
				}
			}
				
			public System.String FactsheetFile
			{
				get
				{
					System.String data = entity.FactsheetFile;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FactsheetFile = null;
					else entity.FactsheetFile = Convert.ToString(value);
				}
			}
				
			public System.String FactsheetURL
			{
				get
				{
					System.String data = entity.FactsheetURL;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FactsheetURL = null;
					else entity.FactsheetURL = Convert.ToString(value);
				}
			}
				
			public System.String BidPrice
			{
				get
				{
					System.Double? data = entity.BidPrice;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BidPrice = null;
					else entity.BidPrice = Convert.ToDouble(value);
				}
			}
				
			public System.String OfferPrice
			{
				get
				{
					System.Double? data = entity.OfferPrice;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OfferPrice = null;
					else entity.OfferPrice = Convert.ToDouble(value);
				}
			}
				
			public System.String Omamtv
			{
				get
				{
					System.String data = entity.Omamtv;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Omamtv = null;
					else entity.Omamtv = Convert.ToString(value);
				}
			}
				
			public System.String CityWireRatingURL
			{
				get
				{
					System.String data = entity.CityWireRatingURL;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CityWireRatingURL = null;
					else entity.CityWireRatingURL = Convert.ToString(value);
				}
			}
				
			public System.String CityWitreRatingCopy
			{
				get
				{
					System.String data = entity.CityWitreRatingCopy;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CityWitreRatingCopy = null;
					else entity.CityWitreRatingCopy = Convert.ToString(value);
				}
			}
				
			public System.String OBSRRatingURL
			{
				get
				{
					System.String data = entity.OBSRRatingURL;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OBSRRatingURL = null;
					else entity.OBSRRatingURL = Convert.ToString(value);
				}
			}
				
			public System.String OBSRRatingCopy
			{
				get
				{
					System.String data = entity.OBSRRatingCopy;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OBSRRatingCopy = null;
					else entity.OBSRRatingCopy = Convert.ToString(value);
				}
			}
				
			public System.String SPRatingCopy
			{
				get
				{
					System.String data = entity.SPRatingCopy;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SPRatingCopy = null;
					else entity.SPRatingCopy = Convert.ToString(value);
				}
			}
				
			public System.String SPRatingURL
			{
				get
				{
					System.String data = entity.SPRatingURL;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SPRatingURL = null;
					else entity.SPRatingURL = Convert.ToString(value);
				}
			}
				
			public System.String CategoryId
			{
				get
				{
					System.Int32? data = entity.CategoryId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CategoryId = null;
					else entity.CategoryId = Convert.ToInt32(value);
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
			

			private esVwFundsDisplay entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esVwFundsDisplayQuery query)
		{
			query.OnLoadEvent += new esDynamicQuery.QueryLoadedDelegate(OnQueryLoaded);
			query.es.Connection = ((IEntity)this).Connection;
		}

		protected bool OnQueryLoaded(DataTable table)
		{
			bool dataFound = this.PopulateEntity(table);

			if (this.RowCount > 1)
			{
				throw new Exception("esVwFundsDisplay can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}



	[Serializable]
	abstract public class esVwFundsDisplayQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return VwFundsDisplayMetadata.Meta();
			}
		}	
		

		public esQueryItem FundName
		{
			get
			{
				return new esQueryItem(this, VwFundsDisplayMetadata.ColumnNames.FundName, esSystemType.String);
			}
		} 
		
		public esQueryItem FundId
		{
			get
			{
				return new esQueryItem(this, VwFundsDisplayMetadata.ColumnNames.FundId, esSystemType.Int32);
			}
		} 
		
		public esQueryItem FactsheetFile
		{
			get
			{
				return new esQueryItem(this, VwFundsDisplayMetadata.ColumnNames.FactsheetFile, esSystemType.String);
			}
		} 
		
		public esQueryItem FactsheetURL
		{
			get
			{
				return new esQueryItem(this, VwFundsDisplayMetadata.ColumnNames.FactsheetURL, esSystemType.String);
			}
		} 
		
		public esQueryItem BidPrice
		{
			get
			{
				return new esQueryItem(this, VwFundsDisplayMetadata.ColumnNames.BidPrice, esSystemType.Double);
			}
		} 
		
		public esQueryItem OfferPrice
		{
			get
			{
				return new esQueryItem(this, VwFundsDisplayMetadata.ColumnNames.OfferPrice, esSystemType.Double);
			}
		} 
		
		public esQueryItem Omamtv
		{
			get
			{
				return new esQueryItem(this, VwFundsDisplayMetadata.ColumnNames.Omamtv, esSystemType.String);
			}
		} 
		
		public esQueryItem CityWireRatingURL
		{
			get
			{
				return new esQueryItem(this, VwFundsDisplayMetadata.ColumnNames.CityWireRatingURL, esSystemType.String);
			}
		} 
		
		public esQueryItem CityWitreRatingCopy
		{
			get
			{
				return new esQueryItem(this, VwFundsDisplayMetadata.ColumnNames.CityWitreRatingCopy, esSystemType.String);
			}
		} 
		
		public esQueryItem OBSRRatingURL
		{
			get
			{
				return new esQueryItem(this, VwFundsDisplayMetadata.ColumnNames.OBSRRatingURL, esSystemType.String);
			}
		} 
		
		public esQueryItem OBSRRatingCopy
		{
			get
			{
				return new esQueryItem(this, VwFundsDisplayMetadata.ColumnNames.OBSRRatingCopy, esSystemType.String);
			}
		} 
		
		public esQueryItem SPRatingCopy
		{
			get
			{
				return new esQueryItem(this, VwFundsDisplayMetadata.ColumnNames.SPRatingCopy, esSystemType.String);
			}
		} 
		
		public esQueryItem SPRatingURL
		{
			get
			{
				return new esQueryItem(this, VwFundsDisplayMetadata.ColumnNames.SPRatingURL, esSystemType.String);
			}
		} 
		
		public esQueryItem CategoryId
		{
			get
			{
				return new esQueryItem(this, VwFundsDisplayMetadata.ColumnNames.CategoryId, esSystemType.Int32);
			}
		} 
		
		public esQueryItem PortalId
		{
			get
			{
				return new esQueryItem(this, VwFundsDisplayMetadata.ColumnNames.PortalId, esSystemType.Int32);
			}
		} 
		
	}
	


	[Serializable]
	[XmlType("VwFundsDisplayCollection")]
	public partial class VwFundsDisplayCollection : esVwFundsDisplayCollection, IEnumerable<VwFundsDisplay>
	{
		public VwFundsDisplayCollection()
		{

		}	
		
        public static implicit operator List<VwFundsDisplay>(VwFundsDisplayCollection coll)
        {
            List<VwFundsDisplay> list = new List<VwFundsDisplay>();

            foreach (VwFundsDisplay emp in coll)
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
				return  VwFundsDisplayMetadata.Meta();
			}
		}	

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new VwFundsDisplayQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new VwFundsDisplay(row);
		}

		override protected esEntity CreateEntity()
		{
			return new VwFundsDisplay();
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
		public VwFundsDisplayQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new VwFundsDisplayQuery();
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
        public bool Load(VwFundsDisplayQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }		

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
		public VwFundsDisplay AddNew()
		{
			VwFundsDisplay entity = base.AddNewEntity() as VwFundsDisplay;
			
			return entity;		
		}


		#region IEnumerable<VwFundsDisplay> Members

		IEnumerator<VwFundsDisplay> IEnumerable<VwFundsDisplay>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as VwFundsDisplay;
			}
		}

		#endregion
		
		private VwFundsDisplayQuery query;
	}
	

	
	/// <summary>
	/// Encapsulates the 'vw_FundsDisplay' view
	/// </summary>

	[Serializable]
	public partial class VwFundsDisplay : esVwFundsDisplay
	{
		public VwFundsDisplay()
		{

		}	
	
		public VwFundsDisplay(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return VwFundsDisplayMetadata.Meta();
			}
		}

		override protected esVwFundsDisplayQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new VwFundsDisplayQuery();
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
		public VwFundsDisplayQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new VwFundsDisplayQuery();
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
        public bool Load(VwFundsDisplayQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }			

		private VwFundsDisplayQuery query;
	}
	


	[Serializable]
	public partial class VwFundsDisplayQuery : esVwFundsDisplayQuery
	{
		public VwFundsDisplayQuery()
		{

		}		
		
        public VwFundsDisplayQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }		

	}
	


	[Serializable]
	public partial class VwFundsDisplayMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected VwFundsDisplayMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(VwFundsDisplayMetadata.ColumnNames.FundName, 0, typeof(System.String), esSystemType.String);	
			c.PropertyName = VwFundsDisplayMetadata.PropertyNames.FundName;
			c.CharacterMaxLength = 100;
			_columns.Add(c); 
				
			c = new esColumnMetadata(VwFundsDisplayMetadata.ColumnNames.FundId, 1, typeof(System.Int32), esSystemType.Int32);	
			c.PropertyName = VwFundsDisplayMetadata.PropertyNames.FundId;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(VwFundsDisplayMetadata.ColumnNames.FactsheetFile, 2, typeof(System.String), esSystemType.String);	
			c.PropertyName = VwFundsDisplayMetadata.PropertyNames.FactsheetFile;
			c.CharacterMaxLength = 100;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(VwFundsDisplayMetadata.ColumnNames.FactsheetURL, 3, typeof(System.String), esSystemType.String);	
			c.PropertyName = VwFundsDisplayMetadata.PropertyNames.FactsheetURL;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(VwFundsDisplayMetadata.ColumnNames.BidPrice, 4, typeof(System.Double), esSystemType.Double);	
			c.PropertyName = VwFundsDisplayMetadata.PropertyNames.BidPrice;
			c.NumericPrecision = 15;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(VwFundsDisplayMetadata.ColumnNames.OfferPrice, 5, typeof(System.Double), esSystemType.Double);	
			c.PropertyName = VwFundsDisplayMetadata.PropertyNames.OfferPrice;
			c.NumericPrecision = 15;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(VwFundsDisplayMetadata.ColumnNames.Omamtv, 6, typeof(System.String), esSystemType.String);	
			c.PropertyName = VwFundsDisplayMetadata.PropertyNames.Omamtv;
			c.CharacterMaxLength = 1;
			_columns.Add(c); 
				
			c = new esColumnMetadata(VwFundsDisplayMetadata.ColumnNames.CityWireRatingURL, 7, typeof(System.String), esSystemType.String);	
			c.PropertyName = VwFundsDisplayMetadata.PropertyNames.CityWireRatingURL;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(VwFundsDisplayMetadata.ColumnNames.CityWitreRatingCopy, 8, typeof(System.String), esSystemType.String);	
			c.PropertyName = VwFundsDisplayMetadata.PropertyNames.CityWitreRatingCopy;
			c.CharacterMaxLength = 1073741823;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(VwFundsDisplayMetadata.ColumnNames.OBSRRatingURL, 9, typeof(System.String), esSystemType.String);	
			c.PropertyName = VwFundsDisplayMetadata.PropertyNames.OBSRRatingURL;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(VwFundsDisplayMetadata.ColumnNames.OBSRRatingCopy, 10, typeof(System.String), esSystemType.String);	
			c.PropertyName = VwFundsDisplayMetadata.PropertyNames.OBSRRatingCopy;
			c.CharacterMaxLength = 1073741823;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(VwFundsDisplayMetadata.ColumnNames.SPRatingCopy, 11, typeof(System.String), esSystemType.String);	
			c.PropertyName = VwFundsDisplayMetadata.PropertyNames.SPRatingCopy;
			c.CharacterMaxLength = 1073741823;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(VwFundsDisplayMetadata.ColumnNames.SPRatingURL, 12, typeof(System.String), esSystemType.String);	
			c.PropertyName = VwFundsDisplayMetadata.PropertyNames.SPRatingURL;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(VwFundsDisplayMetadata.ColumnNames.CategoryId, 13, typeof(System.Int32), esSystemType.Int32);	
			c.PropertyName = VwFundsDisplayMetadata.PropertyNames.CategoryId;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(VwFundsDisplayMetadata.ColumnNames.PortalId, 14, typeof(System.Int32), esSystemType.Int32);	
			c.PropertyName = VwFundsDisplayMetadata.PropertyNames.PortalId;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
		}
		#endregion

		static public VwFundsDisplayMetadata Meta()
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
			 public const string FundId = "FundId";
			 public const string FactsheetFile = "FactsheetFile";
			 public const string FactsheetURL = "FactsheetURL";
			 public const string BidPrice = "BidPrice";
			 public const string OfferPrice = "OfferPrice";
			 public const string Omamtv = "OMAMTV";
			 public const string CityWireRatingURL = "CityWireRatingURL";
			 public const string CityWitreRatingCopy = "CityWitreRatingCopy";
			 public const string OBSRRatingURL = "OBSRRatingURL";
			 public const string OBSRRatingCopy = "OBSRRatingCopy";
			 public const string SPRatingCopy = "SPRatingCopy";
			 public const string SPRatingURL = "SPRatingURL";
			 public const string CategoryId = "CategoryId";
			 public const string PortalId = "PortalId";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string FundName = "FundName";
			 public const string FundId = "FundId";
			 public const string FactsheetFile = "FactsheetFile";
			 public const string FactsheetURL = "FactsheetURL";
			 public const string BidPrice = "BidPrice";
			 public const string OfferPrice = "OfferPrice";
			 public const string Omamtv = "Omamtv";
			 public const string CityWireRatingURL = "CityWireRatingURL";
			 public const string CityWitreRatingCopy = "CityWitreRatingCopy";
			 public const string OBSRRatingURL = "OBSRRatingURL";
			 public const string OBSRRatingCopy = "OBSRRatingCopy";
			 public const string SPRatingCopy = "SPRatingCopy";
			 public const string SPRatingURL = "SPRatingURL";
			 public const string CategoryId = "CategoryId";
			 public const string PortalId = "PortalId";
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
            lock (typeof(VwFundsDisplayMetadata))
            {
				if(VwFundsDisplayMetadata.mapDelegates == null)
				{
					VwFundsDisplayMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
			
                if (VwFundsDisplayMetadata.meta == null)
                {
                    VwFundsDisplayMetadata.meta = new VwFundsDisplayMetadata();
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
				meta.AddTypeMap("FundId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("FactsheetFile", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("FactsheetURL", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("BidPrice", new esTypeMap("float", "System.Double"));
				meta.AddTypeMap("OfferPrice", new esTypeMap("float", "System.Double"));
				meta.AddTypeMap("OMAMTV", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CityWireRatingURL", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("CityWitreRatingCopy", new esTypeMap("ntext", "System.String"));
				meta.AddTypeMap("OBSRRatingURL", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("OBSRRatingCopy", new esTypeMap("ntext", "System.String"));
				meta.AddTypeMap("SPRatingCopy", new esTypeMap("ntext", "System.String"));
				meta.AddTypeMap("SPRatingURL", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("CategoryId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("PortalId", new esTypeMap("int", "System.Int32"));
			
				
				
				
				meta.Source = "vw_FundsDisplay";
				meta.Destination = "vw_FundsDisplay";
				
				meta.spInsert = "proc_vw_FundsDisplayInsert";				
				meta.spUpdate = "proc_vw_FundsDisplayUpdate";		
				meta.spDelete = "proc_vw_FundsDisplayDelete";
				meta.spLoadAll = "proc_vw_FundsDisplayLoadAll";
				meta.spLoadByPrimaryKey = "proc_vw_FundsDisplayLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private VwFundsDisplayMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
