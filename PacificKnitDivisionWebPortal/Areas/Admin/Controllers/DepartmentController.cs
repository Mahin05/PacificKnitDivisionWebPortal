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
        public async Task<IActionResult> Index()
        {
            var departmentList = unitOfWork.department.GetAll().ToListAsync();
            return View(await departmentList);
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
                if (department.Id == null || department.Id == 0)
                {
                    TempData["success"] = "Department Created Successfully!";

                    // Call stored procedure to insert
                    await db.Database.ExecuteSqlRawAsync("EXEC CreateDepartment @p0", department.Name);
                }
                else
                {
                    TempData["success"] = "Department Updated Successfully!";

                    // Use your existing EF update logic or a separate stored procedure for update
                    unitOfWork.department.Update(department);
                    unitOfWork.Save();
                }

                return RedirectToAction(nameof(Index));
            }

            return View(department);
        }



        [HttpPost]
        public async Task<IActionResult> Import(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                using (var stream = new MemoryStream())
                {
                    await file.CopyToAsync(stream);
                    using (var package = new ExcelPackage(stream))
                    {
                        var worksheet = package.Workbook.Worksheets[0];
                        int rowCount = worksheet.Dimension.Rows;

                        for (int row = 1; row <= rowCount; row++) // Assuming row 1 is header
                        {
                            string deptName = worksheet.Cells[row, 1].Value?.ToString();

                            if (!string.IsNullOrWhiteSpace(deptName))
                            {
                                // Option 1: Using repository
                                var department = new Department { Name = deptName };
                                await db.Database.ExecuteSqlRawAsync("EXEC CreateDepartment @p0", department.Name);
                            }
                        }
                    }
                }

                TempData["success"] = "Departments imported successfully!";
            }
            else
            {
                TempData["error"] = "Please select a valid Excel file.";
            }

            return RedirectToAction(nameof(Index));
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

            var department = await db.departments.FindAsync(id);
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

            var department = await db.departments
                .FirstOrDefaultAsync(m => m.Id == id);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        // POST: Admin/Department/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var department = await unitOfWork.department.Get(x=>x.Id==id);
            if (department != null)
            {
                TempData["success"] = "Department Removed Successfully!";
                await unitOfWork.department.Remove(department);
            }

            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DepartmentExists(int id)
        {
            return db.departments.Any(e => e.Id == id);
        }
    }
}
