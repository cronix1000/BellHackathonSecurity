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
            ClaimsPrincipal currentUser = this.User;
            var currentUserID = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;

            var userData = await _context.userDatas.Where(a => a.UserId == currentUserID).FirstOrDefaultAsync();

            return View(userData);
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

        // GET: UserDatas
        public async Task<IActionResult> Index()
        {
            return _context.userDatas != null ?
                        View(await _context.userDatas.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.userDatas'  is null.");
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
