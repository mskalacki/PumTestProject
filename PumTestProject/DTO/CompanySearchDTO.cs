using PumTestProject.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PumTestProject.DTO
{
    public class CompanySearchDTO
    {
        public string Keyword { get; set; }
        public DateTime EmployeeDateOfBirthFrom { get; set; }
        public DateTime EmployeeDateOfBirthTo { get; set; }
        public List<JobTitle> EmployeeJobTitles { get; set; }
    }
}
