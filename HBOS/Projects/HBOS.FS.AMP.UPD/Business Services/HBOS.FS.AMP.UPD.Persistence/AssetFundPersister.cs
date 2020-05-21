using System;
using System.Data;
using System.Data.SqlClient;
using HBOS.FS.AMP.UPD.Types.AssetFunds;
using HBOS.FS.Support.Tex;
using Microsoft.ApplicationBlocks.Data;

namespace HBOS.FS.AMP.UPD.Persistence
{
	/// <summary>
	/// The class to use for persisting or retrieving AssetFund objects and
	/// AssetFundCollection objects.
	/// </summary>
	public abstract class AssetFundPersister : EntityPersister
	{
		#region Constructors

		/// <summary>
		/// Constructor used to initialise the ConnectionString property.
		/// </summary>
		/// <param name="connectionString"></param>
		protected AssetFundPersister(string connectionString) : base(connectionString)
		{
		}

		#endregion

		#region Abstract Methods

		/// <summary>
		/// 
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		protected abstract override object CreateEntity(SafeDataReader reader);

		#endregion

		#region Validation Methods

		/// <summary>
		/// Checks to see if the updated fullname or shortname already exists in the system for another asset fund
		/// </summary>
		/// <param name="assetFundID"></param>
		/// <param name="fullName"></param>
		/// <param name="shortName"></param>
		/// <param name="fullNameExists"></param>
		/// <param name="shortNameExists"></param>
		public void CheckFullNameOrShortNameDuplicationForExistingAssetFund(string assetFundID, string fullName, string shortName,
		                                                                    out bool fullNameExists, out bool shortNameExists)
		{
			T.E();
			SqlParameter[] parameters = null;
			fullNameExists = false;
			shortNameExists = false;
			try
			{
				const string storedProcName = "dbo.usp_AssetFundCheckFullNameShortNameDuplication";

				parameters = new SqlParameter[5];
				parameters[0] = new SqlParameter("@assetFundID", SqlDbType.Char, 8);
				parameters[0].Value = assetFundID;
				parameters[1] = new SqlParameter("@fullName", SqlDbType.VarChar, 100);
				parameters[1].Value = fullName;
				parameters[2] = new SqlParameter("@shortName", SqlDbType.VarChar, 50);
				parameters[2].Value = shortName;
				parameters[3] = new SqlParameter("@fullNameExists", SqlDbType.Bit);
				parameters[3].Direction = ParameterDirection.Output;
				parameters[4] = new SqlParameter("@shortNameExists", SqlDbType.Bit);
				parameters[4].Direction = ParameterDirection.Output;

				SqlHelper.ExecuteNonQuery(this.ConnectionString, CommandType.StoredProcedure, storedProcName, parameters);

				fullNameExists = (bool) parameters[3].Value;
				shortNameExists = (bool) parameters[4].Value;

			}
			catch (SqlException ex)
			{
				this.ThrowDBException(ex, ex.Procedure, parameters);
			}
			finally
			{
				T.E();
			}
		}


		/// <summary>
		/// Checks that no other fund exists with this id, fullname or shortname
		/// </summary>
		/// <param name="assetFundID"></param>
		/// <param name="fullName"></param>
		/// <param name="shortName"></param>
		/// <param name="assetFundIDExists"></param>
		/// <param name="fullNameExists"></param>
		/// <param name="shortNameExists"></param>
		public void CheckAssetFundExistence(string assetFundID, string fullName, string shortName,
		                                    out bool assetFundIDExists, out bool fullNameExists, out bool shortNameExists)
		{
			T.E();
			SqlParameter[] parameters = null;
			assetFundIDExists = false;
			fullNameExists = false;
			shortNameExists = false;
			try
			{
				const string storedProcName = "dbo.usp_AssetFundCheckExistence";

				parameters = new SqlParameter[6];
				parameters[0] = new SqlParameter("@assetFundID", SqlDbType.Char, 8);
				parameters[0].Value = assetFundID;
				parameters[1] = new SqlParameter("@fullName", SqlDbType.VarChar, 100);
				parameters[1].Value = fullName;
				parameters[2] = new SqlParameter("@shortName", SqlDbType.VarChar, 50);
				parameters[2].Value = shortName;
				parameters[3] = new SqlParameter("@assetFundIDExists", SqlDbType.Bit);
				parameters[3].Direction = ParameterDirection.Output;
				parameters[4] = new SqlParameter("@fullNameExists", SqlDbType.Bit);
				parameters[4].Direction = ParameterDirection.Output;
				parameters[5] = new SqlParameter("@shortNameExists", SqlDbType.Bit);
				parameters[5].Direction = ParameterDirection.Output;

				SqlHelper.ExecuteNonQuery(this.ConnectionString, CommandType.StoredProcedure, storedProcName, parameters);

				assetFundIDExists = (bool) parameters[3].Value;
				fullNameExists = (bool) parameters[4].Value;
				shortNameExists = (bool) parameters[5].Value;

			}
			catch (SqlException ex)
			{
				this.ThrowDBException(ex, ex.Procedure, parameters);
			}
			finally
			{
				T.E();
			}
		}

		#endregion

		#region Public Static methods

