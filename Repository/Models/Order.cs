using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Repository.Models
{
    public class Order
    {
        public Order()
        {
            this.Product = new HashSet<Product>();
        }
        public int Id { get; set; }
        [Display(Name = "Order Id:")]
        public string OrderId { get; set; }
        [Display(Name = "Order date:")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}",ApplyFormatInEditMode = true)]
        public System.DateTime OrderDate { get; set; }
        [Display(Name = "Order value:")]
        public string OrderValue { get; set; }
        public virtual ICollection<Product> Product { get; set; }
        public string UserId { get; set; }
        public virtual User User { get; set; }
    }
}