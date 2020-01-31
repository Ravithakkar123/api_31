using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Webapi.Models
{
    public class FoodItem
    {

        [Key]
        public int FoodItemId { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(15)")]
        public string FoodItemName { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string FoodItemInfo { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(5)")]
        public int FoodItemPrice { get; set; }
    }
}
