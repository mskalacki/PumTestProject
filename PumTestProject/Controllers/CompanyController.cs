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
using PumTestProject.Services;

namespace PumTestProject.Controllers
{
    public class CompanyController : ApiController
    {
        private IEmployeeService _empService;

        public CompanyController(IEmployeeService empService)
        {
            this._empService = empService;
        }

        [HttpGet]
        public IHttpActionResult Foo()
        {

            Employee emp = _empService.GetEmployee();


            return Ok<Employee>(emp);
        }
         
          
        }
    }

