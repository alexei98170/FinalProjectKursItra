using FinalProjectKursItra.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectKursItra.Models
{
    sealed public class CompanyHelper
    {
        public static List<string> GetAllCompanyTags(int companyId, ApplicationDbContext context)
        {
            Company company = context.Companies.Find(companyId);
            List<CompanyTag> companyTags = context.CompanyTags.Where(t => t.CompanyId == companyId).ToList();
            List<string> tags = new List<string>();
            foreach (CompanyTag man in companyTags)
            {
                tags.Add(context.Tags.Find(man.TagId).Name);
            }
            return tags;
        }

        public static List<Comment> GetCompanyComments(int companyId, ApplicationDbContext context)
        {
            Company company = context.Companies.Find(companyId);
            List<Comment> manualComments = context.Comments.Where(t => t.CompanyId == companyId).ToList();
            return manualComments;
        }
        public static List<CompanyTag> GetCompanyTags(int companyId, ApplicationDbContext context)
        {
            Company company = context.Companies.Find(companyId);
            List<CompanyTag> manualTags = context.CompanyTags.Where(t => t.CompanyId == companyId).ToList();
            return manualTags;
        }
        public static int CountSameNameCompanyTags(int CompanyTagId, ApplicationDbContext context)
        {
            CompanyTag companyTag = context.CompanyTags.Find(CompanyTagId);
            Tag tag = context.Tags.Find(companyTag.TagId);
            List<CompanyTag> manualTags = context.CompanyTags.Where(t => t.TagId == tag.TagId).ToList();
            return manualTags.Count();
        }
        public static List<string> GetAllManualTags(int companyId, ApplicationDbContext context)
        {
            Company company = context.Companies.Find(companyId);
            List<CompanyTag> companyTags = context.CompanyTags.Where(t => t.CompanyId == companyId).ToList();
            List<string> tags = new List<string>();
            foreach (CompanyTag man in companyTags)
            {
                tags.Add(context.Tags.Find(man.TagId).Name);
            }
            return tags;
        }
    }
}
