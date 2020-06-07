using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using My_Scripture_Journal.Models;

namespace My_Scripture_Journal.Pages.JournalEntries
{
    public class IndexModel : PageModel
    {
        private readonly My_Scripture_Journal.Models.My_Scripture_JournalContext _context;

        public IndexModel(My_Scripture_Journal.Models.My_Scripture_JournalContext context)
        {
            _context = context;
        }

        public IList<JournalEntry> JournalEntry { get;set; }

        public SelectList Books { get; set; }
        [BindProperty(SupportsGet = true)]
        public string JournalEntryBook { get; set; }
        [BindProperty(SupportsGet = true)]
        public string newSearchString { get; set; }
       
        [BindProperty(SupportsGet = true)] 
        public string BookSort { get; set; }
        [BindProperty(SupportsGet = true)]
        public string DateSort { get; set; }

        public async Task OnGetAsync(string sortOrder)
        {
            BookSort = String.IsNullOrEmpty(sortOrder) ? "book_desc" : "";
            DateSort = sortOrder == "Date" ? "date_desc" : "Date";
   
            // Use LINQ to get list of genres.
            IQueryable<string> bookQuery = from e in _context.JournalEntry
                                            orderby e.Book
                                            select e.Book;

            IQueryable<JournalEntry> Journal = from s in _context.JournalEntry
                                               select s;
             
            if (!string.IsNullOrEmpty(JournalEntryBook))
            {
                Journal = Journal.Where(x => x.Book == JournalEntryBook);
            }
            if (!string.IsNullOrEmpty(newSearchString))
             {
                 Journal = Journal.Where(s => s.Notes.Contains(newSearchString));
             }

            switch (sortOrder)
            {
                case "book_desc":
                    Journal = Journal.OrderByDescending(s => s.Book);
                    break;
                case "Date":
                    Journal = Journal.OrderBy(s => s.EntryDate);
                    break;
                case "date_desc":
                    Journal = Journal.OrderByDescending(s => s.EntryDate);
                    break;
                default:
                    Journal = Journal.OrderBy(s => s.Book);
                    break;
            }

            JournalEntry = await Journal.AsNoTracking().ToListAsync();
            Books = new SelectList(await bookQuery.Distinct().ToListAsync());
        }
    }
}
