Namespace DataObjects

    Public Class TaxonomyXMLHelper
        Public Shared Sub Insert(ByVal tablename As String, ByVal XMLDataSet As DataSet, ByVal transaction As System.Data.SqlClient.SqlTransaction)

            'create a connection...
            Dim connection As System.Data.SqlClient.SqlConnection
            If (transaction Is Nothing) Then
                connection = New System.Data.SqlClient.SqlConnection(EnterpriseObjects.EnterpriseApplication.Application.ConnectionString)
                connection.Open()
            Else
                connection = transaction.Connection
            End If
            'create a command...
            Dim command As System.Data.SqlClient.SqlCommand = New System.Data.SqlClient.SqlCommand("SELECT * FROM " & tablename & " WHERE 1 = 2", connection)
            command.CommandType = System.Data.CommandType.Text
            'check transaction...
            If (Not (transaction) Is Nothing) Then
                command.Transaction = transaction
            End If

            Dim objDSDBTable As New DataSet(tablename)
            Dim objAdapter As System.Data.SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter(command)

            objAdapter.Fill(objDSDBTable, tablename)
            Dim objDBRow As DataRow
            For Each objDataRow As DataRow In XMLDataSet.Tables(0).Rows
                With objDSDBTable.Tables(0)
                    objDBRow = .NewRow()
                    For Each col As DataColumn In XMLDataSet.Tables(0).Columns
                        objDBRow(col.ColumnName) = objDataRow(col.ColumnName)
                    Next
                    .Rows.Add(objDBRow)
                End With
            Next
            Dim ObjCmdBuilder As System.Data.SqlClient.SqlCommandBuilder
            ObjCmdBuilder = New System.Data.SqlClient.SqlCommandBuilder(objAdapter)
            objAdapter.Update(objDSDBTable, tablename)

            If (transaction Is Nothing) Then
                connection.Close()
            End If

        End Sub
    End Class
End Namespace

