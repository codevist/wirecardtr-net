using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using Wirecard.DeveloperPortal.Core.Request;

namespace Wirecard.DeveloperPortal.Core
{
    /// <summary>
    /// Istekleri XML olarak oluşturucak sınıftır. 
    /// </summary>
    public class XmlBuilder
    {
        public static StringContent SerializeToXMLString(object request)
        {
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            var stringwriter = new System.IO.StringWriter();
            var serializer = new XmlSerializer(request.GetType());
            serializer.Serialize(stringwriter, request,ns);
            var str = stringwriter.ToString().Replace("utf-16", "utf-8");
            return new StringContent(str, Encoding.UTF8, "application/xml");
        }
        public static T DeserializeObject<T>(string xmlString)
        {
            try
            {
                XmlSerializer xs = new XmlSerializer(typeof(T));
                MemoryStream memoryStream = new MemoryStream(StringToUTF8ByteArray(xmlString));
                XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);

                return (T)Convert.ChangeType(xs.Deserialize(memoryStream), typeof(T));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static Byte[] StringToUTF8ByteArray(String xmlString)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            Byte[] byteArray = encoding.GetBytes(xmlString);
            return byteArray;
        }
        public static string GetXMLFromObject(object o)
        {
            StringWriter sw = new StringWriter();
            XmlTextWriter tw = null;
            try
            {
                XmlSerializer serializer = new XmlSerializer(o.GetType());
                tw = new XmlTextWriter(sw);
                serializer.Serialize(tw, o);
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
            finally
            {
                sw.Close();
                if (tw != null)
                {
                    tw.Close();
                }
            }
            return sw.ToString();
        }

        public static string GetXMLFromObjectWCFService(object o)
        {
            StringWriter sw = new StringWriter();
            XmlTextWriter tw = null;
            try
            {
                XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                ns.Add("", "");
                XmlSerializer serializer = new XmlSerializer(o.GetType(), "");
                tw = new XmlTextWriter(sw);
                serializer.Serialize(tw, o, ns);
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
            finally
            {
                sw.Close();
                if (tw != null)
                {
                    tw.Close();
                }
            }
            return DeleteNameSpace3Pay(sw.ToString());
        }
        public static string DeleteNameSpace3Pay(string xmlString)
        {
            return xmlString.Replace("xmlns=\"http://www.wirecard.com.tr/services/\"", "");
        }
    }
}
