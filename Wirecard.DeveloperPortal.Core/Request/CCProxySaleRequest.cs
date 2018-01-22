using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Wirecard.DeveloperPortal.Core.Entity;

namespace Wirecard.DeveloperPortal.Core.Request
{
    [XmlRoot("WIRECARD")]
    public class CCProxySaleRequest
    {
        [XmlElement("ServiceType")]
        public string ServiceType { get; set; }
        [XmlElement("OperationType")]
        public string OperationType { get; set; }
        [XmlElement("Token")]
        public Token Token { get; set; }
        [XmlElement("CreditCardInfo")]
        public CreditCardInfo CreditCardInfo { get; set; }
        [XmlElement("MPAY")]
        public string MPAY { get; set; }
        [XmlElement("IPAddress")]
        public string IPAddress { get; set; }
        [XmlElement("PaymentContent")]
        public string PaymentContent { get; set; }
        [XmlElement("InstallmentCount")]
        public int InstallmentCount { get; set; }
        [XmlElement("Description")]
        public string Description { get; set; }
        [XmlElement("ExtraParam")]
        public string ExtraParam { get; set; }


        public static string Execute(CCProxySaleRequest request, Settings options)
        {
            return RestHttpCaller.Create().PostXMLString(options.BaseUrl, request);
        }
    }
}
