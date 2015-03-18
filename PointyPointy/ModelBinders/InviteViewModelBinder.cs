using System.Web.Mvc;
using Autofac.Integration.Mvc;
using PointyPointy.Services.BuiltIn;
using PointyPointy.Services;
using PointyPointy.ViewModels;

namespace PointyPointy.ModelBinders
{
    [ModelBinderType(typeof(InviteViewModel))]
    public class InviteViewModelBinder : BaseViewModelBinder
    {
        public InviteViewModelBinder(IApplicationUserManagerFactory applicationUserManagerFactory) 
            : base(applicationUserManagerFactory)
        {

        }

        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var model = base.BindModel(controllerContext, bindingContext) as InviteViewModel;

            return model;
        }
    }
}