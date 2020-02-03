using PumTestProject.Enums;
using PumTestProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PumTestProject.Services
{
    public class EmployeeService : IEmployeeService
    {

        public Employee GetEmployee()
        {
            return new Employee()
            {
                Name = "Marek",
                Surname = "Wacek",
                JobTitle = JobTitle.Administrator,
                BirthDate = new DateTime(1980, 01, 02)
            };
        }
    }
}
