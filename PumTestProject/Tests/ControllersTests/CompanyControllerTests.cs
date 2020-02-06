using NSubstitute;
using NUnit.Framework;
using PumTestProject.Controllers;
using PumTestProject.DTO;
using PumTestProject.Model;
using PumTestProject.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;

namespace PumTestProject.Tests.ControllersTests
{
    [TestFixture]
    public class CompanyControllerTests
    {
        private ICompanyService _service;
        private CompanyController _sut;

        [SetUp]
        public void Setup()
        {
            _service = Substitute.For<ICompanyService>();
            _sut = new CompanyController(_service);
            _sut.Configuration = new HttpConfiguration();
            _sut.Request = new HttpRequestMessage();
        }

        [Test]
        public void SearchTest()
        {
            CompanySearchDTO queryCriteria = new CompanySearchDTO() ;
            List<CompanyDTO> CompaniesFromDb = new List<CompanyDTO>() { new CompanyDTO()};

            _service.Search(queryCriteria).Returns(CompaniesFromDb);

            IHttpActionResult Result = _sut.Search(queryCriteria);

            var result = Result as OkNegotiatedContentResult<List<CompanyDTO>>;
            Assert.AreEqual(CompaniesFromDb, result.Content);
           
        }
        [Test]
        public void Delete_CompanyDoesntExist_Test()
        {
            long id = 2;
            _service.DoesCompanyExists(id).Returns(false);
            HttpResponseMessage result = _sut.Delete(id);

            Assert.AreEqual(result.StatusCode, HttpStatusCode.NotFound);
           
            _service.Received(1).DoesCompanyExists(id);
            _service.ReceivedWithAnyArgs(0).Delete(id);
        }

        [Test]
        public void Delete_CompanyExistsQuerySucess_Test()
        {
            long id = 2;
            _service.Delete(id).Returns(true);
            _service.DoesCompanyExists(id).Returns(true);
            HttpResponseMessage result = _sut.Delete(id);

            Assert.AreEqual(result.StatusCode, HttpStatusCode.OK);

            _service.Received(1).Delete(id);
            _service.Received(1).DoesCompanyExists(id);
        }

        [Test]
        public void Delete_CompanyExistsQueryFalse_Test()
        {
            long id = 2;
            _service.Delete(id).Returns(false);
            _service.DoesCompanyExists(id).Returns(true);
            HttpResponseMessage result = _sut.Delete(id);

            Assert.AreEqual(result.StatusCode, HttpStatusCode.InternalServerError);

            _service.Received(1).Delete(id);
            _service.Received(1).DoesCompanyExists(id);
        }

        [Test]
        public void UpdateTest_ModelNotValid_Test()
        {
            _sut.ModelState.AddModelError("key", "error");
            Company company = new Company() { Id = 2};

           HttpResponseMessage result = _sut.Update(company);

            Assert.AreEqual(result.StatusCode, HttpStatusCode.BadRequest);

            _service.ReceivedWithAnyArgs(0).Update(company);
            _service.ReceivedWithAnyArgs(0).DoesCompanyExists(company.Id);
        }

        [Test]
        public void UpdateTest_elementDoesntExist_Test()
        {
            Company company = new Company() { Id = 2};
            _service.DoesCompanyExists(company.Id).Returns(false);
            HttpResponseMessage result = _sut.Update(company);
            Assert.AreEqual(result.StatusCode, HttpStatusCode.NotFound);

            _service.Received(1).DoesCompanyExists(company.Id);
            _service.ReceivedWithAnyArgs(0).Update(company);
        }

        [Test]
        public void Update_elementExist_Sucess_Test()
        {
            Company company = new Company() ;
            _service.DoesCompanyExists(company.Id).Returns(true);
            _service.Update(company).Returns(true);

            HttpResponseMessage result = _sut.Update(company);

            Assert.AreEqual(result.StatusCode, HttpStatusCode.OK);

            _service.Received(1).DoesCompanyExists(company.Id);
            _service.Received(1).Update(company);
        }

        [Test]
        public void Update_elementExists_Failure_Test()
        {
            Company company = new Company();
            _service.DoesCompanyExists(company.Id).Returns(true);
            _service.Update(company).Returns(false);

            HttpResponseMessage result = _sut.Update(company);

            Assert.AreEqual(result.StatusCode, HttpStatusCode.InternalServerError);

            _service.Received(1).DoesCompanyExists(company.Id);
            _service.Received(1).Update(company);
        }
    }
}
