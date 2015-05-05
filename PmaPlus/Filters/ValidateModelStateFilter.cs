using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace PmaPlus.Filters
{
    public class ValidateModelStateAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (!actionContext.ModelState.IsValid)
            {
                //actionContext.Response = actionContext.Request.CreateErrorResponse(
                //    HttpStatusCode.BadRequest, actionContext.ModelState);

                var errors = new List<string>();
                foreach (var state in actionContext.ModelState)
                {
                    foreach (var error in state.Value.Errors)
                    {
                        errors.Add(error.ErrorMessage);
                    }
                }
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.BadRequest, new{  Message = errors });
            }
        }
    }
}