using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Wirecard.DeveloperPortal.Core.Response
{

    public class WDTicketPaymentFormResponse
    {
        public string Response { get; set; }
        public string RedirectUrl { get; set; }
    }
}