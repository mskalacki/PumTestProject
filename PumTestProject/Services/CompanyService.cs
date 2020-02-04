using PumTestProject.DAO;
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
    }
}
