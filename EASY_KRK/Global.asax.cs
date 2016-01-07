using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace EASY_KRK
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_AcquireRequestState(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session != null)
            {
                CultureInfo cultureInfo = (CultureInfo)this.Session["Culture"];
                if (cultureInfo == null)
                {
                    string langName = "pl";
                    if (HttpContext.Current.Request.UserLanguages != null && HttpContext.Current.Request.UserLanguages.Length != 0)
                    {
                        langName = HttpContext.Current.Request.UserLanguages[0].Substring(0, 2);
                    }
                    cultureInfo = new CultureInfo(langName);
                    this.Session["Culture"] = cultureInfo;
                }
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("en");
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en");
            }
        }
    }
}
