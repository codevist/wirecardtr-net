using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using Wirecard.DeveloperPortal.Core;
using Wirecard.DeveloperPortal.Core.ApiPlusAndProApiService;
using Wirecard.DeveloperPortal.Core.Entity;
using Wirecard.DeveloperPortal.Core.Request;
using Wirecard.DeveloperPortal.Core.Response;
using Wirecard.DeveloperPortal.Core.SendInformationSMSService;
using Wirecard.DeveloperPortal.Core.SubscriberService;
namespace Wirecard.DeveloperPortal.WebSamples.Controllers
{
    public class HomeController : BaseController
    {
        private SaleServiceSoapClient _proApiApiPlusService;
        private SubscriberManagementServiceSoapClient _subscriberManagementService;
        private MSendSMSServiceSoapClient _sendSmsService;
        
        public ActionResult Index()
        {
         
            return View();
        }
        public ActionResult BasicApi()
        {
            return View();
        }

        public ActionResult ProApi()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ProApi(int paymentTypeId,int productCategoryId)
        {
            var url = Request.Url.AbsoluteUri;
            _proApiApiPlusService = new SaleServiceSoapClient();
            #region Token
            Core.ApiPlusAndProApiService.MAuthToken token = new Core.ApiPlusAndProApiService.MAuthToken();
            token.UserCode = base.settings.UserCode;
            token.Pin = base.settings.Pin;
            #endregion
            #region MSaleProduct

            MSaleProduct product = new MSaleProduct();
            product.ProductId = 0;
            product.ProductCategory = productCategoryId;
            product.ProductDescription = "Telefon";
            product.Price = 0.01;
            product.Unit = 1;


            #endregion
            #region MSaleTicketInput
            MSaleTicketInput input = new MSaleTicketInput();
            input.MPAY = "01";
            input.Content = "TLFN01-Telefon";
            input.SendOrderResult = true;
            input.PaymentTypeId = paymentTypeId;
            input.ReceivedSMSObjectId = new Guid("00000000-0000-0000-0000-000000000000");
            input.ProductList = new[] { product };
            input.SendNotificationSMS = true;
            input.OnSuccessfulSMS = "basarili odeme yaptiniz";
            input.OnErrorSMS = "basarisiz odeme yaptiniz";
            input.Url = url;
            input.RequestGsmOperator = 0;
            input.RequestGsmType = 0;
            input.SuccessfulPageUrl = "http://localhost:7597/Home/Success";
            input.ErrorPageUrl = "http://localhost:7597/Home/Fail";
            input.Country = "";
            input.Currency = "";
            input.Extra = "";
            input.TurkcellServiceId = "20923735";
            #endregion
            var saleWithTicketResult = _proApiApiPlusService.SaleWithTicket(token, input);
            return Redirect(saleWithTicketResult.RedirectUrl);
        }
        public ActionResult ApiPlus()
        {          
            return View();
        }

  
        [HttpPost]
        public ActionResult ApiPlus(string gsmNumber, int paymentTypeId,int productCategoryId)
        {
            var url = Request.Url.AbsoluteUri;
            _proApiApiPlusService = new SaleServiceSoapClient();
            #region Token
            var token = new Core.ApiPlusAndProApiService.MAuthToken();
            token.UserCode = base.settings.UserCode;
            token.Pin = base.settings.Pin;
            #endregion
            #region Product
            MSaleProduct product = new MSaleProduct();
            product.ProductId = 0;
            product.ProductCategory = productCategoryId;
            product.ProductDescription = "Telefon";
            product.Price = 0.01;
            product.Unit = 1;
            #endregion
            #region MSaleInput
            MSaleInput input = new MSaleInput();
            input.MPAY = "01";
            input.Gsm = gsmNumber;
            input.Content = "TLFN-Telefon";
            input.SendOrderResult = true;
            input.PaymentTypeId = paymentTypeId;
            input.Url = url;
            input.ReceivedSMSObjectId = new Guid("00000000-0000-0000-0000-000000000000");
            input.ProductList = new[] {product};
            input.SendNotificationSMS = true;
            input.OnSuccessfulSMS = "basarili odeme yaptiniz";
            input.OnErrorSMS = "basarisiz odeme yaptiniz";
            input.RequestGsmOperator = 0;
            input.RequestGsmType = 0;
            input.CustomerIpAddress = "140.127.134.33";
            input.Extra = "";
            input.TurkcellServiceId = "20923735";
            #endregion
            var result = _proApiApiPlusService.SaleWithConfirm(token, input);
            return View(result);
        }

