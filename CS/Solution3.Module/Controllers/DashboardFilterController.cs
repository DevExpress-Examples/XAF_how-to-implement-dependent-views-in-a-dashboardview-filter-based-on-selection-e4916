using System;
using System.Linq;
using DevExpress.ExpressApp;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.SystemModule;


namespace Solution3.Module.Controllers {
    // For more typical usage scenarios, be sure to check out http://documentation.devexpress.com/#Xaf/clsDevExpressExpressAppViewControllertopic.
    public class DashboardFilterController : ViewController<DashboardView> {
        private const string DashboardViewId = "DV";
        private DashboardViewItem SourceItem;
        private DashboardViewItem TargetItem;
        private const string CriteriaName = "Test";

        private void FilterDetailListView(ListView masterListView, ListView detailListView) {
            detailListView.CollectionSource.Criteria.Clear();
            List<object> searchedObjects = new List<object>();
            foreach(object obj in masterListView.SelectedObjects) {
                searchedObjects.Add(detailListView.ObjectSpace.GetKeyValue(obj));
            }
            if(searchedObjects.Count > 0) {
                detailListView.CollectionSource.Criteria[CriteriaName] = new InOperator("Position.Oid", searchedObjects);
            }
        }
        private void SourceItem_ControlCreated(object sender, EventArgs e) {
            DashboardViewItem dashboardItem = (DashboardViewItem)sender;
            ListView innerListView = dashboardItem.InnerView as ListView;
            if(innerListView != null) {
                innerListView.SelectionChanged -= innerListView_SelectionChanged;
                innerListView.SelectionChanged += innerListView_SelectionChanged;
            }
        }
        private void innerListView_SelectionChanged(object sender, EventArgs e) {
            FilterDetailListView((ListView)SourceItem.InnerView, (ListView)TargetItem.InnerView);
        }
        private void DisableNavigationActions(Frame frame) {
            RecordsNavigationController recordsNavigationController = frame.GetController<RecordsNavigationController>();
            if (recordsNavigationController != null){
                recordsNavigationController.Active.SetItemValue("DashboardFiltering", false);
            }
        }

        protected override void OnActivated() {
            base.OnActivated();
            if(View.Id == DashboardViewId) {
                SourceItem = (DashboardViewItem)View.FindItem(FilterSourceID);
                TargetItem = (DashboardViewItem)View.FindItem(FilterTargetId);
                if(SourceItem != null) {
                    SourceItem.ControlCreated += SourceItem_ControlCreated;
                }
                if (TargetItem!=null)
                if (TargetItem.Frame!=null)
                    DisableNavigationActions(TargetItem.Frame);
                else
                    TargetItem.ControlCreated += (s, e) =>
                    {
                        DisableNavigationActions(TargetItem.Frame);
                    };
            }
        }
        protected override void OnDeactivated() {
            if(SourceItem != null) {
                SourceItem.ControlCreated -= SourceItem_ControlCreated;
                SourceItem = null;
            }
            TargetItem = null;
            base.OnDeactivated();
        }
        public DashboardFilterController() {
            FilterSourceID = "FilterSource";
            FilterTargetId = "FilterTarget";
        }
        public string FilterSourceID { get; set; }
        public string FilterTargetId { get; set; }
    }
}