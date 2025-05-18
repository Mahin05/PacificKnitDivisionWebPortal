using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using OnlineBookOrderManagementSystem.Repositories.IRepository;
using OnlineBookOrderManagementSystem.Repositories.Repository;
using PacificKnitDivisionWebPortal.Data;
using PacificKnitDivisionWebPortal.Models;
using PacificKnitDivisionWebPortal.Models.ViewModels;

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
        public IActionResult ViewPdf()
        {
            return View();
        }
        public async Task<IActionResult> Index()
        {
            var list = unitOfWork.iPPhoneDetails.GetAll().Include(i => i.Department);
            return View(await list.ToListAsync());
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
            var iPPhoneDetails = await unitOfWork.iPPhoneDetails.Get(x=>x.Id==id);
            if (iPPhoneDetails != null)
            {
                await unitOfWork.iPPhoneDetails.Remove(iPPhoneDetails);
            }

            unitOfWork.Save();
            return Json(new { success = "IP Phone Removed Successfully!" });
            //return RedirectToAction(nameof(Index));
        }

        private bool IPPhoneDetailsExists(int id)
        {
            return _context.ipphoneDetails.Any(e => e.Id == id);
        }

        #region API
        [HttpGet]
        public IActionResult GetAllIPPhoneDetails()
        {
            var iPPhoneDetails = unitOfWork.iPPhoneDetails.GetAll().Include(x => x.Department);
            var department = unitOfWork.department.GetAll();
            return Json(new { data = iPPhoneDetails,dept = department });
        }
        [HttpGet]
        public IActionResult GetIPDetailsByDepartment(int value)
        {
            if (value == 0)
            {
                var iPPhoneDetails = unitOfWork.iPPhoneDetails.GetAll().Include(x => x.Department);
                return Json(new { data = iPPhoneDetails });
            }
            else
            {
                var iPPhoneDetails = unitOfWork.iPPhoneDetails.GetAll().Include(x => x.Department).Where(x => x.DeptId == value);
                return Json(new { data = iPPhoneDetails });
            }
                
        }
        [HttpGet]
        public IActionResult GetIPDetailsByUnit(string value)
        {
            if(value=="All Unit")
            {

                var iPPhoneDetails = unitOfWork.iPPhoneDetails.GetAll().Include(x => x.Department);
                return Json(new { data = iPPhoneDetails });
            }
            else
            {
                var iPPhoneDetails = unitOfWork.iPPhoneDetails.GetAll().Include(x => x.Department).Where(x => x.Unit == value);
                return Json(new { data = iPPhoneDetails });
            }
                
        }

        [HttpGet]
        public IActionResult GetAllSearch(string value)
        {
            if (value == null)
            {
                var products = unitOfWork.iPPhoneDetails.GetAll().Include(x => x.Department);
                return Json(new { data = products });
            }
            else
            {
                var products = unitOfWork.iPPhoneDetails.GetAll().Include(x => x.Department).Where(
                    x => x.DisplayName.Contains(value) ||
                    x.IPNo.Contains(value)
                    );
                return Json(new { data = products });
            }

        }


        [HttpGet]
        public async Task<IActionResult> GetIPPhoneOfPKD()
        {
            using (var connection = new SqlConnection(_context.Database.GetConnectionString()))
            {
                await connection.OpenAsync();

                using (var multi = await connection.QueryMultipleAsync("GetIPPhoneListPKD", commandType: CommandType.StoredProcedure))
                {
                    var table1 = (await multi.ReadAsync<IPPhoneViewModel>()).ToList();
                    var table2 = (await multi.ReadAsync<IPPhoneViewModel>()).ToList();
                    var table3 = (await multi.ReadAsync<IPPhoneViewModel>()).ToList();

                    return Json(new {Table1 = table1,Table2 = table2,Table3 = table3});
                }
            }
        }



        [HttpGet]
        public IActionResult GetIPPhoneOfPKL()
        {
            //var list = unitOfWork.iPPhoneDetails.GetAll().Include(i => i.Department);
            var list = _context.IPPhoneListVM.FromSqlRaw("EXEC GetIPPhoneListPKL").ToArrayAsync();
            return Json(new {data = list });
        }
        [HttpGet]
        public IActionResult GetIPPhoneOfPCL1()
        {
            //var list = unitOfWork.iPPhoneDetails.GetAll().Include(i => i.Department);
            var list = _context.IPPhoneListVM.FromSqlRaw("EXEC GetIPPhoneListPCL1").ToArrayAsync();
            return Json(new {data = list });
        }
        [HttpGet]
        public  IActionResult GetIPPhoneOfPCL2()
        {
            //var list = unitOfWork.iPPhoneDetails.GetAll().Include(i => i.Department);
            var list = _context.IPPhoneListVM.FromSqlRaw("EXEC GetIPPhoneListPCL2").ToArrayAsync();
            return Json(new {data = list });
        }


        #endregion
    }
}
