using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OnionArchitecture.Application.DTOs.Plants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionArchitecture.Application.Filters.Plant
{
    public class PlantFilters:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            PlantPostDto dto = context.ActionArguments["dto"] as PlantPostDto;
            if(dto != null && !string.IsNullOrWhiteSpace(dto.Name))
            {
                bool isValid = true;
                if (!dto.EnsureDate.HasValue)
                {
                    context.ModelState.AddModelError("EnsureDate"
                        , "EnsureDate can't be null, Please enter some value.");

                    isValid = false;
                }

                if (dto.EnsureDate < DateTime.UtcNow.AddMinutes(-5))
                {
                    context.ModelState
                        .AddModelError("EnsureDate", "EnsureDate must be at greater than DateTime.Now");
                    isValid = false;
                }

               
                if (!isValid)
                {
                    ValidationProblemDetails problemDetails = new(context.ModelState)
                    {
                        Status = StatusCodes.Status400BadRequest,
                    };
                    context.Result = new BadRequestObjectResult(problemDetails);
                }


            }
        }
    }
}
