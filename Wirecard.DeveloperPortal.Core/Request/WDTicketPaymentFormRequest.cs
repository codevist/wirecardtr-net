using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Wirecard.DeveloperPortal.Core.Entity;
using Wirecard.DeveloperPortal.Core.Response;

namespace Wirecard.DeveloperPortal.Core.Request
{
    /// <summary>
    /// Ortak ödeme formu 3d secure ve 3d secure olmadan ödeme servis çağrısının başlatılması için gerekli olan alanların bulunduğu sınıftır.
    /// Bu sınıf içerisinde Execute metodunda parametre olarak gönderilen MarketPlaceDeactiveRequest sınıfı verileri xml formatına çevrilerek settings sınıfı içerisinde bulunan url adresine post edilir.
    /// </summary>
    [XmlRoot("WIRECARD")]
    public class WDTicketPaymentFormRequest
    {
        [XmlElement("ServiceType")]
        public string ServiceType { get; set; }
        [XmlElement("OperationType")]
        public string OperationType { get; set; }
        [XmlElement("Price")]
        public int Price { get; set; }
        [XmlElement("MPAY")]
        public string MPAY { get; set; }
        [XmlElement("Description")]
        public string Description { get; set; }
        [XmlElement("ErrorURL")]
        public string ErrorURL { get; set; }
        [XmlElement("SuccessURL")]
        public string SuccessURL { get; set; }
        [XmlElement("ExtraParam")]
        public string ExtraParam { get; set; }
        [XmlElement("PaymentContent")]
        public string PaymentContent { get; set; }
        [XmlElement("PaymentTypeId")]
        public int PaymentTypeId { get; set; }

        public Token Token;

        public static string Execute(WDTicketPaymentFormRequest request, Settings options)
        {
            return RestHttpCaller.Create().PostXMLString(options.BaseUrl , request);
        }
    }
}
