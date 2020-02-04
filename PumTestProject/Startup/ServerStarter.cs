using PumTestProject.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.SelfHost;
using Unity;

namespace PumTestProject.Startup
{
    class ServerStarter
    {
        public void StartTheServer()
        {
            HttpSelfHostConfiguration config = new HttpSelfHostConfiguration("http://localhost:9876");
            config.Routes.MapHttpRoute("default",
                                        "api/{controller}/{action}/{id}",
                                        new { id = RouteParameter.Optional });
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            HttpSelfHostServer server = new HttpSelfHostServer(config);
            var task = server.OpenAsync();
            task.Wait();

            this.SetDependencyContainer(config);
            //CreateDb();

            Console.WriteLine("Web API Server is running at http://localhost:9876");

           
            Console.ReadLine();

        }

        void SetDependencyContainer(HttpSelfHostConfiguration config)
        {
            UnityConfig.RegisterComponents();
            IUnityContainer container = UnityConfig.getContainer();

            config.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);
        }

        void CreateDb()
        {
            PumContext context = new PumContext();
            context.Database.CreateIfNotExists();
        }
    }
}
