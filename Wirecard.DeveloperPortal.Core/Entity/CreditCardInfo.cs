using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Wirecard.DeveloperPortal.Core.Entity
{

    /// <summary>
    /// Pazaryeri 3D secure ve 3d secure olmadan ödeme yapabilmek için
    /// ve Ödeme formu ödeme işleminin başlatılması için gerekli olan kredi kartı bilgilerinin bulunduğu sınıf
    /// </summary>
    public class CreditCardInfo
    {
        [XmlElement("CreditCardNo")]
        public string CreditCardNo { get; set; }
        [XmlElement("OwnerName")]
        public string OwnerName { get; set; }
        [XmlElement("ExpireYear")]
        public int ExpireYear { get; set; }
        [XmlElement("ExpireMonth")]
        public int ExpireMonth { get; set; }

        [XmlElement("Cvv")]
        public string Cvv { get; set; }
        [XmlElement("Price")]
        public int Price { get; set; }
    }
}