        //Abonelik Listeleme
        public ActionResult SelectSubscriber()
        { 
            return View();
        }

        /// <summary>
        /// Bu servis daha önceden abone olan kullanıcıları telefon numaralarına ve çeşitli kriterlere göre aramamızı sağlar.
        /// </summary>
        /// <param name="gsm"></param>
        /// <param name="activeTypeId"></param>
        /// <param name="orderChannelId"></param>
        /// <param name="subscriberTypeId"></param>
        /// <param name="startDateMin"></param>
        /// <param name="startDateMax"></param>
        /// <param name="lastSuccessfulPaymentDateMin"></param>
        /// <param name="lastSuccessfulPaymentDateMax"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SelectSubscriber(string gsm,int activeTypeId, int orderChannelId,int subscriberTypeId,DateTime startDateMin, DateTime startDateMax, DateTime lastSuccessfulPaymentDateMin, DateTime lastSuccessfulPaymentDateMax)
        {
            _subscriberManagementService = new SubscriberManagementServiceSoapClient();

            #region Token

            Core.SubscriberService.MAuthToken token = new Core.SubscriberService.MAuthToken();
            token.UserCode = base.settings.UserCode;
            token.Pin = base.settings.Pin;

            #endregion

            #region MSelectSubscriberInput

            MSelectSubscriberInput input = new MSelectSubscriberInput();
            input.ProductId = 0;
            input.GSM =gsm;
            input.OrderChannelId = orderChannelId;
            input.Active = activeTypeId;
            input.SubscriberType = subscriberTypeId;
            input.StartDateMin = startDateMin;
            input.StartDateMax = startDateMax;
            input.LastSuccessfulPaymentDateMin = lastSuccessfulPaymentDateMin;
            input.LastSuccessfulPaymentDateMax = lastSuccessfulPaymentDateMax;
            #endregion
           
            var result = _subscriberManagementService.SelectSubscriber(token, input);
            return View(result);
        }
        //Abonelik Detay
        public ActionResult SelectSubscriberDetail()
        {
            return View();
        }
        /// <summary>
        /// Sms Aboneliği görüntülemek için abonelik numarası gereklidir.
        /// Web sayfasından girilen abonelik numarasına göre abonenin detayı görüntülenir.
        /// </summary>
        /// <param name="subscriberId"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SelectSubscriberDetail(Guid subscriberId)
        {
            _subscriberManagementService = new SubscriberManagementServiceSoapClient();
            Core.SubscriberService.MAuthToken token = new Core.SubscriberService.MAuthToken();
            token.UserCode = base.settings.UserCode;
            token.Pin = base.settings.Pin;
            var guid = new Guid(subscriberId.ToString());
            var response = _subscriberManagementService.SelectSubscriberDetail(token, guid);
            return View(response);
        }
        //Aboneliği Pasife Çekme
        public ActionResult DeactivateSubscriber()
        {          
            return View();
        }
        /// <summary>
        /// Sms aboneliğini iptal etmek için abonelik numarası gereklidir.
        /// Web sayfasından girilen abonelik numarasına göre abonenin pasif duruma getirilmesi sağlanır.
        /// </summary>
        /// <param name="subscriberId"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeactivateSubscriber(Guid subscriberId)
        {
            _subscriberManagementService = new SubscriberManagementServiceSoapClient();
            Core.SubscriberService.MAuthToken token = new Core.SubscriberService.MAuthToken();
            token.UserCode = base.settings.UserCode;
            token.Pin = base.settings.Pin;
            var guid = new Guid(subscriberId.ToString());
            var response = _subscriberManagementService.DeactivateSubscriber(token, guid);
            return View(response);
        }
        public ActionResult SendInformationSmsService()
        {
            return View();
        }
        /// <summary>
        /// Web sayfasında gsm numarası ve gsm içeriği girilerek bilgi sms'i gönderimi servisinin çalıştırılması sağlanır.
        /// </summary>
        /// <param name="gsm"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SendInformationSmsService(string gsm, string content)
        {
            _sendSmsService = new MSendSMSServiceSoapClient();
            Core.SendInformationSMSService.MAuthToken token= new Core.SendInformationSMSService.MAuthToken();
            token.UserCode = base.settings.UserCode;
            token.Pin = base.settings.Pin;
            MSendSMSInput input= new MSendSMSInput();
            input.Gsm = gsm;
            input.Content = content;
            input.RequestGsmOperator = 0;
            var response = _sendSmsService.SendSMS(token, input);
            return View(response);
        }


