using System;
using System.Data;
using System.Data.SqlClient;
using HBOS.FS.AMP.Entities;
using HBOS.FS.AMP.UPD.Exceptions;
using HBOS.FS.AMP.UPD.Types.AssetFunds;
using HBOS.FS.Support.Tex;
using Microsoft.ApplicationBlocks.Data;

namespace HBOS.FS.AMP.UPD.Persistence
{
	/// <summary>
	/// Summary description for PriceFileStaticDataPersister.
	/// </summary>
	public class PriceFileStaticDataPersister : EntityPersister
	{
		#region Constructor

		/// <summary>
		/// Creates a new <see cref="PriceFileStaticDataPersister"/> instance.
		/// </summary>
		/// <param name="connectionString">Database connection string.</param>
		public PriceFileStaticDataPersister(string connectionString) : base(connectionString)
		{
		}

		#endregion

		#region Public methods

		/// <summary>
		/// returns the number of files related to the company for the given file name.
		/// </summary>
		/// <param name="fileName">Name of the file.</param>
		/// <param name="companyCode">Company code.</param>
		/// /// <param name="extension">File extension</param>
		/// <returns>
		/// 	<c>true</c> if [is file related to company] [the specified fileName]; otherwise, <c>false</c>.
		/// </returns>
		public int NumberOfRelatedFiles(string fileName, string companyCode,string extension)
		{
			T.E();
			int count=0;
			const string loadSp = "usp_ImportIsFileRelatedToCompany";
			// Build the parameters collection
			SqlParameter[] parameters = new SqlParameter[3];
			try
			{
				using (SqlConnection connection = new SqlConnection(this.ConnectionString))
				{
					using (SqlCommand command = new SqlCommand())
					{
						// Set up the parameters.
						parameters[0] = new SqlParameter("@fileName", SqlDbType.VarChar,50);
						parameters[0].Value = fileName;
						command.Parameters.Add(parameters[0]);

						parameters[1] = new SqlParameter("@companyCode", SqlDbType.VarChar,10);
						parameters[1].Value = companyCode;
						command.Parameters.Add(parameters[1]);

						parameters[2] = new SqlParameter("@extension", SqlDbType.Char,3);
						parameters[2].Value = extension;
						command.Parameters.Add(parameters[2]);

						command.Connection = connection;
						command.CommandText = loadSp;
						command.CommandType = CommandType.StoredProcedure;
						connection.Open();
						count = Convert.ToInt32(command.ExecuteScalar());
						connection.Close();
					}
				}

			}
			catch (SqlException ex)
			{
				ThrowDBException(ex, loadSp);
			}
			finally
			{
				T.X();
			}
			return count;
		}

		/// <summary>
		/// Loads the price files.
		/// </summary>
		/// <param name="companyCode">Code.</param>
		/// <returns></returns>
		public PriceFileCollection LoadPriceFiles(string companyCode)
		{
			T.E();
			// Create the countries collection.
			PriceFileCollection files = new PriceFileCollection();

			SqlParameter[] spParameters = new SqlParameter[1];

			// Set up the stored procedure parameters.
			spParameters[0] = new SqlParameter("@CompanyCode", SqlDbType.VarChar,10);
			spParameters[0].Value = companyCode;
			try
			{
				this.LoadEntityCollection ("usp_PriceFileList", spParameters,files);
			}
			finally
			{
				T.X();
			}
			
			return files;
		}

		/// <summary>
		/// Loads the static data.
		/// </summary>
		/// <param name="priceFileId">Price File unique id.</param>
		/// <returns>A PriceFile object</returns>
		public PriceFile Load(int priceFileId)
		{
			T.E();

			const string spName = "usp_PriceFileGetStaticData";
			SqlParameter[] spParameters = new SqlParameter[1];

			// Set up the stored procedure parameters.
			spParameters[0] = new SqlParameter("@FileId", SqlDbType.Int);
			spParameters[0].Value = priceFileId;

			// Create the fund object.
			PriceFile PriceFile = null;

			try
			{
				PriceFile = (PriceFile) this.LoadEntity(spName, spParameters);

				// Test for valid object
				if (PriceFile == null)
				{
					throw new ArgumentException(string.Format("Failed to load PriceFile ({0})", priceFileId));
				}
			}
			catch (SqlException ex)
			{
				ThrowDBException(ex, spName, spParameters);
			}
			finally
			{
				T.X();
			}
			return PriceFile;
		}

