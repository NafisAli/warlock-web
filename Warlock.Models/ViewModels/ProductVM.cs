﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Warlock.Models.ViewModels
{
    public class ProductVM
    {
        public required Product Product { get; set; }

        [ValidateNever]
        public required IEnumerable<SelectListItem> CategoryList { get; set; }
    }
}
