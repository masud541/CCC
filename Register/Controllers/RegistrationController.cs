using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Register.Data;
using Register.Models;

namespace Register.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly RegisterDBContext _context;

        public RegistrationController(RegisterDBContext context)
        {
            _context = context;
        }


        // Default
        public ActionResult Default()
        {           
            return View();
        }

        public ActionResult RegistrationSerialCheckResult()
        {
            return View();
        }

        public ActionResult RegistrationAssistance()
        {
            return View();
        }

        // GET: Registration
        public async Task<IActionResult> Index(int pg=1)
        {
            const int pageSize = 5;
            if (pg < 1)
                pg = 1;
            int recsCount = _context.Registrations.Count();
            var pager = new Pager(recsCount, pg, pageSize);
            int recSkip = (pg -1) * pageSize;
            //List<Registration> registration = _context.Registrations.Skip(recSkip).Take(pager.PageSize).ToList();
            this.ViewBag.pager = pager;
            return View(await _context.Registrations.Skip(recSkip).Take(pager.PageSize).ToListAsync());
        }

        // GET: Registration/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Registrations == null)
            {
                return NotFound();
            }

            var registration = await _context.Registrations
                .FirstOrDefaultAsync(m => m.Id == id);
            if (registration == null)
            {
                return NotFound();
            }

            return View(registration);
        }

        // GET: Registration/AddOrEdit
        public IActionResult AddOrEdit(int id=0)
        {
            if (id == 0)
                return View(new Registration());
            else
                return View(_context.Registrations.Find(id));
        }

        // POST: Registration/AddOrEdit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit([Bind("Id,Name,ComanyName,City,State","PhoneNo","Email", "CompanyAddress", "Country", "Zipcode")] Registration registration)
        {
            if (ModelState.IsValid)
            {
                if (registration.Id == 0)
                    _context.Add(registration);
                else
                    _context.Update(registration);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(registration);
        }

        // GET: Registration/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Registrations == null)
            {
                return NotFound();
            }

            var registration = await _context.Registrations.FindAsync(id);
            if (registration == null)
            {
                return NotFound();
            }
            return View(registration);
        }

        // POST: Registration/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ComanyName,City,State")] Registration registration)
        {
            if (id != registration.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(registration);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RegistrationExists(registration.Id))
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
            return View(registration);
        }

        // GET: Registration/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Registrations == null)
            {
                return NotFound();
            }

            var registration = await _context.Registrations
                .FirstOrDefaultAsync(m => m.Id == id);
            if (registration == null)
            {
                return NotFound();
            }

            return View(registration);
        }

        // POST: Registration/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Registrations == null)
            {
                return Problem("Entity set 'RegisterDBContext.Registrations'  is null.");
            }
            var registration = await _context.Registrations.FindAsync(id);
            if (registration != null)
            {
                _context.Registrations.Remove(registration);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RegistrationExists(int id)
        {
          return _context.Registrations.Any(e => e.Id == id);
        }
    }
}
