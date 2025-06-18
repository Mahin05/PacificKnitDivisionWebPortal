using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
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
    public class DesignationController : Controller
    {
        private readonly ApplicationDBContext db;
        private readonly IUnitOfWork unitOfWork;

        public DesignationController(ApplicationDBContext _db, IUnitOfWork _unitOfWork)
        {
            db = _db;
            unitOfWork = _unitOfWork;
        }

        // GET: Admin/Department
        public IActionResult Index()
        {
            return View();
        }

        // GET: Admin/Department/Create
        public async Task<IActionResult> Upsert(int? Id)
        {
            if (Id == null)
            {
                return View(new Designation());
            }
            else
            {
                var designation = await unitOfWork.designation.Get(m => m.Id == Id);
                if (designation == null)
                {
                    return NotFound();
                }
                return View(designation);
            }
        }

        // POST: Admin/Department/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(Designation model)
        {
            if (ModelState.IsValid)
            {

                if (model.Id == null || model.Id == 0)
                {
                    var DesigExist = unitOfWork.department.GetAll().FirstOrDefault(x => x.Name == model.Name);
                    if (DesigExist == null)
                    {
                        TempData["success"] = "Department Created Successfully!";

                        // Call stored procedure to insert
                        await db.Database.ExecuteSqlRawAsync("EXEC CreateAndUpdateDepartment @p0,@p1,@p2", model.Id, model.Name,model.DisplayNo);
                    }
                    else
                    {
                        TempData["error"] = $"Designation {model.Name} Is Already Exists!";
                        return View(model);
                    }
                }
                else
                {
                    var DesigExist = unitOfWork.department.GetAll().FirstOrDefault(x => x.Name == model.Name);
                    if (DesigExist == null)
                    {

                        TempData["success"] = "Designation Updated Successfully!";
                        await db.Database.ExecuteSqlRawAsync("EXEC CreateAndUpdateDepartment @p0,@p1,@p2", model.Id, model.Name,model.DisplayNo);

                        //unitOfWork.department.Update(department);
                        //unitOfWork.Save();
                    }
                    else
                    {
                        TempData["error"] = $"Designation {model.Name} Is Already Exists!";
                        return View(model);
                    }
                }

                return RedirectToAction(nameof(Upsert));
                //return View(department);
            }

            return View(model);
        }

        // GET: Admin/Department/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var designation = await db.designation
                .FirstOrDefaultAsync(m => m.Id == id);
            if (designation == null)
            {
                return NotFound();
            }

            return View(designation);
        }

        // POST: Admin/Department/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {

                var designation = await unitOfWork.designation.Get(x => x.Id == id);

                if (designation != null)
                {
                    //TempData["success"] = "Department Removed Successfully!";
                    await unitOfWork.designation.Remove(designation);
                }

                unitOfWork.Save();
                return Json(new { success = "Designation Removed Successfully!" });

                //var ipphonelist = unitOfWork.iPPhoneDetails.GetAll().FirstOrDefault(x => x.DeptId == id);

                //if (ipphonelist == null)
                //{
                //    if (department != null)
                //    {
                //        //TempData["success"] = "Department Removed Successfully!";
                //        await unitOfWork.department.Remove(department);
                //    }

                //    unitOfWork.Save();
                //    return Json(new { success = "Department Removed Successfully!" });
                //}
                //else
                //{
                //    return Json(new { error = "Cannot Remove Department!" });
                //}

                //return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                return Json(new { error = "Cannot delete designation!" });
            }
        }

        private bool DepartmentExists(int id)
        {
            return db.department.Any(e => e.Id == id);
        }
        #region
        [HttpGet]
        public async Task<IActionResult> GetAllUnit()
        {
            var unitList =await unitOfWork.unit.GetAll().ToListAsync();
            return Json(new { unit = unitList });
        }
        [HttpGet]
        public async Task<IActionResult> GetAllDepartment()
        {
            var departmentList = await unitOfWork.department.GetAll().OrderBy(x => x.UnitId).ThenBy(x => x.DisplayNo).ToListAsync();
            var unitList =await unitOfWork.unit.GetAll().ToListAsync();
            return Json(new { data = departmentList, unit = unitList });
        }
        [HttpGet]
        public IActionResult GetDisplayNo(int Id)
        {
            var displayOrders = unitOfWork.department.GetAll().Where(x => x.Id == Id).Select(x => x.DisplayNo).ToList();
            if (!displayOrders.Any())
            {
                return Ok(new { data = 0 });
            }

            var displayOrder = displayOrders.Max();

            if (displayOrder == 0)
            {
                return Ok(new { data = 0 });
            }
            else
            {
                return Json(new { data = displayOrder });
            }

                
        }
        [HttpGet]
        public IActionResult GetAllSearch(string value)
        {
            if (value == null)
            {
                var designation = unitOfWork.designation.GetAll().ToListAsync();
                return Json(new { data = designation });
            }
            if(value != null)
            {
                var designation = unitOfWork.designation.GetAll().Where(x=>x.Name.Contains(value)).ToListAsync();
                return Json(new { data = designation });
            }
            else
            {
                var designation = unitOfWork.designation.GetAll().Where(x => x.Name.Contains(value)).ToListAsync();
                return Json(new { data = designation });
            }

        }
        [HttpGet]
        public async Task<IActionResult> GetDepartmentByUnit(int UnitId)
        {
            var DepartmentList = await unitOfWork.department.GetAll().Where(x => x.UnitId == UnitId).ToListAsync();
            return Json(new { data = DepartmentList });
        }
        [HttpGet]
        public IActionResult GetDisplayOrderByUnit(int UnitId)
        {
            var displayOrders = unitOfWork.department.GetAll().Where(x => x.UnitId == UnitId).Select(x => x.DisplayNo).ToList();
            if (!displayOrders.Any())
            {
                return Ok(new { data = 0 });
            }

            var displayOrder = displayOrders.Max();

            if (displayOrder == 0)
            {
                return Ok(new { data = 0 });
            }
            else
            {
                return Json(new { data = displayOrder });
            }
        }
        #endregion
    }
}
