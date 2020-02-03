using PumTestProject.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PumTestProject.Model
{
    public class Employee
    {
        public long Id { get; set;}
        public string Name { get; set;}
        public string Surname { get; set;}
        public DateTime BirthDate { get; set;}
        public Position  Position { get; set;}

        
    }
}
