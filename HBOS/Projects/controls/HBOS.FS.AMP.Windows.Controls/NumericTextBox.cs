using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace HBOS.FS.AMP.Windows.Controls
{
	/// <summary>
	/// A text box that only allows the entry of numbers.
	/// </summary>
	/// <remarks>Useful for currency fields etc where we want to prevent the use from entering alphabetic characters.</remarks>
	/// <example>
	/// <para>A number of properties on the control should be set to ensure the user is limited to entering correct data.</para>
	/// <code>
	/// numericTextBox1.MaxValue = 100;
	/// numericTextBox1.MinValue = 0;
	/// numericTextBox1.ValueMultiplier = 1;
	/// numericTextBox1.DecimalPlaces = 2;
	/// </code>
	/// </example>
	[ToolboxItem(true)]
	[ToolboxBitmap(typeof(NumericTextBox))]
	public class NumericTextBox : System.Windows.Forms.TextBox
	{
		#region Private fields

		private int		m_decimalPlaces = 0;
		private decimal m_minValue = 0;
		private decimal m_maxValue = 100;
		private Regex	m_maskExpression = null;
		private	bool	m_showToolTips = true;
		private decimal	m_valueMultiplier = 1;
		private System.Windows.Forms.ToolTip toolTipProvider;

		#endregion

		#region Private methods

		/// <summary>
		/// Set the tooltip for the control.
		/// </summary>
		private void setToolTip()
		{
			if( m_showToolTips )
			{
				string toolTip = "Valid values range from " + m_minValue.ToString( "0.##########" ) + " to " + m_maxValue.ToString( "0.##########" );

				if( m_decimalPlaces > 0 )
				{
					toolTip += " (with " + m_decimalPlaces.ToString() + " d.p.s)";
				}

				toolTipProvider.SetToolTip( this, toolTip );
			}
			else
			{
				toolTipProvider.RemoveAll();
			}
		}


		/// <summary>
		/// Set the mask regular expression for the control. This is based on the
		/// number for decimal places and the minimum and maximum expression.
		/// </summary>
		private void setMaskExpression()
		{
			if( m_minValue >= 0.0M && m_decimalPlaces == 0 )
			{
				m_maskExpression = new Regex( TextValidator.PositiveIntegerRegEx);
			}
			else if( m_minValue < 0.0M && m_decimalPlaces == 0 )
			{
				m_maskExpression = new Regex( TextValidator.NegativeIntegerRegEx);
			}
			else if( m_minValue >= 0.0M && m_decimalPlaces > 0 )
			{
				m_maskExpression = new Regex( TextValidator.PositiveRealRegEx( m_decimalPlaces ) );
			}
			else
			{
				m_maskExpression = new Regex( TextValidator.NegativeRealRegEx( m_decimalPlaces ) );
			}
		}


		/// <summary>
		/// Validate whether a given string is a valid number for this control.
		/// </summary>
		/// <param name="text">number</param>
		/// <returns></returns>
		private bool validString( string text )
		{
			bool valid = m_maskExpression.IsMatch( text );

			if( valid )
			{
				if( text != "-" && text != "." && text != "-." )
				{
					decimal number = decimal.Parse( text );
					valid = ( number >= m_minValue && number <= m_maxValue );
				}
			}

			return valid;
		}


		/// <summary>
		/// Validate the entry of a new character.
		/// </summary>
		/// <param name="proposedCharacter">The character that the user wants to add to the control</param>
		/// <returns>true if valid, false if not</returns>
		private bool validCharacter( char proposedCharacter )
		{
			if( proposedCharacter == Convert.ToChar( ((int)Keys.Back) ) )	// Backspace
				return true;

			string text = this.Text.Remove( this.SelectionStart, this.SelectionLength );
			text = text.Insert( this.SelectionStart, proposedCharacter.ToString() );

			bool valid = validString( text );

			return valid;
		}


		/// <summary>
		/// Tests whether a string is numeric.
		/// </summary>
		/// <param name="text">string to test</param>
		/// <returns>true if numeric, false if not</returns>
		private bool isNumeric( string text )
		{
			try
			{
				decimal.Parse( text );

				return true;
			}
			catch
			{
				return false;
			}
		}

		#endregion

		#region Public properties

		/// <summary>
		/// The number of digits to the right of the decimal point.
		/// </summary>
		[
		Browsable(true),
		CategoryAttribute("Custom"),
		Description("Number of decimal places"),
		DefaultValue(0)
		]
		public int DecimalPlaces
		{
			get
			{
				return m_decimalPlaces;
			}
			set
			{
				m_decimalPlaces = value; 
				setMaskExpression();

				setToolTip();
			}
		}


		/// <summary>
		/// The minimum value the control will accept
		/// </summary>
		[
		Browsable(true),
		CategoryAttribute("Custom"),
		Description("Minimum value allowed"),
		DefaultValue(0)
		]
		public decimal MinValue
		{
			get
			{
				return m_minValue;
			}
			set
			{
				m_minValue = value; 
				setMaskExpression();

				setToolTip();
			}
		}


		/// <summary>
		/// The maximum value the control will accept
		/// </summary>
		[
		Browsable(true),
		CategoryAttribute("Custom"),
		Description("Maximum value allowed"),
		DefaultValue(100)
		]
		public decimal MaxValue
		{
			get
			{
				return m_maxValue;
			}
			set 
			{ 
				m_maxValue = value; 
				setMaskExpression();

				setToolTip();
			}
		}


		/// <summary>
		/// Indicates whether the control shows tool tips.
		/// </summary>
		[
		Browsable(true),
		CategoryAttribute("Custom"),
		Description("Display tooltips"),
		DefaultValue(true)
		]
		public bool ShowToolTips
		{
			get
			{
				return m_showToolTips;
			}
			set 
			{ 
				m_showToolTips = value; 

				setToolTip();
			}
		}

		
		/// <summary>
		/// The text displayed in the control
		/// </summary>
		[
		Browsable(true),
		CategoryAttribute("Custom"),
		Description("Text displayed in the control")
		]
		public override string Text
		{
			get
			{
				return base.Text;
			}
			set
			{
				if( value == string.Empty || !isNumeric( value ) )
				{
					base.Text = string.Empty;
				}
				else
				{
					string stringValue = value;

					if( stringValue.LastIndexOf( "." ) >= 0 )
					{
						// Remove any superfluous trailing zeros and decimal points
						stringValue = value.TrimEnd( new char[] { '0' } );
						stringValue = stringValue.TrimEnd( new char[] { '.' } );
					}

					if( !validString( stringValue ) )
					{
						base.Text = string.Empty;
					}
					else
					{
						decimal decimalValue = decimal.Parse( stringValue );

						if( decimalValue == decimal.Zero )
						{
							base.Text = "0";
						}
						else
						{
							base.Text = decimalValue.ToString( "F" + m_decimalPlaces.ToString() );
						}
					}
				}
			}
		}


		/// <summary>
		/// The decimal value of the text displayed - can be different from the Text value if the TextMultiplier is not 1.
		/// </summary>
		[
		Browsable(true),
		CategoryAttribute("Custom"),
		Description("Current numeric value of the control")
		]
		public decimal Value
		{
			get
			{
				// try to cast as decimal and return, if unsuccessful return zero
				return decimal.Parse(base.Text) / m_valueMultiplier;
			}
			set
			{
				this.Text = (value * m_valueMultiplier).ToString();
			}
		}

		/// <summary>
		/// The value multiplier.
		/// 
		/// Values set/get via the Value property have the multiplier applied to them. i.e. for a multiplier of 100:
		///
		/// 12 passed in to the Value property would result in "1200" being displayed
		/// If "6.0000" was displayed, this value accessed via the Value property would return 0.06
		/// </summary>
		[
		Browsable(true),
		CategoryAttribute("Custom"),
		Description("Applied to the value property to result in the value displayed"),
		DefaultValue(1)
		]
		public decimal ValueMultiplier
		{
			get
			{
				return m_valueMultiplier;
			}
			set
			{
				// dont allow zero multiplier to be set
				m_valueMultiplier = value == 0 ? 1M : value;
			}
		}

		#endregion

		#region Public constructors

		/// <summary>
		/// Construct a new numeric text box
		/// </summary>
		public NumericTextBox()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
		}
		#endregion

		#region Event handlers
		/// <summary>
		/// A key has been pressed so check it's validity.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void NumericTextBox_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			e.Handled = !validCharacter( e.KeyChar );
		}


		/// <summary>
		/// The control is losing focus so remove any garbage left behind.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void NumericTextBox_Leave(object sender, System.EventArgs e)
		{
			if( this.Text == "-" || this.Text == "." || this.Text == "-." )
			{
				this.Text = string.Empty;
			}
		}
		#endregion

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.toolTipProvider = new System.Windows.Forms.ToolTip(this.components);
			// 
			// NumericTextBox
			// 
			this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.NumericTextBox_KeyPress);
			this.Leave += new System.EventHandler(this.NumericTextBox_Leave);

		}

		private System.ComponentModel.IContainer components;

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

		#endregion
	}
}
