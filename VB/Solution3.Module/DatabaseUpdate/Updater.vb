Imports System
Imports System.Linq
Imports DevExpress.Xpo
Imports DevExpress.ExpressApp
Imports DevExpress.Data.Filtering
Imports DevExpress.ExpressApp.Xpo
Imports DevExpress.Persistent.Base
Imports DevExpress.ExpressApp.Updating
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.ExpressApp.Security
Imports Solution4.Module.BusinessObjects
'using DevExpress.ExpressApp.Reports;
'using DevExpress.ExpressApp.PivotChart;
'using DevExpress.ExpressApp.Security.Strategy;
'using Solution3.Module.BusinessObjects;

Namespace Solution3.Module.DatabaseUpdate
    ' For more typical usage scenarios, be sure to check out http://documentation.devexpress.com/#Xaf/clsDevExpressExpressAppUpdatingModuleUpdatertopic
    Public Class Updater
        Inherits ModuleUpdater

        Public Sub New(ByVal objectSpace As IObjectSpace, ByVal currentDBVersion As Version)
            MyBase.New(objectSpace, currentDBVersion)
        End Sub
        Public Overrides Sub UpdateDatabaseAfterUpdateSchema()
            MyBase.UpdateDatabaseAfterUpdateSchema()
            Dim Customer As Position = ObjectSpace.FindObject(Of Position)(New BinaryOperator("Title", "Customer", BinaryOperatorType.Equal))
            If Customer Is Nothing Then
                Customer = ObjectSpace.CreateObject(Of Position)()
                Customer.Title = "Customer"
            End If
            Dim Manager As Position = ObjectSpace.FindObject(Of Position)(New BinaryOperator("Title", "Manager", BinaryOperatorType.Equal))
            If Manager Is Nothing Then
                Manager = ObjectSpace.CreateObject(Of Position)()
                Manager.Title = "Manager"
            End If
            Dim AlexSmith As Contact = ObjectSpace.FindObject(Of Contact)(New BinaryOperator("FirstName", "Alex", BinaryOperatorType.Equal))
            If AlexSmith Is Nothing Then
                AlexSmith = ObjectSpace.CreateObject(Of Contact)()
                AlexSmith.FirstName = "Alex"
                AlexSmith.LastName = "Smith"
                AlexSmith.Position = Manager
                AlexSmith.TitleOfCourtesy = TitleOfCourtesy.Dr
            End If
            Dim BrianGrant As Contact = ObjectSpace.FindObject(Of Contact)(New BinaryOperator("FirstName", "Brian", BinaryOperatorType.Equal))
            If BrianGrant Is Nothing Then
                BrianGrant = ObjectSpace.CreateObject(Of Contact)()
                BrianGrant.FirstName = "Brian"
                BrianGrant.LastName = "Grant"
                BrianGrant.Position = Customer
                BrianGrant.TitleOfCourtesy = TitleOfCourtesy.Mr
            End If
            Dim ElizabetMur As Contact = ObjectSpace.FindObject(Of Contact)(New BinaryOperator("FirstName", "Elizabet", BinaryOperatorType.Equal))
            If ElizabetMur Is Nothing Then
                ElizabetMur = ObjectSpace.CreateObject(Of Contact)()
                ElizabetMur.FirstName = "Elizabet"
                ElizabetMur.LastName = "Mur"
                ElizabetMur.Position = Customer
                ElizabetMur.TitleOfCourtesy = TitleOfCourtesy.Miss
            End If
        End Sub

    End Class
End Namespace
