using System;
using HBOS.FS.AMP.Windows.Controls;
using System.Reflection;
using System.Text;
using HBOS.FS.AMP.UPD.Types.Funds;

namespace HBOS.FS.AMP.UPD.WinUI.Classes
{
	/// <summary>
	/// Summary description for FundPropertyFormatter.
	/// </summary>
	public class FundPropertyFormatter : IPropertyDisplayFormatter
	{
		/// <summary>
		/// 
		/// </summary>
		public FundPropertyFormatter()
		{
		}

		/// <summary>
		/// Provides custom formatting for fund properties. In particular to enable display of 'Unavailable'
		/// </summary>
		/// <param name="propertyToDisplay">The property being displayed</param>
		/// <param name="propertyValue">The value of the property being displayed</param>
		/// <param name="customType">The fund business object (type) that contains other data that may affect formatting (such as a Set flag)</param>
		/// <returns>The formatted value for display purposes</returns>
		public string FormatProperty (PropertyInfo propertyToDisplay, Object propertyValue, Object customType)
		{			
			Fund fund = ((Fund )customType);
			switch (propertyToDisplay.Name)
			{
				case ("Price") :
					return DisplayFormat.Decimal(fund.Price, fund.PriceSet, "#,##0.00");

				case ("PreviousPrice") :
					return DisplayFormat.Decimal(fund.PreviousPrice, fund.PreviousPriceSet);

				case ("FundStatus") :
					return fund.FundStatus.ToString();

				case ("PredictedPrice") :
					return DisplayFormat.Decimal(fund.PredictedPrice, fund.PredictedPriceSet, "#,##0.00");

				case ("PriceMovementPercent") :
					return DisplayFormat.Percent(fund.PriceMovementPercent, (fund.PriceSet && fund.PreviousPriceSet));

				case ("PredictedPriceMovementPercent") :
					return DisplayFormat.Percent(fund.PredictedPriceMovementPercent, (fund.PredictedPriceSet && fund.PreviousPriceSet));

				case ("PriceMovementVariance"):
					return DisplayFormat.Percent(fund.PriceMovementVariance, fund.PriceSet && fund.PreviousPriceSet && fund.PredictedPriceSet);

				case ("AssetUnitPrice") :
					return DisplayFormat.Decimal(fund.AssetUnitPrice, fund.AssetUnitPriceSet , "N4" );

				case ("AssetMovement") :
					return DisplayFormat.Percent(fund.AssetMovement, fund.AssetMovementSet);

				case ("PredictedAssetMovement") :
					return DisplayFormat.Percent(fund.PredictedAssetMovement, fund.PredictedAssetMovementSet);

				case ("AssetMovementVariance") :
					return DisplayFormat.Percent(fund.AssetMovementVariance, fund.AssetMovementVarianceSet);

				case ("WithinAssetMovementTolerance") :
					return (fund.WithinAssetMovementTolerance ? "Y" : "N");

				case ("PriceMovementTolerance") :
					return priceMovementToleranceDisplay (fund);

				case ("PriceMovementRoundedTolerance") :
					return priceMovementRoundedTolerance (fund);

				case ("PriceMovementLowerTolerance") :
					return (fund.WithinPriceLowerTolerance ? "Y" : "N");
			
				case ("PriceMovementUpperTolerance") :
					return (fund.WithinPriceUpperTolerance ? "Y" : "N");

				case ("PriceMovementDirectionTolerance") :
					return (fund.WithinPriceDirectionTolerance ? "Y" : "N");

				default:
					return propertyValue.ToString();
			}
		}

		/// <summary>
		/// Used to determine if a given property is to be custom formatted
		/// </summary>
		/// <param name="propertyName">The name of the property in question</param>
		/// <returns>boolean flag indicating whether this property is custom formatted</returns>
		public bool IsCustomFormatted (string propertyName)
		{

			switch (propertyName)
			{
				case ("Price") :
				case ("PreviousPrice") :
				case ("FundStatus") :
				case ("PredictedPrice") :
				case ("PriceMovementPercent") :
				case ("PredictedPriceMovementPercent") :
				case ("PriceMovementVariance"):
				case ("AssetUnitPrice") :
				case ("AssetMovement") :
				case ("PredictedAssetMovement") :
				case ("AssetMovementVariance") :
				case ("WithinAssetMovementTolerance") :
				case ("PriceMovementTolerance") :
				case ("PriceMovementRoundedTolerance") :
				case ("PriceMovementLowerTolerance") :
				case ("PriceMovementUpperTolerance") :
				case ("PriceMovementDirectionTolerance") :
					return true;
				default:
					return false;
			}
		}

		/// <summary>
		/// Display friendly string giving result of price movement tolerance checks.
		/// </summary>
		/// <param name="fund"></param>
		/// <returns></returns>
		private string priceMovementToleranceDisplay(Fund fund)
		{
			StringBuilder builder = new StringBuilder();

			if (fund.WithinPriceLowerTolerance && fund.WithinPriceUpperTolerance
				&& fund.WithinPriceDirectionTolerance)
			{
				builder.Append("Y");
			}
			else
			{
				builder.Append("UT - ");
				builder.Append(fund.WithinPriceUpperTolerance ? "Y" : "N");
				builder.Append("  LT - ");
				builder.Append(fund.WithinPriceLowerTolerance ? "Y" : "N");
				builder.Append("  PI - ");
				builder.Append(fund.WithinPriceDirectionTolerance ? "Y" : "N");
			}

			return builder.ToString();
		}

		/// <summary>
		/// Display friendly string giving result of price movement tolerance checks including rounding.
		/// </summary>
		/// <param name="fund"></param>
		/// <returns></returns>
		private string priceMovementRoundedTolerance (Fund fund)
		{
			StringBuilder builder = new StringBuilder();

			if (fund.WithinRoundedPriceLowerTolerance && fund.WithinRoundedPriceUpperTolerance
				&& fund.WithinPriceDirectionTolerance)
			{
				builder.Append("Y");
			}
			else
			{
				builder.Append("UT - ");
				builder.Append(fund.WithinRoundedPriceUpperTolerance ? "Y" : "N");
				builder.Append("  LT - ");
				builder.Append(fund.WithinRoundedPriceLowerTolerance ? "Y" : "N");
				builder.Append("  PI - ");
				builder.Append(fund.WithinPriceDirectionTolerance ? "Y" : "N");
			}

			return builder.ToString();
		}

	}
}
