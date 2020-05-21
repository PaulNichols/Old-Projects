using System;
using System.Collections;

namespace HBOS.FS.AMP.UPD.WinUI.Classes
{
	#region RequestObject

	/// <summary>
	/// RequestObjectHandler delegate
	/// </summary>
	public delegate void RequestObjectHandler(object sender, RequestObjectArgs e);

	/// <summary>
	/// RequestObjectArgs class
	/// </summary>
	public class RequestObjectArgs: EventArgs
	{
		/// <summary>
		/// Creates a new <see cref="RequestObjectArgs"/> instance.
		/// </summary>
		public RequestObjectArgs(object key): base()
		{
			this.key = key;
		}

		private object requestedObject;

		/// <summary>
		/// Gets or sets the requested object.
		/// </summary>
		/// <value></value>
		public object RequestedObject
		{
			get {return requestedObject;}
			set {requestedObject = value;}
		}

		private object key;

		/// <summary>
		/// The key for the requested object
		/// </summary>
		/// <value></value>
		public object Key
		{
			get {return key;}
		}

	}

	#endregion

	#region RequestList

	/// <summary>
	/// RequestListHandler delegate
	/// </summary>
	public delegate void RequestListHandler(object sender, RequestListArgs e);

	/// <summary>
	/// RequestListArgs class
	/// </summary>
	public class RequestListArgs: EventArgs
	{
		/// <summary>
		/// Creates a new <see cref="RequestListArgs"/> instance.
		/// </summary>
		public RequestListArgs(): base()
		{
		}

		/// <summary>
		/// Creates a new <see cref="RequestListArgs"/> instance.
		/// </summary>
		/// <param name="key">Key to lookup.</param>
		public RequestListArgs(object key): this()
		{
			this.key = key;
		}

		private IList requestedList;

		/// <summary>
		/// Gets or sets the requested list.
		/// </summary>
		/// <value></value>
		public IList RequestedList
		{
			get {return requestedList;}
			set {requestedList = value;}
		}

		private object key;

		/// <summary>
		/// Gets the key to lookup the list for.
		/// </summary>
		/// <value></value>
		public object Key
		{
			get {return key;}
		}

	}

	#endregion
}
