using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingEvents.Models
{
    public class Item
    {
        [Key]
        [Display(Name = "ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ItemCode { get; set; }
        [Required]
        [ForeignKey("Department")]
        [Display(Name = "Category")]
        public int Department_ID { get; set; }
        public virtual Department Department { get; set; }

        [Required]
        [Display(Name = "Name")]
        [MinLength(3)]
        [MaxLength(80)]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Description")]
        [DataType(DataType.MultilineText)]
        [MinLength(3)]
        [MaxLength(255)]
        public string Description { get; set; }
        [Display(Name = "Qty in Stock")]
        public int QuantityInStock { get; set; }
        //[Required]
        [Display(Name = "Picture")]
        //[DataType(DataType.Upload)]
        public byte[] Picture { get; set; }
        [Required]
        [Display(Name = "Price")]
        [DataType(DataType.Currency)]
        public double Price { get; set; }

        [Required]
        [Display(Name = "Cost Price")]
        [DataType(DataType.Currency)]
        public double CostPrice { get; set; }

        public virtual ICollection<Cart_Item> Cart_Items { get; set; }
        //public virtual ICollection<Supplier_Item> Supplier_Items { get; set; } 
        public virtual ICollection<FoodOrder> FoodOrders { get; set; }

        public double calculateCostDifference()
        {
            return Price - CostPrice;
        }

    }
}
