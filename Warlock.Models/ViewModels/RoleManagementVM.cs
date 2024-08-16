using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Warlock.Models.ViewModels
{
    public class RoleManagementVM
    {
        public required ApplicationUser ApplicationUser { get; set; }
        public required IEnumerable<SelectListItem> RoleList { get; set; }
        public required IEnumerable<SelectListItem> FactionList { get; set; }
    }
}
