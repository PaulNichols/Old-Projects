using System;

using HBOS.FS.AMP.UPD.Types.Funds;
using HBOS.FS.AMP.UPD.WinUI.UserControls;
using HBOS.FS.Support.Tex;

namespace HBOS.FS.AMP.UPD.WinUI.Classes
{
	/// <summary>
	/// Filter that checks if the CurrentFundStatus is within an acceptable range
	/// </summary>
	public class FundStatusFilter: IStatusFilter
	{
		/// <summary>
		/// Creates a new <see cref="FundStatusFilter"/> instance.
		/// </summary>
		/// <param name="minStatus">The minimum allowable status.</param>
		/// <param name="maxStatus">The maximum allowable status.</param>
		/// <param name="displayInGui">Display in GUI.</param>
		/// <param name="guiText">Caption to use in gui</param>
		public FundStatusFilter(Fund.FundStatusType minStatus,Fund.FundStatusType maxStatus,bool displayInGui,string guiText)
		{
			T.E();
			this.minStatus    = minStatus;
			this.maxStatus    = maxStatus;
			this.displayInGui = displayInGui;
			this.guiText      = guiText;
			T.X();
		}

		#region IStatusFilter Members

		private bool applied ;
		/// <summary>
		/// Gets or sets a value indicating whether this filter is applied.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if applied; otherwise, <c>false</c>.
		/// </value>
		public bool Applied
		{
			get { return applied;}
			set
			{
				applied=value;
				onAppliedChanged(EventArgs.Empty);
			}
		}

//		/// <summary>
//		/// Applies this filter to the subject
//		/// </summary>
//		/// <param name="subject">Subject.</param>
//		public void Apply(System.Collections.IList subject)
//		{
//			T.E();
//
//			for(int idx=0; idx<subject.Count; idx++)
//			{
//				Fund fund = subject[idx] as Fund;
//				if (fund != null)
//				{
//					if (fund.FundStatus < this.minStatus || fund.FundStatus > this.maxStatus)
//					{
//						subject.RemoveAt(idx);
//						idx--;
//					}
//				}
//			}
//
//			T.X();
//		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="Item"></param>
		/// <returns></returns>
		public bool FilterOut(object Item)
		{
            // Amended by MAW 15/09/05 (Issue: UA133)
            // The fund object was being checked for but the fund object was now wrapped up inside a decorator object
			FundDecorator fund = Item as FundDecorator;
			bool returnValue=false;
			if (fund != null)
			{
				returnValue= ((fund.Fund.FundStatus < this.minStatus || fund.Fund.FundStatus > this.maxStatus));
			}
			return returnValue;
		}

		private bool displayInGui;
		
		/// <summary>
		/// Gets a value indicating whether this filter should be displayed in the gui
		/// </summary>
		/// <value>
		/// 	<c>true</c> if [display in GUI]; otherwise, <c>false</c>.
		/// </value>
		public bool DisplayInGui
		{
			get	{return displayInGui;}
		}

		/// <summary>
		/// Event raised when the Applied property is changed
		/// </summary>
		public event System.EventHandler AppliedChanged;

		private void onAppliedChanged(EventArgs e)
		{
			if (this.AppliedChanged!=null)
				this.AppliedChanged(this,e);
		}

		private Fund.FundStatusType minStatus;
		private Fund.FundStatusType maxStatus;

		string guiText;

		/// <summary>
		/// Overridden to display properly in gui
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return guiText;
		}

		#endregion
	}
}

