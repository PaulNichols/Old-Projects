 ' DatePicker.cs
'
Imports System
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Globalization


'/ <summary>
'/ Summary description for DatePicker.
'/ </summary>

Public Class DatePicker
    Inherits WebControl
    Implements INamingContainer 'ToDo: Add Implements Clauses for implementation methods of these interface(s)
    
    
    Private dropMonth As DropDownList
    Private dropDay As DropDownList
    Private dropYear As DropDownList
    
    
    Public Property Month() As Integer
        Get
            If ViewState("Month") Is Nothing Then
                Return DateTime.Now.Month
            Else
                Return Fix(ViewState("Month"))
            End If
        End Get
        Set
            ViewState("Month") = value
        End Set
    End Property 
    
    Public Property Day() As Integer
        Get
            If ViewState("Day") Is Nothing Then
                Return DateTime.Now.Day
            Else
                Return Fix(ViewState("Day"))
            End If
        End Get
        Set
            ViewState("Day") = value
        End Set
    End Property 
    
    Public Property Year() As Integer
        Get
            If ViewState("Year") Is Nothing Then
                Return DateTime.Now.Year
            Else
                Return Fix(ViewState("Year"))
            End If
        End Get
        Set
            ViewState("Year") = value
        End Set
    End Property
    
    
    Public Property [Date]() As DateTime
        Get
            EnsureChildControls()
            
            Dim _year As Integer = Int32.Parse(dropYear.SelectedItem.Value)
            Dim _month As Integer = Int32.Parse(dropMonth.SelectedItem.Value)
            Dim _day As Integer = Int32.Parse(dropDay.SelectedItem.Value)
            
            If _day > DateTime.DaysInMonth(_year, _month) Then
                Throw New ArgumentException("Invalid date!")
            End If 
            Return New DateTime(_year, _month, _day)
        End Get
        Set
            Month = value.Month
            Day = value.Day
            Year = value.Year
        End Set
    End Property
    
    
    
    Protected Overrides Sub CreateChildControls()
        dropMonth = New DropDownList()
        Controls.Add(dropMonth)
        
        dropDay = New DropDownList()
        Controls.Add(dropDay)
        
        dropYear = New DropDownList()
        Controls.Add(dropYear)
        
        If Not Page.IsPostBack Then
            Dim i As Integer

            ' Get a DateTimeFormatInfo object
            Dim objDateInfo As DateTimeFormatInfo = DateTimeFormatInfo.CurrentInfo

            ' Display Months
            For i = 1 To objDateInfo.MonthNames.Length - 1
                dropMonth.Items.Add(New ListItem(objDateInfo.GetMonthName(i), i.ToString()))
            Next i

           ' Display Days
            For i = 1 To 31
                dropDay.Items.Add(i.ToString())
            Next i

            ' Display Years
            For i = DateTime.Now.Year - 5 To (DateTime.Now.Year + 5) - 1
                dropYear.Items.Add(i.ToString())
            Next i

            dropMonth.Items.FindByValue(Month.ToString()).Selected = True
            dropDay.Items.FindByValue(Day.ToString()).Selected = True
            dropYear.Items.FindByValue(Year.ToString()).Selected = True
        End If
    End Sub 'CreateChildControls
     
    
    
    Protected Overrides Sub Render(tw As HtmlTextWriter)
        ' Get Date Parts
        Dim dateParts() As String
        Try
            Dim objDateInfo As DateTimeFormatInfo = DateTimeFormatInfo.CurrentInfo
            dateParts = objDateInfo.ShortDatePattern.Split("/"c)
        Catch
            dateParts = New String() {"m", "d", "y"}
        End Try

        tw.RenderBeginTag(HtmlTextWriterTag.Table)
        tw.RenderBeginTag(HtmlTextWriterTag.Tr)
        tw.RenderBeginTag(HtmlTextWriterTag.Td)
        RenderDatePart(dateParts(0), tw)
        tw.RenderEndTag()

        tw.RenderBeginTag(HtmlTextWriterTag.Td)
        RenderDatePart(dateParts(1), tw)
        tw.RenderEndTag()

        tw.RenderBeginTag(HtmlTextWriterTag.Td)
        RenderDatePart(dateParts(2), tw)
        tw.RenderEndTag()

        tw.RenderEndTag() ' close row
        tw.RenderEndTag() ' close table
    End Sub 'Render
    
    
    
    
    Private Sub RenderDatePart(datePart As String, tw As HtmlTextWriter)
        datePart = datePart.ToLower()
        
        If datePart.StartsWith("m") Then
            dropMonth.RenderControl(tw)
        ElseIf datePart.StartsWith("d") Then
            dropDay.RenderControl(tw)
        Else
            dropYear.RenderControl(tw)
        End If
    End Sub 'RenderDatePart 
End Class 'DatePicker 
