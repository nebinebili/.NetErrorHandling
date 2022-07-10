using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UdemyErrorHandling.Filter
{
    public class CustomHandleExceptionFilterAttribute:ExceptionFilterAttribute
    {
        public string ErrorPage { get; set; }

        public override void OnException(ExceptionContext context)
        {
            // loglama edildi

            if (ErrorPage == "Error1")
            {
                // ferqli bir loglama
            }
            else
            {
                // ferqli bir loglama
            }


            var result = new ViewResult() { ViewName = ErrorPage };

            result.ViewData = new ViewDataDictionary(new EmptyModelMetadataProvider(), context.ModelState);

            result.ViewData.Add("Exception", context.Exception);

            result.ViewData.Add("Url", context.HttpContext.Request.Path.Value);

            context.Result = result;
        }
    }
}
