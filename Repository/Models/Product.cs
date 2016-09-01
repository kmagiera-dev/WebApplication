using DataAnnotationsExtensions;
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
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(100)]
        public string Name { get; set; }
        [Display(Name = "Description:")]
        [Required(ErrorMessage = "Description is required")]
        [MaxLength(500)]
        public string Description { get; set; }
        [Display(Name = "Price:")]
        [Required(ErrorMessage = "Price Required")]
        [Min(0, ErrorMessage = "Price must be above 0")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        [DataType("Currency")]
        public decimal Price { get; set; }
    }
}