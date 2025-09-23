using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace AgriWiki_Project.Pages
{
    public class AddPlant : PageModel
    {
        private readonly ILogger<AddPlant> _logger;

        public AddPlant(ILogger<AddPlant> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}