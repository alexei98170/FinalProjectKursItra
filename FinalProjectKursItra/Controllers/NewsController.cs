using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FinalProjectKursItra.Data;
using FinalProjectKursItra.Models;
using Microsoft.AspNetCore.Identity;
using HeyRed.MarkdownSharp;
using FinalProjectKursItra.ViewModels;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace FinalProjectKursItra.Controllers
{
    public class NewsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;
        Markdown mark = new Markdown(new MarkdownOptions
        {
            AutoHyperlink = true,
            AutoNewLines = true,
            LinkEmails = true,
            QuoteSingleLine = true,
            StrictBoldItalic = true
        });

        public NewsController(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager, 
        ApplicationDbContext dbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = dbContext;
        }

        // GET: News
        public async Task<IActionResult> Index()
        {
            var news = _context.News.Where(i => i.Saved == false).ToList();

            foreach (News man in news)
            {
                _context.News.Remove(man);
            }

            _context.SaveChanges();

            var user = await _userManager.GetUserAsync(User);

            return View(user);
        }

        // GET: News/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var news = await _context.News
                .FirstOrDefaultAsync(m => m.NewsId == id);
            if (news == null)
            {
                return NotFound();
            }

            return View(news);
        }

        // GET: News/Create
        public IActionResult Create(string id)
        {
            CreateViewModelForNews model = new CreateViewModelForNews()
            {
                News = null,
                AuthorId = id,
                CompanyId = id,
            };
            return View(model);
        }
      
        [HttpPost]
        [ActionName("CreateNews")]
        public async Task<IActionResult> CreateNews(CreateNewsViewModel model)
        {
            CreateViewModelForNews newModel = new CreateViewModelForNews();

            if (
                (model.Title != null) &&
                (model.Title != "") &&
                (model.Description != null) &&
                (model.Description != "")
                
            )
            {
                News news = new News()
                {
                    AuthorId = model.AuthorId,
                    CompanyId = model.CompanyId,
                    Title = mark.Transform(model.Title),
                    Description = mark.Transform(model.Description),
                    Photo = model.Photo,
                    ReleaseDate = DateTime.Now,
                    LastUpdate = DateTime.Now
                };

                EntityEntry<News> e = _context.News.Add(news);
                _context.SaveChanges();
                News i = e.Entity;

                newModel = new CreateViewModelForNews()
                {
                    News = i,
                    AuthorId = i.AuthorId,
                    CompanyId = i.CompanyId,

                };
                news.Saved = true;
                _context.SaveChanges();
            }

            return View("Create", newModel);
        }

        // GET: News/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var news = await _context.News.FindAsync(id);
            if (news == null)
            {
                return NotFound();
            }
            return View(news);
        }

   
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NewsId,AuthorId,Title,Description,Photo,Saved,ReleaseDate,LastUpdate")] News news)
        {
            if (id != news.NewsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(news);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NewsExists(news.NewsId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(news);
        }

        // GET: News/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var news = await _context.News
                .FirstOrDefaultAsync(m => m.NewsId == id);
            if (news == null)
            {
                return NotFound();
            }

            return View(news);
        }

        // POST: News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var news = await _context.News.FindAsync(id);
            _context.News.Remove(news);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NewsExists(int id)
        {
            return _context.News.Any(e => e.NewsId == id);
        }
        public async Task<IActionResult> News(int id, string companyid)
        {
            //var user = await _userManager.FindByIdAsync(userid);
            var user = await _userManager.GetUserAsync(User);
            News news = _context.News.Find(id);
            ViewBag.companyid = companyid;
            NewsViewModel model = new NewsViewModel
            {
                News = news,
                User = user,
                Context = _context,
            };

            return View(model);
        }
    }
}
