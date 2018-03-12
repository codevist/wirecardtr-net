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
            UserCode = "20923",
            Pin = "535D7D1B5DA6407EB7F6",
            BaseUrl = "https://www.wirecard.com.tr/SGate/Gate"
        };
    }
}