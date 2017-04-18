using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Linq;

namespace Eventos.IO.Services.Api.Configuration
{
    public class RouteConvention : IApplicationModelConvention
    {
        private readonly AttributeRouteModel _centralPrefix;

        public RouteConvention(IRouteTemplateProvider routeTemplateProvider)
        {
            _centralPrefix = new AttributeRouteModel(routeTemplateProvider);
        }

        public void Apply(ApplicationModel application)
        {
            foreach (var controller in application.Controllers)
            {
                var matchedSelectors = controller.Selectors.Where(x => x.AttributeRouteModel != null).ToList();
                if (matchedSelectors.Any())
                {
                    foreach (var selectModel in matchedSelectors)
                    {
                        selectModel.AttributeRouteModel = AttributeRouteModel.CombineAttributeRouteModel(_centralPrefix, selectModel.AttributeRouteModel);
                    }
                }

                var unmatchedSelector = controller.Selectors.Where(x => x.AttributeRouteModel == null).ToList();
                if (unmatchedSelector.Any())
                {
                    foreach (var selectorModel in unmatchedSelector)
                    {
                        selectorModel.AttributeRouteModel = _centralPrefix;
                    }
                }
            }
        }
    }

    public static class MvcOptionsExtensions
    {
        public static void UseCentralRoutePrefix(this MvcOptions opts, IRouteTemplateProvider routeAttribute)
        {
            opts.Conventions.Insert(0, new RouteConvention(routeAttribute));
        }
    }
}
