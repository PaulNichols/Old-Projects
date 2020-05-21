using System;
using HBOS.FS.AMP.UPD.Types.Funds;

namespace HBOS.FS.AMP.UPD.WinUI.Classes
{
	/// <summary>
	/// 
	/// </summary>
	public class PropertyFilter : IStatusFilter
	{
		private readonly bool displayInGui;
		private readonly NameValuePair[] propertyNames;
		private readonly string guiText;
		private bool applied;

		/// <summary>
		/// 
		/// </summary>
		public class NameValuePair
		{
			private readonly string propertyName;
			private readonly object value;

			/// <summary>
			/// Gets the name of the property.
			/// </summary>
			/// <value></value>
			internal string PropertyName
			{
				get { return propertyName; }
			}

			/// <summary>
			/// Gets a value indicating whether this <see cref="NameValuePair"/> is value.
			/// </summary>
			/// <value>
			/// 	<c>true</c> if value; otherwise, <c>false</c>.
			/// </value>
			internal object Value
			{
				get { return value; }
			}

			/// <summary>
			/// 
			/// </summary>
			/// <param name="propertyName"></param>
			/// <param name="value"></param>
			public NameValuePair(string propertyName, object value)
			{
				this.propertyName = propertyName;
				this.value = value;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="booleanPropertyNames"></param>
		/// <param name="displayInGUI"></param>
		/// <param name="guiText"></param>
		public PropertyFilter( NameValuePair[] booleanPropertyNames, bool displayInGUI, string guiText)
		{
			this.propertyNames = booleanPropertyNames;
			this.displayInGui = displayInGUI;
			this.guiText = guiText;
		}

		#region IStatusFilter Members
		/// <summary>
		/// Gets or sets a value indicating whether this filter is applied. Raise the 
		/// AppliedChanged event on a set.
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

		private void onAppliedChanged(EventArgs e)
		{
			if (this.AppliedChanged!=null)
				this.AppliedChanged(this,e);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
		public bool FilterOut(object item)
		{
			bool returnValue=false;
			if (item != null && this.propertyNames.Length>0)
			{
				foreach (NameValuePair propertyToCheck  in this.propertyNames)
				{
					try
					{
						object actualValue=item.GetType().GetProperty(propertyToCheck.PropertyName).GetValue(item,null);
						if ( ! actualValue.Equals(propertyToCheck.Value))
							{
								returnValue=true;
								break;
							}
					}
					catch 
					{
						throw;
					}
				
				}
			}
			return returnValue;
		}

		/// <summary>
		/// Gets a value indicating whether this filter is shown in the GUI
		/// </summary>
		/// <value>
		/// 	<c>true</c> if [display in GUI]; otherwise, <c>false</c>.
		/// </value>
		public bool DisplayInGui
		{
			get { return this.displayInGui; }
		}

		/// <summary>
		/// Overridden to display properly in gui
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return guiText;
		}

		/// <summary>
		/// Event to raise when the Applied property is changed
		/// </summary>
		public event EventHandler AppliedChanged;
		
		#endregion
	}
}