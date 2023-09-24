using BelLHackathonSecurity.Data;
using BelLHackathonSecurity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BelLHackathonSecurity.Controllers
{
    public class UserDatasController : Controller
    {
        private readonly ApplicationDbContext _context;


        public UserDatasController(ApplicationDbContext context)
        {
            _context = context;
          
        }

        public async Task<IActionResult> Dashboard()
        {



            string currentUserID = "";
            if (User != null)
            {
                ClaimsPrincipal currentUser = this.User;
                currentUserID = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            }

            HomeViewModel hm = new()
            {
                companiesSignedUpFor = await _context.UsersToCompany.Where(a => a.UserId.ToString() == currentUserID).CountAsync()
            };

            return View(hm);
        }

        public async Task<IActionResult> RevokeData(Guid? id)
        {
            if (id == null || _context.userDatas == null)
            {
                return NotFound();
            }

            var userData = await _context.userDatas.FindAsync(id);
            if (userData == null)
            {
                return NotFound();
            }

            return View(userData);
        }
        public async Task<IActionResult> RevokeName(string id)
        {
            var userData = await _context.userDatas.FindAsync(Guid.Parse(id));
            if (userData == null)
            {
                return NotFound();
            }
            userData.PhoneNumber = null;
            _context.Update(userData);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> RevokeDataPhoneNumber(string id)
        {
            var userData = await _context.userDatas.FindAsync(Guid.Parse(id));
            if (userData == null)
            {
                return NotFound();
            }
            userData.PhoneNumber = null;
            _context.Update(userData);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        
        public async Task<IActionResult> RevokeDataEmail(string id)
        {
            var userData = await _context.userDatas.FindAsync(Guid.Parse(id));
            if (userData == null)
            {
                return NotFound();
            }
            userData.PhoneNumber = null;
            _context.Update(userData);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RevokeData(Guid id, UserData userData)
        {
            if (id != userData.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userData);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserDataExists(userData.Id))
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
            return View(userData);
        }

        public class HomeViewModel
        {
            public int companiesSignedUpFor { get; set; }
        }

        // GET: UserDatas
        public async Task<IActionResult> Index()
        {
            string currentUserID = "";
            if (User != null)
            {
                ClaimsPrincipal currentUser = this.User;
                currentUserID = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            }

            HomeViewModel hm = new()
            {
                companiesSignedUpFor = await _context.UsersToCompany.Where(a => a.UserId.ToString() == currentUserID).CountAsync()
            };

            return View(hm);
        }

        // GET: UserDatas/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.userDatas == null)
            {
                return NotFound();
            }

            var userData = await _context.userDatas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userData == null)
            {
                return NotFound();
            }

            return View(userData);
        }

        public async Task<IActionResult> SignUpForCompany()
        {
            List<Company> companies = _context.Companies.ToList();
            return View(companies);
        }

        public async Task<IActionResult> SignupToCompany(string id) {
            string currentUserID = "";
            if (User != null) {
                ClaimsPrincipal currentUser = this.User;
                currentUserID = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            }

            UsersToCompany usersToCompany = new UsersToCompany()
            {
                Id = Guid.NewGuid(),
                UserId = Guid.Parse(currentUserID),
                CompanyId = Guid.Parse(id)
            };

            await _context.UsersToCompany.AddAsync(usersToCompany);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(SignUpForCompany));
        }

        public async Task<IActionResult> RetrieveDataFromCompany(string id)
        {
            string currentUserID = "";
            if (User != null)
            {
                ClaimsPrincipal currentUser = this.User;
                currentUserID = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            }

            var userData = await _context.UsersToCompany.Where(a => a.CompanyId.ToString() == id && a.UserId.ToString() == currentUserID).FirstOrDefaultAsync();

            if (userData != null)
                _context.UsersToCompany.Remove(userData);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(SignUpForCompany));
        }



        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.userDatas == null)
            {
                return NotFound();
            }

            var userData = await _context.userDatas.FindAsync(id);
            if (userData == null)
            {
                return NotFound();
            }
            return View(userData);
        }

        // POST: UserDatas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, UserData userData)
        {
            if (id != userData.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userData);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserDataExists(userData.Id))
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
            return View(userData);
        }



        // GET: UserDatas/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.userDatas == null)
            {
                return NotFound();
            }

            var userData = await _context.userDatas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userData == null)
            {
                return NotFound();
            }

            return View(userData);
        }

        // POST: UserDatas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.userDatas == null)
            {
                return Problem("Entity set 'ApplicationDbContext.userDatas'  is null.");
            }
            var userData = await _context.userDatas.FindAsync(id);
            if (userData != null)
            {
                _context.userDatas.Remove(userData);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserDataExists(Guid id)
        {
            return (_context.userDatas?.Any(e => e.Id == id)).GetValueOrDefault();
        }



    }
}
