using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace UnitTests
{
    public class TestViewDataContainer : IViewDataContainer
    {
        private ViewDataDictionary _viewData = new ViewDataDictionary();
        public ViewDataDictionary ViewData { get { return _viewData; } set { _viewData = value; } }
    }
}
