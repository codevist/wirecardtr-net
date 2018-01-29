using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Wirecard.DeveloperPortal.Core.Entity
{

    public class CardTokenization
    {
        [XmlElement("RequestType")]
        public int RequestType { get; set; }
        [XmlElement("CustomerId")]
        public string CustomerId { get; set; }
        [XmlElement("ValidityPeriod")]
        public int ValidityPeriod { get; set; }
        [XmlElement("CCTokenId")]
        public Guid CCTokenId { get; set; }
    }
}
