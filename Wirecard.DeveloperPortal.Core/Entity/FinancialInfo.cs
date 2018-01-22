using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Wirecard.DeveloperPortal.Core.Entity
{
    public class FinancialInfo
    {
        [XmlElement("IdentityNumber")]
        public string IdentityNumber { get; set; }
        [XmlElement("TaxOffice")]
        public string TaxOffice { get; set; }
        [XmlElement("TaxNumber")]
        public string TaxNumber { get; set; }
        [XmlElement("BankName")]
        public string BankName { get; set; }
        [XmlElement("IBAN")]
        public string IBAN { get; set; }
        [XmlElement("AccountName")]
        public string AccountName { get; set; }

    }
}
