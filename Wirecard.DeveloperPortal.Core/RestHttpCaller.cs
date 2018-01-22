using System;
using System.Net;
using System.Net.Http;
using System.Xml;
using Wirecard.DeveloperPortal.Core.Request;
using Newtonsoft.Json;

namespace Wirecard.DeveloperPortal.Core
{
    /// <summary>
    /// JSON ve XML'leri verilen adreslere post eden sınıftır. Verilen Response sınıfına göre geri dönüş yapar, 
    /// aynen kopyalanarak kullanılabilir
    /// </summary>
    public class RestHttpCaller
    {
        public static RestHttpCaller Create()
        {
            return new RestHttpCaller();
        }

       
        public T GetXML<T>(String url)
        {
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage httpResponseMessage = httpClient.GetAsync(url).Result;

            return JsonConvert.DeserializeObject<T>(httpResponseMessage.Content.ReadAsStringAsync().Result);
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