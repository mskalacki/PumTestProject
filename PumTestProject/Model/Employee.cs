using PumTestProject.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PumTestProject.Model
{
    [Table("employees")]
    public class Employee
    {
        
        [Key]
        [Column("id")]
        public long Id { get; set;}

        
        [Column("name")]
        [Required]
        public string Name { get; set;}

        [Column("surname")]
        [Required]
        public string Surname { get; set;}

        [Column("birthdate")]
        [Required]
        public DateTime BirthDate { get; set;}

        [Column("jobtitle")]
        [Required]
        public JobTitle  JobTitle { get; set;}

        [Column("company")]
        public Company Company { get; set; }

        
    }
}
