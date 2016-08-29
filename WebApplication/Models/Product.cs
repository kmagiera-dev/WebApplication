using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class Product
    {
        public Product()
        {
        }
        [Display(Name = "Id:")]
        public int Id { get; set; }
        [Display(Name = "Nazwa:")]
        [MaxLength(100)]
        public string Name { get; set; }
        [Display(Name = "Opis:")]
        [MaxLength(500)]
        public string Description { get; set; }
    }
}