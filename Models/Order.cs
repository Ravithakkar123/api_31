using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Webapi.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(5)")]
        public int OrderNo { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(5)")]
        public float TotalAmount { get; set; }
        public IList<OrderItem> OrderItem { get; set; }

      
        public int CustomerId { get; set; }
        public Customer customer { get; set;}
   
     


    }
}
