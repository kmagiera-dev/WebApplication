using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Repository.Models
{
    public class OrderItem
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Display(Name = "Product Id:")]
        public int ProductId { get; set; }
        [Display(Name = "Amount:")]
        public int Amount { get; set; }
        [NotMapped]
        [Display(Name = "Total price:")]
        public string TotalPrice
        {
            get { return (Amount*Product.Price).ToString("C2"); }
        }
        public virtual Product Product { get; set; }
        [Display(Name = "Order Id:")]
        public string OrderId { get; set;  }
        public virtual Order Order { get; set; }
    }
}