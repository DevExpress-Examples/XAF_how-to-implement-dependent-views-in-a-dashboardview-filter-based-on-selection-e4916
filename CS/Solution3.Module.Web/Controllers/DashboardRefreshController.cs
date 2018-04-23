using System;
using System.Linq;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Web;
using System.Collections.Generic;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Web.Templates;
using DevExpress.ExpressApp.Web.Editors.ASPx;

namespace Solution3.Module.Web.Controllers {
    public class DashboardRefreshController : ViewController<DashboardView> {
        public const string FilterSourceID = "FilterSource";
        protected override void OnViewControlsCreated() {
            base.OnViewControlsCreated();
            if(WebWindow.CurrentRequestWindow != null) {
                WebWindow.CurrentRequestWindow.PagePreRender -= CurrentRequestWindow_PagePreRender;
                WebWindow.CurrentRequestWindow.PagePreRender += CurrentRequestWindow_PagePreRender;
            }
        }
        private void CurrentRequestWindow_PagePreRender(object sender, EventArgs e) {
            var SourceItem = (DashboardViewItem)View.FindItem(FilterSourceID);
            if (SourceItem.InnerView != null) {
                var gridView = ((ASPxGridListEditor)(SourceItem.InnerView as ListView).Editor).Grid;
                if (gridView != null) {
                    var CallbackJavaScript = gridView.ClientSideEvents.SelectionChanged.Insert(gridView.ClientSideEvents.SelectionChanged.LastIndexOf("}"), ((ICallbackManagerHolder)gridView.Page).CallbackManager.GetScript());
                    gridView.ClientSideEvents.SelectionChanged = CallbackJavaScript;
                }
            }
        }
        protected override void OnDeactivated() {
            WebWindow.CurrentRequestWindow.PagePreRender -= CurrentRequestWindow_PagePreRender;
            base.OnDeactivated();
        }
    }
}
