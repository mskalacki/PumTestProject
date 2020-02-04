using System.Collections.Generic;
using PumTestProject.Model;

namespace PumTestProject.Services
{
    public interface ICompanyService
    {
        List<Company> GetAllCompanies();
    }
}