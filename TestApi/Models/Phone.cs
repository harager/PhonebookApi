using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TestApi.Models
{
    public class Phone
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(30)")]
        public string PhoneNumber { get; set; }
        [Required]
        [Column(TypeName = "int")]
        public int ContactID { get; set; }

        public virtual Contact Contact { get; set; }
    }
}
