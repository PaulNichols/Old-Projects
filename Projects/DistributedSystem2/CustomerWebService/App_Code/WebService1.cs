using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;

namespace CustomerWebService
{
    [System.Web.Services.WebServiceBinding(Name = "WebService1", ConformsTo = System.Web.Services.WsiProfiles.BasicProfile1_1, EmitConformanceClaims = true), System.Web.Services.Protocols.SoapDocumentService()]
    public class WebService1 : System.Web.Services.WebService
    {
        [System.Web.Services.WebMethod(), System.Web.Services.Protocols.SoapDocumentMethod(Binding = "WebService1")]
        public Customer GetCustomer()
        {
            throw new System.NotImplementedException();
        }
    }
}
