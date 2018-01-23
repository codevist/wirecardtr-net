using System.Xml.Serialization;

namespace Wirecard.DeveloperPortal.Core.Entity
{
    /// <summary>
    /// Tüm xml ve soap çağrılarında kullanılan token sınıfını temsil eder.
    /// </summary>
    public class Token
    {
        [XmlElement("UserCode")]
        public string UserCode { get; set; }
        [XmlElement("Pin")]
        public string Pin { get; set; }
    }
}
