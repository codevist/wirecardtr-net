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
    /// Pazaryeri oluşturma ve güncelleme  Xml servis çağrısının başlatılması için gerekli olan alanların bulunduğu sınıftır.
    /// Bu sınıf içerisinde Execute metodunda parametre olarak gönderilen MarketPlaceAddOrUpdateRequest sınıfı verileri xml formatına çevrilerek settings sınıfı içerisinde bulunan url adresine post edilir.
    /// </summary>
    [XmlRoot("WIRECARD")]
    public class MarketPlaceAddOrUpdateRequest
    {
        [XmlElement("ServiceType")]
        public string ServiceType { get; set; }
        [XmlElement("OperationType")]
        public string OperationType { get; set; }
        [XmlElement("Token")]
        public Token Token { get; set; }
        [XmlElement("UniqueId")]
        public string UniqueId { get; set; }
        [XmlElement("SubPartnerType")]
        public SubPartnerTypeEnum SubPartnerType { get; set; }
        [XmlElement("Name")]
        public string Name { get; set; }
        [XmlElement("ContactInfo")]
        public ContactInfo ContactInfo { get; set; }
        [XmlElement("FinancialInfo")]
        public FinancialInfo FinancialInfo { get; set; }
        [XmlElement("SubPartnerId")]
        public int SubPartnerId { get; set; }

        public static string Execute(MarketPlaceAddOrUpdateRequest request, Settings options)
        {
            return RestHttpCaller.Create().PostXMLString(options.BaseUrl, request);
        }
    }
}