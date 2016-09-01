using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Repository.Models
{
    public class Order
    {
        public Order()
        {
            this.Items = new HashSet<OrderItem>();
        }
        [Display(Name = "Order Id:")]
        public string OrderId { get; set; }
        [Display(Name = "Order date:")]
        [Required(ErrorMessage = "Order date is required")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}",ApplyFormatInEditMode = true)]
        public System.DateTime OrderDate { get; set; }
        [Display(Name = "Order value:")]
        [Required(ErrorMessage = "Order Value is required")]
        public string OrderValue { get; set; }
        public virtual ICollection<OrderItem> Items { get; set; }
        [Display(Name = "User Id:")]
        [Required(ErrorMessage = "User Id is required")]
        public string UserId { get; set; }
        public virtual User User { get; set; }
    }
}