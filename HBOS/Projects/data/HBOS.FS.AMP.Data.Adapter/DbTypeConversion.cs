using System;
using System.Data;
using System.Collections;

namespace HBOS.FS.AMP.Data.Adapter
{
	/// <summary>
	/// DBTypeConversion - allows some simple type conversions between .Net and SQL Types.
	/// </summary>
	public class DBTypeConversion 
	{ 
		#region Variables

		private static string[,] DBTypeConversionKey = new string[,] 
	{ 
		{"BigInt","System.Int64"}, 
		{"VarChar","System.String"}, 
		{"Binary","System.Byte[]"}, 
		{"Bit","System.Boolean"}, 
		{"Char","System.String"}, 
		{"DateTime","System.DateTime"}, 
		{"Decimal","System.Decimal"}, 
		{"Float","System.Double"}, 
		{"Image","System.Byte[]"}, 
		{"Int","System.Int32"}, 
		{"Money","System.Decimal"}, 
		{"NChar","System.String"}, 
		{"NText","System.String"}, 
		{"NVarChar","System.String"}, 
		{"Real","System.Single"}, 
		{"SmallDateTime","System.DateTime"}, 
		{"SmallInt","System.Int16"}, 
		{"SmallMoney","System.Decimal"}, 
		{"Text","System.String"}, 
		{"Timestamp","System.DateTime"}, 
		{"TinyInt","System.Byte"}, 
		{"UniqueIdentifer","System.Guid"}, 
		{"VarBinary","System.Byte[]"}, 
		{"Variant","System.Object"} 
	}; 

		#endregion

		#region Methods

		/// <summary>
		/// Convert a .Net system type to a SQL Server Type
		/// </summary>
		/// <param name="sourceType">Source .Net data type to convert</param>
		/// <returns>The corresponding SQL Server type.</returns>
		/// <example>
		///		<para>An example of converting from System Types to SQL Server types is:</para>
		///		<code lang="C#">
		///			SqlParameter parameter = new SqlParameter();
		///			parameter.SqlDbType = DBTypeConversion.SystemTypeToDbType(Type.GetType(schema.Rows[i]["DataType"].ToString()));
		///		</code>
		/// </example>
		public static SqlDbType SystemTypeToDbType( System.Type sourceType ) 
		{ 
			SqlDbType result; 
			string SystemType = sourceType.ToString(); 
			string DBType = String.Empty; 
			int keyCount = DBTypeConversionKey.GetLength(0); 
            
			for(int i=0;i<keyCount;i++) 
			{ 
				if(DBTypeConversionKey[i,1].Equals(SystemType))
				{
					DBType = DBTypeConversionKey[i,0]; 
					break;
				}
			} 

			if (DBType==String.Empty)
			{
				DBType = "Variant"; 
			}

			result = (SqlDbType)Enum.Parse(typeof(SqlDbType), DBType); 

			return result; 
		} 

		/// <summary>
		/// Convert a DB type into a system type
		/// </summary>
		/// <param name="sourceType">The SQL Server tpye to convert</param>
		/// <returns>The corresponse .Net native type.</returns>
		/// <example>
		/// </example>
		public static Type DbTypeToSystemType( SqlDbType sourceType ) 
		{ 
			Type result; 
			string SystemType = String.Empty; 
			string DBType = sourceType.ToString(); 
			int keyCount = DBTypeConversionKey.GetLength(0); 

			for(int i=0;i<keyCount;i++) 
			{ 
				if(DBTypeConversionKey[i,0].Equals(DBType)) 
				{
					SystemType = DBTypeConversionKey[i,1]; 
					break;
				}
			} 

			if (SystemType==String.Empty)
			{
				SystemType = "System.Object"; 
			}

			result = Type.GetType(SystemType); 

			return result; 
		}

		#endregion
	} 
}
