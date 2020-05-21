/*
	This C# source code was automatically generated using:

		SQL Server Centric .NET Code Generator
			v 1.0.1697.21165

			Generation Date: 15/01/2005 18:39:14
			Generator name: MAINSERVER\Administrator
			Template last update: 13/10/2003 04:51:40
			Template revision: 56177501

			SQL Server version: 08.00.0760
			Server: MAINSERVER\MAINSERVER
			Database: [Bob]

	WARNING: This source is provided "AS IS" without warranty of any kind.
	The author disclaims all warranties, either express or implied, including
	the warranties of merchantability and fitness for a particular purpose.
	In no event shall the author or its suppliers be liable for any damages
	whatsoever including direct, indirect, incidental, consequential, loss of
	business profits or special damages, even if the author or its suppliers
	have been advised of the possibility of such damages.

	    More information: http://www.microsoft.com/france/msdn/olymars
	Latest interim build: http://www.olymars.net/latest.zip
	       Author's blog: http://blogs.msdn.com/olymars
*/

using System;

namespace Bob.Windows.Forms {

	/// <summary>
	/// Describes the supported database type in this TextBox
	/// </summary>
	internal enum SupportedDatabaseTypes {

		/// <value>[To be supplied.]</value>
		DBType_datetime,
		/// <value>[To be supplied.]</value>
		DBType_smalldatetime,
		/// <value>[To be supplied.]</value>
		DBType_numeric,
		/// <value>[To be supplied.]</value>
		DBType_decimal,
		/// <value>[To be supplied.]</value>
		DBType_float,
		/// <value>[To be supplied.]</value>
		DBType_uniqueidentifier,
		/// <value>[To be supplied.]</value>
		DBType_smallint,
		/// <value>[To be supplied.]</value>
		DBType_int,
		/// <value>[To be supplied.]</value>
		DBType_bigint,
		/// <value>[To be supplied.]</value>
		DBType_money,
		/// <value>[To be supplied.]</value>
		DBType_smallmoney,
		/// <value>[To be supplied.]</value>
		DBType_real,
		/// <value>[To be supplied.]</value>
		DBType_nvarchar,
		/// <value>[To be supplied.]</value>
		DBType_ntext,
		/// <value>[To be supplied.]</value>
		DBType_text,
		/// <value>[To be supplied.]</value>
		DBType_varchar,
		/// <value>[To be supplied.]</value>
		DBType_char,
		/// <value>[To be supplied.]</value>
		DBType_nchar
	}

	/// <summary>
	/// This TextBox is able to manage the content according to the data corresponding database type.
	/// </summary>
	internal class WinForm_DatabaseTextBox: System.Windows.Forms.TextBox {

		private System.ComponentModel.Container components = null;

		private SupportedDatabaseTypes dBType = SupportedDatabaseTypes.DBType_varchar;
		/// <summary> 
		/// Indicates if the TextBox content is a valid varchar.
		/// </summary>
		internal SupportedDatabaseTypes DBType {

			get {

				return(this.dBType);
			}
			set {

				if (!System.Enum.IsDefined(typeof(SupportedDatabaseTypes), value)) {

					throw new System.ArgumentOutOfRangeException("Invalid value", value, "DBType");
				}

				this.dBType = value;
			}
		}

		private bool isValid = false;
		/// <summary> 
		/// Indicates if the TextBox content is a valid varchar.
		/// </summary>
		public bool IsValid {

			get {

				return(isValid);
			}
		}

		private void InternalIsValid() {

			string Content = this.Text.Trim();

			isValid = this.nullIsAllow;

			if (Content == String.Empty && !this.nullIsAllow) {

				isValid = false;
				return;
			}

			switch (this.dBType) {

				case SupportedDatabaseTypes.DBType_varchar:
					if (Content.Length > dataSize) {

						isValid = false;
						return;
					}
					break;

				case SupportedDatabaseTypes.DBType_char:
					if (Content.Length > dataSize) {

						isValid = false;
						return;
					}
					break;

				case SupportedDatabaseTypes.DBType_text:
					if (Content.Length > dataSize) {

						isValid = false;
						return;
					}
					break;

				case SupportedDatabaseTypes.DBType_nvarchar:
					if (Content.Length > dataSize) {

						isValid = false;
						return;
					}
					break;

				case SupportedDatabaseTypes.DBType_nchar:
					if (Content.Length > dataSize) {

						isValid = false;
						return;
					}
					break;

				case SupportedDatabaseTypes.DBType_ntext:
					if (Content.Length > dataSize) {

						isValid = false;
						return;
					}
					break;
			}
			object Value = this.GetSqlTypesValue;
		}

		/// <summary> 
		/// [To be supplied.]
		/// </summary>
		public object GetSqlTypesValue {

