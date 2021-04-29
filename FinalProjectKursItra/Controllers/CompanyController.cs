using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FinalProjectKursItra.Data;
using FinalProjectKursItra.Models;
using FinalProjectKursItra.ViewModels;
using HeyRed.MarkdownSharp;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.AspNetCore.Identity;

namespace FinalProjectKursItra.Controllers
{
    public class CompanyController : Controller
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

        public CompanyController(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager, ApplicationDbContext dbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = dbContext;
        }

        // GET: Company
        public async Task<IActionResult> Index()
        {
            var companies = _context.Companies.Where(i => i.Saved == false).ToList();
            
            foreach (Company man in companies)
            {
                DelEmptyTags(man.CompanyId, _context);
                _context.Companies.Remove(man);
            }

            _context.SaveChanges();

            var user = await _userManager.GetUserAsync(User);

            return View(user);
        }
           
            public async Task<IActionResult> Details(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var company = await _context.Companies
                    .FirstOrDefaultAsync(m => m.CompanyId == id);
                if (company == null)
                {
                    return NotFound();
                }

                return View(company);
            }

      
            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult Create(string id)
            {
                CreateViewModel model = new CreateViewModel()
                {
                    Company = null,
                    AuthorId = id,

                    Tags = new List<string>()
                };
                return View(model);
            }
            [HttpPost]
            [ActionName("CreateCompany")]
            public ActionResult CreateCompany(CreateCompanyViewModel model)
            {
                CreateViewModel newModel = new CreateViewModel();

                if (
                    (model.Title != null) &&
                    (model.Title != "") &&
                    (model.Description != null) &&
                    (model.Description != "") &&
                    (model.Tags != null) &&
                    (model.Tags != "")
                )
                {
                    Company company = new Company()
                    {
                        AuthorId = model.AuthorId,
                        Title = mark.Transform(model.Title),
                        Description = mark.Transform(model.Description),
                        Photo = model.Photo,
                        Category = model.Category,
                        ReleaseDate = DateTime.Now,
                        LastUpdate = DateTime.Now
                    };

                    EntityEntry<Company> e = _context.Companies.Add(company);
                    _context.SaveChanges();
                    Company i = e.Entity;


                    string[] tags = null;
                    if ((model?.Tags != null) && (model.Tags.Split(',').Count() > 0))
                    {
                        tags = model.Tags.Split(',');
                        foreach (string t in tags)
                        {
                            if (t != "")
                            {
                                Tag fTag = null;

                                if (_context.Tags.Where(dt => dt.Name == t).ToList().Count != 0)
                                {
                                    fTag = _context.Tags.Where(dt => dt.Name == t).ToList().First();
                                }

                                if (fTag != null)
                                {
                                    _context.CompanyTags.Add(new CompanyTag() { TagId = fTag.TagId, CompanyId = i.CompanyId });
                                }
                                else
                                {
                                    EntityEntry<Tag> added = _context.Tags.Add(new Tag() { Name = t });
                                    _context.SaveChanges();
                                    Tag aTag = added.Entity;
                                    _context.CompanyTags.Add(new CompanyTag() { TagId = aTag.TagId, CompanyId = i.CompanyId });
                                }
                            }
                        }
                    }

                    newModel = new CreateViewModel()
                    {
                        Company = i,
                        AuthorId = i.AuthorId,
                        Tags = tags.ToList(),

                    };
                    company.Saved = true;
                    _context.SaveChanges();
                }

                return View("Create", newModel);
            }

            public async Task<IActionResult> Edit(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var company = await _context.Companies.FindAsync(id);
                if (company == null)
                {
                    return NotFound();
                }
                return View(company);
            }

         
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit(int id, [Bind("CompanyId,AuthorId,Title,Description,Photo,Category,Saved,ReleaseDate,LastUpdate,RatesAmount,RatesCount")] Company company)
            {
                if (id != company.CompanyId)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(company);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!CompanyExists(company.CompanyId))
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
                return View(company);
            }

            // GET: Company/Delete/5
            public async Task<IActionResult> Delete(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var company = await _context.Companies
                    .FirstOrDefaultAsync(m => m.CompanyId == id);
                if (company == null)
                {
                    return NotFound();
                }

                return View(company);
            }

            // POST: Company/Delete/5
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> DeleteConfirmed(int id)
            {
                var company = await _context.Companies.FindAsync(id);
                _context.Companies.Remove(company);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
        private void DelEmptyTags(int id, ApplicationDbContext context)
        {
            List<CompanyTag> manualTags = CompanyHelper.GetCompanyTags(id, context);
            foreach (CompanyTag mTag in manualTags)
            {
                if (CompanyHelper.CountSameNameCompanyTags(mTag.CompanyTagId, context) < 2)
                {
                    context.Tags.Remove(context.Tags.Find(mTag.TagId));
                }
            }
            context.SaveChanges();
        }
        private bool CompanyExists(int id)
            {
                return _context.Companies.Any(e => e.CompanyId == id);
            }
        }
    } 
