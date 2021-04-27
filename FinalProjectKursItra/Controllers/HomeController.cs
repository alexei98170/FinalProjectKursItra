using FinalProjectKursItra.Data;
using FinalProjectKursItra.Models;
using HeyRed.MarkdownSharp;
using ManualsSite.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectKursItra.Controllers
{
    public class HomeController : Controller
    {
        Markdown mark = new Markdown(new MarkdownOptions
        {
            AutoHyperlink = true,
            AutoNewLines = true,
            LinkEmails = true,
            QuoteSingleLine = true,
            StrictBoldItalic = true
        });
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext context;
    
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public async Task<IActionResult> Company(int id, string userid)
        {

            var user = await _userManager.GetUserAsync(User);
            Company company = context.Companies.Find(id);
            ViewBag.userid = userid;
            CompanyViewModel model = new CompanyViewModel
            {
                Company = company,
                Comments = context.Comments.Where(c => c.CompanyId == company.CompanyId).ToList(),
                User = user,
                Context = context,
                Tags = CompanyHelper.GetAllManualTags(id, context)
            };

            return View(model);
        }
    }
}