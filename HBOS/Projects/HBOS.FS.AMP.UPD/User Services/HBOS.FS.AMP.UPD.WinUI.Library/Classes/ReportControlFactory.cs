using System.Windows.Forms;
using HBOS.FS.AMP.UPD.WinUI.UserControls;

namespace HBOS.FS.AMP.UPD.WinUI.Classes
{
	/// <summary>
	/// Factory methods to create report controls.
	/// </summary>
	public class ReportControlFactory
	{
		/// <summary>
		/// Type of report control to create
		/// </summary>
		public enum ControlTypeEnum
		{
			/// <summary>
			/// 
			/// </summary>
			Historic,
			/// <summary>
			/// Fund Drift Report
			/// </summary>
			FundDrift,
			/// <summary>
			/// ,Price comparision report (cr16)
			/// </summary>
			PriceComparision
		}

		/// <summary>
		/// Creates a new <see cref="ReportControlFactory"/> instance.
		/// </summary>
		public ReportControlFactory()
		{
			// No action
		}

		/// <summary>
		/// Loads the report control.
		/// </summary>
		/// <param name="ControlType">The report control type to load</param>
		/// <returns>A new instance of the requested control</returns>
		public UserControl LoadControl(ControlTypeEnum ControlType)
		{
			UserControl requestedControl;

			switch (ControlType)
			{
				case ControlTypeEnum.Historic:
					requestedControl = new HistoricPredictedPricesReportControl();
					break;
				case ControlTypeEnum.FundDrift:
					requestedControl = new FundDriftReportControl();
					break;
				case ControlTypeEnum.PriceComparision:
					requestedControl = new PriceComparisionControl();
					break;
				default:
					requestedControl = new UserControl();
					break;
			}

			return requestedControl;
		}
	}
}