        public ActionResult CCProxySale()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CCProxySale(string creditCardNo,string ownerName, int expireYear, int expireMonth,string cvv,int installmentCount)
        {
            var request= new CCProxySaleRequest();
            request.ServiceType = "CCProxy";
            request.OperationType = "Sale";
            request.MPAY = "001";
            request.IPAddress = "140.127.134.33";
            request.PaymentContent = "Bilgisayar";
            request.InstallmentCount = installmentCount;
            request.Description = "Bilgisayar Ödemesi";
            request.ExtraParam = "";

            #region Token
            request.Token= new Token();
            request.Token.UserCode = settings.UserCode;
            request.Token.Pin = settings.Pin;
            #endregion

            #region CreditCardInfo
            request.CreditCardInfo= new CreditCardInfo();
            request.CreditCardInfo.CreditCardNo = creditCardNo;
            request.CreditCardInfo.OwnerName = ownerName;
            request.CreditCardInfo.ExpireMonth = expireMonth;
            request.CreditCardInfo.ExpireYear = expireYear;
            request.CreditCardInfo.Cvv = cvv;
            request.CreditCardInfo.Price = 1;//0,01 TL
            #endregion

            var response = CCProxySaleRequest.Execute(request, settings);
            ServicesXmlResponse responseMessage = new ServicesXmlResponse();
            responseMessage.XmlResponse = response;
            return View(responseMessage);
        }

        public ActionResult WDTicketSale3DURLProxy()
        {

            return View();
        }
        /// <summary>
        /// 3D ile ödeme başlatmak için servis girdi parametrelerimizi PaymentFormRequest sınıfı ile ekliyoruz.
        /// 3D olmadan yapılan ödemeden tek farkı PaymentFormRequest sınıfı içerisinde OperationType alanı "Sale3DSURLProxy" olarak tanımlanması gerekiyor.
        /// Bu sınıf içerisinde bulunan değerleri  PaymentFormRequest.Execute(request, settings) kısmında https://www.wirecard.com.tr/SGate/Gate adresine post ediyoruz.
        /// işlem sonucunda oluşan xml metnini ve xml metni içerisinde bulunan RedirectUrl bilgisini paymentFormResponse sınıfına ekleyerek view sayfasına gönderiyoruz.
        /// View sayfası içerisinde ise ödemeyi tamamla butonuna basılırsa servis sonucunda oluşan RedirectUrl bilgisine yönlendirme yapılmış oluyor.
        /// </summary>
        /// <returns></returns>
        public ActionResult WDTicketSale3DURLProxyComplete()
        {
            WDTicketPaymentFormRequest request = new WDTicketPaymentFormRequest();
            request.ServiceType = "WDTicket";
            request.OperationType = "Sale3DSURLProxy";
            request.Price = 1;//0,01 TL
            request.MPAY = "";
            request.ErrorURL = "http://localhost:7597/Home/Fail";
            request.SuccessURL = "http://localhost:7597/Home/Success";
            request.ExtraParam = "";
            request.PaymentContent = "Bilgisayar";
            request.Description = "BLGSYR01";
            request.PaymentTypeId = 1;
            request.Token=new Token();
            request.Token.Pin = base.settings.Pin;
            request.Token.UserCode = base.settings.UserCode;
            var response = WDTicketPaymentFormRequest.Execute(request, settings);

            var paymentFormResponse = new WDTicketPaymentFormResponse();
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(response);
            XmlNode xmlNode = doc.SelectSingleNode("/Result/Item[@Key='RedirectUrl']");
            paymentFormResponse.RedirectUrl = xmlNode.Attributes["Value"].Value;
            paymentFormResponse.Response = response;
            return View(paymentFormResponse);
        }


