using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Wirecard.DeveloperPortal.Core.Entity;

namespace Wirecard.DeveloperPortal.Core.Request
{
    /// <summary>
    /// Pazaryeri 3d secure ve 3d secure olmadan ödeme servis çağrısının başlatılması için gerekli olan alanların bulunduğu sınıftır.
    /// Bu sınıf içerisinde Execute metodunda parametre olarak gönderilen MarketPlaceDeactiveRequest sınıfı verileri xml formatına çevrilerek settings sınıfı içerisinde bulunan url adresine post edilir.
    /// </summary>
    [XmlRoot("WIRECARD")]
    public class MarketPlaceSale3DSecOrMpSaleRequest
    {
        [XmlElement("ServiceType")]
        public string ServiceType { get; set; }
        [XmlElement("OperationType")]
        public string OperationType { get; set; }
        [XmlElement("Token")]
        public Token Token { get; set; }
        [XmlElement("CreditCardInfo")]
        public CreditCardInfo CreditCardInfo { get; set; }

        [XmlElement("Price")]
        public int Price { get; set; }
        [XmlElement("MPAY")]
        public string MPAY { get; set; }
        [XmlElement("IPAddress")]
        public string IPAddress { get; set; }
        [XmlElement("Port")]
        public string Port { get; set; }
        [XmlElement("Description")]
        public string Description { get; set; }
        [XmlElement("InstallmentCount")]
        public int InstallmentCount { get; set; }
        [XmlElement("ErrorURL")]
        public string ErrorURL { get; set; }
        [XmlElement("SuccessURL")]
        public string SuccessURL { get; set; }

        [XmlElement("CommissionRate")]
        public int CommissionRate { get; set; }

        [XmlElement("ExtraParam")]
        public string ExtraParam { get; set; }
        [XmlElement("PaymentContent")]
        public string PaymentContent { get; set; }

        [XmlElement("SubPartnerId")]
        public int SubPartnerId { get; set; }

        [XmlElement("CardTokenization")]
        public CardTokenization CardTokenization { get; set; }

        public static string Execute(MarketPlaceSale3DSecOrMpSaleRequest request,Settings options)
        {
            return RestHttpCaller.Create().PostXMLString(options.BaseUrl, request);
        }
    }
}