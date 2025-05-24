using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineBookOrderManagementSystem.Data;
using OnlineBookOrderManagementSystem.Repositories.IRepository;
using PacificKnitDivisionWebPortal.Data;
using PacificKnitDivisionWebPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PacificKnitDivisionWebPortal.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class UnitController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly IUnitOfWork unitOfWork;

        public UnitController(ApplicationDBContext context, IUnitOfWork _unitOfWork)
        {
            _context = context;
            unitOfWork= _unitOfWork;
        }

        // GET: Admin/Unit
        public IActionResult Index()
        {
            return View();
        }

        // GET: Admin/Unit/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var unit = await _context.unit
                .FirstOrDefaultAsync(m => m.Id == id);
            if (unit == null)
            {
                return NotFound();
            }

            return View(unit);
        }

        public async Task<IActionResult> Upsert(int? Id)
        {
            if (Id == null)
            {
                return View(new Unit());
            }
            else
            {
                var unit = await unitOfWork.unit.Get(m => m.Id == Id);
                if (unit == null)
                {
                    return NotFound();
                }
                return View(unit);
            }
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(Unit model)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    if (model.Id == null || model.Id==0)
                    {
                        TempData["success"] = "Unit Created Successfully!";
                        unitOfWork.unit.Add(model);
                    }
                    else
                    {
                        TempData["success"] = "Unit Updated Successfully!";
                        unitOfWork.unit.Update(model);
                    }
                    unitOfWork.Save();
                    //return RedirectToAction(nameof(Index));

                }
                catch (Exception ex)
                {
                    TempData["error"] = "Something went wrong!";
                }
            }
            return RedirectToAction(nameof(Upsert));
        }

        // POST: Admin/Unit/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var unit = await unitOfWork.unit.Get(x=>x.Id==id);
            if (unit != null)
            {
                await unitOfWork.unit.Remove(unit);
            }

            unitOfWork.Save();
            return Json(new { success = "Unit Removed Successfully!" });
        }

        #region
        [HttpGet]
        public IActionResult GetAllUnit()
        {
            var unitList = unitOfWork.unit.GetAll().ToListAsync();
            return Json(new { data = unitList });
        }
        #endregion
    }
}
