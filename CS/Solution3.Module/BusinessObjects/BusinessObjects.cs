using System;
using System.Linq;
using DevExpress.Xpo;
using System.ComponentModel;
using DevExpress.Persistent.Base;
using System.Collections.Generic;
using DevExpress.Persistent.BaseImpl;

// With XPO, the data model is declared by classes (so-called Persistent Objects) that will define the database structure, and consequently, the user interface (http://documentation.devexpress.com/#Xaf/CustomDocument2600).
namespace Solution4.Module.BusinessObjects {
    public enum TitleOfCourtesy { Dr, Miss, Mr, Mrs, Ms };

    [DefaultClassOptions]
    public class Contact : Person {
        private TitleOfCourtesy titleOfCourtesy;
        public Contact(Session session) : base(session) { }
        public TitleOfCourtesy TitleOfCourtesy {
            get { return titleOfCourtesy; }
            set { SetPropertyValue("TitleOfCourtesy", ref titleOfCourtesy, value); }
        }
        private Position position;
        [ImmediatePostDataAttribute]
        public Position Position {
            get { return position; }
            set { SetPropertyValue("Position", ref position, value); }
        }
    }

    [DefaultClassOptions]
    [DefaultProperty("Title")]
    public class Position : BaseObject {
        public Position(Session session) : base(session) { }
        private string title;
        public string Title {
            get { return title; }
            set { SetPropertyValue("Title", ref title, value); }
        }
    }
}
