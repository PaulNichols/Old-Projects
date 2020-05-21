using System;
using System.Data;
using HBOS.FS.AMP.UPD.Exceptions;

namespace HBOS.FS.AMP.UPD.Persistence
{
	/// <summary>
	/// Wraps a data reader so that is returns 'safe' null values for DBNulls
	/// </summary>
	public class SafeDataReader
	{
		#region Constructor

		/// <summary>
		/// Creates a new <see cref="SafeDataReader"/> instance.
		/// </summary>
		/// <param name="record">The reader being wrapped</param>
		public SafeDataReader(IDataRecord record)
		{
			if (record == null) throw new ArgumentNullException("record");
			this.record = record;
		}

		#endregion

		#region GetString

		/// <summary>
		/// Gets a column as a string from reader
		/// </summary>
		/// <param name="ordinal">The position of the column in the reader</param>
		/// <returns>The value of the column</returns>
		public string GetString(int ordinal)
		{
			validateOrdinal(ordinal);
			if (record.IsDBNull(ordinal))
			{
				return string.Empty;
			}
			else
			{
				return record.GetString(ordinal);
			}
		}

		/// <summary>
		/// Gets a column as a string from reader
		/// </summary>
		/// <param name="columnName">The name of the column in the reader</param>
		/// <returns>The value of the column, string.Empty when null</returns>
		public string GetString(string columnName)
		{
			int ord = this.getOrdinal(columnName);
			return GetString(ord);
		}

		#endregion

		#region GetInt32

		/// <summary>
		/// Gets a column as an int32 from reader
		/// </summary>
		/// <param name="ordinal">The position of the column in the reader</param>
		/// <returns>The value of the column. Zero when null</returns>
		public int GetInt32(int ordinal)
		{
			validateOrdinal(ordinal);
			if (record.IsDBNull(ordinal))
			{
				return 0;
			}
			else
			{
				return record.GetInt32(ordinal);
			}
		}

		/// <summary>
		/// Gets a column as an int16 from reader
		/// </summary>
		/// <param name="ordinal">The position of the column in the reader</param>
		/// <returns>The value of the column. Zero when null</returns>
		public int GetInt16(int ordinal)
		{
			validateOrdinal(ordinal);
			if (record.IsDBNull(ordinal))
			{
				return 0;
			}
			else
			{
				return record.GetInt16(ordinal);
			}
		}

		/// <summary>
		/// Gets a column as an int32 from reader
		/// </summary>
		/// <param name="columnName">The name of the column in the reader</param>
		/// <returns>The value of the column, 0 when null</returns>
		public int GetInt16(string columnName)
		{
			int ord = this.getOrdinal(columnName);
			return GetInt16(ord);
		}

		/// <summary>
		/// Gets a column as an int32 from reader
		/// </summary>
		/// <param name="columnName">The name of the column in the reader</param>
		/// <returns>The value of the column, 0 when null</returns>
		public int GetInt32(string columnName)
		{
			int ord = this.getOrdinal(columnName);
			return GetInt32(ord);
		}

		#endregion

		#region GetInt64

		/// <summary>
		/// Gets a column as an int64 from reader
		/// </summary>
		/// <param name="ordinal">The position of the column in the reader</param>
		/// <returns>The value of the column. Zero when null</returns>
		public Int64 GetInt64(int ordinal)
		{
			validateOrdinal(ordinal);
			if (record.IsDBNull(ordinal))
			{
				return 0;
			}
			else
			{
				return record.GetInt64(ordinal);
			}
		}

		/// <summary>
		/// Gets a column as an int64 from reader
		/// </summary>
		/// <param name="columnName">The name of the column in the reader</param>
		/// <returns>The value of the column, 0 when null</returns>
		public Int64 GetInt64(string columnName)
		{
			int ord = this.getOrdinal(columnName);
			return GetInt64(ord);
		}

		#endregion

		#region GetTimestamp

		/// <summary>
		/// Gets a column as a timestamp from reader
		/// </summary>
		/// <param name="ordinal">The position of the column in the reader</param>
		/// <returns>The value of the column</returns>
		public byte[] GetTimestamp(int ordinal)
		{
			validateOrdinal(ordinal);
			if (record.IsDBNull(ordinal))
			{
				return new byte[0];
			}
			else
			{
				return (byte[]) record.GetValue(ordinal);
			}
		}

