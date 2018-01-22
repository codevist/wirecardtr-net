﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Wirecard.DeveloperPortal.Core.Entity;

namespace Wirecard.DeveloperPortal.Core.Request
{
    [XmlRoot("WIRECARD")]
    public class MarketPlaceReleasePaymentRequest
    {
        [XmlElement("ServiceType")]
        public string ServiceType { get; set; }
        [XmlElement("OperationType")]
        public string OperationType { get; set; }
        [XmlElement("Token")]
        public Token Token { get; set; }
        [XmlElement("SubPartnerId")]
        public int SubPartnerId { get; set; }
        [XmlElement("CommissionRate")]
        public int CommissionRate { get; set; }
        [XmlElement("MPAY")]
        public string MPAY { get; set; }
        [XmlElement("OrderId")]
        public Guid OrderId { get; set; }
        [XmlElement("Description")]
        public string Description { get; set; }

        public static string Execute(MarketPlaceReleasePaymentRequest request, Settings options)
        {
            return RestHttpCaller.Create().PostXMLString(options.BaseUrl, request);
        }

    }
}