        public ActionResult WDTicketSaleURLProxy()
        {
            return View();
        }

        /// <summary>
        /// //3D olmadan ödeme başlatmak için servis girdi parametrelerimizi PaymentFormRequest sınıfı ile ekliyoruz.
        /// 3D ile yapılan ödemeden tek farkı PaymentFormRequest sınıfı içerisinde OperationType alanı "SaleURLProxy" olarak tanımlanması gerekiyor.
        /// Bu sınıf içerisinde bulunan değerleri  PaymentFormRequest.Execute(request, settings) kısmında https://www.wirecard.com.tr/SGate/Gate adresine post ediyoruz.
        /// işlem sonucunda oluşan xml metnini ve xml metni içerisinde bulunan RedirectUrl bilgisini paymentFormResponse sınıfına ekleyerek view sayfasına gönderiyoruz.
        /// View sayfası içerisinde ise ödemeyi tamamla butonuna basılırsa servis sonucunda oluşan RedirectUrl bilgisine yönlendirme yapılmış oluyor.
        /// </summary>
        /// <returns></returns>
        public ActionResult WDTicketSaleUrlProxyComplete()
        {
            #region Request
            var request = new WDTicketPaymentFormRequest();
            request.ServiceType = "WDTicket";
            request.OperationType = "SaleURLProxy";
            request.Price = 1;//0,01 TL
            request.MPAY = "";
            request.Description = "BLGSYR01";
            request.ErrorURL = "http://localhost:7597/Home/Fail";
            request.SuccessURL = "http://localhost:7597/Home/Success";
            request.ExtraParam = "";
            request.PaymentContent = "Bilgisayar";
            request.PaymentTypeId = 1;
            request.Token = new Token();
            request.Token.Pin = settings.Pin;
            request.Token.UserCode = settings.UserCode;
            #endregion

            #region Response
            var response = WDTicketPaymentFormRequest.Execute(request, settings);
            var paymentFormResponse = new WDTicketPaymentFormResponse();
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(response);
            XmlNode xmlNode = doc.SelectSingleNode("/Result/Item[@Key='RedirectUrl']");
            paymentFormResponse.RedirectUrl = xmlNode.Attributes["Value"].Value;
            paymentFormResponse.Response = response;
            #endregion
            
            return View(paymentFormResponse);
        }


        //Market Place
        public ActionResult MarketPlaceAddSubPartner()
        {
            return View();
        }