		/// <summary>
		/// Gets a column as a TimeStamp (byte[]) from reader
		/// </summary>
		/// <param name="columnName">The name of the column in the reader</param>
		/// <returns>The value of the column, byte[0] when null</returns>
		public byte[] GetTimestamp(string columnName)
		{
			int ord = getOrdinal(columnName);
			return GetTimestamp(ord);
		}

		#endregion

		#region GetBoolean

		/// <summary>
		/// Gets a column as an bool from reader
		/// </summary>
		/// <param name="ordinal">The position of the column in the reader</param>
		/// <returns>The value of the column. false when null</returns>
		public bool GetBoolean(int ordinal)
		{
			validateOrdinal(ordinal);
			if (record.IsDBNull(ordinal))
			{
				return false;
			}
			else
			{
				return record.GetBoolean(ordinal);
			}
		}

		/// <summary>
		/// Gets a column as a bool from reader
		/// </summary>
		/// <param name="columnName">The name of the column in the reader</param>
		/// <returns>The value of the column, false when null</returns>
		public bool GetBoolean(string columnName)
		{
			int ord = this.getOrdinal(columnName);
			return GetBoolean(ord);
		}

		#endregion

		#region GetDateTime

		/// <summary>
		/// Gets a column as an DateTime from reader
		/// </summary>
		/// <param name="ordinal">The position of the column in the reader</param>
		/// <returns>The value of the column. Now when null</returns>
		public DateTime GetDateTime(int ordinal)
		{
			validateOrdinal(ordinal);
			if (record.IsDBNull(ordinal))
			{
				return DateTime.Now;
			}
			else
			{
				return record.GetDateTime(ordinal);
			}
		}

		/// <summary>
		/// Gets a column as a DateTime from reader
		/// </summary>
		/// <param name="columnName">The name of the column in the reader</param>
		/// <returns>The value of the column, Now when null</returns>
		public DateTime GetDateTime(string columnName)
		{
			int ord = this.getOrdinal(columnName);
			return GetDateTime(ord);
		}

		#endregion

		#region GetDecimal

		/// <summary>
		/// Returns the decimal value for a given column by ordinal
		/// </summary>
		/// <param name="ordinal"></param>
		/// <returns></returns>
		public decimal GetDecimal(int ordinal)
		{
			validateOrdinal(ordinal);
			if (record.IsDBNull(ordinal))
			{
				return 0M;
				;
			}
			else
			{
				return record.GetDecimal(ordinal);
			}
		}

		/// <summary>
		/// Returns the decimal value for a given column by column name
		/// </summary>
		/// <param name="columnName"></param>
		/// <returns></returns>
		public decimal GetDecimal(string columnName)
		{
			int ord = this.getOrdinal(columnName);
			return GetDecimal(ord);
		}

		#endregion

		#region IsNull

		/// <summary>
		/// Checks whether returned value is system.DBNull (by ordinal)
		/// </summary>
		/// <param name="ordinal"></param>
		/// <returns></returns>
		public bool IsNull(int ordinal)
		{
			validateOrdinal(ordinal);
			return record.IsDBNull(ordinal);
		}

		/// <summary>
		/// Checks whether returned value is system.DBNull (by column name)
		/// </summary>
		/// <param name="columnName"></param>
		/// <returns></returns>
		public bool IsNull(string columnName)
		{
			int ordinal = this.getOrdinal(columnName);
			return IsNull(ordinal);
		}

		#endregion

		#region ColumnExists

		/// <summary>
		/// Determines whether a specified column name exists in a result set
		/// </summary>
		/// <param name="columnName">Name of the column to test.</param>
		/// <returns>bool</returns>
		public bool ColumnExists(string columnName)
		{
			bool result = true;
			try
			{
				record.GetOrdinal(columnName);
			}
			catch (IndexOutOfRangeException)
			{
				result = false;
			}
			return result;
		}

		#endregion

		#region PrivateMembers

		private IDataRecord record;

		private void validateOrdinal(int ordinal)
		{
			if (ordinal >= record.FieldCount || ordinal < 0)
				throw new SchemaMismatchException("Cannot find column by ordinal", ordinal.ToString());
		}

		private int getOrdinal(string columnName)
		{
			try
			{
				return record.GetOrdinal(columnName);
			}
			catch (IndexOutOfRangeException ex)
			{
				throw new SchemaMismatchException("Cannot find column by name", columnName, ex);
			}
		}

		#endregion
	}
}