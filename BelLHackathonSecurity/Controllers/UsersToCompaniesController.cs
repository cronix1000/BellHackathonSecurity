using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BelLHackathonSecurity.Data;
using BelLHackathonSecurity.Models;

namespace BelLHackathonSecurity.Controllers
{
    public class UsersToCompaniesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UsersToCompaniesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: UsersToCompanies
        public async Task<IActionResult> Index()
        {
              return _context.UsersToCompany != null ? 
                          View(await _context.UsersToCompany.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.UsersToCompany'  is null.");
        }
        /**
        // GET: UsersToCompanies/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.UsersToCompany == null)
            {
                return NotFound();
            }

            var usersToCompany = await _context.UsersToCompany
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usersToCompany == null)
            {
                return NotFound();
            }

            return View(usersToCompany);
        }

        // GET: UsersToCompanies/Create
        public IActionResult Create()
        {
            return View();
        }
        **/
        // POST: UsersToCompanies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,CompanyId")] UsersToCompany usersToCompany)
        {
            if (ModelState.IsValid)
            {
                usersToCompany.Id = Guid.NewGuid();
                _context.Add(usersToCompany);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(usersToCompany);
        }

        // GET: UsersToCompanies/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.UsersToCompany == null)
            {
                return NotFound();
            }

            var usersToCompany = await _context.UsersToCompany.FindAsync(id);
            if (usersToCompany == null)
            {
                return NotFound();
            }
            return View(usersToCompany);
        }

        // POST: UsersToCompanies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,UserId,CompanyId")] UsersToCompany usersToCompany)
        {
            if (id != usersToCompany.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usersToCompany);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsersToCompanyExists(usersToCompany.Id))
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
            return View(usersToCompany);
        }

        // GET: UsersToCompanies/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.UsersToCompany == null)
            {
                return NotFound();
            }

            var usersToCompany = await _context.UsersToCompany
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usersToCompany == null)
            {
                return NotFound();
            }

            return View(usersToCompany);
        }

        // POST: UsersToCompanies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.UsersToCompany == null)
            {
                return Problem("Entity set 'ApplicationDbContext.UsersToCompany'  is null.");
            }
            var usersToCompany = await _context.UsersToCompany.FindAsync(id);
            if (usersToCompany != null)
            {
                _context.UsersToCompany.Remove(usersToCompany);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsersToCompanyExists(Guid id)
        {
          return (_context.UsersToCompany?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public async Task<IActionResult> Details()
        {
            // Use LINQ to join the tables
            var joinedData = await (from company in _context.Companies
                                    join userToCompany in _context.UsersToCompany
                                    on company.Id equals userToCompany.CompanyId
                                    select new
                                    {
                                        company.Id,
                                        company.CompanyName,
                                        company.CompanyLink,
                                        company.CompanyImage,
                                        userToCompany.UserId
                                    }).ToListAsync();

            // Now you can work with the joined data, for example, pass it to a view
            // or return it as JSON
            return View(joinedData);
        }

        public class YourViewModel
        {
            public int Id { get; set; }
            public string CompanyName { get; set; }
            public string CompanyLink { get; set; }
            public string CompanyImage { get; set; }
            public string UserId { get; set; }
        }
    }
}
