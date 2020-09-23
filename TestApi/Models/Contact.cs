using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TestApi.Models
{
    public class Contact
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(30)")]
        public string FirstName { get; set; }
        
        [Column(TypeName = "nvarchar(30)")]
        public string LastName { get; set; }

        [Column(TypeName = "nvarchar(30)")]
        public string Email { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string Address { get; set; }
        [Required]
        [Column(TypeName = "int")]
        public int UserID { get; set; }

        public virtual User User { get; set; }

        public virtual List<Phone> Phones { get; set; }
    }
}
