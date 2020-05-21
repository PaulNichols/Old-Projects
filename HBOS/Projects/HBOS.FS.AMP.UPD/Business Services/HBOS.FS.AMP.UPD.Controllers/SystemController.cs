using System;
using HBOS.FS.AMP.UPD.Persistence;
using HBOS.FS.Support.Tex;

namespace HBOS.FS.AMP.UPD.Controllers
{
	/// <summary>
	/// Summary description for UPDIO.
	/// </summary>
	public class SystemController
	{
		/// <summary>
		/// Creates a new <see cref="SystemController"/> instance.
		/// </summary>
		private SystemController()
		{
		}

		/// <summary>
		/// Gets the server date time.
		/// </summary>
		/// <returns></returns>
		public static DateTime GetServerDateTime(string connectionString)
		{
			T.E();
			try
			{
				SystemPersister dataPersister = new SystemPersister(connectionString);
				return dataPersister.GetServerDateTime();
			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// works out next working day from today
		/// </summary>
		/// <returns></returns>
		/// 	private DateTime NextWorkingDay
		public  static DateTime GetNextWorkingDay(string connectionString,DateTime fromDate)
		{
			T.E();
			try
			{
				if (!m_fromDate.Equals(fromDate) || m_nextWorkingDay.Ticks==0)
				{
					try
					{
						SystemPersister systemPersister = new SystemPersister(connectionString);
						m_nextWorkingDay =systemPersister.GetNextWorkingDay(fromDate);
					}
					catch
					{
						throw;
					}
				}

				m_fromDate=fromDate;
				return m_nextWorkingDay;
				//				Hashtable holidays = fundPersister.LoadHolidays();
				//			
				//				DateTime nextWorkingDay = DateTime.Today.AddDays (1);
				//				
				//				while (holidays.ContainsKey (nextWorkingDay) ||
				//					nextWorkingDay.DayOfWeek == DayOfWeek.Saturday ||
				//					nextWorkingDay.DayOfWeek == DayOfWeek.Sunday)
				//				{
				//					nextWorkingDay=nextWorkingDay.AddDays(1);
				//				}
				//				return nextWorkingDay;

			}
			finally
			{
				T.E();
			}

		}
		private static DateTime m_nextWorkingDay;
		private static DateTime m_fromDate;
	}
}