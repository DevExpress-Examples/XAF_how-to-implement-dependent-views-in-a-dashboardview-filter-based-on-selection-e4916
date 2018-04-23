using System;
using System.Linq;
using DevExpress.Xpo;
using DevExpress.ExpressApp;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Updating;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Security;
using Solution4.Module.BusinessObjects;
//using DevExpress.ExpressApp.Reports;
//using DevExpress.ExpressApp.PivotChart;
//using DevExpress.ExpressApp.Security.Strategy;
//using Solution3.Module.BusinessObjects;

namespace Solution3.Module.DatabaseUpdate {
    // For more typical usage scenarios, be sure to check out http://documentation.devexpress.com/#Xaf/clsDevExpressExpressAppUpdatingModuleUpdatertopic
    public class Updater : ModuleUpdater {
        public Updater(IObjectSpace objectSpace, Version currentDBVersion) :
            base(objectSpace, currentDBVersion) {
        }
        public override void UpdateDatabaseAfterUpdateSchema() {
            base.UpdateDatabaseAfterUpdateSchema();
            Position Customer = ObjectSpace.FindObject<Position>(new BinaryOperator("Title", "Customer", BinaryOperatorType.Equal));
            if(Customer == null) {
                Customer = ObjectSpace.CreateObject<Position>();
                Customer.Title = "Customer";
            }
            Position Manager = ObjectSpace.FindObject<Position>(new BinaryOperator("Title", "Manager", BinaryOperatorType.Equal));
            if(Manager == null) {
                Manager = ObjectSpace.CreateObject<Position>();
                Manager.Title = "Manager";
            }
            Contact AlexSmith = ObjectSpace.FindObject<Contact>(new BinaryOperator("FirstName", "Alex", BinaryOperatorType.Equal));
            if(AlexSmith == null) {
                AlexSmith = ObjectSpace.CreateObject<Contact>();
                AlexSmith.FirstName = "Alex";
                AlexSmith.LastName = "Smith";
                AlexSmith.Position = Manager;
                AlexSmith.TitleOfCourtesy = TitleOfCourtesy.Dr;
            }
            Contact BrianGrant = ObjectSpace.FindObject<Contact>(new BinaryOperator("FirstName", "Brian", BinaryOperatorType.Equal));
            if(BrianGrant == null) {
                BrianGrant = ObjectSpace.CreateObject<Contact>();
                BrianGrant.FirstName = "Brian";
                BrianGrant.LastName = "Grant";
                BrianGrant.Position = Customer;
                BrianGrant.TitleOfCourtesy = TitleOfCourtesy.Mr;
            }
            Contact ElizabetMur = ObjectSpace.FindObject<Contact>(new BinaryOperator("FirstName", "Elizabet", BinaryOperatorType.Equal));
            if(ElizabetMur == null) {
                ElizabetMur = ObjectSpace.CreateObject<Contact>();
                ElizabetMur.FirstName = "Elizabet";
                ElizabetMur.LastName = "Mur";
                ElizabetMur.Position = Customer;
                ElizabetMur.TitleOfCourtesy = TitleOfCourtesy.Miss;
            }
        }

    }
}
