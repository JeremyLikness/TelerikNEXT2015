using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.OData.Builder;
using System.Web.Http.OData.Extensions;
using AA.Crud.Domain;

namespace AE.Crud.WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.EnableCors(new EnableCorsAttribute("*", "*", "*"));

            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<FoodDescription>("FoodDescriptions");
            config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    
        }
    }
}
