using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Repository.Models
{
    public class Product
    {
        public Product()
        {
        }
        [Display(Name = "Id:")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Display(Name = "Name:")]
        [MaxLength(100)]
        public string Name { get; set; }
        [Display(Name = "Description:")]
        [MaxLength(500)]
        public string Description { get; set; }
    }
}