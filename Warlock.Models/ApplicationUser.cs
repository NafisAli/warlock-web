using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Warlock.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public required string Name { get; set; }

        public string? StreetAddress { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? PostCode { get; set; }
        public int? FactionId { get; set; }

        [ForeignKey("FactionId")]
        [ValidateNever]
        public Faction? Faction { get; set; }

        [NotMapped]
        public string? Role { get; set; }
    }
}
