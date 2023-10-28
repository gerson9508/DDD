namespace _360.API.Extensions
{
   using Microsoft.AspNetCore.Authorization;
   using Microsoft.OpenApi.Models;
   using Swashbuckle.AspNetCore.SwaggerGen;
   using System.Collections.Generic;
   using _360.API.Domain.Shared;

   public class AuthorizeCheckOperationFilterExtencions : IOperationFilter
   {
      public void Apply(OpenApiOperation operation, OperationFilterContext context)
      {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
         var hasAuthorize = context.MethodInfo.DeclaringType
           .GetCustomAttributes(true).OfType<AuthorizeAttribute>().Any()
           || context.MethodInfo
           .GetCustomAttributes(true).OfType<AuthorizeAttribute>().Any();
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.

         AddOperation(operation, context);
         if (hasAuthorize) AddOperationAuthorize(operation);
      }
      private static void AddOperation(OpenApiOperation operation, OperationFilterContext context)
      {
         _ = new Dictionary<string, OpenApiMediaType>()
         {
            ["application/json"] = new OpenApiMediaType()
            {
               Schema = context.SchemaGenerator.GenerateSchema(
                  typeof(SZGResponse), context.SchemaRepository)
            }
         };

         operation.Responses.Clear();
         operation.Responses.Add("200", new() { Description = "Success" });
         operation.Responses.Add("400", new() { Description = "Bad Request" });
         operation.Responses.Add("500", new() { Description = "InternalServerError" });
      }
      private static void AddOperationAuthorize(OpenApiOperation operation)
      {
         operation.Responses.Add("401", new() { Description = "Unauthorized" });
         operation.Responses.Add("403", new() { Description = "Forbidden" });
         AddOperationSecurity(operation);
      }
      private static void AddOperationSecurity(OpenApiOperation operation)
      {
         operation.Security = new List<OpenApiSecurityRequirement>
            {
               new OpenApiSecurityRequirement
               {
                  [
                     new OpenApiSecurityScheme
                     {
                        Reference = new OpenApiReference
                        {
                           Type = ReferenceType.SecurityScheme,
                           Id = "oauth2"
                        }
                     }
                  ] = new[] { "http://localhost:7245" }
               }
            };
      }
   }
}
