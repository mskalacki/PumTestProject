using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.SelfHost;

namespace PumTestProject.Startup
{
    class ServerStarter
    {
        public void StartTheServer()
        {
            HttpSelfHostConfiguration config = new HttpSelfHostConfiguration("http://localhost:9876");
            config.Routes.MapHttpRoute("default",
                                        "api/{controller}/{id}",
                                        new { controller = "Home", id = RouteParameter.Optional });

            HttpSelfHostServer server = new HttpSelfHostServer(config);
            var task = server.OpenAsync();
            task.Wait();

            Console.WriteLine("Web API Server is running at http://localhost:9876");
            Console.ReadLine();
        }
    }
}
