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
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }
        // Requires using Microsoft.AspNetCore.Mvc.Rendering;
        public SelectList Books { get; set; }
        [BindProperty(SupportsGet = true)]
        public string JournalEntryBook { get; set; }
        [BindProperty(SupportsGet = true)]
        public string newSearchString { get; set; }
        // Requires using Microsoft.AspNetCore.Mvc.Rendering;
        public SelectList Notes { get; set; }
        [BindProperty(SupportsGet = true)]
        public string NoteKeyWord { get; set; }
        [BindProperty(SupportsGet = true)] 
        public string BookSort { get; set; }
        [BindProperty(SupportsGet = true)]
        public string DateSort { get; set; }
        // public string CurrentFilter { get; set; }
        // public string CurrentSort { get; set; }

        public async Task OnGetAsync(string sortOrder)
        {
       

            // Use LINQ to get list of genres.
            IQueryable<string> bookQuery = from e in _context.JournalEntry
                                            orderby e.EntryDate
                                            orderby e.Book
                                            select e.Book;
            IQueryable<string> notesQuery = from e in _context.JournalEntry
                                            orderby e.EntryDate
                                            orderby e.Book
                                            select e.Notes;

            IQueryable<JournalEntry> Journal = from s in _context.JournalEntry
                                               select s;
            if (!string.IsNullOrEmpty(SearchString))
            {
                Journal = Journal.Where(s => s.Book.Contains(SearchString));
            }
            if (!string.IsNullOrEmpty(JournalEntryBook))
            {
                Journal = Journal.Where(x => x.Book == JournalEntryBook);
            }
            if (!string.IsNullOrEmpty(newSearchString))
            {
                Journal = Journal.Where(s => s.Notes.Contains(newSearchString));
            }
            if (!string.IsNullOrEmpty(NoteKeyWord))
            {
                Journal = Journal.Where(x => x.Notes == NoteKeyWord);
            }

            BookSort = String.IsNullOrEmpty(sortOrder) ? "book_desc" : "";
            DateSort = sortOrder == "Date" ? "date_desc" : "Date";

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
            Notes = new SelectList(await notesQuery.Distinct().ToListAsync());
        }
    }
}
