Imports System.Reflection

Public Class EnterpriseCounters

    ' members...
    Public PerformanceObjectName As String = "MyEnterpriseApplication"
    Private _counters As New EnterpriseCounterCollection()
    Private _counterCapableObjects As New ArrayList()

    Public Sub ScanAssembly(ByVal scanAssembly As [Assembly])

        ' go through the types...
        Dim scanType As Type
        For Each scanType In scanAssembly.GetTypes()

            ' get the attributes...
            Dim counterInterface As Type
            For Each counterInterface In scanType.GetInterfaces()

                ' do we have one?
                If counterInterface Is GetType(ICounterProvider) Then

                    ' create it...
                    Try

                        ' call it and add it to the list...
                        Dim targetObject As ICounterProvider = CType(Activator.CreateInstance(scanType), ICounterProvider)
                        targetObject.CreateCounters(Me)
                        _counterCapableObjects.Add(targetObject)

                    Catch
                    End Try

                End If

            Next

        Next

    End Sub

    Public Sub ScanAssembly(ByVal seedObject As Object)
        ScanAssembly(seedObject.GetType().Assembly)
    End Sub

    Public Sub CreateCounters()
        CreateCounters(False)
    End Sub

    Public Sub CreateCounters(ByVal force As Boolean)

        ' does the object exist?
        Dim countersExist As Boolean = PerformanceCounterCategory.Exists(PerformanceObjectName)

        ' delete the category?
        If countersExist = True And force = True Then
            PerformanceCounterCategory.Delete(PerformanceObjectName)
            countersExist = False
        End If

        ' do we need to create it?
        Dim counter As EnterpriseCounter
        If countersExist = False Then

            ' create a collection...
            Dim list As New CounterCreationDataCollection()

            ' go through each counter...
            For Each counter In Counters

                ' create some new data...
                Dim data As New CounterCreationData()
                data.CounterName = counter.Name
                data.CounterHelp = counter.HelpText
                data.CounterType = counter.Type

                ' add it...
                list.Add(data)

            Next

            ' create the category and all of the counters...
            PerformanceCounterCategory.Create(PerformanceObjectName, "", list)

        End If

        ' now, go back through the counters and create instances...
        For Each counter In Counters

            ' create an instance and store it...
            counter.Counter = New PerformanceCounter(PerformanceObjectName, counter.Name, "", False)

            ' reset the value...
            counter.Counter.RawValue = 0

        Next

        ' ok, now tell all the objects that registered counters that they have been created...
        Dim counterObject As ICounterProvider
        For Each counterObject In _counterCapableObjects
            counterObject.CountersCreated(Me)
        Next
        _counterCapableObjects.Clear()

    End Sub

    Public ReadOnly Property Counters() As EnterpriseCounterCollection
        Get
            Return _counters
        End Get
    End Property

End Class
