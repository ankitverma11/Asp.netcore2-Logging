using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetCoreLogging.Models
{
    public class AuthorizeControllerModelConvention : IControllerModelConvention
    {
        public void Apply(ControllerModel controller)
        {
            if(controller.ControllerName.Contains("Api"))
            {
                controller.Filters.Add(new AuthorizeFilter("policyforapicontrollers"));
            }
            else
            {
                controller.Filters.Add(new AuthorizeFilter("policyforcontrollers"));
            }
        }
    }
}
