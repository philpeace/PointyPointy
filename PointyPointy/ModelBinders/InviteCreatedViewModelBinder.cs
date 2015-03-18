using System.Web.Mvc;
using Autofac.Integration.Mvc;
using PointyPointy.Services;
using PointyPointy.ViewModels;
using PointyPointy.Services.BuiltIn;

namespace PointyPointy.ModelBinders
{
    [ModelBinderType(typeof(InviteCreatedViewModel))]
    public class InviteCreatedViewModelBinder : BaseViewModelBinder
    {
        public InviteCreatedViewModelBinder(IApplicationUserManagerFactory applicationUserManagerFactory) 
            : base(applicationUserManagerFactory)
        {

        }

        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var model = base.BindModel(controllerContext, bindingContext) as InviteCreatedViewModel;

            return model;
        }
    }
}