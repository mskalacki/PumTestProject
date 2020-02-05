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
using PumTestProject.DAO;
using PumTestProject.DTO;
using PumTestProject.Enums;
using PumTestProject.Model;
using PumTestProject.Services;

namespace PumTestProject.Controllers
{
    public class CompanyController : ApiController
    {
        private ICompanyService _companyService;
        private IContextFactory _contextFactory;

        public CompanyController(ICompanyService companyService, IContextFactory factory)
        {
            this._companyService = companyService;
            this._contextFactory = factory;
        }

        [HttpGet]
        public IHttpActionResult AllCompanies()
        {
            List<Company> Result = new List<Company>();


            Result = _companyService.GetAllCompanies();

            foreach (Company company in Result)
            {

            }
            if (Result.Count == 0)
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
        [HttpPost]
        public IHttpActionResult Search(CompanySearchDTO queryCriteria)
        {
            List<CompanyDTO> Results = new List<CompanyDTO>();

            Results = _companyService.Search(queryCriteria);

            return Ok(Results);
        }

       
        [HttpPost]
        public HttpResponseMessage Create(Company company)
        {
            long id = -1;
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid model");
            }

            id = _companyService.Create(company);

            if (id != -1)
            {
                return Request.CreateResponse(HttpStatusCode.Created, id);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
        
        [HttpPut]
        public HttpResponseMessage Update (Company company)
        {
            bool updateResult = false;

            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid model");
            }
                if (_companyService.DoesCompanyExists(company.Id))
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Entity not found.");
                }
            
            updateResult = _companyService.Update(company);

            if (updateResult == true)
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Entity updated");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Inernal server error.");
            }
        }
    }
}

