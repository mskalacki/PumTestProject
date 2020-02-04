using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
        private ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            this._companyService = companyService;
        }

        [HttpGet]
        public IHttpActionResult AllCompanies()
        {
            List<Company> Result = new List<Company>();
            
                Result = _companyService.GetAllCompanies();
                if (Result.Count > 0)
                {
                    var response = new HttpResponseMessage(HttpStatusCode.NotFound)
                    {
                        Content = new StringContent("Company doesn't exist", System.Text.Encoding.UTF8, "text/plain"),
                        StatusCode = HttpStatusCode.NotFound
                    };

                    throw new HttpResponseException(response);
                }
            

            

            return Ok<List<Company>>(Result);

        }


    }
}

