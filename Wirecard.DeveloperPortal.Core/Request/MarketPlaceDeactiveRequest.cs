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
    /// Pazaryeriüye işyeri silme  Xml servis çağrısının başlatılması için gerekli olan alanların bulunduğu sınıftır.
    /// Bu sınıf içerisinde Execute metodunda parametre olarak gönderilen MarketPlaceDeactiveRequest sınıfı verileri xml formatına çevrilerek settings sınıfı içerisinde bulunan url adresine post edilir.
    /// </summary>
    [XmlRoot("WIRECARD")]
    public class MarketPlaceDeactiveRequest
    {
        [XmlElement("ServiceType")]
        public string ServiceType { get; set; }
        [XmlElement("OperationType")]
        public string OperationType { get; set; }
        [XmlElement("Token")]
        public Token Token { get; set; }
        [XmlElement("UniqueId")]
        public string UniqueId { get; set; }

        public static string Execute(MarketPlaceDeactiveRequest request, Settings options)
        {
            return RestHttpCaller.Create().PostXMLString(options.BaseUrl, request);
        }
    }
}
