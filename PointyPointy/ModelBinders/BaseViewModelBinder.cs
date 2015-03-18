using System.Web.Mvc;
using Autofac.Integration.Mvc;
using Microsoft.AspNet.Identity;
using PointyPointy.ViewModels;
using PointyPointy.Services.BuiltIn;

namespace PointyPointy.ModelBinders
{
    [ModelBinderType(typeof(BaseViewModel))]
    public class BaseViewModelBinder : DefaultModelBinder
    {
        private readonly ApplicationUserManager _applicationUserManager;

        public BaseViewModelBinder(IApplicationUserManagerFactory applicationUserManagerFactory)
        {
            _applicationUserManager = applicationUserManagerFactory.Create();
        }

        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var model = base.BindModel(controllerContext, bindingContext) as BaseViewModel;

            if (model != null)
            {
                if (controllerContext.HttpContext.User.Identity.IsAuthenticated)
                {
                    model.User = _applicationUserManager.FindByIdAsync(controllerContext.HttpContext.User.Identity.GetUserId()).Result;
                }
            }

            return model;
        }
    }
}