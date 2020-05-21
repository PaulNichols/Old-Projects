using System;

namespace HBOS.FS.AMP.Windows.Controls
{
	/// <summary>
	/// TextValidator - various regular expressions which are useful for validating text entry.
	/// </summary>
	/// <remarks>Used by the <see cref="NumericTextBox"/> to ensure only valid data is entered</remarks>
	public class TextValidator
	{
		#region "Regular Expressions"
		//Set of predefined Regular Expressions for checking and reformatting values entered into the system.

		/// <summary>
		/// A Regular Expression which requires a mandatory UK PostCode. This contains p1 and p2 regular expression parameters.
		/// </summary>
		public const string PostCodeRegEx = "^(?<p1>[a-zA-Z]{1,2}[0-9][0-9A-Za-z]{0,1}) {0,1}(?<p2>[0-9][A-Za-z]{2})$";
		/// <summary>
		/// A Regular Expression which requires an optional UK PostCode. This contains p1 and p2 regular expression parameters.
		/// </summary>
		public const string PostCodeRegExOpt = "(^(?<p1>[a-zA-Z]{1,2}[0-9][0-9A-Za-z]{0,1}) {0,1}(?<p2>[0-9][A-Za-z]{2})$)|^$";
		/// <summary>
		/// The corresponding parameter expression for reformatting the postcodes from above.
		/// </summary>
		public const string PostCodeRegReformat = "${p1} ${p2}";

		/// <summary>
		/// A Regular Expression which requires mandatory number.
		/// </summary>
		public const string NumericOnlyRegEx = "^\\d+$";

		/// <summary>
		/// A Regular Expression which requires optional number.
		/// </summary>
		public const string NumericOnlyRegExOpt = "^(^\\d+$)|^$";

		/// <summary>
		/// A regular expression for positive integers
		/// </summary>
		public const string PositiveIntegerRegEx = @"^\d*$";

		/// <summary>
		/// A regular expression for integers which allows negatives.
		/// </summary>
		public const string NegativeIntegerRegEx = @"^-?\d*$";

		/// <summary>
		/// A regular expression for not blank.
		/// </summary>
		public const string NotBlankEx = @"^.+$";

		/// <summary>
		/// A regular expression for is blank.
		/// </summary>
		public const string IsBlankEx = @"^$";

		/// <summary>
		/// A regular expression for positive real numbers.
		/// </summary>
		/// <param name="decimalPlaces">decimal places</param>
		/// <returns></returns>
		public static string PositiveRealRegEx( int decimalPlaces )
		{
			return @"^\d*\.{0,1}\d{0,DP}$".Replace( "DP", decimalPlaces.ToString() );
		}

		/// <summary>
		/// A regular expression for real numbers which allows negatives.
		/// </summary>
		/// <param name="decimalPlaces">decimal places</param>
		/// <returns></returns>
		public static string NegativeRealRegEx( int decimalPlaces )
		{
			return @"^-?\d*\.{0,1}\d{0,DP}$".Replace( "DP", decimalPlaces.ToString() );
		}

		#endregion

	}
}
