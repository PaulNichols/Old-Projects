using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using HBOS.FS.AMP.UPD.Security;
using HBOS.FS.AMP.UPD.Types.DistributionFiles;

namespace HBOS.FS.AMP.UPD.WinUI.Forms
{
	/// <summary>
	/// Summary description for DistributionDataManipulationBase.
	/// </summary>
	public  class DistributionDataManipulationBase : Form
	{
		private System.Windows.Forms.Button ButtonOK;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private Container components = null;

		private Hashtable m_BoundControls;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.Label labelPermissionWarning;
		private DataSet m_Data;
		private readonly DistributionFile m_DistributionFile;

		/// <summary>
		/// Creates a new <see cref="DistributionDataManipulationBase"/> instance.
		/// </summary>
		public DistributionDataManipulationBase()
		{
		}

		/// <summary>
		/// Creates a new <see cref="DistributionDataManipulationBase"/> instance.
		/// </summary>
		public DistributionDataManipulationBase(DataSet data,DistributionFile distributionFile)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			m_Data=data;
			this.m_DistributionFile = distributionFile;
		}

		private Hashtable buildCollectionOfBoundControls()
		{
			Hashtable boundControls=new Hashtable();
		
			FieldInfo[] fields = this.GetType().GetFields(BindingFlags.NonPublic |BindingFlags.Instance);

			for(int i = 0; i < fields.Length; i++)
			{
				Object[] myAttributes = fields[i].GetCustomAttributes(typeof(DistributionDataBindingAttribute),true);
				if(myAttributes.Length > 0)
				{
					Object control=fields[i].GetValue(this);
					if (control is Control)
					{
						boundControls.Add(control,myAttributes[0]);
						checkPermission((Control) control);
					}
				}
			}
				

			return boundControls;
		}

		private void checkPermission(Control control)
		{
			control.Enabled= hasPermission();
		}

		private bool hasPermission()
		{
			UPDPrincipal currentPrincipal = (UPDPrincipal) Thread.CurrentPrincipal;
			string permission=getPopupPermission();
			return (permission!="" && currentPrincipal.IsInRole(permission));
		}

