using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace UnitTests
{
    public class TestViewContext: ViewContext
    {
        HttpContextBase _httpContext = new TestHttpContext();

        public override HttpContextBase HttpContext
        {
            get
            {
                return _httpContext;
            }
            set
            {
                _httpContext = value;
            }
        }
    }
}
