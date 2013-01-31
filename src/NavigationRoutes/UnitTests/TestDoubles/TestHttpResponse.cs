using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace UnitTests
{
    public class TestHttpResponse : HttpResponseBase
    {
        public override string ApplyAppPathModifier(string virtualPath)
        {
            return virtualPath;
        }
    }
}
