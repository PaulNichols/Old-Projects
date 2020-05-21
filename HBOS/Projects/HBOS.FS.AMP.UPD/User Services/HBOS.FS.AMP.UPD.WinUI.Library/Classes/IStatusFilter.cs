using System;

namespace HBOS.FS.AMP.UPD.WinUI.Classes
{
	/// <summary>
	/// An interface to be supported by the filters of the status views
	/// </summary>
	public interface IStatusFilter
	{
		/// <summary>
		/// Gets or sets a value indicating whether this filter is applied. Raise the 
		/// AppliedChanged event on a set.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if applied; otherwise, <c>false</c>.
		/// </value>
		bool Applied { get; set; }

//		/// <summary>
//		/// Apply the filter to the subject
//		/// </summary>
//		/// <param name="subject">Subject.</param>
		//	void Apply(IList subject);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
		bool FilterOut(object item);

		/// <summary>
		/// Gets a value indicating whether this filter is shown in the GUI
		/// </summary>
		/// <value>
		/// 	<c>true</c> if [display in GUI]; otherwise, <c>false</c>.
		/// </value>
		bool DisplayInGui { get; }

		/// <summary>
		/// Event to raise when the Applied property is changed
		/// </summary>
		event EventHandler AppliedChanged;
	}
}