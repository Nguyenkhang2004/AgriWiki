using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace AgriWiki_Project.Pages
{
    public class PlantsManagement : PageModel
    {
        private readonly ILogger<PlantsManagement> _logger;

        public PlantsManagement(ILogger<PlantsManagement> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}