		/// <summary>
		/// Method to allow this popup forms permission to be specified
		/// </summary>
		/// <returns></returns>
		protected virtual string getPopupPermission()
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.ButtonOK = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.labelPermissionWarning = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// ButtonOK
			// 
			this.ButtonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.ButtonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.ButtonOK.Location = new System.Drawing.Point(352, 168);
			this.ButtonOK.Name = "ButtonOK";
			this.ButtonOK.TabIndex = 0;
			this.ButtonOK.Text = "OK";
			// 
			// buttonCancel
			// 
			this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new System.Drawing.Point(32, 168);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.TabIndex = 0;
			this.buttonCancel.Text = "Cancel";
			// 
			// labelPermissionWarning
			// 
			this.labelPermissionWarning.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.labelPermissionWarning.Location = new System.Drawing.Point(120, 152);
			this.labelPermissionWarning.Name = "labelPermissionWarning";
			this.labelPermissionWarning.Size = new System.Drawing.Size(216, 32);
			this.labelPermissionWarning.TabIndex = 1;
			this.labelPermissionWarning.Text = "You do not have the required permissions to edit this data";
			this.labelPermissionWarning.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.labelPermissionWarning.Visible = false;
			// 
			// DistributionDataManipulationBase
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
			this.ClientSize = new System.Drawing.Size(450, 209);
			this.ControlBox = false;
			this.Controls.Add(this.labelPermissionWarning);
			this.Controls.Add(this.ButtonOK);
			this.Controls.Add(this.buttonCancel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(320, 128);
			this.Name = "DistributionDataManipulationBase";
			this.ShowInTaskbar = false;
			this.Text = "Please make {0} alterations if required...";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.DistributionDataManipulationBase_Closing);
			this.Load += new System.EventHandler(this.DistributionDataManipulationBase_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private void DistributionDataManipulationBase_Load(object sender, System.EventArgs e)
		{
			m_BoundControls=buildCollectionOfBoundControls();
			bindControls();
			this.labelPermissionWarning.Visible=!this.hasPermission();
			this.Text=String.Format( this.Text ,FormCaption);
		}

		/// <summary>
		/// Property to get the text that should be inserted into the forms header
		/// </summary>
		protected virtual string FormCaption
		{
			get
			{
				return m_DistributionFile.FileDescription;
			}
		}

		private void bindControls()
		{
			DataTable table=m_Data.Tables["ReleasedFunds"];
			if (table.Rows.Count>0)
			{
				foreach (DictionaryEntry boundControl in m_BoundControls)
				{
					DistributionDataBindingAttribute bindingAttribute=(DistributionDataBindingAttribute) boundControl.Value;
					Control control=(Control) boundControl.Key;
					int ColumnIndex=table.Columns.IndexOf(bindingAttribute.DataField);

					object value= table.Rows[0][ColumnIndex];
					alterValuePreBind(ref value, control);
					PropertyInfo property=control.GetType().GetProperty(bindingAttribute.ControlPropertyToBindTo);
					if (value!=Convert.DBNull)
					{
						property.SetValue(control,value,null);
					}
				}
				
			}
			else
			{
				Close();
			}
		
		}

		/// <summary>
		/// Override this method to alter a value before binding occurs.
		/// </summary>
		/// <param name="value">Value.</param>
		/// <param name="control">Control.</param>
		protected virtual void alterValuePreBind(ref object value,Control control)
		{}

		/// <summary>
		/// Override this method to alter a value before binding occurs.
		/// </summary>
		/// <param name="value">Value.</param>
		/// <param name="control">Control.</param>
		protected virtual void alterValuePreUnBind(ref object value,Control control)
		{}

//		private void ButtonOK_Click(object sender, System.EventArgs e)
//		{
//			if (validate())
//			{
//				unbindControls();
//				Close();
//			}
//		}

	

		private bool unbindControls()
		{
			if (hasPermission())
			{
				if (this.validate())
				{
					DataTable table=m_Data.Tables["ReleasedFunds"];
					foreach (DataRow row in table.Rows)
					{
						try
						{
							row.BeginEdit();
				
							foreach (DictionaryEntry boundControl in m_BoundControls)
							{
								DistributionDataBindingAttribute bindingAttribute=(DistributionDataBindingAttribute) boundControl.Value;
								Control control=(Control) boundControl.Key;
								PropertyInfo property=control.GetType().GetProperty(bindingAttribute.ControlPropertyToBindTo);
								object value=property.GetValue(control,null);
								int ColumnIndex=table.Columns.IndexOf(bindingAttribute.DataField);
								alterValuePreUnBind(ref value, control);
								row[ColumnIndex]=value;
							}

							row.AcceptChanges();
						}
						catch 
						{
							row.RejectChanges();
							throw;
						}
					}
				}
				else
				{
					return false;
				}
			}
			return true;
		}

		private void DistributionDataManipulationBase_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			e.Cancel=!unbindControls();
		}

		/// <summary>
		/// Oppertunity to validate the values entered.
		/// </summary>
		/// <returns></returns>
		protected virtual bool validate()
		{
			return true;
		}
	}

	/// <summary>
	/// This attribute is used to automatically bind fields to the export dataset
	/// </summary>
	[AttributeUsage(AttributeTargets.Field,AllowMultiple=false)]
	public class DistributionDataBindingAttribute : Attribute
	{
		private string controlPropertyToBindTo;
		private string dataField;

		/// <summary>
		/// Gets or sets the control property to bind to.
		/// </summary>
		/// <value></value>
		public string ControlPropertyToBindTo
		{
			get { return controlPropertyToBindTo; }
			set { controlPropertyToBindTo = value; }
		}

		/// <summary>
		/// Gets or sets the data field.
		/// </summary>
		/// <value></value>
		public string DataField
		{
			get { return dataField; }
			set { dataField = value; }
		}

		/// <summary>
		/// Creates a new <see cref="DistributionDataBindingAttribute"/> instance.
		/// </summary>
		/// <param name="controlPropertyToBindTo">Control property to bind to.</param>
		/// <param name="dataField">Data field.</param>
		public DistributionDataBindingAttribute(string controlPropertyToBindTo, string dataField) : base()
		{
			this.ControlPropertyToBindTo = controlPropertyToBindTo;
			this.DataField = dataField;
		}
	}



}