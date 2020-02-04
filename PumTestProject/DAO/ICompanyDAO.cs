using System.Collections.Generic;
using PumTestProject.DTO;
using PumTestProject.Model;

namespace PumTestProject.DAO
{
    public interface ICompanyDAO
    {
        List<Company> GetAllCompanies();
        List<CompanyDTO> Search(CompanySearchDTO queryCriteria);
    }
}