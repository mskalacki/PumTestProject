using NSubstitute;
using NUnit.Framework;
using PumTestProject.DAO;
using PumTestProject.DTO;
using PumTestProject.Model;
using PumTestProject.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PumTestProject.Tests.ServiceTests
{
    [TestFixture]
    public class CompanyServiceTest
    {
        private ICompanyService _sut;
        private ICompanyDAO _dao;

        [SetUp]
        public void Setup()
        {
            _dao = Substitute.For<ICompanyDAO>();
            _sut = new CompanyService(_dao);
        }

        [Test]
        public void GetAllCompaniesTest()
        {
            List<Company> CompaniesFromDb = new List<Company>();
            _dao.GetAllCompanies().Returns(CompaniesFromDb);

            List<Company> Result = _sut.GetAllCompanies();

            Assert.AreSame(Result, CompaniesFromDb);

            _dao.Received(1).GetAllCompanies();

        }
        [Test]
        public void SearchTest()
        {

            CompanySearchDTO queryCriteria = new CompanySearchDTO();
            List<CompanyDTO> elementsFromb = new List<CompanyDTO>();
            _dao.Search(queryCriteria).Returns(elementsFromb);

            List<CompanyDTO> result = _sut.Search(queryCriteria);

            Assert.AreSame(result, elementsFromb);

            _dao.Received(1).Search(queryCriteria);

        }
        [Test]
        public void CreateTest()
        {
            long id = 1;
            Company company = new Company();
            _dao.Create(company).Returns(id);

            long result = _sut.Create(company);

            Assert.AreEqual(result, id);

            _dao.Received(1).Create(company);

        }
        [Test]
        public void UpdateTest()
        {

            Company company = new Company();

            bool queryResult = true;
            _dao.Update(company).Returns(queryResult);

            bool result = _sut.Update(company);
            Assert.IsTrue(result);

            _dao.Received(1).Update(company);
            
        }
        [Test]
        public void DoesCompanyExistsTest()
        {
            long id = 1;

            _dao.DoesCompanyExists(id).Returns(true);

            bool result = _sut.DoesCompanyExists(id);

                Assert.IsTrue(result);

            _dao.Received(1).DoesCompanyExists(id);

        }


    }
}
