

Imports System
Imports System.Data.SqlClient

Namespace Events

'*********************************************************************
'
' EventInfo Class
'
' Represents all information about a particular event. 
'
'*********************************************************************

Public Class EventInfo
    Inherits ContentInfo

    Private _link As String = String.Empty
    Private _fullDescription As String
    Private _location As String
    Private _speaker As String
    Private _speakerBiography As String
    Private _date As DateTime
    Private _dateVisible As DateTime
    Private _imageID As Integer = -1
    Private _imageName As String = String.Empty



    '*********************************************************************
    '
    ' EventInfo Constructor
    '
    ' Initializes event information from a SqlDataReader. 
    '
    '*********************************************************************
    Public Sub New(ByVal dr As SqlDataReader)
        MyBase.New(dr)
        ' Populate required fields
        _date = CType(dr("ContentPage_SortOrder"), DateTime)
        _dateVisible = CType(dr("ContentPage_DateVisible"), DateTime)
        _location = CStr(dr("Event_Location"))
        _speaker = CStr(dr("Event_Speaker"))
        _speakerBiography = CStr(dr("Event_SpeakerBiography"))

        ' Populate optional fields
        If Not IsDBNull(dr("Event_Link")) Then
            _link = CStr(dr("Event_Link"))
        End If
        If Not IsDBNull(dr("Event_FullDescription")) Then
            _fullDescription = CStr(dr("Event_FullDescription"))
        End If
        If Not IsDBNull(dr("Image_ID")) Then
            _imageID = Fix(dr("Image_ID"))
        End If
        If Not IsDBNull(dr("Image_FileName")) Then
            _imageName = CStr(dr("Image_FileName"))
        End If
    End Sub 'New 

    '*********************************************************************
    '
    ' Date Property
    '
    ' The date of the event. 
    '
    '*********************************************************************

    Public Property [Date]() As DateTime
        Get
            Return _date
        End Get
        Set(ByVal Value As DateTime)
            _date = Value
        End Set
    End Property

    '*********************************************************************
    '
    ' DateVisible Property
    '
    ' The date when the event will be displayed. 
    '
    '*********************************************************************

    Public Property DateVisible() As DateTime
        Get
            Return _dateVisible
        End Get
        Set(ByVal Value As DateTime)
            _dateVisible = Value
        End Set
    End Property

    '*********************************************************************
    '
    ' Link Property
    '
    ' The URL for additional event information. 
    '
    '*********************************************************************

    Public Property Link() As String
        Get
            Return _link
        End Get
        Set(ByVal Value As String)
            _link = Value
        End Set
    End Property

    '*********************************************************************
    '
    ' FullDescription Property
    '
    ' The full description of the event. 
    '
    '*********************************************************************

    Public Property FullDescription() As String
        Get
            Return _fullDescription
        End Get
        Set(ByVal Value As String)
            _fullDescription = Value
        End Set
    End Property


    '*********************************************************************
    '
    ' Location Property
    '
    ' The location of the event. 
    '
    '*********************************************************************

    Public Property Location() As String
        Get
            Return _location
        End Get
        Set(ByVal Value As String)
            _location = Value
        End Set
    End Property


    '*********************************************************************
    '
    ' Speaker Property
    '
    ' The speaker for the event. 
    '
    '*********************************************************************

    Public Property Speaker() As String
        Get
            Return _speaker
        End Get
        Set(ByVal Value As String)
            _speaker = Value
        End Set
    End Property

    '*********************************************************************
    '
    ' SpeakerBiography Property
    '
    ' The speaker biography. 
    '
    '*********************************************************************

    Public Property SpeakerBiography() As String
        Get
            Return _speakerBiography
        End Get
        Set(ByVal Value As String)
            _speakerBiography = Value
        End Set
    End Property

    '*********************************************************************
    '
    ' ImageID Property
    '
    ' The id of an image associated with the event. 
    '
    '*********************************************************************

    Public Property ImageID() As Integer
        Get
            Return _imageID
        End Get
        Set(ByVal Value As Integer)
            _imageID = Value
        End Set
    End Property

    '*********************************************************************
    '
    ' ImageName Property
    '
    ' The name of an image associated with the event. 
    '
    '*********************************************************************

    Public Property ImageName() As String
        Get
            Return _imageName
        End Get
        Set(ByVal Value As String)
            _imageName = Value
        End Set
    End Property
End Class 'EventInfo
End Namespace