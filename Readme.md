<!-- default file list -->
*Files to look at*:

* [DashboardRefreshController.cs](./CS/Solution3.Module.Web/Controllers/DashboardRefreshController.cs) (VB: [DashboardRefreshController.vb](./VB/Solution3.Module.Web/Controllers/DashboardRefreshController.vb))
* **[DashboardFilterController.cs](./CS/Solution3.Module/Controllers/DashboardFilterController.cs) (VB: [DashboardFilterController.vb](./VB/Solution3.Module/Controllers/DashboardFilterController.vb))**
<!-- default file list end -->
# How to implement dependent views in a DashboardView (filter based on selection)


<p>This example illustrates how to filter a ListView displayed in a <strong>DashboardView</strong> based on another ListView's selection.</p>
<p><strong>Scenario</strong><br /> When a <a href="http://documentation.devexpress.com/#xaf/clsDevExpressExpressAppDashboardViewtopic"><u>DashboardView</u></a> contains several list views, it is often required to make them dependent, e.g. display items of one ListView based on items or selection of another ListView.</p>
<p><img src="https://raw.githubusercontent.com/DevExpress-Examples/how-to-implement-dependent-views-in-a-dashboardview-filter-based-on-selection-e4916/15.1.3+/media/102b0bfd-189f-11e4-80b8-00155d624807.png"></p>
<p><strong>Steps to implement</strong><br /> <strong>1. </strong>Add a new <a href="http://documentation.devexpress.com/#Xaf/clsDevExpressExpressAppViewControllertopic"><u>ViewController</u></a> to your platform-agnostic module (<strong>DashboardFilterController</strong>).<br /> <strong>2.</strong> In the <strong>OnActivated</strong> method retrieve the necessary <a href="http://documentation.devexpress.com/#Xaf/clsDevExpressExpressAppEditorsDashboardViewItemtopic"><u>DashboardViewItems</u></a> via the <a href="http://documentation.devexpress.com/#Xaf/DevExpressExpressAppCompositeView_FindItemtopic"><u>FindItem</u></a> method. After that subscribe to the <strong>ControlCreated</strong> event of the DashboardViewItem whose ListView will be used for filtering (hereinafter referred to as <strong>SourceView</strong>).<br /> <strong>3. </strong>In the <strong>ControlCreated</strong> event handler retrieve the <strong>SourceView</strong> via the <a href="http://documentation.devexpress.com/#Xaf/DevExpressExpressAppEditorsDashboardViewItem_InnerViewtopic"><u>DashboardViewItem.InnerView</u></a> property and subscribe to its <a href="http://documentation.devexpress.com/#Xaf/DevExpressExpressAppView_SelectionChangedtopic"><u>SelectionChanged</u></a> event.<br /> <strong>4. </strong>In the <strong>SelectionChanged</strong> event handler retrieve the View that will be filtered (hereinafter referred to as <strong>TargetView</strong>) in the same manner as in the previous step.<br /> <strong>5. </strong>To retrieve an object by which filtering will be performed, use the <a href="http://documentation.devexpress.com/#Xaf/DevExpressExpressAppListView_CurrentObjecttopic"><u>ListView.CurrentObject</u></a> property. This object must be loaded from<strong> SourceView ObjectSpace to TargetView ObjectSpace</strong> via the <a href="http://documentation.devexpress.com/#Xaf/DevExpressExpressAppIObjectSpace_GetObjecttopic"><u>GetObject</u></a> method.<br /> <strong>6. </strong>Now you can simply filter the <strong>TargetView</strong> by adding <strong>CriteriaOperator</strong> to the <a href="http://documentation.devexpress.com/#Xaf/DevExpressExpressAppCollectionSourceBase_Criteriatopic"><u>TargetView.CollectionSource.Criteria</u></a> dictionary. In my example, I created a simple <strong>InOperator</strong> to filter the Position column via objects from SourceView.</p>
<p><strong>Important notes:<br /> </strong>The provided algorithm is platform-agnostic. It will work without any additional code in WinForms applications, but in ASP.NET the TargetView will not be refreshed immediately after changing the SourceView's selection. It is required to post changes made on the server side to the client to show them in the browser. That is why I have implemented the <strong>DashboardViewRefreshController</strong> that will refresh DashboardView when selection in the SourceView is changed. Handle the <strong>WebWindow.CurrentRequestWindow.PagePreRender</strong> event. In this event handler, find the <strong>ASPxGridView</strong>, modify its <strong>client-side SelectionChanged</strong> event and the script using the <strong>CallbackManager</strong> as shown in the <em>DashboardRefreshController.cs</em> file.<br /> Then deactivate the <a href="http://documentation.devexpress.com/#xaf/clsDevExpressExpressAppSystemModuleRecordsNavigationControllertopic"><u>RecordsNavigationController</u></a> to prevent its activation for the <strong>TargetView in the DashboardView</strong>. This controller can lead to an <strong>exception</strong> during navigation <strong>to the record's DetailView on a click in the SourceView</strong>. This behavior is caused by the fact that <strong>RecordsNavigationController</strong> lies inside the <strong>ListView </strong>and collects information about records when this <strong>ListView</strong> is disposed of. It collects records based on visible rows, but since we filter the <strong>CollectionSource,</strong> it does not contain the required object.</p>

<br/>


