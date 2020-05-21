Namespace Application.Bird.Registration
    <Serializable()> _
    Public Class Statements
        Public Sub New()
        End Sub

        Private Const INSPECTOR_SIGNED As String = "InspectorSignedDORConfirmMarksFitted"
        Private Const KEEPER_CONFIRMED As String = "KeeperConfirmsIDMarksFitted"

        Friend Shared Function CreateStatement(ByVal statementRows As DataRow()) As Statements
            'create a new object
            Dim NewStatement As New Statements
            For Each StatementRow As DataRow In statementRows
                If StatementRow.IsNull(INSPECTOR_SIGNED) Then
                    NewStatement.InspectorSignedDORConfirmMarksFitted = False
                Else
                    NewStatement.InspectorSignedDORConfirmMarksFitted = CType(StatementRow.Item(INSPECTOR_SIGNED), Boolean)
                End If
                If StatementRow.IsNull(KEEPER_CONFIRMED) Then
                    NewStatement.KeeperConfirmsIDMarksFitted = False
                Else
                    NewStatement.KeeperConfirmsIDMarksFitted = CType(StatementRow.Item(KEEPER_CONFIRMED), Boolean)
                End If
                'should only be one row, so bail after
                Exit For
            Next StatementRow
            Return NewStatement
        End Function

        Friend Sub UpdateStatement(ByVal newStatementRow As DataRow)
            With newStatementRow
                .Item(INSPECTOR_SIGNED) = mInspectorSignedDORConfirmMarksFitted
                .Item(KEEPER_CONFIRMED) = mKeeperConfirmsIDMarksFitted
            End With
        End Sub

        Public Property InspectorSignedDORConfirmMarksFitted() As Boolean
            Get
                Return mInspectorSignedDORConfirmMarksFitted
            End Get
            Set(ByVal Value As Boolean)
                mInspectorSignedDORConfirmMarksFitted = Value
            End Set
        End Property
        Private mInspectorSignedDORConfirmMarksFitted As Boolean

        Public Property KeeperConfirmsIDMarksFitted() As Boolean
            Get
                Return mKeeperConfirmsIDMarksFitted
            End Get
            Set(ByVal Value As Boolean)
                mKeeperConfirmsIDMarksFitted = Value
            End Set
        End Property
        Private mKeeperConfirmsIDMarksFitted As Boolean

    End Class
End Namespace
