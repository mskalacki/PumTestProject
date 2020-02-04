using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PumTestProject.Model
{
    [Table("companies")]
    public class Company
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Column("name")]
        [Required]
        public string Name { get; set; }

        [Column("establishmentyear")]
        [Required]
        public int EstablishmentYear{ get; set; }

        
        [Required]
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
