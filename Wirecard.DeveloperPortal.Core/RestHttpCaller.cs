using System;
using System.Net;
using System.Net.Http;
using System.Xml;
using Wirecard.DeveloperPortal.Core.Request;
using Newtonsoft.Json;

namespace Wirecard.DeveloperPortal.Core
{
    /// <summary>
    ///XML çağrıları için oluşturulan xml'i verilen adrese post eden sınıftır. Response sonucu oluşan xml çıktısı execute metoduna gönderilir.
    /// </summary>
    public class RestHttpCaller
    {
        public static RestHttpCaller Create()
        {
            return new RestHttpCaller();
        }

        
        public string PostXMLString(String url, object request)
        {
            HttpClient httpClient = new HttpClient();
            var xml = XmlBuilder.SerializeToXMLString(request);
            HttpResponseMessage httpResponseMessage = httpClient.PostAsync(url, xml).Result;
            var a = httpResponseMessage.Content.ReadAsStringAsync().Result;
            return a;
        }
    }
}