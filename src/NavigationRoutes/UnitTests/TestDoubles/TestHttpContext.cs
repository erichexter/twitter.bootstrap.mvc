using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace UnitTests
{
    public class TestHttpContext : HttpContextBase
    {
        HttpRequestBase _httpRequest = new TestHttpRequest();
        HttpResponseBase _httpResponse = new TestHttpResponse();

        public override HttpRequestBase Request
        {
            get
            {
                return _httpRequest;
            }
        }

        public override HttpResponseBase Response
        {
            get
            {
                return _httpResponse;
            }
        }
    }
}
