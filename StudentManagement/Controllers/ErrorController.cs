using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagement.Controllers
{
    public class ErrorController:Controller
    {
        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            var statusCodeResult = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();

            switch (statusCode)
            {
                case 404:
                    ViewBag.ErrorMessage = "抱歉，你访问的页面不存在";
                    ViewBag.Path = statusCodeResult.OriginalPath;
                    break;
            }
            return View("NotFound");
        }

        [AllowAnonymous]//允许匿名访问
        [Route("Error")] 
        public IActionResult Error()
        {
            var exceptionHandlerFeature =HttpContext.Features.Get<IExceptionHandlerFeature>();
            //ViewBag.ExceptionPath = exceptionHandlerFeature.Path;
            ViewBag.ExceptionMessage = exceptionHandlerFeature.Error.Message;
            ViewBag.StackTrace = exceptionHandlerFeature.Error.StackTrace;

            return View("Error");
        }
    }
}
