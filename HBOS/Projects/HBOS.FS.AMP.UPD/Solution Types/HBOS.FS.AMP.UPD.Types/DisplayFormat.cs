using System;

namespace HBOS.FS.AMP.UPD.Types
{
	/// <summary>
	/// Summary description for DisplayFormat.
	/// </summary>
	public abstract class DisplayFormat
	{
		#region Constructors

		/// <summary>
		/// Creates a new <see cref="DisplayFormat"/> instance.
		/// </summary>
		public DisplayFormat()
		{
		}

		#endregion

		#region Constants

		private const string m_unavailableText = "Unavailable";

		#endregion

		#region Static public methods

		/// <summary>
		/// Convert currency fields to display friendly fields, catering for nulls.
		/// </summary>
		/// <param name="amount">The currency value to display.</param>
		/// <param name="amountSet">Flag indicating whether the value has been set or not.</param>
		/// <returns>The amount formatted to a currency display string for the current culture.</returns>
		public static string Currency(decimal amount, bool amountSet)
		{
			if (amountSet)
			{
				return amount.ToString("c", System.Globalization.CultureInfo.CurrentCulture);
			}
			else
			{
				return m_unavailableText;
			}
		}

		/// <summary>
		/// Convert decimal fields to display friendly fields, catering for nulls.
		/// </summary>
		/// <param name="amount">The decimal value to display.</param>
		/// <param name="amountSet">Flag indicating whether the value has been set or not.</param>
		/// <returns>The amount if it is set or the unavailable text if it isn't.</returns>
		public static string Decimal(decimal amount, bool amountSet)
		{
			return amountSet ? amount.ToString() : m_unavailableText;
		}
		
		/// <summary>
		/// Convert decimal fields to display friendly fields, catering for nulls.
		/// Allows scale and precision to be set. e.g. 123.45 has precision of 5 and scale of 2
		/// </summary>
		/// <param name="amount">The decimal value to display.</param>
		/// <param name="amountSet">Flag indicating whether the value has been set or not.</param>
		/// <param name="formatString">A format string </param>
		/// <returns>The amount if it is set or the unavailable text if it isn't.</returns>
		public static string Decimal(decimal amount, bool amountSet, string formatString)
		{            					
			return amountSet ? amount.ToString(formatString) : m_unavailableText;
		}

		/// <summary>
		/// Convert fractional fields to display friendly percent fields, catering for nulls.
		/// </summary>
		/// <param name="amount">The decimal value to display.</param>
		/// <param name="amountSet">Flag indicating whether the value has been set or not.</param>
		/// <returns>The percent value if it is set or the unavailable text if it isn't.</returns>
		public static string Percent(decimal amount, bool amountSet)
		{
			if (amountSet)
			{
				return amount.ToString("p4");
			}
			else
			{
				return m_unavailableText;
			}
		}

		/// <summary>
		/// Formats date to short date or displays 'Unavailable'
		/// </summary>
		/// <param name="dt"></param>
		/// <param name="dateSet"></param>
		/// <returns></returns>
		public static string ShortDate(DateTime dt, bool dateSet)
		{
			if (dateSet || dt == DateTime.MinValue || dt == DateTime.MaxValue)
			{
				return m_unavailableText;
			}
			else
			{
				//to incorporate locale / culture?
				return dt.ToShortDateString();
			}
		}

		#endregion
	}
}
