using System.Collections.Generic;
using PumTestProject.DTO;
using PumTestProject.Model;

namespace PumTestProject.Services
{
    public interface ICompanyService
    {
        List<Company> GetAllCompanies();
        List<CompanyDTO> Search(CompanySearchDTO queryCriteria);
        long Create(Company company);
        bool Update(Company company);
    }
}