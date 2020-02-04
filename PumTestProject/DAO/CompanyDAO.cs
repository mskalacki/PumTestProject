using PumTestProject.DTO;
using PumTestProject.Model;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
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

        public List<CompanyDTO> Search(CompanySearchDTO queryCriteria)
        {
            queryCriteria.Keyword = queryCriteria.Keyword != null ? queryCriteria.Keyword.ToLower() : "";

            List<CompanyDTO> Result = new List<CompanyDTO>();

            using (PumContext context = _factory.CreateContext())
            {
                context.Configuration.ProxyCreationEnabled = false;
                try
                {
                    Result = context.Companies.Select(x =>
                                    new
                                    {
                                        x.Name,
                                        x.EstablishmentYear,
                                        Employees = x.Employees.ToList()
                                    }

                                    ).Select(y =>
                                    new CompanyDTO()
                                    {
                                        Name = y.Name,
                                        EstablishmentYear = y.EstablishmentYear,
                                        Employees = y.Employees
                                    }

                                    ).Where(c =>
                                    c.Name.ToLower().Contains(queryCriteria.Keyword)
                                    ||
                                    (((c.Employees.Where(o =>
                                    o.Name.ToLower().Contains(queryCriteria.Keyword))).FirstOrDefault().Name.ToLower().Contains(queryCriteria.Keyword)
                                    ||
                                    (c.Employees.Where(o =>
                                    o.Surname.ToLower().Contains(queryCriteria.Keyword))).FirstOrDefault().Surname.ToLower().Contains(queryCriteria.Keyword))
                                     &&
                                    (((c.Employees.Where(e => e.BirthDate >= queryCriteria.EmployeeDateOfBirthFrom && e.BirthDate <= queryCriteria.EmployeeDateOfBirthTo).FirstOrDefault().BirthDate >= queryCriteria.EmployeeDateOfBirthFrom
                                    &&
                                    ((c.Employees.Where(e => e.BirthDate >= queryCriteria.EmployeeDateOfBirthFrom && e.BirthDate <= queryCriteria.EmployeeDateOfBirthTo).FirstOrDefault().BirthDate <= queryCriteria.EmployeeDateOfBirthTo)
                                    )))))

                                    ).ToList();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return Result;
        }

        public long Create(Company company)
        {
             long result = -1;

            using (PumContext context = _factory.CreateContext())
            {
                using (DbContextTransaction tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        context.Companies.Add(company);
                        context.SaveChanges();
                        tran.Commit();
                        result = company.Id;
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            return result;
        }
    }
}
