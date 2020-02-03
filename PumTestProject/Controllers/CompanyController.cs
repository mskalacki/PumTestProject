using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using Newtonsoft.Json;
using PumTestProject.Enums;
using PumTestProject.Model;

namespace PumTestProject.Controllers
{
    public class CompanyController : ApiController
    {
       [HttpGet]
        public IHttpActionResult Foo(string pos)
        {

            Employee emp = new Employee()
            {
                Name = "Marek",
                Surname = "Wacek",
                Position = Position.Administrator
            };
             if (pos  == Position.Administrator.ToString())
            {
                
                return Ok<Employee>(emp);
            }
             else
            {

            return NotFound();

            }
         
            

            //return Json(new
            //{
            //    message = "Jupi!",
            //    count = 2
            //}) ;
        }
    }
}
