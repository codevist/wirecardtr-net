using System.Xml.Serialization;

namespace Wirecard.DeveloperPortal.Core.Entity
{
    public class Token
    {
        [XmlElement("UserCode")]
        public string UserCode { get; set; }
        [XmlElement("Pin")]
        public string Pin { get; set; }
    }
}
