using System;
using System.Collections.Generic;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Swashbuckle.AspNetCore.Swagger;

namespace ExploreDotnet.SharedKernel
{
    public class AddRequiredHeaderParameter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            (operation.Parameters ??= new List<OpenApiParameter>())
                .Add(new OpenApiParameter
                {
                    Name = "X-Tenant-Id",
                    In = ParameterLocation.Header,
                    Required = false
                });
        }
    }
}
