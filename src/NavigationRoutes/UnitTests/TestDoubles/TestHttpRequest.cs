using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web;

namespace UnitTests
{
    public class TestHttpRequest : HttpRequestBase
    {
        NameValueCollection _serverVariables = new NameValueCollection();

        public override string ApplicationPath
        {
            get
            {
                return "";
            }
        }

        public override NameValueCollection ServerVariables
        {
            get
            {
                return _serverVariables;
            }
        }
    }
}
