namespace Wirecard.DeveloperPortal.Core
{
    /// <summary>
    /// Tüm çağrılarda kullanılacak ayarların tutulduğu sınıftır. 
    /// Bu sınıf üzerinde size özel parametreler fonksiyonlar arasında taşınabilir.
    /// </summary>
    public class Settings
    {      
        public string UserCode { get; set; }
        public string Pin { get; set; }
        public string BaseUrl { get; set; }
    }
}
