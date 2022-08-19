using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnionArchitecture.Domain.Entities;
using OnionArchitecture.Application.DTOs.Categories;
using Microsoft.AspNetCore.Mvc;

namespace OnionArchitecture.Application.Filters.Category
{
    public class EnsureNameActionFilter:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);    
            var category = context.ActionArguments["dto"] as CategoryPostDto;
            if(category != null && !string.IsNullOrWhiteSpace(category.Name) && category.Name.Length<10)
            {
                context.ModelState.AddModelError("Name", "Name must be longer than 10");
                context.Result = new BadRequestObjectResult(context.ModelState);
            }

        }
    }
}
