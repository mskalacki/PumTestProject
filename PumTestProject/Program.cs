using PumTestProject.Startup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.SelfHost;

namespace PumTestProject
{
    class Program
    {
        static void Main(string[] args)
        {
            ServerStarter starter = new ServerStarter();
            starter.StartTheServer();
            
        }
    }
}
