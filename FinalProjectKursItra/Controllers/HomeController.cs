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

        public ActionResult ShowComments(Int32 companyId)
        {
            ApplicationUser user = (_userManager.GetUserAsync(User)).Result;
            Company company = context.Companies.Find(companyId);
            CompanyViewModel model = new CompanyViewModel()
            {
                Company = company,
                User = user,
                Tags = CompanyHelper.GetAllCompanyTags(company.CompanyId, context),
                Comments = context.Comments.Where(c => c.CompanyId == company.CompanyId).ToList(),
                Context = context
            };
            ActionResult actionResult = View("Company", model);

            if (company != null)
            {
                actionResult = PartialView("_Comments", model);
            }

            return actionResult;
        }

        public HomeController(
            UserManager<ApplicationUser> userManager,
          SignInManager<ApplicationUser> signInManager, 
          ApplicationDbContext dbContext)

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
        public IActionResult ShowCategoryCompanies(int category)
        {
            List<Company> companyList = context.Companies.Where(company => Convert.ToInt32(company.Category) == category).ToList();
            HomeIndexViewModel newModel = new HomeIndexViewModel()
            {
                TagId = null,
                Tags = context.Tags.ToList(),
                Companies = companyList,
                Context = context
            };
            return View("Index", newModel);
        }
        public IActionResult LikeComment(string userId, int commentId, string path)
        {
            Comment comment = context.Comments.Find(commentId);

            if (context.Votes.Where(c => c.CommentId == commentId && c.UserId == userId).ToList().Count == 0)
            {
                comment.VoteCount++;
                Vote vote = new Vote()
                {
                    CommentId = commentId,
                    UserId = userId,
                };
                context.Votes.Add(vote);
            }
            Company company = context.Companies.Find(comment.CompanyId);
            context.SaveChanges();
            CompanyViewModel model = new CompanyViewModel()
            {
                Company = company,
                User = context.ApplicationUsers.Find(userId),
                Tags = CompanyHelper.GetAllManualTags(company.CompanyId, context),
                Comments = context.Comments.Where(c => c.CompanyId == company.CompanyId).ToList(),
                Context = context
            };
            return View("Company", model);
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
        public async Task<IActionResult> Company(int id, string userid)
        {
            //var user = await _userManager.FindByIdAsync(userid);
            var user = await _userManager.GetUserAsync(User);
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