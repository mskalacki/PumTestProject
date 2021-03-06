﻿using System.Collections.Generic;
using PumTestProject.DTO;
using PumTestProject.Model;

namespace PumTestProject.DAO
{
    public interface ICompanyDAO
    {
        List<Company> GetAllCompanies();
        List<CompanyDTO> Search(CompanySearchDTO queryCriteria);
        long Create(Company company);
        bool Update(Company company);
        bool Delete(long id);
        bool DoesCompanyExists(long id);
    }
}