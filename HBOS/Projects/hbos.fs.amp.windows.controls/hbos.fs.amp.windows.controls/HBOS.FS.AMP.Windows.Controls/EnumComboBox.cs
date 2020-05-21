using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Reflection;

namespace HBOS.FS.AMP.Windows.Controls
{

	/// <summary>
	/// A combo box that displays the contents of an enum whose items are decorated
	/// with the EnumDisplayTextAttribute attribute.
	/// </summary>
	/// <example>
	///		<para>The enum must be attributed with the appropriate information.</para>
	///		<code>
	///			public enum MyColours : int
	///			{
	///				[EnumDisplayText("Blaze Red")]
	///				Red,
	///				[EnumDisplayText("Shocking Blue")]
	///				Blue,
	///				[EnumDisplayText("Grass Green")]
	///				Green,
	///				[EnumDisplayText("Storm Purple")]
	///				Purple
	///			}
	///		</code>
	///		<para>The enum type is then assigned to the Enum combo</para>
	///		<code>
	///			enumComboBox1.Enum = typeof(MyColours);
	///		</code>
	/// </example>
	public class EnumComboBox : System.Windows.Forms.ComboBox
	{
		private System.ComponentModel.Container components = null;
		private Hashtable	m_keys = new Hashtable();
		private Hashtable	m_values = new Hashtable();

		/// <summary>
		/// Constructs a new EnumComboBox
		/// </summary>
		/// <remarks>DropDownStyle is set to DropDownList.</remarks>
		public EnumComboBox(): base()
		{
			InitializeComponent();

			this.DropDownStyle = ComboBoxStyle.DropDownList;
		}


		/// <summary>
		/// Returns the selected enum value
		/// </summary>
		/// <example> Retrieving the selected value:
		/// <code>
		/// AuditEventSource auditEventSource = (AuditEventSource)enumComboBox1.SelectedItem;
		/// </code>
		/// Setting the selected value:
		/// <code>
		/// enumComboBox1.SelectedItem = AuditEventSource.AgencyEarnings;
		/// </code>
		/// </example>
		public new object SelectedItem
		{
			get
			{
				return m_keys[this.Text];
			}
			set
			{
				this.Text = m_values[value].ToString();
			}
		}


		/// <summary>
		/// Set the enum that will be displayed in the combo box
		/// </summary>
		/// <example> Populating an EnumComboBox:
		/// <code>
		/// enumComboBox1.Enum = typeof(AuditEventSource);
		/// </code>
		/// </example>
		public System.Type Enum
		{
			set
			{
				if( value != null )
				{
					// Clear the key values hash table
					m_keys.Clear();

					// Clear the list
					this.Items.Clear();

					// Add each enum item that's been decorated with an EnumDisplayTextAttribute
					foreach( FieldInfo fi in value.GetFields() )
					{
						EnumDisplayTextAttribute[] edtas = (EnumDisplayTextAttribute[])fi.GetCustomAttributes( typeof( EnumDisplayTextAttribute ), false );

						if( edtas.Length > 0 )
						{
							m_keys.Add( edtas[0].DisplayText, fi.GetValue( fi.Name ) );
							m_values.Add( fi.GetValue( fi.Name ), edtas[0].DisplayText );

							this.Items.Add( edtas[0].DisplayText );
						}
					}
				}
			}
		}


		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}


		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			// 
			// EnumComboBox
			// 
			this.Name = "EnumComboBox";
			this.Size = new System.Drawing.Size(136, 24);

		}
		#endregion
	}
}
