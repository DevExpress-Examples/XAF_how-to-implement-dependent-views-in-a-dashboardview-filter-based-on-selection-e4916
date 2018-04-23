Imports System
Imports System.Linq
Imports DevExpress.ExpressApp
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports DevExpress.ExpressApp.Editors
Imports DevExpress.ExpressApp.SystemModule


Namespace Solution3.Module.Controllers
    ' For more typical usage scenarios, be sure to check out http://documentation.devexpress.com/#Xaf/clsDevExpressExpressAppViewControllertopic.
    Public Class DashboardFilterController
        Inherits ViewController(Of DashboardView)

        Private Const DashboardViewId As String = "DV"
        Private SourceItem As DashboardViewItem
        Private TargetItem As DashboardViewItem
        Private Const CriteriaName As String = "Test"

        Private Sub FilterDetailListView(ByVal masterListView As ListView, ByVal detailListView As ListView)
            detailListView.CollectionSource.Criteria.Clear()
            Dim searchedObjects As New List(Of Object)()
            For Each obj As Object In masterListView.SelectedObjects
                searchedObjects.Add(detailListView.ObjectSpace.GetKeyValue(obj))
            Next obj
            If searchedObjects.Count > 0 Then
                detailListView.CollectionSource.Criteria(CriteriaName) = New InOperator("Position.Oid", searchedObjects)
            End If
        End Sub
        Private Sub SourceItem_ControlCreated(ByVal sender As Object, ByVal e As EventArgs)
            Dim dashboardItem As DashboardViewItem = DirectCast(sender, DashboardViewItem)
            Dim innerListView As ListView = TryCast(dashboardItem.InnerView, ListView)
            If innerListView IsNot Nothing Then
                RemoveHandler innerListView.SelectionChanged, AddressOf innerListView_SelectionChanged
                AddHandler innerListView.SelectionChanged, AddressOf innerListView_SelectionChanged
            End If
        End Sub
        Private Sub innerListView_SelectionChanged(ByVal sender As Object, ByVal e As EventArgs)
            FilterDetailListView(CType(SourceItem.InnerView, ListView), CType(TargetItem.InnerView, ListView))
        End Sub
        Private Sub DisableNavigationActions(ByVal frame As Frame)
            Dim recordsNavigationController As RecordsNavigationController = frame.GetController(Of RecordsNavigationController)()
            If recordsNavigationController IsNot Nothing Then
                recordsNavigationController.Active.SetItemValue("DashboardFiltering", False)
            End If
        End Sub

        Protected Overrides Sub OnActivated()
            MyBase.OnActivated()
            If View.Id = DashboardViewId Then
                SourceItem = CType(View.FindItem(FilterSourceID), DashboardViewItem)
                TargetItem = CType(View.FindItem(FilterTargetId), DashboardViewItem)
                If SourceItem IsNot Nothing Then
                    AddHandler SourceItem.ControlCreated, AddressOf SourceItem_ControlCreated
                End If
                If TargetItem IsNot Nothing Then
                If TargetItem.Frame IsNot Nothing Then
                    DisableNavigationActions(TargetItem.Frame)
                Else
                    AddHandler TargetItem.ControlCreated, Sub(s, e)
                        DisableNavigationActions(TargetItem.Frame)
                    End Sub
                End If
                End If
            End If
        End Sub
        Protected Overrides Sub OnDeactivated()
            If SourceItem IsNot Nothing Then
                RemoveHandler SourceItem.ControlCreated, AddressOf SourceItem_ControlCreated
                SourceItem = Nothing
            End If
            TargetItem = Nothing
            MyBase.OnDeactivated()
        End Sub
        Public Sub New()
            FilterSourceID = "FilterSource"
            FilterTargetId = "FilterTarget"
        End Sub
        Public Property FilterSourceID() As String
        Public Property FilterTargetId() As String
    End Class
End Namespace