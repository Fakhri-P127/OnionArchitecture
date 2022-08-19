//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Filters;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace OnionArchitecture.Application.Filters
//{
//    public class VersionDiscontinueResourceFilter : Attribute, IResourceFilter
//    {
//        public void OnResourceExecuted(ResourceExecutedContext context)
//        {

//        }

//        public void OnResourceExecuting(ResourceExecutingContext context)
//        {
//            if (!context.HttpContext.Request.Path.Value.ToLower().Trim().Contains("v1")
//                && !context.HttpContext.Request.Path.Value.ToLower().Trim().Contains("v2"))
//            {
//                context.ModelState.AddModelError("", "This version of the API either is still in development or it doesn't exist.");
//                ValidationProblemDetails problemDetails = new(context.ModelState)
//                {
//                    Status = StatusCodes.Status400BadRequest,

//                };
//                context.Result = new BadRequestObjectResult(problemDetails);
//            }

//        }
//    }
//}
