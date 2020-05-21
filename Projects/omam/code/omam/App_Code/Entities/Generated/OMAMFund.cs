/*
===============================================================================
                     EntitySpaces(TM) by EntitySpaces, LLC
                 A New 2.0 Architecture for the .NET Framework
                          http://www.entityspaces.net
===============================================================================
                       EntitySpaces Version # 2007.1.1210.0
                       MyGeneration Version # 1.3.0.3
                           03/03/2008 21:41:27
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
	abstract public class esOMAMFundCollection : esEntityCollection
	{
		public esOMAMFundCollection()
		{

		}	
		
		protected override string GetCollectionName()
		{
			return "OMAMFundCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esOMAMFundQuery query)
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
            this.InitQuery(query as esOMAMFundQuery);
        }		
		#endregion
		
		virtual public OMAMFund DetachEntity(OMAMFund entity)
		{
			return base.DetachEntity(entity) as OMAMFund;
		}
		
		virtual public OMAMFund AttachEntity(OMAMFund entity)
		{
			return base.AttachEntity(entity) as OMAMFund;
		}
		
		virtual public void Combine(OMAMFundCollection collection)
		{
			base.Combine(collection);
		}
		
		new public OMAMFund this[int index]
		{
			get
			{
				return base[index] as OMAMFund;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(OMAMFund);
		}
	}
	


	[Serializable]
	abstract public class esOMAMFund : esEntity
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esOMAMFundQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esOMAMFund()
		{
	
		}
	
		public esOMAMFund(DataRow row)
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
			esOMAMFundQuery query = this.GetDynamicQuery();
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
						case "FundCode": this.str.FundCode = (string)value; break;							
						case "FundName": this.str.FundName = (string)value; break;							
						case "FundSnippet": this.str.FundSnippet = (string)value; break;							
						case "RiskWarning": this.str.RiskWarning = (string)value; break;							
						case "FundAims": this.str.FundAims = (string)value; break;							
						case "FundFactSheetURL": this.str.FundFactSheetURL = (string)value; break;							
						case "SPRatingURL": this.str.SPRatingURL = (string)value; break;							
						case "SPRatingFile": this.str.SPRatingFile = (string)value; break;							
						case "SPRatingCopy": this.str.SPRatingCopy = (string)value; break;							
						case "OBSRRatingURL": this.str.OBSRRatingURL = (string)value; break;							
						case "OBSRRatingCopy": this.str.OBSRRatingCopy = (string)value; break;							
						case "CityWireRatingURL": this.str.CityWireRatingURL = (string)value; break;							
						case "CityWitreRatingCopy": this.str.CityWitreRatingCopy = (string)value; break;							
						case "OMAMTVFile": this.str.OMAMTVFile = (string)value; break;							
						case "FactsheetFile": this.str.FactsheetFile = (string)value; break;							
						case "FactsheetURL": this.str.FactsheetURL = (string)value; break;							
						case "ReasonsWhyFile": this.str.ReasonsWhyFile = (string)value; break;							
						case "ReasonsWhyURL": this.str.ReasonsWhyURL = (string)value; break;							
						case "AnnualReportFile": this.str.AnnualReportFile = (string)value; break;							
						case "AnnualReportURL": this.str.AnnualReportURL = (string)value; break;							
						case "InterimReportFile": this.str.InterimReportFile = (string)value; break;							
						case "InterimReportURL": this.str.InterimReportURL = (string)value; break;							
						case "OBSRReportFile": this.str.OBSRReportFile = (string)value; break;							
						case "OBSRReportURL": this.str.OBSRReportURL = (string)value; break;							
						case "SPReportFile": this.str.SPReportFile = (string)value; break;							
						case "SPReportURL": this.str.SPReportURL = (string)value; break;							
						case "SalesAidFile": this.str.SalesAidFile = (string)value; break;							
						case "SalesAidURL": this.str.SalesAidURL = (string)value; break;							
						case "TermsheetFile": this.str.TermsheetFile = (string)value; break;							
						case "TermsheetURL": this.str.TermsheetURL = (string)value; break;							
						case "OffShore": this.str.OffShore = (string)value; break;							
						case "Currency": this.str.Currency = (string)value; break;							
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
						
						case "OffShore":
						
							if (value == null || value.GetType().ToString() == "System.Boolean")
								this.OffShore = (System.Boolean?)value;
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
		/// Maps to OMAMFund.Id
		/// </summary>
		virtual public System.Int32? Id
		{
			get
			{
				return base.GetSystemInt32(OMAMFundMetadata.ColumnNames.Id);
			}
			
			set
			{
				if(base.SetSystemInt32(OMAMFundMetadata.ColumnNames.Id, value))
				{
					this.MarkFieldAsModified(OMAMFundMetadata.ColumnNames.Id);
				}
			}
		}
		
		/// <summary>
		/// Maps to OMAMFund.FundCode
		/// </summary>
		virtual public System.String FundCode
		{
			get
			{
				return base.GetSystemString(OMAMFundMetadata.ColumnNames.FundCode);
			}
			
			set
			{
				if(base.SetSystemString(OMAMFundMetadata.ColumnNames.FundCode, value))
				{
					this.MarkFieldAsModified(OMAMFundMetadata.ColumnNames.FundCode);
				}
			}
		}
		
		/// <summary>
		/// Maps to OMAMFund.FundName
		/// </summary>
		virtual public System.String FundName
		{
			get
			{
				return base.GetSystemString(OMAMFundMetadata.ColumnNames.FundName);
			}
			
			set
			{
				if(base.SetSystemString(OMAMFundMetadata.ColumnNames.FundName, value))
				{
					this.MarkFieldAsModified(OMAMFundMetadata.ColumnNames.FundName);
				}
			}
		}
		
		/// <summary>
		/// Maps to OMAMFund.FundSnippet
		/// </summary>
		virtual public System.String FundSnippet
		{
			get
			{
				return base.GetSystemString(OMAMFundMetadata.ColumnNames.FundSnippet);
			}
			
			set
			{
				if(base.SetSystemString(OMAMFundMetadata.ColumnNames.FundSnippet, value))
				{
					this.MarkFieldAsModified(OMAMFundMetadata.ColumnNames.FundSnippet);
				}
			}
		}
		
		/// <summary>
		/// Maps to OMAMFund.RiskWarning
		/// </summary>
		virtual public System.String RiskWarning
		{
			get
			{
				return base.GetSystemString(OMAMFundMetadata.ColumnNames.RiskWarning);
			}
			
			set
			{
				if(base.SetSystemString(OMAMFundMetadata.ColumnNames.RiskWarning, value))
				{
					this.MarkFieldAsModified(OMAMFundMetadata.ColumnNames.RiskWarning);
				}
			}
		}
		
		/// <summary>
		/// Maps to OMAMFund.FundAims
		/// </summary>
		virtual public System.String FundAims
		{
			get
			{
				return base.GetSystemString(OMAMFundMetadata.ColumnNames.FundAims);
			}
			
			set
			{
				if(base.SetSystemString(OMAMFundMetadata.ColumnNames.FundAims, value))
				{
					this.MarkFieldAsModified(OMAMFundMetadata.ColumnNames.FundAims);
				}
			}
		}
		
		/// <summary>
		/// Maps to OMAMFund.FundFactSheetURL
		/// </summary>
		virtual public System.String FundFactSheetURL
		{
			get
			{
				return base.GetSystemString(OMAMFundMetadata.ColumnNames.FundFactSheetURL);
			}
			
			set
			{
				if(base.SetSystemString(OMAMFundMetadata.ColumnNames.FundFactSheetURL, value))
				{
					this.MarkFieldAsModified(OMAMFundMetadata.ColumnNames.FundFactSheetURL);
				}
			}
		}
		
		/// <summary>
		/// Maps to OMAMFund.SPRatingURL
		/// </summary>
		virtual public System.String SPRatingURL
		{
			get
			{
				return base.GetSystemString(OMAMFundMetadata.ColumnNames.SPRatingURL);
			}
			
			set
			{
				if(base.SetSystemString(OMAMFundMetadata.ColumnNames.SPRatingURL, value))
				{
					this.MarkFieldAsModified(OMAMFundMetadata.ColumnNames.SPRatingURL);
				}
			}
		}
		
		/// <summary>
		/// Maps to OMAMFund.SPRatingFile
		/// </summary>
		virtual public System.String SPRatingFile
		{
			get
			{
				return base.GetSystemString(OMAMFundMetadata.ColumnNames.SPRatingFile);
			}
			
			set
			{
				if(base.SetSystemString(OMAMFundMetadata.ColumnNames.SPRatingFile, value))
				{
					this.MarkFieldAsModified(OMAMFundMetadata.ColumnNames.SPRatingFile);
				}
			}
		}
		
		/// <summary>
		/// Maps to OMAMFund.SPRatingCopy
		/// </summary>
		virtual public System.String SPRatingCopy
		{
			get
			{
				return base.GetSystemString(OMAMFundMetadata.ColumnNames.SPRatingCopy);
			}
			
			set
			{
				if(base.SetSystemString(OMAMFundMetadata.ColumnNames.SPRatingCopy, value))
				{
					this.MarkFieldAsModified(OMAMFundMetadata.ColumnNames.SPRatingCopy);
				}
			}
		}
		
		/// <summary>
		/// Maps to OMAMFund.OBSRRatingURL
		/// </summary>
		virtual public System.String OBSRRatingURL
		{
			get
			{
				return base.GetSystemString(OMAMFundMetadata.ColumnNames.OBSRRatingURL);
			}
			
			set
			{
				if(base.SetSystemString(OMAMFundMetadata.ColumnNames.OBSRRatingURL, value))
				{
					this.MarkFieldAsModified(OMAMFundMetadata.ColumnNames.OBSRRatingURL);
				}
			}
		}
		
		/// <summary>
		/// Maps to OMAMFund.OBSRRatingCopy
		/// </summary>
		virtual public System.String OBSRRatingCopy
		{
			get
			{
				return base.GetSystemString(OMAMFundMetadata.ColumnNames.OBSRRatingCopy);
			}
			
			set
			{
				if(base.SetSystemString(OMAMFundMetadata.ColumnNames.OBSRRatingCopy, value))
				{
					this.MarkFieldAsModified(OMAMFundMetadata.ColumnNames.OBSRRatingCopy);
				}
			}
		}
		
		/// <summary>
		/// Maps to OMAMFund.CityWireRatingURL
		/// </summary>
		virtual public System.String CityWireRatingURL
		{
			get
			{
				return base.GetSystemString(OMAMFundMetadata.ColumnNames.CityWireRatingURL);
			}
			
			set
			{
				if(base.SetSystemString(OMAMFundMetadata.ColumnNames.CityWireRatingURL, value))
				{
					this.MarkFieldAsModified(OMAMFundMetadata.ColumnNames.CityWireRatingURL);
				}
			}
		}
		
		/// <summary>
		/// Maps to OMAMFund.CityWitreRatingCopy
		/// </summary>
		virtual public System.String CityWitreRatingCopy
		{
			get
			{
				return base.GetSystemString(OMAMFundMetadata.ColumnNames.CityWitreRatingCopy);
			}
			
			set
			{
				if(base.SetSystemString(OMAMFundMetadata.ColumnNames.CityWitreRatingCopy, value))
				{
					this.MarkFieldAsModified(OMAMFundMetadata.ColumnNames.CityWitreRatingCopy);
				}
			}
		}
		
		/// <summary>
		/// Maps to OMAMFund.OMAMTVFile
		/// </summary>
		virtual public System.String OMAMTVFile
		{
			get
			{
				return base.GetSystemString(OMAMFundMetadata.ColumnNames.OMAMTVFile);
			}
			
			set
			{
				if(base.SetSystemString(OMAMFundMetadata.ColumnNames.OMAMTVFile, value))
				{
					this.MarkFieldAsModified(OMAMFundMetadata.ColumnNames.OMAMTVFile);
				}
			}
		}
		
		/// <summary>
		/// Maps to OMAMFund.FactsheetFile
		/// </summary>
		virtual public System.String FactsheetFile
		{
			get
			{
				return base.GetSystemString(OMAMFundMetadata.ColumnNames.FactsheetFile);
			}
			
			set
			{
				if(base.SetSystemString(OMAMFundMetadata.ColumnNames.FactsheetFile, value))
				{
					this.MarkFieldAsModified(OMAMFundMetadata.ColumnNames.FactsheetFile);
				}
			}
		}
		
		/// <summary>
		/// Maps to OMAMFund.FactsheetURL
		/// </summary>
		virtual public System.String FactsheetURL
		{
			get
			{
				return base.GetSystemString(OMAMFundMetadata.ColumnNames.FactsheetURL);
			}
			
			set
			{
				if(base.SetSystemString(OMAMFundMetadata.ColumnNames.FactsheetURL, value))
				{
					this.MarkFieldAsModified(OMAMFundMetadata.ColumnNames.FactsheetURL);
				}
			}
		}
		
		/// <summary>
		/// Maps to OMAMFund.ReasonsWhyFile
		/// </summary>
		virtual public System.String ReasonsWhyFile
		{
			get
			{
				return base.GetSystemString(OMAMFundMetadata.ColumnNames.ReasonsWhyFile);
			}
			
			set
			{
				if(base.SetSystemString(OMAMFundMetadata.ColumnNames.ReasonsWhyFile, value))
				{
					this.MarkFieldAsModified(OMAMFundMetadata.ColumnNames.ReasonsWhyFile);
				}
			}
		}
		
		/// <summary>
		/// Maps to OMAMFund.ReasonsWhyURL
		/// </summary>
		virtual public System.String ReasonsWhyURL
		{
			get
			{
				return base.GetSystemString(OMAMFundMetadata.ColumnNames.ReasonsWhyURL);
			}
			
			set
			{
				if(base.SetSystemString(OMAMFundMetadata.ColumnNames.ReasonsWhyURL, value))
				{
					this.MarkFieldAsModified(OMAMFundMetadata.ColumnNames.ReasonsWhyURL);
				}
			}
		}
		
		/// <summary>
		/// Maps to OMAMFund.AnnualReportFile
		/// </summary>
		virtual public System.String AnnualReportFile
		{
			get
			{
				return base.GetSystemString(OMAMFundMetadata.ColumnNames.AnnualReportFile);
			}
			
			set
			{
				if(base.SetSystemString(OMAMFundMetadata.ColumnNames.AnnualReportFile, value))
				{
					this.MarkFieldAsModified(OMAMFundMetadata.ColumnNames.AnnualReportFile);
				}
			}
		}
		
		/// <summary>
		/// Maps to OMAMFund.AnnualReportURL
		/// </summary>
		virtual public System.String AnnualReportURL
		{
			get
			{
				return base.GetSystemString(OMAMFundMetadata.ColumnNames.AnnualReportURL);
			}
			
			set
			{
				if(base.SetSystemString(OMAMFundMetadata.ColumnNames.AnnualReportURL, value))
				{
					this.MarkFieldAsModified(OMAMFundMetadata.ColumnNames.AnnualReportURL);
				}
			}
		}
		
		/// <summary>
		/// Maps to OMAMFund.InterimReportFile
		/// </summary>
		virtual public System.String InterimReportFile
		{
			get
			{
				return base.GetSystemString(OMAMFundMetadata.ColumnNames.InterimReportFile);
			}
			
			set
			{
				if(base.SetSystemString(OMAMFundMetadata.ColumnNames.InterimReportFile, value))
				{
					this.MarkFieldAsModified(OMAMFundMetadata.ColumnNames.InterimReportFile);
				}
			}
		}
		
		/// <summary>
		/// Maps to OMAMFund.InterimReportURL
		/// </summary>
		virtual public System.String InterimReportURL
		{
			get
			{
				return base.GetSystemString(OMAMFundMetadata.ColumnNames.InterimReportURL);
			}
			
			set
			{
				if(base.SetSystemString(OMAMFundMetadata.ColumnNames.InterimReportURL, value))
				{
					this.MarkFieldAsModified(OMAMFundMetadata.ColumnNames.InterimReportURL);
				}
			}
		}
		
		/// <summary>
		/// Maps to OMAMFund.OBSRReportFile
		/// </summary>
		virtual public System.String OBSRReportFile
		{
			get
			{
				return base.GetSystemString(OMAMFundMetadata.ColumnNames.OBSRReportFile);
			}
			
			set
			{
				if(base.SetSystemString(OMAMFundMetadata.ColumnNames.OBSRReportFile, value))
				{
					this.MarkFieldAsModified(OMAMFundMetadata.ColumnNames.OBSRReportFile);
				}
			}
		}
		
		/// <summary>
		/// Maps to OMAMFund.OBSRReportURL
		/// </summary>
		virtual public System.String OBSRReportURL
		{
			get
			{
				return base.GetSystemString(OMAMFundMetadata.ColumnNames.OBSRReportURL);
			}
			
			set
			{
				if(base.SetSystemString(OMAMFundMetadata.ColumnNames.OBSRReportURL, value))
				{
					this.MarkFieldAsModified(OMAMFundMetadata.ColumnNames.OBSRReportURL);
				}
			}
		}
		
		/// <summary>
		/// Maps to OMAMFund.SPReportFile
		/// </summary>
		virtual public System.String SPReportFile
		{
			get
			{
				return base.GetSystemString(OMAMFundMetadata.ColumnNames.SPReportFile);
			}
			
			set
			{
				if(base.SetSystemString(OMAMFundMetadata.ColumnNames.SPReportFile, value))
				{
					this.MarkFieldAsModified(OMAMFundMetadata.ColumnNames.SPReportFile);
				}
			}
		}
		
		/// <summary>
		/// Maps to OMAMFund.SPReportURL
		/// </summary>
		virtual public System.String SPReportURL
		{
			get
			{
				return base.GetSystemString(OMAMFundMetadata.ColumnNames.SPReportURL);
			}
			
			set
			{
				if(base.SetSystemString(OMAMFundMetadata.ColumnNames.SPReportURL, value))
				{
					this.MarkFieldAsModified(OMAMFundMetadata.ColumnNames.SPReportURL);
				}
			}
		}
		
		/// <summary>
		/// Maps to OMAMFund.SalesAidFile
		/// </summary>
		virtual public System.String SalesAidFile
		{
			get
			{
				return base.GetSystemString(OMAMFundMetadata.ColumnNames.SalesAidFile);
			}
			
			set
			{
				if(base.SetSystemString(OMAMFundMetadata.ColumnNames.SalesAidFile, value))
				{
					this.MarkFieldAsModified(OMAMFundMetadata.ColumnNames.SalesAidFile);
				}
			}
		}
		
		/// <summary>
		/// Maps to OMAMFund.SalesAidURL
		/// </summary>
		virtual public System.String SalesAidURL
		{
			get
			{
				return base.GetSystemString(OMAMFundMetadata.ColumnNames.SalesAidURL);
			}
			
			set
			{
				if(base.SetSystemString(OMAMFundMetadata.ColumnNames.SalesAidURL, value))
				{
					this.MarkFieldAsModified(OMAMFundMetadata.ColumnNames.SalesAidURL);
				}
			}
		}
		
		/// <summary>
		/// Maps to OMAMFund.TermsheetFile
		/// </summary>
		virtual public System.String TermsheetFile
		{
			get
			{
				return base.GetSystemString(OMAMFundMetadata.ColumnNames.TermsheetFile);
			}
			
			set
			{
				if(base.SetSystemString(OMAMFundMetadata.ColumnNames.TermsheetFile, value))
				{
					this.MarkFieldAsModified(OMAMFundMetadata.ColumnNames.TermsheetFile);
				}
			}
		}
		
		/// <summary>
		/// Maps to OMAMFund.TermsheetURL
		/// </summary>
		virtual public System.String TermsheetURL
		{
			get
			{
				return base.GetSystemString(OMAMFundMetadata.ColumnNames.TermsheetURL);
			}
			
			set
			{
				if(base.SetSystemString(OMAMFundMetadata.ColumnNames.TermsheetURL, value))
				{
					this.MarkFieldAsModified(OMAMFundMetadata.ColumnNames.TermsheetURL);
				}
			}
		}
		
		/// <summary>
		/// Maps to OMAMFund.OffShore
		/// </summary>
		virtual public System.Boolean? OffShore
		{
			get
			{
				return base.GetSystemBoolean(OMAMFundMetadata.ColumnNames.OffShore);
			}
			
			set
			{
				if(base.SetSystemBoolean(OMAMFundMetadata.ColumnNames.OffShore, value))
				{
					this.MarkFieldAsModified(OMAMFundMetadata.ColumnNames.OffShore);
				}
			}
		}
		
		/// <summary>
		/// Maps to OMAMFund.Currency
		/// </summary>
		virtual public System.String Currency
		{
			get
			{
				return base.GetSystemString(OMAMFundMetadata.ColumnNames.Currency);
			}
			
			set
			{
				if(base.SetSystemString(OMAMFundMetadata.ColumnNames.Currency, value))
				{
					this.MarkFieldAsModified(OMAMFundMetadata.ColumnNames.Currency);
				}
			}
		}
		
		/// <summary>
		/// Maps to OMAMFund.CreatedDate
		/// </summary>
		virtual public System.DateTime? CreatedDate
		{
			get
			{
				return base.GetSystemDateTime(OMAMFundMetadata.ColumnNames.CreatedDate);
			}
			
			set
			{
				if(base.SetSystemDateTime(OMAMFundMetadata.ColumnNames.CreatedDate, value))
				{
					this.MarkFieldAsModified(OMAMFundMetadata.ColumnNames.CreatedDate);
				}
			}
		}
		
		/// <summary>
		/// Maps to OMAMFund.CreatedBy
		/// </summary>
		virtual public System.Int32? CreatedBy
		{
			get
			{
				return base.GetSystemInt32(OMAMFundMetadata.ColumnNames.CreatedBy);
			}
			
			set
			{
				if(base.SetSystemInt32(OMAMFundMetadata.ColumnNames.CreatedBy, value))
				{
					this.MarkFieldAsModified(OMAMFundMetadata.ColumnNames.CreatedBy);
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
			public esStrings(esOMAMFund entity)
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
				
			public System.String FundCode
			{
				get
				{
					System.String data = entity.FundCode;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FundCode = null;
					else entity.FundCode = Convert.ToString(value);
				}
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
				
			public System.String FundSnippet
			{
				get
				{
					System.String data = entity.FundSnippet;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FundSnippet = null;
					else entity.FundSnippet = Convert.ToString(value);
				}
			}
				
			public System.String RiskWarning
			{
				get
				{
					System.String data = entity.RiskWarning;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RiskWarning = null;
					else entity.RiskWarning = Convert.ToString(value);
				}
			}
				
			public System.String FundAims
			{
				get
				{
					System.String data = entity.FundAims;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FundAims = null;
					else entity.FundAims = Convert.ToString(value);
				}
			}
				
			public System.String FundFactSheetURL
			{
				get
				{
					System.String data = entity.FundFactSheetURL;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FundFactSheetURL = null;
					else entity.FundFactSheetURL = Convert.ToString(value);
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
				
			public System.String SPRatingFile
			{
				get
				{
					System.String data = entity.SPRatingFile;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SPRatingFile = null;
					else entity.SPRatingFile = Convert.ToString(value);
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
				
			public System.String OMAMTVFile
			{
				get
				{
					System.String data = entity.OMAMTVFile;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OMAMTVFile = null;
					else entity.OMAMTVFile = Convert.ToString(value);
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
				
			public System.String ReasonsWhyFile
			{
				get
				{
					System.String data = entity.ReasonsWhyFile;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReasonsWhyFile = null;
					else entity.ReasonsWhyFile = Convert.ToString(value);
				}
			}
				
			public System.String ReasonsWhyURL
			{
				get
				{
					System.String data = entity.ReasonsWhyURL;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReasonsWhyURL = null;
					else entity.ReasonsWhyURL = Convert.ToString(value);
				}
			}
				
			public System.String AnnualReportFile
			{
				get
				{
					System.String data = entity.AnnualReportFile;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AnnualReportFile = null;
					else entity.AnnualReportFile = Convert.ToString(value);
				}
			}
				
			public System.String AnnualReportURL
			{
				get
				{
					System.String data = entity.AnnualReportURL;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AnnualReportURL = null;
					else entity.AnnualReportURL = Convert.ToString(value);
				}
			}
				
			public System.String InterimReportFile
			{
				get
				{
					System.String data = entity.InterimReportFile;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.InterimReportFile = null;
					else entity.InterimReportFile = Convert.ToString(value);
				}
			}
				
			public System.String InterimReportURL
			{
				get
				{
					System.String data = entity.InterimReportURL;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.InterimReportURL = null;
					else entity.InterimReportURL = Convert.ToString(value);
				}
			}
				
			public System.String OBSRReportFile
			{
				get
				{
					System.String data = entity.OBSRReportFile;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OBSRReportFile = null;
					else entity.OBSRReportFile = Convert.ToString(value);
				}
			}
				
			public System.String OBSRReportURL
			{
				get
				{
					System.String data = entity.OBSRReportURL;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OBSRReportURL = null;
					else entity.OBSRReportURL = Convert.ToString(value);
				}
			}
				
			public System.String SPReportFile
			{
				get
				{
					System.String data = entity.SPReportFile;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SPReportFile = null;
					else entity.SPReportFile = Convert.ToString(value);
				}
			}
				
			public System.String SPReportURL
			{
				get
				{
					System.String data = entity.SPReportURL;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SPReportURL = null;
					else entity.SPReportURL = Convert.ToString(value);
				}
			}
				
			public System.String SalesAidFile
			{
				get
				{
					System.String data = entity.SalesAidFile;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SalesAidFile = null;
					else entity.SalesAidFile = Convert.ToString(value);
				}
			}
				
			public System.String SalesAidURL
			{
				get
				{
					System.String data = entity.SalesAidURL;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SalesAidURL = null;
					else entity.SalesAidURL = Convert.ToString(value);
				}
			}
				
			public System.String TermsheetFile
			{
				get
				{
					System.String data = entity.TermsheetFile;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TermsheetFile = null;
					else entity.TermsheetFile = Convert.ToString(value);
				}
			}
				
			public System.String TermsheetURL
			{
				get
				{
					System.String data = entity.TermsheetURL;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TermsheetURL = null;
					else entity.TermsheetURL = Convert.ToString(value);
				}
			}
				
			public System.String OffShore
			{
				get
				{
					System.Boolean? data = entity.OffShore;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OffShore = null;
					else entity.OffShore = Convert.ToBoolean(value);
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
			

			private esOMAMFund entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esOMAMFundQuery query)
		{
			query.OnLoadEvent += new esDynamicQuery.QueryLoadedDelegate(OnQueryLoaded);
			query.es.Connection = ((IEntity)this).Connection;
		}

		protected bool OnQueryLoaded(DataTable table)
		{
			bool dataFound = this.PopulateEntity(table);

			if (this.RowCount > 1)
			{
				throw new Exception("esOMAMFund can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	/// <summary>
	/// Hierarchical for the 'OMAMFund' table
	/// </summary>
	public partial class OMAMFund : esOMAMFund
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
	abstract public class esOMAMFundQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return OMAMFundMetadata.Meta();
			}
		}	
		

		public esQueryItem Id
		{
			get
			{
				return new esQueryItem(this, OMAMFundMetadata.ColumnNames.Id, esSystemType.Int32);
			}
		} 
		
		public esQueryItem FundCode
		{
			get
			{
				return new esQueryItem(this, OMAMFundMetadata.ColumnNames.FundCode, esSystemType.String);
			}
		} 
		
		public esQueryItem FundName
		{
			get
			{
				return new esQueryItem(this, OMAMFundMetadata.ColumnNames.FundName, esSystemType.String);
			}
		} 
		
		public esQueryItem FundSnippet
		{
			get
			{
				return new esQueryItem(this, OMAMFundMetadata.ColumnNames.FundSnippet, esSystemType.String);
			}
		} 
		
		public esQueryItem RiskWarning
		{
			get
			{
				return new esQueryItem(this, OMAMFundMetadata.ColumnNames.RiskWarning, esSystemType.String);
			}
		} 
		
		public esQueryItem FundAims
		{
			get
			{
				return new esQueryItem(this, OMAMFundMetadata.ColumnNames.FundAims, esSystemType.String);
			}
		} 
		
		public esQueryItem FundFactSheetURL
		{
			get
			{
				return new esQueryItem(this, OMAMFundMetadata.ColumnNames.FundFactSheetURL, esSystemType.String);
			}
		} 
		
		public esQueryItem SPRatingURL
		{
			get
			{
				return new esQueryItem(this, OMAMFundMetadata.ColumnNames.SPRatingURL, esSystemType.String);
			}
		} 
		
		public esQueryItem SPRatingFile
		{
			get
			{
				return new esQueryItem(this, OMAMFundMetadata.ColumnNames.SPRatingFile, esSystemType.String);
			}
		} 
		
		public esQueryItem SPRatingCopy
		{
			get
			{
				return new esQueryItem(this, OMAMFundMetadata.ColumnNames.SPRatingCopy, esSystemType.String);
			}
		} 
		
		public esQueryItem OBSRRatingURL
		{
			get
			{
				return new esQueryItem(this, OMAMFundMetadata.ColumnNames.OBSRRatingURL, esSystemType.String);
			}
		} 
		
		public esQueryItem OBSRRatingCopy
		{
			get
			{
				return new esQueryItem(this, OMAMFundMetadata.ColumnNames.OBSRRatingCopy, esSystemType.String);
			}
		} 
		
		public esQueryItem CityWireRatingURL
		{
			get
			{
				return new esQueryItem(this, OMAMFundMetadata.ColumnNames.CityWireRatingURL, esSystemType.String);
			}
		} 
		
		public esQueryItem CityWitreRatingCopy
		{
			get
			{
				return new esQueryItem(this, OMAMFundMetadata.ColumnNames.CityWitreRatingCopy, esSystemType.String);
			}
		} 
		
		public esQueryItem OMAMTVFile
		{
			get
			{
				return new esQueryItem(this, OMAMFundMetadata.ColumnNames.OMAMTVFile, esSystemType.String);
			}
		} 
		
		public esQueryItem FactsheetFile
		{
			get
			{
				return new esQueryItem(this, OMAMFundMetadata.ColumnNames.FactsheetFile, esSystemType.String);
			}
		} 
		
		public esQueryItem FactsheetURL
		{
			get
			{
				return new esQueryItem(this, OMAMFundMetadata.ColumnNames.FactsheetURL, esSystemType.String);
			}
		} 
		
		public esQueryItem ReasonsWhyFile
		{
			get
			{
				return new esQueryItem(this, OMAMFundMetadata.ColumnNames.ReasonsWhyFile, esSystemType.String);
			}
		} 
		
		public esQueryItem ReasonsWhyURL
		{
			get
			{
				return new esQueryItem(this, OMAMFundMetadata.ColumnNames.ReasonsWhyURL, esSystemType.String);
			}
		} 
		
		public esQueryItem AnnualReportFile
		{
			get
			{
				return new esQueryItem(this, OMAMFundMetadata.ColumnNames.AnnualReportFile, esSystemType.String);
			}
		} 
		
		public esQueryItem AnnualReportURL
		{
			get
			{
				return new esQueryItem(this, OMAMFundMetadata.ColumnNames.AnnualReportURL, esSystemType.String);
			}
		} 
		
		public esQueryItem InterimReportFile
		{
			get
			{
				return new esQueryItem(this, OMAMFundMetadata.ColumnNames.InterimReportFile, esSystemType.String);
			}
		} 
		
		public esQueryItem InterimReportURL
		{
			get
			{
				return new esQueryItem(this, OMAMFundMetadata.ColumnNames.InterimReportURL, esSystemType.String);
			}
		} 
		
		public esQueryItem OBSRReportFile
		{
			get
			{
				return new esQueryItem(this, OMAMFundMetadata.ColumnNames.OBSRReportFile, esSystemType.String);
			}
		} 
		
		public esQueryItem OBSRReportURL
		{
			get
			{
				return new esQueryItem(this, OMAMFundMetadata.ColumnNames.OBSRReportURL, esSystemType.String);
			}
		} 
		
		public esQueryItem SPReportFile
		{
			get
			{
				return new esQueryItem(this, OMAMFundMetadata.ColumnNames.SPReportFile, esSystemType.String);
			}
		} 
		
		public esQueryItem SPReportURL
		{
			get
			{
				return new esQueryItem(this, OMAMFundMetadata.ColumnNames.SPReportURL, esSystemType.String);
			}
		} 
		
		public esQueryItem SalesAidFile
		{
			get
			{
				return new esQueryItem(this, OMAMFundMetadata.ColumnNames.SalesAidFile, esSystemType.String);
			}
		} 
		
		public esQueryItem SalesAidURL
		{
			get
			{
				return new esQueryItem(this, OMAMFundMetadata.ColumnNames.SalesAidURL, esSystemType.String);
			}
		} 
		
		public esQueryItem TermsheetFile
		{
			get
			{
				return new esQueryItem(this, OMAMFundMetadata.ColumnNames.TermsheetFile, esSystemType.String);
			}
		} 
		
		public esQueryItem TermsheetURL
		{
			get
			{
				return new esQueryItem(this, OMAMFundMetadata.ColumnNames.TermsheetURL, esSystemType.String);
			}
		} 
		
		public esQueryItem OffShore
		{
			get
			{
				return new esQueryItem(this, OMAMFundMetadata.ColumnNames.OffShore, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem Currency
		{
			get
			{
				return new esQueryItem(this, OMAMFundMetadata.ColumnNames.Currency, esSystemType.String);
			}
		} 
		
		public esQueryItem CreatedDate
		{
			get
			{
				return new esQueryItem(this, OMAMFundMetadata.ColumnNames.CreatedDate, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem CreatedBy
		{
			get
			{
				return new esQueryItem(this, OMAMFundMetadata.ColumnNames.CreatedBy, esSystemType.Int32);
			}
		} 
		
	}
	


	[Serializable]
	[XmlType("OMAMFundCollection")]
	public partial class OMAMFundCollection : esOMAMFundCollection, IEnumerable<OMAMFund>
	{
		public OMAMFundCollection()
		{

		}	
		
        public static implicit operator List<OMAMFund>(OMAMFundCollection coll)
        {
            List<OMAMFund> list = new List<OMAMFund>();

            foreach (OMAMFund emp in coll)
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
				return  OMAMFundMetadata.Meta();
			}
		}	

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new OMAMFundQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new OMAMFund(row);
		}

		override protected esEntity CreateEntity()
		{
			return new OMAMFund();
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
		public OMAMFundQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new OMAMFundQuery();
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
        public bool Load(OMAMFundQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }		

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
		public OMAMFund AddNew()
		{
			OMAMFund entity = base.AddNewEntity() as OMAMFund;
			
			return entity;		
		}

		public OMAMFund FindByPrimaryKey(System.Int32 id)
		{
			return base.FindByPrimaryKey(id) as OMAMFund;
		}


		#region IEnumerable<OMAMFund> Members

		IEnumerator<OMAMFund> IEnumerable<OMAMFund>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as OMAMFund;
			}
		}

		#endregion
		
		private OMAMFundQuery query;
	}
	

	
	/// <summary>
	/// Encapsulates the 'OMAMFund' table
	/// </summary>

	[Serializable]
	public partial class OMAMFund : esOMAMFund
	{
		public OMAMFund()
		{

		}	
	
		public OMAMFund(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return OMAMFundMetadata.Meta();
			}
		}

		override protected esOMAMFundQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new OMAMFundQuery();
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
		public OMAMFundQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new OMAMFundQuery();
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
        public bool Load(OMAMFundQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }			

		private OMAMFundQuery query;
	}
	


	[Serializable]
	public partial class OMAMFundQuery : esOMAMFundQuery
	{
		public OMAMFundQuery()
		{

		}		
		
        public OMAMFundQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }		

	}
	


	[Serializable]
	public partial class OMAMFundMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected OMAMFundMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(OMAMFundMetadata.ColumnNames.Id, 0, typeof(System.Int32), esSystemType.Int32);	
			c.PropertyName = OMAMFundMetadata.PropertyNames.Id;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(OMAMFundMetadata.ColumnNames.FundCode, 1, typeof(System.String), esSystemType.String);	
			c.PropertyName = OMAMFundMetadata.PropertyNames.FundCode;
			c.CharacterMaxLength = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(OMAMFundMetadata.ColumnNames.FundName, 2, typeof(System.String), esSystemType.String);	
			c.PropertyName = OMAMFundMetadata.PropertyNames.FundName;
			c.CharacterMaxLength = 100;
			_columns.Add(c); 
				
			c = new esColumnMetadata(OMAMFundMetadata.ColumnNames.FundSnippet, 3, typeof(System.String), esSystemType.String);	
			c.PropertyName = OMAMFundMetadata.PropertyNames.FundSnippet;
			c.CharacterMaxLength = 1073741823;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(OMAMFundMetadata.ColumnNames.RiskWarning, 4, typeof(System.String), esSystemType.String);	
			c.PropertyName = OMAMFundMetadata.PropertyNames.RiskWarning;
			c.CharacterMaxLength = 1073741823;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(OMAMFundMetadata.ColumnNames.FundAims, 5, typeof(System.String), esSystemType.String);	
			c.PropertyName = OMAMFundMetadata.PropertyNames.FundAims;
			c.CharacterMaxLength = 1073741823;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(OMAMFundMetadata.ColumnNames.FundFactSheetURL, 6, typeof(System.String), esSystemType.String);	
			c.PropertyName = OMAMFundMetadata.PropertyNames.FundFactSheetURL;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(OMAMFundMetadata.ColumnNames.SPRatingURL, 7, typeof(System.String), esSystemType.String);	
			c.PropertyName = OMAMFundMetadata.PropertyNames.SPRatingURL;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(OMAMFundMetadata.ColumnNames.SPRatingFile, 8, typeof(System.String), esSystemType.String);	
			c.PropertyName = OMAMFundMetadata.PropertyNames.SPRatingFile;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(OMAMFundMetadata.ColumnNames.SPRatingCopy, 9, typeof(System.String), esSystemType.String);	
			c.PropertyName = OMAMFundMetadata.PropertyNames.SPRatingCopy;
			c.CharacterMaxLength = 1073741823;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(OMAMFundMetadata.ColumnNames.OBSRRatingURL, 10, typeof(System.String), esSystemType.String);	
			c.PropertyName = OMAMFundMetadata.PropertyNames.OBSRRatingURL;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(OMAMFundMetadata.ColumnNames.OBSRRatingCopy, 11, typeof(System.String), esSystemType.String);	
			c.PropertyName = OMAMFundMetadata.PropertyNames.OBSRRatingCopy;
			c.CharacterMaxLength = 1073741823;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(OMAMFundMetadata.ColumnNames.CityWireRatingURL, 12, typeof(System.String), esSystemType.String);	
			c.PropertyName = OMAMFundMetadata.PropertyNames.CityWireRatingURL;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(OMAMFundMetadata.ColumnNames.CityWitreRatingCopy, 13, typeof(System.String), esSystemType.String);	
			c.PropertyName = OMAMFundMetadata.PropertyNames.CityWitreRatingCopy;
			c.CharacterMaxLength = 1073741823;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(OMAMFundMetadata.ColumnNames.OMAMTVFile, 14, typeof(System.String), esSystemType.String);	
			c.PropertyName = OMAMFundMetadata.PropertyNames.OMAMTVFile;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(OMAMFundMetadata.ColumnNames.FactsheetFile, 15, typeof(System.String), esSystemType.String);	
			c.PropertyName = OMAMFundMetadata.PropertyNames.FactsheetFile;
			c.CharacterMaxLength = 100;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(OMAMFundMetadata.ColumnNames.FactsheetURL, 16, typeof(System.String), esSystemType.String);	
			c.PropertyName = OMAMFundMetadata.PropertyNames.FactsheetURL;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(OMAMFundMetadata.ColumnNames.ReasonsWhyFile, 17, typeof(System.String), esSystemType.String);	
			c.PropertyName = OMAMFundMetadata.PropertyNames.ReasonsWhyFile;
			c.CharacterMaxLength = 100;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(OMAMFundMetadata.ColumnNames.ReasonsWhyURL, 18, typeof(System.String), esSystemType.String);	
			c.PropertyName = OMAMFundMetadata.PropertyNames.ReasonsWhyURL;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(OMAMFundMetadata.ColumnNames.AnnualReportFile, 19, typeof(System.String), esSystemType.String);	
			c.PropertyName = OMAMFundMetadata.PropertyNames.AnnualReportFile;
			c.CharacterMaxLength = 100;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(OMAMFundMetadata.ColumnNames.AnnualReportURL, 20, typeof(System.String), esSystemType.String);	
			c.PropertyName = OMAMFundMetadata.PropertyNames.AnnualReportURL;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(OMAMFundMetadata.ColumnNames.InterimReportFile, 21, typeof(System.String), esSystemType.String);	
			c.PropertyName = OMAMFundMetadata.PropertyNames.InterimReportFile;
			c.CharacterMaxLength = 100;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(OMAMFundMetadata.ColumnNames.InterimReportURL, 22, typeof(System.String), esSystemType.String);	
			c.PropertyName = OMAMFundMetadata.PropertyNames.InterimReportURL;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(OMAMFundMetadata.ColumnNames.OBSRReportFile, 23, typeof(System.String), esSystemType.String);	
			c.PropertyName = OMAMFundMetadata.PropertyNames.OBSRReportFile;
			c.CharacterMaxLength = 100;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(OMAMFundMetadata.ColumnNames.OBSRReportURL, 24, typeof(System.String), esSystemType.String);	
			c.PropertyName = OMAMFundMetadata.PropertyNames.OBSRReportURL;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(OMAMFundMetadata.ColumnNames.SPReportFile, 25, typeof(System.String), esSystemType.String);	
			c.PropertyName = OMAMFundMetadata.PropertyNames.SPReportFile;
			c.CharacterMaxLength = 100;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(OMAMFundMetadata.ColumnNames.SPReportURL, 26, typeof(System.String), esSystemType.String);	
			c.PropertyName = OMAMFundMetadata.PropertyNames.SPReportURL;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(OMAMFundMetadata.ColumnNames.SalesAidFile, 27, typeof(System.String), esSystemType.String);	
			c.PropertyName = OMAMFundMetadata.PropertyNames.SalesAidFile;
			c.CharacterMaxLength = 100;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(OMAMFundMetadata.ColumnNames.SalesAidURL, 28, typeof(System.String), esSystemType.String);	
			c.PropertyName = OMAMFundMetadata.PropertyNames.SalesAidURL;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(OMAMFundMetadata.ColumnNames.TermsheetFile, 29, typeof(System.String), esSystemType.String);	
			c.PropertyName = OMAMFundMetadata.PropertyNames.TermsheetFile;
			c.CharacterMaxLength = 100;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(OMAMFundMetadata.ColumnNames.TermsheetURL, 30, typeof(System.String), esSystemType.String);	
			c.PropertyName = OMAMFundMetadata.PropertyNames.TermsheetURL;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(OMAMFundMetadata.ColumnNames.OffShore, 31, typeof(System.Boolean), esSystemType.Boolean);	
			c.PropertyName = OMAMFundMetadata.PropertyNames.OffShore;
			c.HasDefault = true;
			c.Default = @"(0)";
			_columns.Add(c); 
				
			c = new esColumnMetadata(OMAMFundMetadata.ColumnNames.Currency, 32, typeof(System.String), esSystemType.String);	
			c.PropertyName = OMAMFundMetadata.PropertyNames.Currency;
			c.CharacterMaxLength = 5;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(OMAMFundMetadata.ColumnNames.CreatedDate, 33, typeof(System.DateTime), esSystemType.DateTime);	
			c.PropertyName = OMAMFundMetadata.PropertyNames.CreatedDate;
			_columns.Add(c); 
				
			c = new esColumnMetadata(OMAMFundMetadata.ColumnNames.CreatedBy, 34, typeof(System.Int32), esSystemType.Int32);	
			c.PropertyName = OMAMFundMetadata.PropertyNames.CreatedBy;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
		}
		#endregion

		static public OMAMFundMetadata Meta()
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
			 public const string FundCode = "FundCode";
			 public const string FundName = "FundName";
			 public const string FundSnippet = "FundSnippet";
			 public const string RiskWarning = "RiskWarning";
			 public const string FundAims = "FundAims";
			 public const string FundFactSheetURL = "FundFactSheetURL";
			 public const string SPRatingURL = "SPRatingURL";
			 public const string SPRatingFile = "SPRatingFile";
			 public const string SPRatingCopy = "SPRatingCopy";
			 public const string OBSRRatingURL = "OBSRRatingURL";
			 public const string OBSRRatingCopy = "OBSRRatingCopy";
			 public const string CityWireRatingURL = "CityWireRatingURL";
			 public const string CityWitreRatingCopy = "CityWitreRatingCopy";
			 public const string OMAMTVFile = "OMAMTVFile";
			 public const string FactsheetFile = "FactsheetFile";
			 public const string FactsheetURL = "FactsheetURL";
			 public const string ReasonsWhyFile = "ReasonsWhyFile";
			 public const string ReasonsWhyURL = "ReasonsWhyURL";
			 public const string AnnualReportFile = "AnnualReportFile";
			 public const string AnnualReportURL = "AnnualReportURL";
			 public const string InterimReportFile = "InterimReportFile";
			 public const string InterimReportURL = "InterimReportURL";
			 public const string OBSRReportFile = "OBSRReportFile";
			 public const string OBSRReportURL = "OBSRReportURL";
			 public const string SPReportFile = "SPReportFile";
			 public const string SPReportURL = "SPReportURL";
			 public const string SalesAidFile = "SalesAidFile";
			 public const string SalesAidURL = "SalesAidURL";
			 public const string TermsheetFile = "TermsheetFile";
			 public const string TermsheetURL = "TermsheetURL";
			 public const string OffShore = "OffShore";
			 public const string Currency = "Currency";
			 public const string CreatedDate = "CreatedDate";
			 public const string CreatedBy = "CreatedBy";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string Id = "Id";
			 public const string FundCode = "FundCode";
			 public const string FundName = "FundName";
			 public const string FundSnippet = "FundSnippet";
			 public const string RiskWarning = "RiskWarning";
			 public const string FundAims = "FundAims";
			 public const string FundFactSheetURL = "FundFactSheetURL";
			 public const string SPRatingURL = "SPRatingURL";
			 public const string SPRatingFile = "SPRatingFile";
			 public const string SPRatingCopy = "SPRatingCopy";
			 public const string OBSRRatingURL = "OBSRRatingURL";
			 public const string OBSRRatingCopy = "OBSRRatingCopy";
			 public const string CityWireRatingURL = "CityWireRatingURL";
			 public const string CityWitreRatingCopy = "CityWitreRatingCopy";
			 public const string OMAMTVFile = "OMAMTVFile";
			 public const string FactsheetFile = "FactsheetFile";
			 public const string FactsheetURL = "FactsheetURL";
			 public const string ReasonsWhyFile = "ReasonsWhyFile";
			 public const string ReasonsWhyURL = "ReasonsWhyURL";
			 public const string AnnualReportFile = "AnnualReportFile";
			 public const string AnnualReportURL = "AnnualReportURL";
			 public const string InterimReportFile = "InterimReportFile";
			 public const string InterimReportURL = "InterimReportURL";
			 public const string OBSRReportFile = "OBSRReportFile";
			 public const string OBSRReportURL = "OBSRReportURL";
			 public const string SPReportFile = "SPReportFile";
			 public const string SPReportURL = "SPReportURL";
			 public const string SalesAidFile = "SalesAidFile";
			 public const string SalesAidURL = "SalesAidURL";
			 public const string TermsheetFile = "TermsheetFile";
			 public const string TermsheetURL = "TermsheetURL";
			 public const string OffShore = "OffShore";
			 public const string Currency = "Currency";
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
            lock (typeof(OMAMFundMetadata))
            {
				if(OMAMFundMetadata.mapDelegates == null)
				{
					OMAMFundMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
			
                if (OMAMFundMetadata.meta == null)
                {
                    OMAMFundMetadata.meta = new OMAMFundMetadata();
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
				meta.AddTypeMap("FundCode", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("FundName", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("FundSnippet", new esTypeMap("ntext", "System.String"));
				meta.AddTypeMap("RiskWarning", new esTypeMap("ntext", "System.String"));
				meta.AddTypeMap("FundAims", new esTypeMap("ntext", "System.String"));
				meta.AddTypeMap("FundFactSheetURL", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("SPRatingURL", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("SPRatingFile", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("SPRatingCopy", new esTypeMap("ntext", "System.String"));
				meta.AddTypeMap("OBSRRatingURL", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("OBSRRatingCopy", new esTypeMap("ntext", "System.String"));
				meta.AddTypeMap("CityWireRatingURL", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("CityWitreRatingCopy", new esTypeMap("ntext", "System.String"));
				meta.AddTypeMap("OMAMTVFile", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("FactsheetFile", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("FactsheetURL", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("ReasonsWhyFile", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("ReasonsWhyURL", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("AnnualReportFile", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("AnnualReportURL", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("InterimReportFile", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("InterimReportURL", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("OBSRReportFile", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("OBSRReportURL", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("SPReportFile", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("SPReportURL", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("SalesAidFile", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("SalesAidURL", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("TermsheetFile", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("TermsheetURL", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("OffShore", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("Currency", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("CreatedDate", new esTypeMap("smalldatetime", "System.DateTime"));
				meta.AddTypeMap("CreatedBy", new esTypeMap("int", "System.Int32"));
			
				
				
				
				meta.Source = "OMAMFund";
				meta.Destination = "OMAMFund";
				
				meta.spInsert = "proc_OMAMFundInsert";				
				meta.spUpdate = "proc_OMAMFundUpdate";		
				meta.spDelete = "proc_OMAMFundDelete";
				meta.spLoadAll = "proc_OMAMFundLoadAll";
				meta.spLoadByPrimaryKey = "proc_OMAMFundLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private OMAMFundMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
