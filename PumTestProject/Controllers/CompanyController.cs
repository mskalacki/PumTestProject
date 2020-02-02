using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace PumTestProject.Controllers
{
    public class CompanyController : ApiController
    {
       [HttpGet]
        public string Foo(string name)
        {
            return "Dogadało się!";
        }
    }
}
