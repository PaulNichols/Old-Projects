using System;
using HBOS.FS.AMP.UPD.Types.Lookups;

namespace HBOS.FS.AMP.UPD.WinUI.Classes
{

	#region FundGroupLookup event

	/// <summary>
	/// Delegate for events where a SimpleLookup needs sending
	/// </summary>
	public delegate void SimpleLookupHandler(object sender, SimpleLookupEventArgs e);

	/// <summary>
	/// Event arguments for SimpleLookupHandler delegate
	/// </summary>
	public class SimpleLookupEventArgs: EventArgs
	{
		/// <summary>
		/// Creates a new <see cref="SimpleLookupEventArgs"/> instance.
		/// </summary>
		/// <param name="lookup">Lookup.</param>
		public SimpleLookupEventArgs(SimpleLookup lookup): base()
		{
			this.lookup = lookup;
		}

		private SimpleLookup lookup;

		/// <summary>
		/// Gets the lookup.
		/// </summary>
		/// <value></value>
		public SimpleLookup Lookup
		{
			get {return lookup;}
		}
	}

	#endregion
}
