using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Webapi.Models
{
    public enum AddressTypes
    {
        Home=0,
        Work=1,
        Other=2
    }
    public class Address
    {
        [Key]
        public int AddressId { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(10)")]
        public AddressTypes AddressType { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string Street1 { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string Street2 { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(20)")]
        public string City { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(20)")]
        public string District { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(6)")]
        public long PinCode { get; set; }
    }
}
