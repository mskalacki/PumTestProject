using PumTestProject.DAO;
using PumTestProject.DTO;
using PumTestProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PumTestProject.Services
{
    public class CompanyService : ICompanyService
    {
        private ICompanyDAO _dao;

        public CompanyService(ICompanyDAO dao)
        {
            this._dao = dao;
        }

        public List<Company> GetAllCompanies()
        {
            return _dao.GetAllCompanies();
        }

        public List<CompanyDTO>Search(CompanySearchDTO queryCriteria)
        {
            return _dao.Search(queryCriteria);
        }

        public long Create(Company company)
        {
            return _dao.Create(company);
        }
    }
}
