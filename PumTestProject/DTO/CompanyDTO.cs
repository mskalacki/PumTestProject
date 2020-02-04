using PumTestProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PumTestProject.DTO
{
   public class CompanyDTO
    {
        
        public long Id { get; set; }
        public string Name { get; set; }
        public int EstablishmentYear { get; set; }

        public List<Employee> Employees { get; set; }
    }
}