		/// <summary>
		/// takes an asset fund, checks its concrete type and returns db type
		/// </summary>
		/// <param name="assetFund"></param>
		/// <returns></returns>
		public static string resolveAssetFundToDBType(AssetFund assetFund)
		{
			T.E();
			try
			{
				AssetFund.AssetFundTypeEnum afType = assetFund.AssetFundType;

				switch (afType)
				{
					case (AssetFund.AssetFundTypeEnum.Composite):
						return "C";
					case (AssetFund.AssetFundTypeEnum.Linked):
						return "L";
					case (AssetFund.AssetFundTypeEnum.Oeic):
						return "O";
					default:
						throw new ArgumentException("Asset fund not of a valid asset fund type");
				}
			}
			finally
			{
				T.X();
			}
		}

//		/// <summary>
//		/// Checks the specific type of the abstract class and returns its type as an enum
//		/// </summary>
//		/// <param name="assetFund"></param>
//		/// <returns></returns>
//		/// <exception cref="ArgumentException">Invalid asset fund type</exception>
//		public static AssetFundType ResolveAssetFundType(AssetFund assetFund)
//		{
//			OEICAssetFund oeic = null;
//			LinkedAssetFund linked = null;
//			CompositeAssetFund comp = null;
//			return ResolveAssetFundType(assetFund, out oeic, out linked, out comp);
//		}

//		/// <summary>
//		/// Encapsulates the type checking of the abstract base class.
//		/// Throws exception if not of any type. 
//		/// Returns two null references and the reference to the specific object type, as well as the return enum type
//		/// </summary>
//		/// <param name="assetFund"></param>
//		/// <param name="oeic"></param>
//		/// <param name="linked"></param>
//		/// <param name="comp"></param>
//		/// <returns></returns>
//		/// <exception cref="ArgumentException">Invalid asset fund type</exception>
//		public static AssetFundType ResolveAssetFundType(AssetFund assetFund, out OEICAssetFund oeic, out LinkedAssetFund linked, out CompositeAssetFund comp)
//		{
//			//this section is to save casting multiple times later
//			oeic = assetFund as OEICAssetFund;
//			comp = null;
//			linked = null;
//
//			AssetFundType retType;
//
//			if (oeic == null)
//			{
//				linked = assetFund as LinkedAssetFund;
//				if (linked == null)
//				{
//					comp = assetFund as CompositeAssetFund;
//					if (comp == null)
//					{
//						throw new ArgumentException("Asset fund not of a valid asset fund type");
//					}
//					else
//					{
//						retType = AssetFundType.Composite;
//					}
//				}
//				else
//				{
//					retType = AssetFundType.Linked;
//				}
//			}
//			else
//			{
//				retType = AssetFundType.Oeic;
//			}
//
//			return retType;
//		}

		/// <summary>
		/// takes a db string representing asset fund type, and returns an enumerated type.
		/// </summary>
		/// <param name="dbAssetFundType"></param>
		/// <returns></returns>
		public static AssetFund.AssetFundTypeEnum resolveDBTypeToAssetFundType(string dbAssetFundType)
		{
			T.E();
			try
			{
				switch (dbAssetFundType)
				{
					case ("C"):
						return AssetFund.AssetFundTypeEnum.Composite;
					case ("L"):
						return AssetFund.AssetFundTypeEnum.Linked;
					case ("O"):
						return AssetFund.AssetFundTypeEnum.Oeic;
					default:
						throw new ArgumentException("Asset fund not of a valid asset fund type");
				}
			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// takes an enumerated asset fund type, and returns the type as a string (char 1) suitable for the database.
		/// </summary>
		/// <param name="assetFundType"></param>
		/// <returns></returns>
		public static string resolveAssetFundTypeToDBType(AssetFund.AssetFundTypeEnum assetFundType)
		{
			T.E();
			try
			{
				switch (assetFundType)
				{
					case AssetFund.AssetFundTypeEnum.Composite:
						return ("C");
					case AssetFund.AssetFundTypeEnum.Linked:
						return ("L");
					case AssetFund.AssetFundTypeEnum.Oeic:
						return ("O");
					default:
						throw new ArgumentException("Asset fund not of a valid asset fund type");
				}
			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="newAssetFund"></param>
		protected static void logAssetFundLoad(AssetFund newAssetFund)
		{
			T.E();
			try
			{
				// Diagnostics 
				T.Log(" *********** ASSET FUND : " + newAssetFund.FullName + " **************");
				T.Log("Asset Fund Type = " + newAssetFund.GetType());
				T.Log("Predicted AssetMovement = " + newAssetFund.PredictedAssetMovement);
				if (newAssetFund.AssetMovementConstituents != null)
				{
					T.Log("Num Constitute Parts = " + newAssetFund.AssetMovementConstituents.Count);

					int j = 0;
					foreach (AssetMovementConstituent c in newAssetFund.AssetMovementConstituents)
					{
						T.Log("Index [" + j + "] : Contribution = " + c.CalculateEffect() + ", type = " + c.BenchMark.GetType());
						j++;
					}
				}
				else
				{
					T.Log("Indices not loaded");
				}
				T.Log(Environment.NewLine);

			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="assetFundCode"></param>
		/// <param name="loadForPricing">Indicates whether or not to include benchmark prices</param>
		/// <returns></returns>
		protected AssetMovementConstituentCollection loadMovementConstituents(string assetFundCode,bool loadForPricing)
		{
			T.E();
			AssetMovementConstituentPersister persister = new AssetMovementConstituentPersister(this.ConnectionString);
			AssetMovementConstituentCollection result = persister.LoadAssetFundMovementConstituents(assetFundCode,loadForPricing);
			T.X();
			return result;
		}

	


		#endregion
	}

}