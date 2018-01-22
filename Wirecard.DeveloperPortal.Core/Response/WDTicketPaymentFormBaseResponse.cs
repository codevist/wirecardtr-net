using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wirecard.DeveloperPortal.Core.Response
{
    public class WDTicketPaymentFormBaseResponse
    {
        public string OrderId { get; set; }
        public string MPay { get; set; }
        public string StatusCode { get; set; }
        public string ResultCode { get; set; }
        public string ResultMessage { get; set; }
        public string LastTransactionDate { get; set; }
        public string MaskedCCNo { get; set; }
        public string ExtraParam { get; set; }
        public string CCTokenId { get; set; }
    }
}