        /// <summary>
        /// Yeni bir Pazaryeri oluşturulması için kullanılan metoddur.
        /// Pazaryeri oluşturulduktan sonra response değeri olarak SubPartnerId değeri bize dönderilir.
        /// </summary>
        /// <param name="subPartnerType"></param>
        /// <param name="name"></param>
        /// <param name="mobilePhoneNumber"></param>
        /// <param name="identityNumber"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult MarketPlaceAddSubPartner(SubPartnerTypeEnum subPartnerType,string name, string mobilePhoneNumber, string identityNumber)
        {
            MarketPlaceAddOrUpdateRequest request = new MarketPlaceAddOrUpdateRequest();
            request.ServiceType = "CCMarketPlace";
            request.OperationType = "AddSubPartner";
            request.UniqueId = Guid.NewGuid().ToString().Replace("-","");
            request.SubPartnerType=subPartnerType;
            request.Name = name;

            #region Token Bilgileri
            request.Token = new Token();
            request.Token.UserCode = settings.UserCode;
            request.Token.Pin = settings.Pin;

            #endregion

            #region ContactInfo Bilgileri
            request.ContactInfo= new ContactInfo();
            request.ContactInfo.Country = "TR";
            request.ContactInfo.City = "34";
            request.ContactInfo.Address = "Gayrettepe Mh. Yıldız Posta Cd. D Plaza No:52 K:6 34349 Beşiktaş / İstanbul";
            request.ContactInfo.MobilePhone = mobilePhoneNumber;
            request.ContactInfo.BusinessPhone = "2121111111";
            #endregion
            #region FinancialInfo Bilgileri
            request.FinancialInfo = new FinancialInfo();
            request.FinancialInfo.IdentityNumber = identityNumber;
            request.FinancialInfo.TaxOffice = "İstanbul";
            request.FinancialInfo.TaxNumber = "11111111111";
            request.FinancialInfo.BankName = "0012";
            request.FinancialInfo.IBAN = "TR330006100519786457841326";
            request.FinancialInfo.AccountName = "Ahmet Yılmaz";

            #endregion
            var response = MarketPlaceAddOrUpdateRequest.Execute(request, settings);

            ServicesXmlResponse responseMessage= new ServicesXmlResponse();

            responseMessage.XmlResponse = response;
            return View(responseMessage);
        }

        public ActionResult MarketPlaceUpdateSubPartner()
        {
            return View();
        }
        /// <summary>
        /// Pazaryerinin güncellenmesi için kullanılır.
        /// Pazaryerinin güncellenmesi için pazaryeri oluşturulduğunda response değeri olarak verilen SubPartnerId değerinin gönderilmesi gerekmektedir.
        /// </summary>
        /// <param name="subPartnerType"></param>
        /// <param name="name"></param>
        /// <param name="mobilePhoneNumber"></param>
        /// <param name="identityNumber"></param>
        /// <param name="subPartnerId"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult MarketPlaceUpdateSubPartner(SubPartnerTypeEnum subPartnerType, string name, string mobilePhoneNumber, string identityNumber,int subPartnerId)
        {
            MarketPlaceAddOrUpdateRequest request = new MarketPlaceAddOrUpdateRequest();
            request.ServiceType = "CCMarketPlace";
            request.OperationType = "UpdateSubPartner";
            request.UniqueId = Guid.NewGuid().ToString();
            request.SubPartnerType = subPartnerType;
            request.Name = name;
            request.SubPartnerId = subPartnerId;
            #region Token Bilgileri
            request.Token = new Token();
            request.Token.UserCode = settings.UserCode;
            request.Token.Pin = settings.Pin;

            #endregion

            #region ContactInfo Bilgileri
            request.ContactInfo = new ContactInfo();
            request.ContactInfo.Country = "TR";
            request.ContactInfo.City = "34";
            request.ContactInfo.Address = "Gayrettepe Mh. Yıldız Posta Cd. D Plaza No:52 K:6 34349 Beşiktaş / İstanbul";
            request.ContactInfo.MobilePhone = mobilePhoneNumber;
            request.ContactInfo.BusinessPhone = "2121111111";
            #endregion
            #region FinancialInfo Bilgileri
            request.FinancialInfo = new FinancialInfo();
            request.FinancialInfo.IdentityNumber = identityNumber;
            request.FinancialInfo.TaxOffice = "İstanbul";
            request.FinancialInfo.TaxNumber = "11111111111";
            request.FinancialInfo.BankName = "0012";
            request.FinancialInfo.IBAN = "TR330006100519786457841326";
            request.FinancialInfo.AccountName = "Ahmet Yılmaz";

            #endregion
            var response = MarketPlaceAddOrUpdateRequest.Execute(request, settings);

            ServicesXmlResponse responseMessage = new ServicesXmlResponse();
            responseMessage.XmlResponse = response;
            return View(responseMessage);
        }

