using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineBookOrderManagementSystem.Repositories.IRepository;
using OnlineBookOrderManagementSystem.Repositories.Repository;
using PacificKnitDivisionWebPortal.Data;
using PacificKnitDivisionWebPortal.Models;

namespace PacificKnitDivisionWebPortal.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class IPPhoneDetailsController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly IUnitOfWork unitOfWork;

        public IPPhoneDetailsController(ApplicationDBContext context, IUnitOfWork _unitOfWork)
        {
            _context = context;
            unitOfWork = _unitOfWork;
        }

        // GET: Admin/IPPhoneDetails
        public async Task<IActionResult> Index()
        {
            var applicationDBContext = _context.ipphoneDetails.Include(i => i.Department);
            return View(await applicationDBContext.ToListAsync());
        }

        // GET: Admin/IPPhoneDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var iPPhoneDetails = await _context.ipphoneDetails
                .Include(i => i.Department)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (iPPhoneDetails == null)
            {
                return NotFound();
            }

            return View(iPPhoneDetails);
        }

        // GET: Admin/IPPhoneDetails/Create
        //public IActionResult Create()
        //{
        //    ViewData["DeptId"] = new SelectList(_context.department, "Id", "Name");
        //    return View();
        //}
        public async Task<IActionResult> Upsert(int? Id)
        {
            // Initialize the Category List for the dropdown
            IEnumerable<SelectListItem> DepartmentList = (unitOfWork.department.GetAll())
                .Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                });
            ViewBag.DepartmentList = DepartmentList;

            // Check if this is an Insert or Update operation
            if (Id == null)
            {
                return View(new IPPhoneDetails());
            }
            else
            {
                // Update: Fetch the product for the given Id
                var IPPhoneDetails = await unitOfWork.iPPhoneDetails.Get(m => m.Id == Id);
                if (IPPhoneDetails == null)
                {
                    return NotFound();
                }
                return View(IPPhoneDetails);
            }
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(IPPhoneDetails model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id == null || model.Id == 0)
                {
                    TempData["success"] = "IP Phone Details Created Successfully!";
                    unitOfWork.iPPhoneDetails.Add(model);
                }
                else
                {
                    TempData["success"] = "IP Phone Details Updated Successfully!";
                    unitOfWork.iPPhoneDetails.Update(model);
                }
                unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }










        // POST: Admin/IPPhoneDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IPNo,DisplayName,Unit,DeptId")] IPPhoneDetails iPPhoneDetails)
        {
            if (ModelState.IsValid)
            {
                _context.Add(iPPhoneDetails);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DeptId"] = new SelectList(_context.department, "Id", "Name", iPPhoneDetails.DeptId);
            return View(iPPhoneDetails);
        }

        // GET: Admin/IPPhoneDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var iPPhoneDetails = await _context.ipphoneDetails.FindAsync(id);
            if (iPPhoneDetails == null)
            {
                return NotFound();
            }
            ViewData["DeptId"] = new SelectList(_context.department, "Id", "Name", iPPhoneDetails.DeptId);
            return View(iPPhoneDetails);
        }

        // POST: Admin/IPPhoneDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IPNo,DisplayName,Unit,DeptId")] IPPhoneDetails iPPhoneDetails)
        {
            if (id != iPPhoneDetails.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(iPPhoneDetails);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IPPhoneDetailsExists(iPPhoneDetails.Id))
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
            ViewData["DeptId"] = new SelectList(_context.department, "Id", "Name", iPPhoneDetails.DeptId);
            return View(iPPhoneDetails);
        }

        // GET: Admin/IPPhoneDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var iPPhoneDetails = await _context.ipphoneDetails
                .Include(i => i.Department)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (iPPhoneDetails == null)
            {
                return NotFound();
            }

            return View(iPPhoneDetails);
        }

        // POST: Admin/IPPhoneDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var iPPhoneDetails = await _context.ipphoneDetails.FindAsync(id);
            if (iPPhoneDetails != null)
            {
                _context.ipphoneDetails.Remove(iPPhoneDetails);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IPPhoneDetailsExists(int id)
        {
            return _context.ipphoneDetails.Any(e => e.Id == id);
        }
    }
}
