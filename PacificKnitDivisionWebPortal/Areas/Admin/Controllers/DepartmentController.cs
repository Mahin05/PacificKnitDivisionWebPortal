using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using OnlineBookOrderManagementSystem.Repositories.IRepository;
using PacificKnitDivisionWebPortal.Data;
using PacificKnitDivisionWebPortal.Models;

namespace PacificKnitDivisionWebPortal.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DepartmentController : Controller
    {
        private readonly ApplicationDBContext db;
        private readonly IUnitOfWork unitOfWork;

        public DepartmentController(ApplicationDBContext _db, IUnitOfWork _unitOfWork)
        {
            db = _db;
            unitOfWork = _unitOfWork;
        }

        // GET: Admin/Department
        public IActionResult Index()
        {
            return View();
        }

        // GET: Admin/Department/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = await unitOfWork.department
                .Get(m => m.Id == id);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        // GET: Admin/Department/Create
        public async Task<IActionResult> Upsert(int? Id)
        {
            IEnumerable<SelectListItem> UnitList = (unitOfWork.unit.GetAll())
                .Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                });
            ViewBag.UnitList = UnitList;
            if (Id == null)
            {
                return View(new Department());
            }
            else
            {
                var department = await unitOfWork.department.Get(m => m.Id == Id);
                if (department == null)
                {
                    return NotFound();
                }
                return View(department);
            }
        }

        // POST: Admin/Department/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(Department department)
        {
            if (ModelState.IsValid)
            {
                IEnumerable<SelectListItem> UnitList = (unitOfWork.unit.GetAll())
               .Select(x => new SelectListItem
               {
                   Text = x.Name,
                   Value = x.Id.ToString()
               });
                ViewBag.UnitList = UnitList;

                var UnitName = unitOfWork.unit.GetAll().Where(x => x.Id == department.UnitId).FirstOrDefault().Name;

                if (department.Id == null || department.Id == 0)
                {
                    var DeptExist = unitOfWork.department.GetAll().FirstOrDefault(x => x.Name == department.Name && x.UnitId==department.UnitId);
                    if (DeptExist == null)
                    {
                        TempData["success"] = "Department Created Successfully!";

                        // Call stored procedure to insert
                        await db.Database.ExecuteSqlRawAsync("EXEC CreateAndUpdateDepartment @p0,@p1,@p2,@p3,@p4", department.Id, department.Name,department.UnitId,department.DisplayNo, department.ShowFlag);
                    }
                    else
                    {
                        TempData["error"] = $"Department {department.Name} in Unit {UnitName} Is Already Exists!";
                        return View(department);
                    }
                }
                else
                {
                    var DeptExist = unitOfWork.department.GetAll().FirstOrDefault(x => x.Name == department.Name && x.Unit == department.Unit);
                    if (DeptExist == null)
                    {

                        TempData["success"] = "Department Updated Successfully!";
                        await db.Database.ExecuteSqlRawAsync("EXEC CreateAndUpdateDepartment @p0,@p1,@p2,@p3,@p4", department.Id, department.Name, department.UnitId, department.DisplayNo, department.ShowFlag);

                        //unitOfWork.department.Update(department);
                        //unitOfWork.Save();
                    }
                    else
                    {
                        TempData["error"] = $"Department {department.Name} Is Already Exists!";
                        return View(department);
                    }
                }

                return RedirectToAction(nameof(Upsert));
                //return View(department);
            }

            return View(department);
        }





        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Upsert(Department department)
        ////[Bind("Id,Name,DisplayOrder,FileType,IsDelete,FileUrl")] 
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (department.Id == null)
        //        {
        //            TempData["success"] = "Department Created Successfully!";
        //            unitOfWork.department.Add(department);
        //        }
        //        else
        //        {
        //            TempData["success"] = "Department Updated Successfully!";
        //            unitOfWork.department.Update(department);
        //        }
        //        unitOfWork.Save();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(department);
        //}

        // GET: Admin/Department/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = await db.department.FindAsync(id);
            if (department == null)
            {
                return NotFound();
            }
            return View(department);
        }

        // POST: Admin/Department/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Department department)
        {
            if (id != department.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(department);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartmentExists(department.Id))
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
            return View(department);
        }

        // GET: Admin/Department/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = await db.department
                .FirstOrDefaultAsync(m => m.Id == id);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        // POST: Admin/Department/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var department = await unitOfWork.department.Get(x=>x.Id==id);
            if (department != null)
            {
                //TempData["success"] = "Department Removed Successfully!";
                await unitOfWork.department.Remove(department);
            }

            unitOfWork.Save();
            return Json(new { success = "Department Removed Successfully!" });
            //return RedirectToAction(nameof(Index));
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
        public IActionResult GetAllSearch(string value, int unit)
        {
            if (value == null && unit==0)
            {
                var department = unitOfWork.department.GetAll().Include(x=>x.Unit).OrderBy(x=> x.DisplayNo).ToListAsync();
                return Json(new { data = department });
            }
            if(value != null && unit != 0)
            {
                var department = unitOfWork.department.GetAll().Include(x => x.Unit).OrderBy(x => x.DisplayNo).Where(x=>x.UnitId==unit && x.Name.Contains(value)).ToListAsync();
                return Json(new { data = department });
            }
            if(value == null && unit != 0)
            {
                var department = unitOfWork.department.GetAll().Include(x=>x.Unit).OrderBy(x=> x.DisplayNo).Where(x=>x.UnitId ==unit).ToListAsync();
                return Json(new { data = department });
            }
            else
            {
                var department = unitOfWork.department.GetAll().Include(x=>x.Unit).OrderBy(x=> x.DisplayNo).Where(
                    x => x.Name.Contains(value)
                    ).ToListAsync();
                return Json(new { data = department });
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