		/// <summary>
		/// Saves the specified PriceFile.
		/// </summary>
		/// <param name="PriceFile">PriceFile.</param>
		/// <exception cref="InvalidSqlParameterException">Attempt to call stored proc with an invalid parameter</exception>
		/// <exception cref="ConstraintViolationException">Unable to insert item as an existing item already exists with unique key</exception>
		/// <exception cref="ConcurrencyViolationException">Unable to update item as item has been modified since last load</exception>
		/// <exception cref="DatabaseException">Unable to create/update or delete items</exception>
		public void Save(PriceFile PriceFile)
		{
			T.E();
			try
			{
				this.SaveEntity(PriceFile);
			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// Saves the specified PriceFile.
		/// </summary>
		/// <param name="PriceFile">PriceFile.</param>
		/// <param name="transaction">Transaction.</param>
		/// <exception cref="InvalidSqlParameterException">Attempt to call stored proc with an invalid parameter</exception>
		/// <exception cref="ConstraintViolationException">Unable to insert item as an existing item already exists with unique key</exception>
		/// <exception cref="ConcurrencyViolationException">Unable to update item as item has been modified since last load</exception>
		/// <exception cref="DatabaseException">Unable to create/update or delete items</exception>
		public void Save(PriceFile PriceFile, SqlTransaction transaction)
		{
			T.E();
			try
			{
				this.SaveEntity(PriceFile,transaction);
			}
			finally
			{
				T.X();
			}
		}

		#endregion

		#region Entity methods

		/// <summary>
		/// Creates the entity from the data reader.
		/// </summary>
		/// <param name="safeReader">Reader to get the data from.</param>
		/// <returns></returns>
		protected override object CreateEntity(SafeDataReader safeReader)
		{
			T.E();

			PriceFile PriceFile = null;
			try
			{
				PriceFile = new PriceFile
					(
						safeReader.GetString("FileName"),
						safeReader.GetString("Description"),
						safeReader.GetString("Extension"),
						safeReader.GetInt32("PriceFilesId"),
						safeReader.GetString("CompanyCode"),
						safeReader.GetTimestamp("ts")
					);
			}
			finally
			{
				T.X();
			}
			return PriceFile;
		}


		/// <summary>
		/// Deletes the PriceFile item.
		/// </summary>
		/// <param name="entity">Entity.</param>
		/// <param name="transaction">Transaction.</param>
		/// <exception cref="ArgumentNullException">The PriceFile object is NULL</exception>
		/// <exception cref="ArgumentException">The entity object is of the wrong type, i.e. not a PriceFile</exception>
		/// <exception cref="DatabaseException">Unable to delete PriceFile item</exception>
		protected override void DeleteEntity(IEntityBase entity, SqlTransaction transaction)
		{
			T.E();
			if (entity == null) throw new ArgumentNullException("entity", "Cannot delete a null PriceFile");
			if (!(entity is PriceFile)) throw new ArgumentException(string.Format("Cannot convert type {0} to type PriceFile for deletion", entity.GetType()), "entity");

			PriceFile PriceFile = (PriceFile) entity;
			const string spName = "usp_PriceFileDelete";
			SqlParameter[] spParams = new SqlParameter[2];
			spParams[0] = new SqlParameter("@FileId", SqlDbType.Int);
			spParams[1] = new SqlParameter("@ts", SqlDbType.Timestamp);

			try
			{
				spParams[0].Value = PriceFile.FileId;
				spParams[1].Value = PriceFile.TimeStamp;

				SqlHelper.ExecuteNonQuery(transaction, CommandType.StoredProcedure, spName, spParams);
			}
			catch (SqlException ex)
			{
				this.ThrowDBException(ex, spName, spParams);
			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// Inserts the entity.
		/// </summary>
		/// <param name="entity">Entity.</param>
		/// <param name="transaction">Transaction.</param>
		/// <exception cref="ArgumentNullException">The PriceFile object is NULL</exception>
		/// <exception cref="ArgumentException">The entity object is of the wrong type, i.e. not a PriceFile</exception>
		/// <exception cref="DatabaseException">Unable to append PriceFile item</exception>
		protected override void InsertEntity(IEntityBase entity, SqlTransaction transaction)
		{
			T.E();
			if (entity == null) throw new ArgumentNullException("entity", "Cannot insert a null PriceFile");
			if (!(entity is PriceFile)) throw new ArgumentException(string.Format("Cannot convert type {0} to type PriceFile for insertion", entity.GetType()), "entity");

			// Establish stored proc environment
			const string spName = "usp_PriceFileCreate";
			PriceFile PriceFile = (PriceFile) entity;
			SqlParameter[] spParams = new SqlParameter[6];
			spParams[0] = new SqlParameter("@PriceFileId", SqlDbType.Int);
			spParams[0].Direction = ParameterDirection.Output;
			spParams[1] = new SqlParameter("@FileName", SqlDbType.VarChar, 50);
			spParams[2] = new SqlParameter("@Description", SqlDbType.VarChar, 200);
			spParams[3] = new SqlParameter("@Extension", SqlDbType.Char, 3);
			spParams[4] = new SqlParameter("@CompanyCode", SqlDbType.VarChar, 10);
			spParams[5] = new SqlParameter("@ts", SqlDbType.Timestamp);
			spParams[5].Direction = ParameterDirection.Output;

			try
			{
				spParams[1].Value = PriceFile.FileName;
				spParams[2].Value = PriceFile.FileDescription;
				spParams[3].Value = PriceFile.Extension;
				spParams[4].Value = PriceFile.CompanyCode;

				// Execute insert query
				SqlHelper.ExecuteNonQuery(transaction, CommandType.StoredProcedure, spName, spParams);

				// Capture return values
				PriceFile.TimeStamp = (byte[]) spParams[5].Value;
				PriceFile.FileId= (int) spParams[0].Value;
				PriceFile.IsNew = false;
				PriceFile.IsDeleted = false;
				PriceFile.IsDirty = false;
			}
			catch (SqlException ex)
			{
				this.ThrowDBException(ex, spName, spParams);
			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// Updates the entity.
		/// </summary>
		/// <param name="entity">Entity.</param>
		/// <param name="transaction">Transaction.</param>
		/// <exception cref="ArgumentNullException">The PriceFile object is NULL</exception>
		/// <exception cref="ArgumentException">The entity object is of the wrong type, i.e. not a PriceFile</exception>
		/// <exception cref="DatabaseException">Unable to update PriceFile item</exception>
		protected override void UpdateEntity(IEntityBase entity, SqlTransaction transaction)
		{
			T.E();
			if (entity == null) throw new ArgumentNullException("entity", "Cannot update a null PriceFile");
			if (!(entity is PriceFile)) throw new ArgumentException(string.Format("Cannot convert type {0} to type PriceFile for insertion", entity.GetType()), "entity");

			// Establish stored proc environment
			const string spName = "usp_PriceFilesUpdate";
			PriceFile PriceFile = (PriceFile) entity;
			SqlParameter[] spParams = new SqlParameter[6];

			spParams[0] = new SqlParameter("@FileId", SqlDbType.Int);
			spParams[1] = new SqlParameter("@PriceFileName", SqlDbType.VarChar, 50);
			spParams[2] = new SqlParameter("@Description", SqlDbType.VarChar, 200);
			spParams[3] = new SqlParameter("@Extension", SqlDbType.Char, 3);
			spParams[4] = new SqlParameter("@CompanyCode", SqlDbType.VarChar, 10);
			spParams[5] = new SqlParameter("@ts", SqlDbType.Timestamp);
			spParams[5].Direction = ParameterDirection.InputOutput;

			try
			{
				spParams[0].Value = PriceFile.FileId;
				spParams[1].Value = PriceFile.FileName;
				spParams[2].Value = PriceFile.FileDescription;
				spParams[3].Value = PriceFile.Extension;
				spParams[4].Value = PriceFile.CompanyCode;
				spParams[5].Value = PriceFile.TimeStamp;

				// Execute insert query
				SqlHelper.ExecuteNonQuery(transaction, CommandType.StoredProcedure, spName, spParams);

				// Capture return values
				PriceFile.TimeStamp = (byte[]) spParams[5].Value;
				PriceFile.IsNew = false;
				PriceFile.IsDeleted = false;
				PriceFile.IsDirty = false;
			}
			catch (SqlException ex)
			{
				this.ThrowDBException(ex, spName, spParams);
			}
			finally
			{
				T.X();
			}
		}

		#endregion

	}
}