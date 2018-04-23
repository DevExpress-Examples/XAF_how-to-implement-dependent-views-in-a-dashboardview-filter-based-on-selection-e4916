Imports System
Imports System.Linq
Imports DevExpress.Xpo
Imports System.ComponentModel
Imports DevExpress.Persistent.Base
Imports System.Collections.Generic
Imports DevExpress.Persistent.BaseImpl

' With XPO, the data model is declared by classes (so-called Persistent Objects) that will define the database structure, and consequently, the user interface (http://documentation.devexpress.com/#Xaf/CustomDocument2600).
Namespace Solution4.Module.BusinessObjects
    Public Enum TitleOfCourtesy
        Dr
        Miss
        Mr
        Mrs
        Ms
    End Enum

    <DefaultClassOptions> _
    Public Class Contact
        Inherits Person


        Private titleOfCourtesy_Renamed As TitleOfCourtesy
        Public Sub New(ByVal session As Session)
            MyBase.New(session)
        End Sub
        Public Property TitleOfCourtesy() As TitleOfCourtesy
            Get
                Return titleOfCourtesy_Renamed
            End Get
            Set(ByVal value As TitleOfCourtesy)
                SetPropertyValue("TitleOfCourtesy", titleOfCourtesy_Renamed, value)
            End Set
        End Property

        Private position_Renamed As Position
        <ImmediatePostDataAttribute> _
        Public Property Position() As Position
            Get
                Return position_Renamed
            End Get
            Set(ByVal value As Position)
                SetPropertyValue("Position", position_Renamed, value)
            End Set
        End Property
    End Class

    <DefaultClassOptions, DefaultProperty("Title")> _
    Public Class Position
        Inherits BaseObject

        Public Sub New(ByVal session As Session)
            MyBase.New(session)
        End Sub

        Private title_Renamed As String
        Public Property Title() As String
            Get
                Return title_Renamed
            End Get
            Set(ByVal value As String)
                SetPropertyValue("Title", title_Renamed, value)
            End Set
        End Property
    End Class
End Namespace