			get {

				if (this.Text.Trim() == String.Empty) {

					return (null);
				}
				else {

					switch (dBType) {

						case SupportedDatabaseTypes.DBType_text:
						case SupportedDatabaseTypes.DBType_ntext:
						case SupportedDatabaseTypes.DBType_varchar:
						case SupportedDatabaseTypes.DBType_nvarchar:
						case SupportedDatabaseTypes.DBType_char:
						case SupportedDatabaseTypes.DBType_nchar:
							isValid = true;
							return(new System.Data.SqlTypes.SqlString(this.Text));

						case SupportedDatabaseTypes.DBType_real:
							try {

								System.Data.SqlTypes.SqlSingle Value = new System.Data.SqlTypes.SqlSingle(System.Convert.ToSingle(this.Text));
								isValid = true;
								return(Value);
							}
							catch {

								isValid = false;
								return(null);
							}					

						case SupportedDatabaseTypes.DBType_bigint:
							try {

								System.Data.SqlTypes.SqlInt64 Value = new System.Data.SqlTypes.SqlInt64(System.Convert.ToInt64(this.Text));
								isValid = true;
								return(Value);
							}
							catch {

								isValid = false;
								return(null);
							}

						case SupportedDatabaseTypes.DBType_int:
							try {

								System.Data.SqlTypes.SqlInt32 Value = new System.Data.SqlTypes.SqlInt32(System.Convert.ToInt32(this.Text));
								isValid = true;
								return(Value);
							}
							catch {

								isValid = false;
								return(null);
							}

						case SupportedDatabaseTypes.DBType_smallint:
							try {

								System.Data.SqlTypes.SqlInt16 Value = new System.Data.SqlTypes.SqlInt16(System.Convert.ToInt16(this.Text));
								isValid = true;
								return(Value);
							}
							catch {

								isValid = false;
								return(null);
							}

						case SupportedDatabaseTypes.DBType_decimal:
						case SupportedDatabaseTypes.DBType_numeric:
							try {

								System.Data.SqlTypes.SqlDecimal Value = new System.Data.SqlTypes.SqlDecimal(System.Convert.ToDecimal(this.Text));
								isValid = true;
								return(Value);
							}
							catch {

								isValid = false;
								return(null);
							}

						case SupportedDatabaseTypes.DBType_float:
							try {

								System.Data.SqlTypes.SqlDouble Value = new System.Data.SqlTypes.SqlDouble(System.Convert.ToDouble(this.Text));
								isValid = true;
								return(Value);
							}
							catch {

								isValid = false;
								return(null);
							}

						case SupportedDatabaseTypes.DBType_smallmoney:
						case SupportedDatabaseTypes.DBType_money:
							try {

								System.Data.SqlTypes.SqlMoney Value = new System.Data.SqlTypes.SqlMoney(System.Convert.ToDecimal(this.Text));
								isValid = true;
								return(Value);
							}
							catch {

								isValid = false;
								return(null);
							}

						case SupportedDatabaseTypes.DBType_smalldatetime:
						case SupportedDatabaseTypes.DBType_datetime:
							try {

								System.Data.SqlTypes.SqlDateTime Value = new System.Data.SqlTypes.SqlDateTime(System.Convert.ToDateTime(this.Text));
								isValid = true;
								return(Value);
							}
							catch {

								isValid = false;
								return(null);
							}

						case SupportedDatabaseTypes.DBType_uniqueidentifier:
							try {

								System.Data.SqlTypes.SqlGuid Value = new System.Data.SqlTypes.SqlGuid(new System.Guid(this.Text));
								isValid = true;
								return(Value);
							}
							catch {

								isValid = false;
								return(null);
							}

						default:
							isValid = false;
							return(null);
					}
				}
			}
		}

		private bool nullIsAllow = false;
		/// <summary> 
		/// Gets or sets if an empty content for this TextBox is valid.
		/// </summary>
		public bool NullIsAllow {

			get {

				return(this.nullIsAllow);
			}
			set {

				nullIsAllow = value;
				if (this.Text.Trim() == String.Empty) {

					isValid = true;
				}
				HandleTextChanged(this, null);
			}
		}

		private int dataSize = -1;
		/// <summary> 
		/// Gets or sets the maximum size allowed for the data.
		/// </summary>
		public int DataSize {

			get {

				return(this.dataSize);
			}
			set {

				this.dataSize = value;
			}
		}

		/// <summary> 
		/// [To be supplied.]
		/// </summary>
		public WinForm_DatabaseTextBox() : base() {

			this.TextChanged += new System.EventHandler(this.HandleTextChanged);
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if(disposing)
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		private System.Drawing.Color invalidBackColor = System.Drawing.SystemColors.Window;

		/// <summary> 
		/// Gets or sets the bakcground color when the content is not valid.
		/// </summary>
		public System.Drawing.Color InvalidBackColor
		{
			get
			{
				return(this.invalidBackColor);
			}
			set
			{
				this.invalidBackColor = value;
			}
		}

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		/// <param name="sender">[To be supplied.]</param>
		/// <param name="e">[To be supplied.]</param>
		public void HandleTextChanged(object sender, System.EventArgs e) {

			this.InternalIsValid();
			if (this.isValid) {

				base.BackColor = System.Drawing.SystemColors.Window;
			}
			else {

				base.BackColor = invalidBackColor;
			}
		}
	}
}
