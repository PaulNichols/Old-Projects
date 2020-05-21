using System;
using System.Data;
using System.Diagnostics;

using HBOS.FS.AMP.Data.Transfer.Interface;
using HBOS.FS.AMP.Data.Validator.Interface;
using HBOS.FS.AMP.Data.Persister.Interface;
using HBOS.FS.AMP.Data.Validator;
using HBOS.FS.AMP.Data.Types;
using HBOS.FS.AMP.Windows.Controls;

namespace HBOS.FS.AMP.Data.Transfer
 {
    /// <summary>
    /// Provides information about the validation errors event
    /// </summary>
   public class ValidationErrorsEventArgs: System.EventArgs
    {
        private DataTable validationEvents;

       /// <summary>
       /// Hide the default constructor
       /// </summary>
        private ValidationErrorsEventArgs()
        {
        }

       /// <summary>
       /// Only public constructor for the class
       /// </summary>
       /// <param name="ValidationEvents">DataTable containing the validation event exceptions</param>
        public ValidationErrorsEventArgs(DataTable ValidationEvents)
        {
            validationEvents = ValidationEvents;
        }

       /// <summary>
       /// Returns the validation event exceptions
       /// </summary>
        public DataTable ValidationEvents
        {
            get{return validationEvents;}
        }
    }

	/// <summary>
	/// Data Transporter - Is responsible for the transfer of data from a Source to a Sink.
	/// </summary>
	/// <remarks>
	/// The data transporter is the link which connects the IDataReaders to the Validators and the Persisters. 
	/// The persisters raise events with Validation errors and the transporter catches these, and displays a Validation error report.
	/// </remarks>
	/// <example>
	///		<code lang="C#">
	///			DataTransporter myDataTransporter = new DataTransporter( true , reader, validators, persisters);
	///			myDataTransporter.ValidationErrorsEvent += new HBOS.FS.AMP.Data.Transfer.DataTransporter.ValidationErrorsDelegate(myDataTransporter_ValidationErrorsEvent);
	///			((IDataTransporter)myDataTransporter).RunDataTransfer();
	///		</code>
	/// </example>
	public class DataTransporter : IDataTransporter
	{
		#region Enum
		
		/// <summary>
		/// What kind of transfer type
		/// </summary>
		private enum TransferType : int
		{
			/// <summary>
			/// Transfer by row
			/// </summary>
			TransferByRow = 0,
			/// <summary>
			/// Transfer by dataset
			/// </summary>
			TransferDataSet = 1
		}

		#endregion

		#region Members

		/// <summary>
		/// Display of a validation report
		/// </summary>
		bool m_displayValidationReport = true;

		/// <summary>
		/// Data Reader for processing the Data Source
		/// </summary>
		private IDataReader m_Reader;

		/// <summary>
		/// Hooked up DataRow persisters
		/// </summary>
		private IPersist[][] m_dataRowPersisters;

		/// <summary>
		/// Hooked up Data Set persisters
		/// </summary>
		private IPersist[] m_dataSetPersisters;

		/// <summary>
		/// Hooked up DataRow Validators
		/// </summary>
		private IDataRowValidator[][] m_DataRowValidators;

		/// <summary>
		/// Data Set processing
		/// </summary>
		private DataSet m_sourceDataSet = null;

		/// <summary>
		/// What type of data transfer are we doing.
		/// </summary>
		private TransferType m_transferType = TransferType.TransferByRow;

		/// <summary>
		/// Remember the validation errors
		/// </summary>
		private DataTable m_validationErrors = null;

		//		protected IXPathNavigable m_sourceCollection = null;
        
		#endregion

		#region Event Declaration

		/// <summary>
		/// Delegate for the validation errors
		/// </summary>
		public delegate void ValidationErrorsDelegate(object sender, ValidationErrorsEventArgs e);

		/// <summary>
		/// Event for the validation errors
		/// </summary>
		public event ValidationErrorsDelegate ValidationErrorsEvent;

		#endregion

		#region Constructor

		/// <summary>
		/// Constructor which links the datareader to the validators to the persisters.
		/// </summary>
		/// <param name="displayValidationReport">Allow display of a validation error report if there are any errors.</param>
		/// <param name="reader">DataReader we are reading the data from.</param>
		/// <param name="validators">Validators we are used to see if the data is valid.</param>
		/// <param name="persisters">Persisters we are using to save the data.</param>
		public DataTransporter( bool displayValidationReport , IDataReader reader, IDataRowValidator[][] validators, IPersistRow[][] persisters)
		{
			m_displayValidationReport = displayValidationReport;
			m_Reader = reader;
			m_DataRowValidators = validators;
			m_dataRowPersisters = persisters;
			m_transferType = TransferType.TransferByRow;
		}

		/// <summary>
		/// Constructor for dataset processing (Disconnected)
		/// </summary>
		/// <param name="sourceDataSet">Dataset we want to persist.</param>
		/// <param name="persisters">Persisters to which we are transferring the data.</param>
		public DataTransporter( DataSet sourceDataSet, IPersistDataSet[] persisters)
		{
			m_sourceDataSet = sourceDataSet;
			m_dataSetPersisters = persisters;
			m_transferType = TransferType.TransferDataSet;
		}

		//
		//		/// <summary>
		//		/// Constructor for Colleciton processing (Disconnected)
		//		/// </summary>
		//		/// <param name="sourceCollection"></param>
		//		/// <param name="validators"></param>
		//		/// <param name="persisters"></param>
		//		public TransferBase( IXPathNavigable sourceCollection, IDataRowValidator[][] validators, ITransferCollection[][] persisters)
		//		{
		//			m_sourceCollection = sourceCollection;
		//			m_Validators = validators;
		//			m_Persisters = persisters;
		//		}

		#endregion

		#region IDataTransporter Implementation

		/// <summary>
		/// Transfer the data from the Data Source (either a data reader or dataset) to the associated persisters using the hooked up validators.
		/// </summary>
		public void RunDataTransfer()
		{
			switch( this.m_transferType )
			{
				case TransferType.TransferByRow:
					this.transferByRow();
					break;
				case TransferType.TransferDataSet:
					this.transferByDataSet();
					break;
			}
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// Transfer the data by row
		/// </summary>
		private void transferByRow()
		{
			DataTable dataDefinition = null;
			int resultSetNum = 0;

			try
			{
				// Hook up the events
				if ( null != m_DataRowValidators)
				{
					for(int i = 0; i< m_DataRowValidators.Length; i++)
					{
						for (int j = 0; j < m_DataRowValidators[i].Length; j++)
						{
							m_DataRowValidators[i][j].InvalidDataRowEvent += new InvalidDataRowDelegate(DataTransporter_InvalidDataRowEvent);
						}
					}
				}

				// Start the transactions on all the persisters
				if (null != m_dataRowPersisters)
				{
					for(int i = 0; i< m_dataRowPersisters.Length; i++)
						for (int j = 0; j < m_dataRowPersisters[i].Length; j++)
							m_dataRowPersisters[i][j].InitialiseTransfer();
				}

				// Process each rowset in the supplied reader
				do
				{
					// Get the schema that describes the current row set
					dataDefinition = m_Reader.GetSchemaTable();

					// Process each row in the current row set
				    while (m_Reader.Read())
				    {
						ValidationErrorSeverity validationError = ValidationErrorSeverity.None;

					    // Create a string array to represent the current row                
					    string[] dataRow = new string[dataDefinition.Rows.Count];
					    for (int i = 0; i < dataDefinition.Rows.Count; i++)
					    {
						    dataRow[i] = m_Reader[i].ToString();
							Debug.WriteLine( dataRow[i] );
					    }

					    // Run the row through each validator
					    if (null != m_DataRowValidators)
					    {
						    for (int i = 0; i < m_DataRowValidators[resultSetNum].Length ; i++)
						    {	
							    validationError = m_DataRowValidators[resultSetNum][i].Validate(dataRow, dataDefinition);

								if ( validationError != ValidationErrorSeverity.None )
								{
									break;
								}

							    // If there wasa validation error, then the event from the validator will remember the validation error
							    Debug.WriteLine( String.Format( "validating {0}-{1}", resultSetNum, i ) );
						    }
					    }

					    // Persist the row if we validate ok
					    if (null != m_dataRowPersisters && validationError == ValidationErrorSeverity.None)
					    {
						    for (int i = 0; i < m_dataRowPersisters[resultSetNum].Length; i++)
						    {
							    ((IPersistRow)m_dataRowPersisters[resultSetNum][i]).PersistRow(dataRow, dataDefinition);
						    }
					    }
				    }
					// note that we will be on the next row in the persister/validator arrays
					resultSetNum++;
				}
				while (m_Reader.NextResult());

				// Make the persistances permanent
				if (null != m_dataRowPersisters)
				{
					for(int i = 0; i< m_dataRowPersisters.Length; i++)
						for (int j = 0; j < m_dataRowPersisters[i].Length; j++)
							m_dataRowPersisters[i][j].CompleteTransfer();
				}

				// Any validation events to raise
				if ( m_DataRowValidators != null )
				{
					for(int i = 0; i< m_DataRowValidators.Length; i++)
					{
						for (int j = 0; j < m_DataRowValidators[i].Length; j++)
						{
							m_DataRowValidators[i][j].InvalidDataRowEvent -= new InvalidDataRowDelegate(DataTransporter_InvalidDataRowEvent);
						}
					}
				}

				// Are we going to display a Validaiton error Report
				if ( m_displayValidationReport && m_validationErrors != null )
				{
					this.displayValidationReport();
				}

				// Any validation errors
				if ( m_validationErrors != null )
				{
					// Lets raise an event back with the data table of validaiton errors ...
					// ... if there are any errors to tell people about.
					if( (m_validationErrors.Rows.Count != 0) && (this.ValidationErrorsEvent != null) )
					{
						ValidationErrorsEvent( this , new ValidationErrorsEventArgs(m_validationErrors) );
					}
				}

			}
			catch (System.Exception ex)
			{
				try
				{
					// Rollback if transactions pending
					if (null != m_dataRowPersisters)
					{
						for (int i=0; i < m_dataRowPersisters.Length; i++)
							for (int j = 0; j < m_dataRowPersisters[i].Length; j++)			
								if (m_dataRowPersisters[i][j].IsTransferInProgress)
									m_dataRowPersisters[i][j].CancelTransfer();
					}
				}
				finally
				{
					// make sure we raise that exception on up
					throw ex;
				}
			}
			finally
			{
				// close the reader
				if (null != m_Reader)
				{
					try
					{
						m_Reader.Close();
						m_Reader.Dispose();
					}
					finally
					{
						// no-op
					}
				}
			}
		}

		/// <summary>
		/// Transfer the data by dataset
		/// </summary>
		private void transferByDataSet()
		{
			try
			{
				// Start the transactions on all the persisters
				if (null != m_dataSetPersisters)
				{
					for(int i = 0; i< m_dataSetPersisters.Length; i++)
							m_dataSetPersisters[i].InitialiseTransfer();
				}


				// Persist the row if we validate ok
				for( int i = 0 ; i < m_dataSetPersisters.Length ; i ++ )
				{
					((IPersistDataSet)m_dataSetPersisters[i]).PersistDataSet( m_sourceDataSet );
				}
				
				// Make the persistances permanent
				if (null != m_dataSetPersisters)
				{
					for(int i = 0; i< m_dataSetPersisters.Length; i++)
							m_dataSetPersisters[i].CompleteTransfer();
				}
			}
			catch (System.Exception ex)
			{
				try
				{
					// Rollback if transactions pending
					if (null != m_dataSetPersisters)
					{
						for (int i=0; i < m_dataSetPersisters.Length; i++)
							if (m_dataSetPersisters[i].IsTransferInProgress)
								m_dataSetPersisters[i].CancelTransfer();
					}
				}
				finally
				{
					// make sure we raise that exception on up
					throw ex;
				}
			}
		}

		/// <summary>
		/// Display a validation report
		/// </summary>
		private void displayValidationReport()
		{
			FormValidationErrors myValidationErrors = new FormValidationErrors();
			myValidationErrors.Display( m_validationErrors );
			myValidationErrors.ShowDialog( );
		}

		#endregion

		#region Event Handlers

		/// <summary>
		/// Handle an Invalid row
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DataTransporter_InvalidDataRowEvent(object sender, InvalidDataRowEventArgs e)
		{
			if ( m_validationErrors == null )
			{
				// First validation error we picked up so build the datatable
				this.m_validationErrors = new DataTable( "ValidationErrors" );

				this.m_validationErrors.Columns.Add( new DataColumn( "Message" ) );
				this.m_validationErrors.Columns.Add( new DataColumn( "Severity" ) );
				for ( int rowNumber = 0 ; rowNumber < e.DataDefinition.Rows.Count; rowNumber ++ )
				{
					this.m_validationErrors.Columns.Add( new DataColumn( e.DataDefinition.Rows[ rowNumber ][ "ColumnName" ].ToString() ) );
				}
			}

			// Set up the Row
			object[] myColumnValues = new object[ m_validationErrors.Columns.Count ];
			myColumnValues[0] = e.Message;
			myColumnValues[1] = e.ValidationErrorSeverity;

			for ( int index = 2 ; index < m_validationErrors.Columns.Count ; index ++ )
			{
				myColumnValues[ index ] = e.Row[ index -2 ];
			}

			m_validationErrors.Rows.Add( myColumnValues );
		}

		#endregion

		#region Legacy

		//		/// <summary>
		//		/// Move the Data using the collection
		//		/// </summary>
		//		public void MoveDataByCollection()
		//		{
		//			try
		//			{
		//				// Start the transactions on all the persisters
		//				if (null != m_Persisters)
		//				{
		//					for(int i = 0; i< m_Persisters.Length; i++)
		//						for (int j = 0; j < m_Persisters[i].Length; j++)
		//							m_Persisters[i][j].InitialiseTransfer();
		//				}
		//
		//
		//				// Persist the row if we validate ok
		//				if (null != m_Persisters)
		//				{
		//					for(int i = 0; i< m_Persisters.Length; i++) // Loop through each result set in the dataSet
		//					{
		//						// See if all the validators are ok
		//						for( int j = 0 ; j < m_Validators[ i ].Length ; j++ )
		//						{
		//							// m_Validators[ i ][ j ].Validate( stuff );
		//						}
		//
		//						// Persist the row if we validate ok
		//						for( int j = 0 ; j < m_Persisters[ i ].Length ; j ++ )
		//						{
		//							((ITransferCollection)m_Persisters[i][j]).PersistCollection( m_sourceCollection );
		//						}
		//					}
		//				}
		//				
		//				// Make the persistances permanent
		//				if (null != m_Persisters)
		//				{
		//					for(int i = 0; i< m_Persisters.Length; i++)
		//						for (int j = 0; j < m_Persisters[i].Length; j++)
		//							m_Persisters[i][j].CompleteTransfer();
		//				}
		//			}
		//			catch (System.Exception ex)
		//			{
		//				try
		//				{
		//					// Rollback if transactions pending
		//					if (null != m_Persisters)
		//					{
		//						for (int i=0; i < m_Persisters.Length; i++)
		//							for (int j = 0; j < m_Persisters[i].Length; j++)			
		//								if (m_Persisters[i][j].IsTransferInProgress)
		//									m_Persisters[i][j].CancelTransfer();
		//					}
		//				}
		//				finally
		//				{
		//					// make sure we raise that exception on up
		//					throw ex;
		//				}
		//			}
		//		}

		//		#region Events
		//
		//		/// <summary>
		//		/// Raise an invalid row event
		//		/// </summary>
		//		/// <param name="e"></param>
		//		protected virtual void OnInvalidDataRow(InvalidDataRowEventArgs e)
		//		{
		//			if (InvalidDataRow != null)
		//				InvalidDataRow(this, e);
		//		}
		//
		//		#endregion

		#endregion
	}
}
