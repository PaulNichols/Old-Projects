using System;
using System.Collections;

using HBOS.FS.AMP.UPD.Types.Lookups;
using HBOS.FS.Support.Tex;

namespace HBOS.FS.AMP.UPD.WinUI.Classes
{
	/// <summary>
	/// Abstract class for managing data for Status views.
	/// </summary>
	public abstract class StatusDataManager
	{
		/// <summary>
		/// Creates a new <see cref="StatusDataManager"/> instance.
		/// </summary>
		protected StatusDataManager()
		{
		}

		#region Properties

		private SimpleLookup fundGroupFilter;

		/// <summary>
		/// Gets or sets the fund group filter for the data.
		/// </summary>
		/// <value></value>
		public SimpleLookup FundGroupFilter
		{
			get {return fundGroupFilter;}
			set 
			{
				fundGroupFilter = value;
				fundGroupFilterIsDirty = true;
				onFiltersChanged(EventArgs.Empty);
			}
		}

		private IList data ;

		private StatusFilterCollection filters;
		
		/// <summary>
		/// Gets or sets the list of filters to apply to this dataset
		/// </summary>
		/// <value></value>
		public StatusFilterCollection Filters
		{
			get {return filters;}
			set
			{
				filters = value;
				wireUpFilterEvents();
				onFiltersChanged(EventArgs.Empty);
			}
		}

		#endregion

		#region Events

		/// <summary>
		/// Event raised when any of the filters are changed
		/// </summary>
		public event EventHandler FiltersChanged;

		/// <summary>
		/// Raises the FiltersChanged event.
		/// </summary>
		/// <param name="e">E.</param>
		protected virtual void onFiltersChanged(EventArgs e)
		{
			if (FiltersChanged != null)
			{
				FiltersChanged(this,e);
			}
		}

		#endregion

		#region Methods

		/// <summary>
		/// Gets the data.
		/// </summary>
		/// <returns></returns>
		public virtual IList GetData()
		{
			T.E();
			if (fundGroupFilterIsDirty)
			{
				fundGroupFilterIsDirty = false;
				data = loadDataFromSource();
			}

			IList filteredData;

			if (Filters == null || Filters.Count == 0)
			{
				filteredData = data;
			}
			else
			{
				ICloneable cloneableList = data as ICloneable;
			
				if (cloneableList != null)
				{
					filteredData = (IList)cloneableList.Clone();
					Filters.Apply(filteredData);
				}
				else
				{
					filteredData = data;
					fundGroupFilterIsDirty = true;
				}
			}

			T.X();
			return filteredData;
		}

		/// <summary>
		/// Forces a refresh of the data, next time GetData is called
		/// </summary>
		public void Refresh()
		{
			fundGroupFilterIsDirty = true;
			onFiltersChanged(EventArgs.Empty);
		}

		/// <summary>
		/// Loads the data from it's source (probably a Controller).
		/// </summary>
		/// <returns></returns>
		protected abstract IList loadDataFromSource();

		#endregion

		#region Privates

		private bool fundGroupFilterIsDirty = true;

		private void wireUpFilterEvents()
		{
			if (Filters != null)
			{
				Filters.ContentsChanged +=new EventHandler(Filters_ContentsChanged);
				foreach(IStatusFilter filter in Filters)
				{
					filter.AppliedChanged +=new EventHandler(filter_AppliedChanged);
				}
			}
		}

		#endregion

		#region Filters Event Handlers
		
		private void Filters_ContentsChanged(object sender, EventArgs e)
		{
			onFiltersChanged(e);
		}

		private void filter_AppliedChanged(object sender, EventArgs e)
		{
			onFiltersChanged(e);
		}

		#endregion
	}
}
