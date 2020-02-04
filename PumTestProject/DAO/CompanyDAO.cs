using PumTestProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PumTestProject.DAO
{
    public class CompanyDAO : ICompanyDAO
    {
        private IContextFactory _factory;

        public CompanyDAO(IContextFactory factory)
        {
            this._factory = factory;
        }

        public List<Company> GetAllCompanies()
        {
            List<Company> Result = new List<Company>();

            using (PumContext context = _factory.CreateContext())
            {
                context.Configuration.ProxyCreationEnabled = false;
                Result = context.Companies.Select(x =>
                new
                {
                    x.Id,
                    x.Name,
                    x.EstablishmentYear,
                    Employees = x.Employees.ToList()
                }

                ).ToList().Select(y =>
                new Company()
                {
                    Id = y.Id,
                    Name = y.Name,
                    EstablishmentYear = y.EstablishmentYear,
                    Employees = y.Employees
                }
                ).ToList();
            }

            return Result;
        }
    }
}
