using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using HBOS.FS.AMP.UPD.Types.DistributionFiles;
using HBOS.FS.AMP.UPD.WinUI.Classes;

namespace HBOS.FS.AMP.UPD.WinUI.Forms
{
	/// <summary>
	/// This form allows users to manipulate the values to be distributed on a UPAS export
	/// </summary>
	public class DistributionDataManipulationUPAS : DistributionDataManipulationBase
	{
		[DistributionDataBinding("SelectedValue","Mode")] private System.Windows.Forms.ComboBox comboBoxMode;
		private System.Windows.Forms.Label labelMode;
		[DistributionDataBinding("Value","StartDate")] private System.Windows.Forms.DateTimePicker dateTimePickerStartDate;
		private System.Windows.Forms.Label labelStartDate;
		[DistributionDataBinding("Value","EndDate")] private System.Windows.Forms.DateTimePicker dateTimePickerEndDate;
		private System.Windows.Forms.Label labelEndDate;
		private System.Windows.Forms.ErrorProvider endDateError;
		private IContainer components = null;

		
		/// <summary>
		/// Creates a new <see cref="DistributionDataManipulationUPAS"/> instance.
		/// </summary>
		public DistributionDataManipulationUPAS(DataSet data,DistributionFile distributionFile):base(data,distributionFile)
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();

			populateControls();


		}

		/// <summary>
		/// Override this method to alter a value before binding occurs.
		/// </summary>
		/// <param name="value">Value.</param>
		/// <param name="control">Control.</param>
		protected override void alterValuePreBind(ref object value, Control control)
		{
			base.alterValuePreBind(ref value, control);
			if (control.Equals(this.dateTimePickerEndDate))
			{
				if (value!=Convert.DBNull && (DateTime)value == new DateTime(1900,1,1))
				{
					value=Convert.DBNull;
					dateTimePickerEndDate.Checked=false;
				}
				else
				{
					dateTimePickerEndDate.Checked=true;
				}
			}

		}


		/// <summary>
		/// Oppertunity to validate the values entered.
		/// </summary>
		/// <returns></returns>
		protected override bool validate()
		{
			bool returnValue=true;

			returnValue=base.validate();
			if (returnValue)
			{
				Control focusControl=dateTimePickerEndDate;

				this.endDateError.SetError(focusControl,string.Empty);

				if (dateTimePickerEndDate.Checked &&  this.dateTimePickerEndDate.Value.CompareTo( this.dateTimePickerStartDate.Value)<0)
				{
					focusControl.Focus();
					this.endDateError.SetError(focusControl,MessageBoxHelper.DialogText("UPASPopupEndDateInvalid"));
					returnValue=false;
				}
			}

			return returnValue;
		}

		private void populateControls()
		{
			ArrayList list=new ArrayList();
			list.Add(new UPASMode("FORCED"));
			list.Add(new UPASMode("STANDARD"));
			list.Add(new UPASMode("VALIDATION"));
			comboBoxMode.DataSource=list;
			comboBoxMode.DisplayMember="Name";
			comboBoxMode.ValueMember="Name";

			this.dateTimePickerEndDate.Value=GlobalRegistry.CurrentCompanyValuationDateAndTime;
		}

		private class UPASMode
		{
			private string name;

			public string Name
			{
				get { return name; }
				set { name = value; }
			}

			public UPASMode(string name)
			{
				this.Name = name;
			}
		}

		/// <summary>
		/// Method to allow this popup forms permission to be specified
		/// </summary>
		/// <returns></returns>
		protected override string getPopupPermission()
		{
			return "UPASPopup";
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

		#region Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.comboBoxMode = new System.Windows.Forms.ComboBox();
			this.labelMode = new System.Windows.Forms.Label();
			this.dateTimePickerStartDate = new System.Windows.Forms.DateTimePicker();
			this.labelStartDate = new System.Windows.Forms.Label();
			this.labelEndDate = new System.Windows.Forms.Label();
			this.dateTimePickerEndDate = new System.Windows.Forms.DateTimePicker();
			this.endDateError = new System.Windows.Forms.ErrorProvider();
			this.SuspendLayout();
			// 
			// comboBoxMode
			// 
			this.comboBoxMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxMode.Location = new System.Drawing.Point(136, 32);
			this.comboBoxMode.Name = "comboBoxMode";
			this.comboBoxMode.Size = new System.Drawing.Size(208, 24);
			this.comboBoxMode.TabIndex = 1;
			// 
			// labelMode
			// 
			this.labelMode.Location = new System.Drawing.Point(32, 32);
			this.labelMode.Name = "labelMode";
			this.labelMode.TabIndex = 2;
			this.labelMode.Text = "Mode:";
			// 
			// dateTimePickerStartDate
			// 
			this.dateTimePickerStartDate.CustomFormat = "dd/mmm/yyyy";
			this.dateTimePickerStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.dateTimePickerStartDate.Location = new System.Drawing.Point(136, 64);
			this.dateTimePickerStartDate.Name = "dateTimePickerStartDate";
			this.dateTimePickerStartDate.Size = new System.Drawing.Size(208, 22);
			this.dateTimePickerStartDate.TabIndex = 3;
			// 
			// labelStartDate
			// 
			this.labelStartDate.Location = new System.Drawing.Point(32, 64);
			this.labelStartDate.Name = "labelStartDate";
			this.labelStartDate.TabIndex = 2;
			this.labelStartDate.Text = "Start Date:";
			// 
			// labelEndDate
			// 
			this.labelEndDate.Location = new System.Drawing.Point(32, 104);
			this.labelEndDate.Name = "labelEndDate";
			this.labelEndDate.TabIndex = 2;
			this.labelEndDate.Text = "End Date:";
			// 
			// dateTimePickerEndDate
			// 
			this.dateTimePickerEndDate.Checked = false;
			this.dateTimePickerEndDate.CustomFormat = "dd/mmm/yyyy";
			this.dateTimePickerEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.dateTimePickerEndDate.Location = new System.Drawing.Point(136, 104);
			this.dateTimePickerEndDate.Name = "dateTimePickerEndDate";
			this.dateTimePickerEndDate.ShowCheckBox = true;
			this.dateTimePickerEndDate.Size = new System.Drawing.Size(208, 22);
			this.dateTimePickerEndDate.TabIndex = 3;
			// 
			// endDateError
			// 
			this.endDateError.ContainerControl = this;
			// 
			// DistributionDataManipulationUPAS
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
			this.ClientSize = new System.Drawing.Size(440, 208);
			this.Controls.Add(this.dateTimePickerStartDate);
			this.Controls.Add(this.labelMode);
			this.Controls.Add(this.comboBoxMode);
			this.Controls.Add(this.labelStartDate);
			this.Controls.Add(this.labelEndDate);
			this.Controls.Add(this.dateTimePickerEndDate);
			this.Name = "DistributionDataManipulationUPAS";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.ResumeLayout(false);

		}

		#endregion

		/// <summary>
		/// Override this method to alter a value before binding occurs.
		/// </summary>
		/// <param name="value">Value.</param>
		/// <param name="control">Control.</param>
		protected override void alterValuePreUnBind(ref object value, Control control)
		{
			base.alterValuePreUnBind(ref value, control);
			if (control==this.dateTimePickerEndDate && !dateTimePickerEndDate.Checked)
			{
				value=Convert.DBNull;	
			}
		}
	}
}