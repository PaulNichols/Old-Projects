//using HBOS.FS.AMP.UPD.DataAccess;
using System;
using System.Data.SqlClient;
using HBOS.FS.AMP.UPD.Exceptions;
using HBOS.FS.AMP.UPD.Types.Lookups;
using HBOS.FS.Support.Tex;

namespace HBOS.FS.AMP.UPD.Persistence
{
	/// <summary>
	/// The class to use for persisting or retrieving Company objects and
	/// CompanyCollection objects.
	/// </summary>
	public class CompanyLookupPersister : EntityPersister
	{
		#region Constructors

		/// <summary>
		/// Constructor used to initialise the ConnectionString property.
		/// </summary>
		/// <param name="connectionString"></param>
		public CompanyLookupPersister(string connectionString) : base(connectionString)
		{
		}

		#endregion

		#region Methods

		/// <summary>
		/// Gets all the companies available to the specified user
		/// </summary>
		/// <returns>A strongly-typed collection of the user's companies</returns>
		/// <exception cref="DatabaseException">Unable to load companies</exception>
		/// <exception cref="InvalidSqlParameterException">Attempt to call stored proc with an invalid parameter</exception>
		/// <exception cref="SchemaMismatchException">Column missing from data returned by stored procedure</exception>
		/// <exception cref="ArgumentException">Incorrect type loaded from base class</exception>
		public SimpleStringLookupCollection LoadCompanies()
		{
			T.E();
			const string loadSp = "usp_HBOSCompanyListForUser";
			// Create the companies collection.
			SimpleStringLookupCollection companies = new SimpleStringLookupCollection();

			try
			{
				//todo - ok to pass null here?
				this.LoadEntityCollection(loadSp,companies);

			}
			catch (SqlException ex)
			{
				ThrowDBException(ex, loadSp);
			}
			finally
			{
				T.X();
			}

			return companies;
		}


		/// <summary>
		/// Creates the entity.
		/// </summary>
		/// <param name="safeReader">Safe reader.</param>
		/// <returns></returns>
		protected override object CreateEntity(SafeDataReader safeReader)
		{
			return new SimpleStringLookup(safeReader.GetString("companyCode"), safeReader.GetString("companyName"));
		}

		#endregion
	}
}