        public ActionResult MarketPlaceDeactiveSubPartner()
        {
            return View();
        }
        /// <summary>
        /// pazaryerinin pasif hale getirilmesi için kullanılır.
        /// pazaryeri oluşturulurken gönderilen uniqueId değeri parametre olarak gönderilmesi gerekmektedir.
        /// </summary>
        /// <param name="uniqueId"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult MarketPlaceDeactiveSubPartner(string uniqueId)
        {
            MarketPlaceDeactiveRequest request = new MarketPlaceDeactiveRequest();
            request.ServiceType = "CCMarketPlace";
            request.OperationType = "DeactivateSubPartner";
            request.UniqueId = uniqueId;
            #region Token
            request.Token= new Token();
            request.Token.UserCode = settings.UserCode;
            request.Token.Pin = settings.Pin;
            var response= MarketPlaceDeactiveRequest.Execute(request, settings);

            ServicesXmlResponse responseMessage = new ServicesXmlResponse();
            responseMessage.XmlResponse = response;
            #endregion
            return View(responseMessage);
        }

        public ActionResult MarketPlaceSale3DSec()
        {
            return View();
        }
        [HttpPost]
        public ActionResult MarketPlaceSale3DSec(string creditCardNo, string ownerName, int expireYear, int expireMonth, string cvv, int installmentCount, int subPartnerId)
        {
            MarketPlaceSale3DSecOrMpSaleRequest request = new MarketPlaceSale3DSecOrMpSaleRequest();
            request.ServiceType = "CCMarketPlace";
            request.OperationType = "Sale3DSEC";
            request.MPAY = "";
            request.IPAddress = "140.127.134.33";
            request.Port = "123";
            request.Description = "Bilgisayar";
            request.InstallmentCount = installmentCount;
            request.CommissionRate = 100; //komisyon oranı 1. 100 ile çarpılıp gönderiliyor
            request.ExtraParam = "";
            request.PaymentContent = "BLGSYR01";
            request.SubPartnerId = subPartnerId;
            request.ErrorURL = "http://localhost:7597/Home/MarketPlaceError";
            request.SuccessURL = "http://localhost:7597/Home/MarketPlaceSuccess";

            #region Token
            request.Token = new Token();
            request.Token.UserCode = settings.UserCode;
            request.Token.Pin = settings.Pin;
            #endregion

            #region CreditCardInfo
            request.CreditCardInfo = new CreditCardInfo();
            request.CreditCardInfo.CreditCardNo = creditCardNo;
            request.CreditCardInfo.OwnerName = ownerName;
            request.CreditCardInfo.ExpireYear = expireYear;
            request.CreditCardInfo.ExpireMonth = expireMonth;
            request.CreditCardInfo.Cvv = cvv;
            request.CreditCardInfo.Price = 1;//0,01 TL
            #endregion

            var response = MarketPlaceSale3DSecOrMpSaleRequest.Execute(request, settings);
            ServicesXmlResponse responseMessage = new ServicesXmlResponse();
            responseMessage.XmlResponse = response;
            return View(responseMessage);
        }

        public ActionResult MarketPlaceMPSale()
        {
            return View();
        }

