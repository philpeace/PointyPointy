using System.Web.Mvc;
using Autofac.Integration.Mvc;
using PointyPointy.Models;
using PointyPointy.Services;

namespace PointyPointy.ModelBinders
{
    [ModelBinderType(typeof(InviteCreateViewModel))]
    public class InviteCreateViewModelBinder : InviteViewModelBinder
    {
        public InviteCreateViewModelBinder(IApplicationUserManagerFactory applicationUserManagerFactory) 
            : base(applicationUserManagerFactory)
        {
        }

        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var model = base.BindModel(controllerContext, bindingContext) as InviteCreateViewModel;

            return model;
        }
    }
}