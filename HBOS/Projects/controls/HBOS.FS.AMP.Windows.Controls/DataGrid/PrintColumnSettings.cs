using System;
using System.Drawing;

namespace HBOS.FS.AMP.Windows.Controls
{
	/// <summary>
	/// PrintSettings - settings for the datagrid to use when printing
	/// </summary>
	public class PrintColumnSettings
	{
		#region Private variables

		private string m_field  = string.Empty;
		private int m_width = 0;
		private StringAlignment m_alignment = StringAlignment.Near;
		private Font m_headerFont = new System.Drawing.Font("Arial", 10, FontStyle.Bold);
		private Font m_detailFont = new System.Drawing.Font("Arial", 8, FontStyle.Regular);
		private string m_headerText = String.Empty;
		private bool m_defaultFonts = true;

		#endregion

		#region Constructor

		/// <summary>
		/// Constructor for PrintSettings taking all the arguments.
		/// </summary>
		/// <param name="field"></param>
		/// <param name="width"></param>
		/// <param name="textAlignment"></param>
		/// <param name="headerText"></param>
		/// <param name="headerFont"></param>
		/// <param name="detailFont"></param>
		public PrintColumnSettings( string field , int width , StringAlignment textAlignment , string headerText , Font headerFont , Font detailFont )
		{
			this.m_field = field;
			this.m_width = width;
			this.m_alignment = textAlignment;
			this.m_headerText = headerText;
			this.m_headerFont = headerFont;
			this.m_detailFont = detailFont;
			this.m_defaultFonts = false;
		}

		/// <summary>
		/// Constructor for Print Settings which defaults the Font settings.
		/// </summary>
		/// <param name="field"></param>
		/// <param name="width"></param>
		/// <param name="textAlignment"></param>
		/// <param name="headerText"></param>
		public PrintColumnSettings( string field , int width , StringAlignment textAlignment , string headerText )
		{
			this.m_field = field;
			this.m_width = width;
			this.m_alignment = textAlignment;
			this.m_headerText = headerText;
			this.m_defaultFonts = true;
		}

		#endregion

		#region Properties

		/// <summary>
		/// Field name
		/// </summary>
		public string Field
		{
			get
			{
				return m_field;
			}
			set
			{
				m_field = value;
			}
		}

		/// <summary>
		/// Width
		/// </summary>
		public int Width
		{
			get
			{
				return m_width;
			}
			set
			{
				m_width = value;
			}
		}

		/// <summary>
		/// Text alignment
		/// </summary>
		public StringAlignment Alignment
		{
			get
			{
				return m_alignment;
			}
			set
			{
				m_alignment = value;
			}
		}

		/// <summary>
		/// Header Text
		/// </summary>
		public string HeaderText
		{
			get
			{
				return m_headerText;
			}
			set
			{
				m_headerText = value;
			}
		}

		/// <summary>
		/// Header Font
		/// </summary>
		public Font HeaderFont
		{
			get
			{
				return m_headerFont;
			}
			set
			{
				m_headerFont = value;
			}
		}

		/// <summary>
		/// Detail Font
		/// </summary>
		public Font DetailFont
		{
			get
			{
				return m_detailFont;
			}
			set
			{
				m_detailFont = value;
			}
		}

		/// <summary>
		/// Are we use Grid default fonts
		/// </summary>
		private bool UseDefaultFonts
		{
			get
			{
				return m_defaultFonts;
			}
			set
			{
				m_defaultFonts = value;
			}
		}

		#endregion
	}
}