        [HttpPost]
        public ActionResult MarketPlaceMPSale(string creditCardNo, string ownerName, int expireYear, int expireMonth, string cvv, int installmentCount,int subPartnerId)
        {
            MarketPlaceSale3DSecOrMpSaleRequest request = new MarketPlaceSale3DSecOrMpSaleRequest();
            request.ServiceType = "CCMarketPlace";
            request.OperationType = "MPSale";
            request.MPAY = "";
            request.IPAddress = "140.127.134.33";
            request.Port = "123";
            request.Description = "Bilgisayar";
            request.InstallmentCount = installmentCount;
            request.CommissionRate = 100; //komisyon oranı 1. 100 ile çarpılıp gönderiliyor
            request.ExtraParam = "";
            request.PaymentContent = "";
            request.SubPartnerId = subPartnerId;
            request.CCTokenId=Guid.NewGuid();
            request.ErrorURL = "";
            request.SuccessURL = "";

            #region Token
            request.Token= new Token();
            request.Token.UserCode = settings.UserCode;
            request.Token.Pin = settings.Pin;
            #endregion

            #region CreditCardInfo
            request.CreditCardInfo = new CreditCardInfo();
            request.CreditCardInfo.CreditCardNo = creditCardNo;
            request.CreditCardInfo.OwnerName = ownerName;
            request.CreditCardInfo.ExpireYear = expireYear;
            request.CreditCardInfo.ExpireMonth = expireMonth;
            request.CreditCardInfo.Cvv = cvv;
            request.CreditCardInfo.Price = 1;//0,01 TL
            #endregion

            var response = MarketPlaceSale3DSecOrMpSaleRequest.Execute(request, settings);
            ServicesXmlResponse responseMessage = new ServicesXmlResponse();
            responseMessage.XmlResponse = response;
            return View(responseMessage);
        }

        public ActionResult MarketPlaceReleasePayment()
        {
            return View();
        }

        [HttpPost]
        public ActionResult MarketPlaceReleasePayment(int subPartnerId)
        {
            var request= new MarketPlaceReleasePaymentRequest();

            request.ServiceType = "CCMarketPlace";
            request.OperationType = "ReleasePayment";
            request.SubPartnerId = subPartnerId;
            request.CommissionRate = 100; //%1
            request.MPAY = "";
            request.OrderId=Guid.NewGuid();
            request.Description = "Bilgisayar ödemesi";

            #region Token
            request.Token= new Token();
            request.Token.UserCode = settings.UserCode;
            request.Token.Pin = settings.Pin;
            #endregion

            var response = MarketPlaceReleasePaymentRequest.Execute(request, settings);
            ServicesXmlResponse responseMessage = new ServicesXmlResponse();
            responseMessage.XmlResponse = response;
            return View(responseMessage);
        }

        public ActionResult Success()
        {

            var model = new WDTicketPaymentFormBaseResponse();
            model.OrderId = Request.Form["OrderId"];
            model.MPay = Request.Form["MPAY"];
            model.StatusCode = Request.Form["Statuscode"];
            model.ResultCode = Request.Form["ResultCode"];
            model.ResultMessage = Request.Form["ResultMessage"];
            model.LastTransactionDate = Request.Form["LastTransactionDate"];
            model.MaskedCCNo = Request.Form["MaskedCCNo"];
            model.CCTokenId = Request.Form["CCTokenId"];
            model.ExtraParam = Request.Form["ExtraParam"];
            return View(model);
        }
        public ActionResult Fail()
        {
            var model = new WDTicketPaymentFormBaseResponse();
            model.OrderId = Request.Form["OrderId"];
            model.MPay = Request.Form["MPAY"];
            model.StatusCode = Request.Form["Statuscode"];
            model.ResultCode = Request.Form["ResultCode"];
            model.ResultMessage = Request.Form["ResultMessage"];
            model.LastTransactionDate = Request.Form["LastTransactionDate"];
            model.MaskedCCNo = Request.Form["MaskedCCNo"];
            model.CCTokenId = Request.Form["CCTokenId"];
            model.ExtraParam = Request.Form["ExtraParam"];
            return View(model);
        }
    }
}