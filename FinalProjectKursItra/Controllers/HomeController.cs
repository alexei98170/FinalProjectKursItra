using FinalProjectKursItra.Data;
using FinalProjectKursItra.Models;
using FinalProjectKursItra.ViewModels;
using HeyRed.MarkdownSharp;
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

        public HomeController(UserManager<ApplicationUser> userManager,
          SignInManager<ApplicationUser> signInManager, ApplicationDbContext dbContext)

        {
            _userManager = userManager;
            _signInManager = signInManager;
            context = dbContext;
        }

        public IActionResult Index(int? tagId = null)
        {
            List<Company> list = new List<Company>();
            ApplicationUser user = (_userManager.GetUserAsync(User)).Result;

            if (tagId != null)
            {
                List<CompanyTag> companyTags = context.CompanyTags.Where(company => company.TagId == tagId).ToList();
                foreach (CompanyTag company in companyTags)
                {
                    list.Add(context.Companies.Find(company.CompanyId));
                }
                list.Reverse();
                HomeIndexViewModel newModel = new HomeIndexViewModel()
                {
                    TagId = tagId,
                    Tags = context.Tags.ToList(),
                    User = user,
                    Companies = list,
                    Context = context
                };
                return View(newModel);
            }
            else
            {
                list = context.Companies.ToList();
                HomeIndexViewModel newModel = new HomeIndexViewModel()
                {
                    TagId = null,
                    Tags = context.Tags.ToList(),
                    User = user,
                    Companies = list,
                    Context = context
                };
                return View(newModel);
            }
        }
        [HttpPost]
        public ActionResult AddComment(string content, string userId, int companyId)
        {
            Comment toAdd = new Comment()
            {
                AuthorId = userId,
                Content = mark.Transform(content),
                CompanyId = companyId,
                VoteCount = 0,
                ReleaseDate = DateTime.Now
            };
            Company company = context.Companies.Find(companyId);
            company.LastUpdate = DateTime.Now;
            context.Comments.Add(toAdd);
            context.SaveChanges();
            CompanyViewModel model = new CompanyViewModel()
            {
                Company = company,
                User = context.ApplicationUsers.Find(userId),

                Tags = CompanyHelper.GetAllCompanyTags(companyId, context),
                Comments = context.Comments.Where(c => c.CompanyId == companyId).ToList(),
                Context = context


            };
            return View("Company", model);
        }
        public ActionResult ShowComments(Int32 manualId)
        {
            ApplicationUser user = (_userManager.GetUserAsync(User)).Result;
            Company company = context.Companies.Find(manualId);
            CompanyViewModel model = new CompanyViewModel()
            {
                Company = company,
                User = user,
                Tags = CompanyHelper.GetAllCompanyTags(company.CompanyId, context),
                Comments = context.Comments.Where(c => c.CompanyId == company.CompanyId).ToList(),
                Context = context
            };
            ActionResult actionResult = View("Manual", model);

            if (company != null)
            {
                actionResult = PartialView("_Comments", model);
            }

            return actionResult;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public async Task<IActionResult> Company(int id, string userid)
        {
            var user = await _userManager.FindByIdAsync(userid);
            //var user = await _userManager.GetUserAsync(User);
            Company company = context.Companies.Find(id);
            ViewBag.userid = userid;
            CompanyViewModel model = new CompanyViewModel
            {
                Company = company,
                Comments = context.Comments.Where(c => c.CompanyId == company.CompanyId).ToList(),
                User = user,
                Context = context,
                Tags = CompanyHelper.GetAllCompanyTags(id, context)
            };

            return View(model);
        }
    }
}