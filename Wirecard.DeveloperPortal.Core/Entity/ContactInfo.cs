using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Wirecard.DeveloperPortal.Core.Entity
{

    /// <summary>
    /// Pazaryeri oluşturmak ve güncellemek için kullanılan Contact info bilgilerini temsil eden sınıf.
    /// </summary>
    public class ContactInfo
    {
        [XmlElement("Country")]
        public string Country { get; set; }
        [XmlElement("City")]
        public string City { get; set; }
        [XmlElement("Address")]
        public string Address { get; set; }
        [XmlElement("BusinessPhone")]
        public string BusinessPhone { get; set; }
        [XmlElement("MobilePhone")]
        public string MobilePhone { get; set; }
    }
}
