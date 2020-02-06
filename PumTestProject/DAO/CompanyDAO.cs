using PumTestProject.DTO;
using PumTestProject.Enums;
using PumTestProject.Model;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
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
            List<CompanyDTO> Result = new List<CompanyDTO>();

            using (PumContext context = _factory.CreateContext())
            {
                context.Configuration.ProxyCreationEnabled = false;
                try
                {
                    if (!string.IsNullOrEmpty(queryCriteria.Keyword))
                    {
                        queryCriteria.Keyword = queryCriteria.Keyword.ToLower();

                        Result = context.Companies.Select(company =>
                                        new
                                        {
                                            company.Name,
                                            company.EstablishmentYear,
                                            Employees = company.Employees.ToList()
                                        }

                                        ).Select(company =>
                                        new CompanyDTO()
                                        {
                                            Name = company.Name,
                                            EstablishmentYear = company.EstablishmentYear,
                                            Employees = company.Employees
                                        }

                                        )
                                        .Where(company =>
                                        company.Name.ToLower().Contains(queryCriteria.Keyword)
                                        ||
                                         company.Employees.Where(employee => employee.Name.ToLower().Contains(queryCriteria.Keyword)).FirstOrDefault().Name.ToLower().Contains(queryCriteria.Keyword)
                                        ||
                                         company.Employees.Where(employee => employee.Surname.ToLower().Contains(queryCriteria.Keyword)).FirstOrDefault().Surname.ToLower().Contains(queryCriteria.Keyword)

                                       ).ToList();
                    }
                  
                    else if(queryCriteria.EmployeeDateOfBirthFrom != new DateTime(0001, 01, 01) && queryCriteria.EmployeeDateOfBirthTo == new DateTime(0001, 01, 01))
                    {
                        queryCriteria.EmployeeDateOfBirthTo = queryCriteria.EmployeeDateOfBirthTo == new DateTime(0001, 01, 01) ? new DateTime(9999, 12, 31) : queryCriteria.EmployeeDateOfBirthTo;
                        Result = context.Companies.Select(company =>
                                        new
                                        {
                                            company.Name,
                                            company.EstablishmentYear,
                                            Employees = company.Employees.ToList()
                                        }

                                        ).Select(company =>
                                        new CompanyDTO()
                                        {
                                            Name = company.Name,
                                            EstablishmentYear = company.EstablishmentYear,
                                            Employees = company.Employees
                                        }

                                        )
                                        .Where(company =>
                                        company.Employees.Any(employee => employee.BirthDate >= queryCriteria.EmployeeDateOfBirthFrom && employee.BirthDate <= queryCriteria.EmployeeDateOfBirthTo)
                                       

                                       ).ToList();
                    }
                    else
                    {
                        Result = context.Companies.Select(company =>
                                        new
                                        {
                                            company.Name,
                                            company.EstablishmentYear,
                                            Employees = company.Employees.ToList()
                                        }

                                        ).Select(company =>
                                        new CompanyDTO()
                                        {
                                            Name = company.Name,
                                            EstablishmentYear = company.EstablishmentYear,
                                            Employees = company.Employees
                                        }
                                        )
                                        .Where(company =>
                                        company.Employees.Any(employee => (employee.JobTitle) == queryCriteria.EmployeeJobTitles.Where(criteriaJobTitle => (int)criteriaJobTitle == (int)employee.JobTitle).FirstOrDefault())      

                                       ).ToList();

                    }
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

        public bool Update(Company company)
        {
            bool result = false;
            using (PumContext context = _factory.CreateContext())
            {
                using (DbContextTransaction tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        Company companyFromDb = context.Companies.Where(x => x.Id == company.Id).FirstOrDefault();

                        if (DoesCompanyExists(company.Id))
                        {
                            company.Id = companyFromDb.Id;
                            context.Entry(companyFromDb).CurrentValues.SetValues(company);
                            context.SaveChanges();
                            tran.Commit();
                            result = true;
                        }
                    }
                    catch (Exception ex)
                    {

                        tran.Rollback();
                        result = false;
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            return result;
        }

        public bool Delete (long id)
        {
            bool result = false;

            using (PumContext context = _factory.CreateContext())
            {
                using (DbContextTransaction tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        Company companyToDelete = context.Companies.Where(x => x.Id == id).FirstOrDefault();
                        if (DoesCompanyExists(id))
                        {
                            context.Companies.Remove(companyToDelete);
                            context.SaveChanges();
                            tran.Commit();
                            result = true;
                        }
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

        public bool DoesCompanyExists(long id)
        {
            bool result = false;
            using( PumContext context = _factory.CreateContext())
            {
                result = context.Companies.Where(x => x.Id == id).FirstOrDefault() != null ? true : false;
            }
            return result;
            
        }
    }
}
