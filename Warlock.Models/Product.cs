using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Warlock.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public required string Title { get; set; }
        public string? Description { get; set; }

        [Required]
        public required int Level { get; set; }

        [Required]
        public required string Tier { get; set; }

        [Required]
        [Display(Name = "List Price")]
        [Range(1, 100)]
        public required double ListPrice { get; set; }

        [Required]
        [Display(Name = "Price for 1-50")]
        [Range(1, 100)]
        public required double Price { get; set; }

        [Required]
        [Display(Name = "Price for 50+")]
        [Range(1, 100)]
        public required double Price50 { get; set; }

        [Required]
        [Display(Name = "Price for 100+")]
        [Range(1, 100)]
        public required double Price100 { get; set; }

        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        [ValidateNever]
        public Category? Category { get; set; }
        public string? ImageUrl { get; set; }
    }
}
