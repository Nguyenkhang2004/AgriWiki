using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace AgriWiki_Project.Pages
{
    public class ViewDetails : PageModel
    {
        private readonly ILogger<ViewDetails> _logger;

        public ViewDetails(ILogger<ViewDetails> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}