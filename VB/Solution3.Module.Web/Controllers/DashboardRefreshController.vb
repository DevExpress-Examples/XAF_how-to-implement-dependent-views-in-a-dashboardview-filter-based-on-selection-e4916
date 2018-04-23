Imports System
Imports System.Linq
Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.Web
Imports System.Collections.Generic
Imports DevExpress.ExpressApp.Editors
Imports DevExpress.ExpressApp.Web.Templates
Imports DevExpress.ExpressApp.Web.Editors.ASPx

Namespace Solution3.Module.Web.Controllers
    Public Class DashboardRefreshController
        Inherits ViewController(Of DashboardView)

        Public Const FilterSourceID As String = "FilterSource"
        Protected Overrides Sub OnViewControlsCreated()
            MyBase.OnViewControlsCreated()
            If WebWindow.CurrentRequestWindow IsNot Nothing Then
                RemoveHandler WebWindow.CurrentRequestWindow.PagePreRender, AddressOf CurrentRequestWindow_PagePreRender
                AddHandler WebWindow.CurrentRequestWindow.PagePreRender, AddressOf CurrentRequestWindow_PagePreRender
            End If
        End Sub
        Private Sub CurrentRequestWindow_PagePreRender(ByVal sender As Object, ByVal e As EventArgs)
            Dim SourceItem = CType(View.FindItem(FilterSourceID), DashboardViewItem)
            If SourceItem.InnerView IsNot Nothing Then
                Dim gridView = CType((TryCast(SourceItem.InnerView, ListView)).Editor, ASPxGridListEditor).Grid
                If gridView IsNot Nothing Then
                    Dim CallbackJavaScript = gridView.ClientSideEvents.SelectionChanged.Insert(gridView.ClientSideEvents.SelectionChanged.LastIndexOf("}"), DirectCast(gridView.Page, ICallbackManagerHolder).CallbackManager.GetScript())
                    gridView.ClientSideEvents.SelectionChanged = CallbackJavaScript
                End If
            End If
        End Sub
        Protected Overrides Sub OnDeactivated()
            RemoveHandler WebWindow.CurrentRequestWindow.PagePreRender, AddressOf CurrentRequestWindow_PagePreRender
            MyBase.OnDeactivated()
        End Sub
    End Class
End Namespace
