using System.Collections.Generic;
using PumTestProject.Model;

namespace PumTestProject.DAO
{
    public interface ICompanyDAO
    {
        List<Company> GetAllCompanies();
    }
}