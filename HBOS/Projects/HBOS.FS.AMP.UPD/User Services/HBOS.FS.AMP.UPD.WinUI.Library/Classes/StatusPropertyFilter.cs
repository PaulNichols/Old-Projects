using System;
using System.Reflection;

namespace HBOS.FS.AMP.UPD.WinUI.Classes
{
	/// <summary>
	/// Filter based on a property name and supplied value. Uses reflection to determine if
	/// an object should be excluded by the filter
	/// </summary>
	public class StatusPropertyFilter : IStatusFilter
	{
		/// <summary>
		/// Creates a new <see cref="StatusPropertyFilter"/> instance.
		/// </summary>
		/// <param name="propertyName">The property name to filter by</param>
		/// <param name="filterValue">The expected value in the property named by property name</param>
		/// <param name="displayInGui">Indicates if this is a Gui filter</param>
		/// <param name="guiText">Text used in guil to describe the filter</param>
		public StatusPropertyFilter(string propertyName, object filterValue, bool displayInGui, string guiText)
		{
			this.propertyName = propertyName;
			this.filterValue = filterValue;
			this.displayInGui = displayInGui;
			this.guiText = guiText;
		}

		private string propertyName;
		private object filterValue;

		/// <summary>
		/// Event raised when Applied property changes
		/// </summary>
		public event EventHandler AppliedChanged;

		private void onAppliedChanged(EventArgs e)
		{
			if (AppliedChanged != null)
				AppliedChanged(this, e);
		}

		private bool applied;

		/// <summary>
		/// Gets or sets a value indicating whether this filter is applied.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if applied; otherwise, <c>false</c>.
		/// </value>
		public bool Applied
		{
			get { return applied; }
			set
			{
				if (applied != value)
				{
					applied = value;
					onAppliedChanged(EventArgs.Empty);
				}
			}
		}

//		/// <summary>
//		/// Applies this filter to the IList in subject
//		/// </summary>
//		/// <param name="subject">Subject.</param>
//		public void Apply(IList subject)
//		{
//			for(int idx=0; idx < subject.Count; idx++)
//			{
//				object obj = subject[idx];
//				Type typ = obj.GetType();
//				PropertyInfo pi = typ.GetProperty(this.propertyName);
//				if (pi != null)
//				{
//					object propertyValue = pi.GetValue(obj,null);
//					if ((propertyValue == null && this.filterValue != null) ||
//						(propertyValue != null && !propertyValue.Equals(filterValue)))
//					{
//						subject.RemoveAt(idx);
//						idx--;
//					}
//				}
//			}
//		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="Item"></param>
		/// <returns></returns>
		public bool FilterOut(object Item)
		{
			Type typ = Item.GetType();
			PropertyInfo pi = typ.GetProperty(this.propertyName);
			bool returnValue = false;
			if (pi != null)
			{
				object propertyValue = pi.GetValue(Item, null);
				returnValue = (((propertyValue == null && this.filterValue != null) ||
					(propertyValue != null && !propertyValue.Equals(filterValue))));
			}
			return returnValue;
		}

		private readonly bool displayInGui;

		/// <summary>
		/// Gets a value indicating whether this filter is shown in the GUI
		/// </summary>
		/// <value>
		/// 	<c>true</c> if [display in GUI]; otherwise, <c>false</c>.
		/// </value>
		public bool DisplayInGui
		{
			get { return displayInGui; }
		}

		private readonly string guiText;

		/// <summary>
		/// Overridden to correct display in gui
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return guiText;
		}
	}
}