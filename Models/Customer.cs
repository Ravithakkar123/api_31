using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Webapi.Models
{
    public class Customer
    {
      

        [Key]
        public int CustomerId { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(40)")]
        public string CustomerName { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(12)")]
        public string CustomerNum { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(20)")]
        public string CustomerEmail { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(6)")]
        public string Gender { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(6)")]
        public float Ratting { get; set; }
        public IList<Address> Addresses { get; set; }
    }
}
