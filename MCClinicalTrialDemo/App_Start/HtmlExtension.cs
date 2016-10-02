using MCClinicalTrialDemo.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MCClinicalTrialDemo
{
    public static class HtmlExtensions
    {
        public static bool IsPublisher(this HtmlHelper htmlHelper)
        {
            var controller = htmlHelper.ViewContext.Controller as BaseController;
            if (controller == null)
            {
                throw new Exception("The controller used to render this view doesn't inherit from BaseContller");
            }
            return controller.CurrentUserRole == controller.Role_Publisher;
        }
    }
}