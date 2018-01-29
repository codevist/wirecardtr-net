using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wirecard.DeveloperPortal.Core;

namespace Wirecard.DeveloperPortal.WebSamples.Controllers
{
    public class BaseController : Controller
    {
        public Settings settings = new Settings()
        {
            UserCode = "",
            Pin = "",
            BaseUrl = "https://www.wirecard.com.tr/SGate/Gate"
        };
    }
}