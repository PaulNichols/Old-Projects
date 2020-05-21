using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace CMS
{
	/// <summary>
	/// Summary description for WebForm1.
	/// </summary>
	public class PerformanceMeasuring : System.Web.UI.Page
	{
		protected System.Data.DataView gridView;
		protected System.Web.UI.WebControls.DataGrid grdElements;
		protected System.Web.UI.WebControls.LinkButton lnkTop;
		protected System.Web.UI.WebControls.LinkButton lnkUp;
		protected System.Web.UI.WebControls.HyperLink lnkPage4;
		protected System.Web.UI.WebControls.HyperLink lnkPage7;
		protected CMS.ServiceElementsDS serviceElementsDS;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			if (!this.IsPostBack)
			{
				this.serviceElementsDS.ReadXml(System.Configuration.ConfigurationSettings.AppSettings["ServiceElementData"].ToString(), System.Data.XmlReadMode.Auto);
				//this.serviceElementsDS.ReadXml(Server.MapPath("ServiceElementData.xml"), System.Data.XmlReadMode.Auto);
				Session.Add("ElementDs",this.serviceElementsDS);
			}
			
		}

		public void grdElements_OnItemCommand(object sender, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			this.ViewState.Add("ThisMonth",e.CommandArgument.ToString());
			//this.ViewState.Add("PreviousFilter",e.Item.DataItem
		}

		public void grdElements_OnItemDataBound( object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if (e.Item.DataSetIndex>-1)
			{
				DisplayNextMonthQuestionMark(e);
				DisplayLastMonthQuestionMark(e);
				DisplayThisMonthQuestionMark(e);
				ShowThisMonthTriangle(e);
				ShowLastMonthTriangle(e);
				//ShowNextMonthTriangle(e);
				ShowNextMonthRAG(e);
				ShowThisMonthRAG(e);
				ShowLastMonthRAG(e);
				ShowServiceDetailColumn(e);
				RemoveDrillDownLink(e);
			}
		}
		
		private void RemoveDrillDownLink(System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			CMS.ServiceElementsDS.ElementRow row=((CMS.ServiceElementsDS.ElementRow)((System.Data.DataRowView)e.Item.DataItem).Row);
			if (row.Level==3)
			{
				e.Item.Cells[3].FindControl("lnkThisMonthText").Visible=false;
				((Label)e.Item.Cells[3].FindControl("lblThisMonthText")).BackColor=((LinkButton)e.Item.Cells[3].FindControl("lnkThisMonthText")).BackColor;
				e.Item.Cells[3].FindControl("lblThisMonthText").Visible=true;
			}
		}

		private void ShowServiceDetailColumn(System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			CMS.ServiceElementsDS.ElementRow row=((CMS.ServiceElementsDS.ElementRow)((System.Data.DataRowView)e.Item.DataItem).Row);
		
			try
			{
				this.grdElements.Columns[6].Visible=(row.Level==3);
				e.Item.Cells[6].FindControl("lnkDetail").Visible=(row.SLADetailId>0);
			}
			catch (System.Data.StrongTypingException)
			{
				e.Item.Cells[6].FindControl("lnkDetail").Visible=false;
			}
		}

		private void ShowThisMonthRAG(System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			try
			{
				//bad use of exception
				CMS.ServiceElementsDS.ElementRow row=((CMS.ServiceElementsDS.ElementRow)((System.Data.DataRowView)e.Item.DataItem).Row);
				((LinkButton)e.Item.Cells[3].FindControl("lnkThisMonthText")).BackColor=RAG(row.ThisMonth,row);
				e.Item.Cells[3].ToolTip="Target="+row.Target.ToString("P")+System.Environment.NewLine +"Red Threshold=" +row.RThreshold.ToString("P") + System.Environment.NewLine + "Amber Threshold="+row.AThreshold.ToString("P") +   System.Environment.NewLine +"Green Threshold="+row.GThreshold.ToString("P");
			}
			catch (System.Data.StrongTypingException)
			{
			}
		}

		private void ShowLastMonthRAG(System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			try
			{
				//bad use of exception
				CMS.ServiceElementsDS.ElementRow row=((CMS.ServiceElementsDS.ElementRow)((System.Data.DataRowView)e.Item.DataItem).Row);
				((Label)e.Item.Cells[2].FindControl("lblLastMonth")).BackColor=RAG(row.LastMonth,row);
				e.Item.Cells[2].ToolTip="Target="+row.Target.ToString("P")+System.Environment.NewLine +"Red Threshold=" +row.RThreshold.ToString("P") + System.Environment.NewLine + "Amber Threshold="+row.AThreshold.ToString("P") +   System.Environment.NewLine +"Green Threshold="+row.GThreshold.ToString("P");
			}
			catch (System.Data.StrongTypingException)
			{
			}
		}

		private void ShowNextMonthRAG(System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			try
			{
				//bad use of exception
				CMS.ServiceElementsDS.ElementRow row=((CMS.ServiceElementsDS.ElementRow)((System.Data.DataRowView)e.Item.DataItem).Row);
				((Label)e.Item.Cells[4].FindControl("lblNextMonthText")).BackColor=RAG(row.NextMonth,row);
			}
			catch (System.Data.StrongTypingException)
			{
			}
		}

		private System.Drawing.Color RAG(decimal monthValue,CMS.ServiceElementsDS.ElementRow row)
		{
			//not sure if this is how the thresholds should work?
			if (monthValue<=row.RThreshold)
			{
				return System.Drawing.Color.Red;
			}
			else if (monthValue>row.RThreshold && monthValue<row.GThreshold)
			{
				return System.Drawing.Color.Yellow;
			}
			else
			{
				return System.Drawing.Color.FromArgb(0,255,0);
			}

		}

		private void ShowThisMonthTriangle(System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			try
			{
				e.Item.Cells[3].FindControl("lblThisMonthTriangle").Visible=((CMS.ServiceElementsDS.ElementRow)((System.Data.DataRowView)e.Item.DataItem).Row).RTThisMonth;
			}
			catch (System.Data.StrongTypingException)
			{
				try
				{
					e.Item.Cells[3].FindControl("lblThisMonthTriangle").Visible=false;
				}
				catch
				{
				}
			}
			return;
		}

		private void ShowLastMonthTriangle(System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			foreach (System.Web.UI.Control ctrl in e.Item.Cells[2].Controls)
			{
				if (ctrl.ID=="lblLastMonthTriangle")
				{
					try
					{
						ctrl.Visible=((CMS.ServiceElementsDS.ElementRow)((System.Data.DataRowView)e.Item.DataItem).Row).RTLastMonth;
					}
					catch (System.Data.StrongTypingException)
					{
						ctrl.Visible=false;
					}
					return;
				}
			}				
		}

		//		private void ShowNextMonthTriangle(System.Web.UI.WebControls.DataGridItemEventArgs e)
		//		{
		//			foreach (System.Web.UI.Control ctrl in e.Item.Cells[4].Controls)
		//			{
		//				if (ctrl.ID=="lblNextMonthTriangle")
		//				{
		//					try
		//					{
		//						ctrl.Visible=((CMS.ServiceElementsDS.ElementRow)((System.Data.DataRowView)e.Item.DataItem).Row).RTNextMonth;
		//					}
		//					catch (System.Data.StrongTypingException)
		//					{
		//						ctrl.Visible=false;
		//					}
		//					return;
		//				}
		//			}				
		//		}

		private void DisplayNextMonthQuestionMark(System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			try
			{
				//bad use of exception
				decimal NextMonth=((CMS.ServiceElementsDS.ElementRow)((System.Data.DataRowView)e.Item.DataItem).Row).NextMonth;
			}
			catch (System.Data.StrongTypingException)
			{
				//bad use of index
				e.Item.Cells[4].Text="?";
			//	e.Item.Cells[4].BackColor=System.Drawing.Color.Gray;
			}
		}

		private void DisplayThisMonthQuestionMark(System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			try
			{
				//bad use of exception
				decimal ThisMonth=((CMS.ServiceElementsDS.ElementRow)((System.Data.DataRowView)e.Item.DataItem).Row).ThisMonth;
			}
			catch (System.Data.StrongTypingException)
			{
				//bad use of index
				e.Item.Cells[3].Text="?";
				//e.Item.Cells[3].BackColor=System.Drawing.Color.Gray;
			}
		}

		private void DisplayLastMonthQuestionMark(System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			try
			{
				//bad use of exception
				decimal LastMonth=((CMS.ServiceElementsDS.ElementRow)((System.Data.DataRowView)e.Item.DataItem).Row).LastMonth;
			}
			catch (System.Data.StrongTypingException)
			{
				//bad use of index
				e.Item.Cells[2].Text="?";
				//e.Item.Cells[2].BackColor=System.Drawing.Color.Gray;
			}
		}


		private void BindGrid()
		{
			this.serviceElementsDS=( CMS.ServiceElementsDS )Session["ElementDs"];
			this.gridView.Table = this.serviceElementsDS.Element;

			try
			{
				gridView.RowFilter="ParentElementId =" + this.ViewState["ThisMonth"].ToString();
			}
			catch 
			{
				gridView.RowFilter="ParentElementId is null";				
			}

			this.grdElements.DataBind();

		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.serviceElementsDS = new CMS.ServiceElementsDS();
			this.gridView = new System.Data.DataView();
			((System.ComponentModel.ISupportInitialize)(this.serviceElementsDS)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView)).BeginInit();
			this.lnkTop.Click += new System.EventHandler(this.lnkTop_Click);
			this.lnkUp.Click += new System.EventHandler(this.lnkUp_Click);
			// 
			// serviceElementsDS
			// 
			this.serviceElementsDS.DataSetName = "ServiceElementsDS";
			this.serviceElementsDS.Locale = new System.Globalization.CultureInfo("en-US");
			// 
			// gridView
			// 
			this.gridView.AllowDelete = false;
			this.gridView.AllowEdit = false;
			this.gridView.AllowNew = false;
			this.Load += new System.EventHandler(this.Page_Load);
			this.PreRender += new System.EventHandler(this.PreRender_Handler);
			((System.ComponentModel.ISupportInitialize)(this.serviceElementsDS)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();

		}
		#endregion

		private void PreRender_Handler(object sender,System.EventArgs e)
		{
			BindGrid();			
		}

		private void lnkTop_Click(object sender, System.EventArgs e)
		{
			this.ViewState.Remove("ThisMonth");
		}

		private int ParentFilter
		{
			get
			{
				return 1;
			}
		}

		private void lnkUp_Click(object sender, System.EventArgs e)
		{
			this.ViewState.Add("ThisMonth",ParentFilter);
		}

		
	}
}
