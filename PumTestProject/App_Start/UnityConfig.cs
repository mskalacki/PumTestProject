using PumTestProject.DAO;
using PumTestProject.Services;
using System.Web.Http;
using Unity;
using Unity.WebApi;

namespace PumTestProject
{
    public static class UnityConfig
    {
        private static IUnityContainer container;

        public static IUnityContainer getContainer()
        {
            return container;
        }
        public static void RegisterComponents()
        {
			container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

           
            container.RegisterType<IContextFactory, ContextFactory>();
            container.RegisterType<ICompanyDAO, CompanyDAO>();
            container.RegisterType<ICompanyService, CompanyService>();


           //GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}