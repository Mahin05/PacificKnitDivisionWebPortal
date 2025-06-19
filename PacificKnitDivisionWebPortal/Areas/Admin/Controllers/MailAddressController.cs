using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using OnlineBookOrderManagementSystem.Data;
using OnlineBookOrderManagementSystem.Repositories.IRepository;
using OnlineBookOrderManagementSystem.Repositories.Repository;
using PacificKnitDivisionWebPortal.Data;
using PacificKnitDivisionWebPortal.Models;
using PacificKnitDivisionWebPortal.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace PacificKnitDivisionWebPortal.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class MailAddressController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly IUnitOfWork unitOfWork;

        public MailAddressController(ApplicationDBContext context, IUnitOfWork _unitOfWork)
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
            var list = unitOfWork.mailAddress.GetAll().Include(i => i.Designation).OrderBy(x=>x.Designation.DisplayNo);
            return View(await list.ToListAsync());
        }

        public async Task<IActionResult> Upsert(int? Id)
        {
            IEnumerable<SelectListItem> UnitList = (unitOfWork.unit.GetAll())
               .Select(x => new SelectListItem
               {
                   Text = x.Name,
                   Value = x.Id.ToString()
               });
            ViewBag.UnitList = UnitList;

            IEnumerable<SelectListItem> DesigList = (unitOfWork.designation.GetAll())
               .Select(x => new SelectListItem
               {
                   Text = x.Name,
                   Value = x.Id.ToString()
               });
            ViewBag.DesigList = DesigList;

            IEnumerable<SelectListItem> DeptList = (unitOfWork.department.GetAll())
               .Select(x => new SelectListItem
               {
                   Text = x.Name,
                   Value = x.Id.ToString()
               });
            ViewBag.DeptList = DeptList;



            // Check if this is an Insert or Update operation
            if (Id == null)
            {
                return View(new MailAddress());
            }
            else
            {
                // Update: Fetch the product for the given Id
                var mailAddress = await unitOfWork.mailAddress.Get(m => m.Id == Id);
                if (mailAddress == null)
                {
                    return NotFound();
                }
                return View(mailAddress);
            }
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(MailAddress model)
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
                IEnumerable<SelectListItem> DeptList = (unitOfWork.department.GetAll())
                   .Select(x => new SelectListItem
                   {
                       Text = x.Name,
                       Value = x.Id.ToString()
                   });
                ViewBag.DeptList = DeptList;

                IEnumerable<SelectListItem> DesigList = (unitOfWork.designation.GetAll())
                   .Select(x => new SelectListItem
                   {
                       Text = x.Name,
                       Value = x.Id.ToString()
                   });
                ViewBag.DesigList = DesigList;

                if (model.Id == null || model.Id == 0)
                {
                    var Email = unitOfWork.mailAddress.GetAll().FirstOrDefault(x => x.Email == model.Email);
                    if(Email == null)
                    {
                        TempData["success"] = "Mail Address Created Successfully!";
                        unitOfWork.mailAddress.Add(model);
                    }
                    else
                    {
                        TempData["error"] = $"Mail {model.Email} Already Exists!";
                    }
                }
                else
                {
                    TempData["success"] = "Mail Address Updated Successfully!";
                    await unitOfWork.mailAddress.Update(model);
                   
                }
                unitOfWork.Save();
                return RedirectToAction(nameof(Upsert));
            }
            return View(model);
        }

        // POST: Admin/IPPhoneDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mailAddress = await unitOfWork.mailAddress.Get(x=>x.Id==id);
            if (mailAddress != null)
            {
                await unitOfWork.mailAddress.Remove(mailAddress);
            }

            unitOfWork.Save();
            return Json(new { success = "Mail Address Removed Successfully!" });
        }

        #region API
        [HttpGet]
        public async Task<IActionResult> GetAllMailAddress()
        {
            var mailAddress = unitOfWork.mailAddress.GetAll().Include(x => x.Designation).Include(x=>x.Department);
            var department = unitOfWork.department.GetAll();
            var unitList = await unitOfWork.unit.GetAll().ToListAsync();
            return Json(new { data = mailAddress, dept = department,unit= unitList });
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
        public IActionResult GetIPDetailsByUnit(int value)
        {
            if(value==0)
            {

                var iPPhoneDetails = unitOfWork.iPPhoneDetails.GetAll().Include(x => x.Department);
                return Json(new { data = iPPhoneDetails });
            }
            else
            {
                var iPPhoneDetails = unitOfWork.iPPhoneDetails.GetAll().Include(x => x.Department).Where(x => x.UnitId == value);
                return Json(new { data = iPPhoneDetails });
            }
                
        }

        [HttpGet]
        public IActionResult GetAllSearch(string value,int Dept,int Unit)
        {
            if (value == null && Dept==0 && Unit== 0)
            {
                var mailAddress = unitOfWork.mailAddress.GetAll().Include(x => x.Department).ThenInclude(x=>x.Unit).Include(x=>x.Designation);
                return Json(new { data = mailAddress });
            }
            if (value == null && Dept != 0 && Unit != 0)
            {
                var mailAddress = unitOfWork.mailAddress.GetAll().Include(x => x.Department).ThenInclude(x => x.Unit).Include(x => x.Designation).Where(x=>x.DeptId==Dept && x.UnitId==Unit);
                return Json(new { data = mailAddress });
            }
            if (value == null && Dept != 0 && Unit == 0)
            {
                var mailAddress = unitOfWork.mailAddress.GetAll().Include(x => x.Department).ThenInclude(x => x.Unit).Include(x => x.Designation).Where(x=>x.DeptId==Dept);
                return Json(new { data = mailAddress });
            }
            if (value == null && Dept == 0 && Unit != 0)
            {
                var mailAddress = unitOfWork.mailAddress.GetAll().Include(x => x.Department).ThenInclude(x=>x.Unit).Include(x => x.Designation).Where(x=>x.UnitId==Unit);
                return Json(new { data = mailAddress });
            }
            if (value != null && Dept != 0 && Unit == 0)
            {
                var mailAddress = unitOfWork.mailAddress.GetAll().Include(x => x.Department).ThenInclude(x=>x.Unit).Include(x=>x.Designation).Where(x=>x.DeptId==Dept && (x.Name.Contains(value) ||
                    x.Email.Contains(value) ||
                    x.Department.Name.Contains(value) ||
                    x.Designation.Name.Contains(value)
                    ));
                return Json(new { data = mailAddress });
            }
            if (value != null && Dept == 0 && Unit != 0)
            {
                var mailAddress = unitOfWork.mailAddress.GetAll().Include(x => x.Department).ThenInclude(x=>x.Unit).Include(x => x.Designation).Where(x=>x.UnitId==Unit && (x.Email.Contains(value) ||
                    x.Name.Contains(value) ||
                    x.Department.Name.Contains(value) ||
                    x.Designation.Name.Contains(value)
                    ));
                return Json(new { data = mailAddress });
            }
            if (value != null && Dept != 0 && Unit != 0)
            {
                var mailAddress = unitOfWork.mailAddress.GetAll().Include(x => x.Department).ThenInclude(x=>x.Unit).Include(x=>x.Designation).Where(x=>x.UnitId==Unit && x.DeptId==Dept && (x.Name.Contains(value) ||
                    x.Email.Contains(value) ||
                    x.Department.Name.Contains(value) ||
                    x.Designation.Name.Contains(value)
                    ));
                return Json(new { data = mailAddress });
            }
            else
            {
                var mailAddress = unitOfWork.mailAddress.GetAll().Include(x => x.Department).ThenInclude(x=>x.Unit).Include(x=>x.Designation).Where(
                    x => x.Name.Contains(value) ||
                    x.Email.Contains(value) ||
                    x.Department.Name.Contains(value) ||
                    x.Designation.Name.Contains(value)
                    );
                return Json(new { data = mailAddress });
            }

        }


        [HttpGet]
        public async Task<IActionResult> GetMailAddressPKD()
        {
            using (var connection = new SqlConnection(_context.Database.GetConnectionString()))
            {
                await connection.OpenAsync();

                using (var multi = await connection.QueryMultipleAsync("GetMailAddressPKD", commandType: CommandType.StoredProcedure))
                {
                    var table1 = (await multi.ReadAsync<MailAddressVM>()).ToList();
                    var table2 = (await multi.ReadAsync<MailAddressVM>()).ToList();
                    var table3 = (await multi.ReadAsync<MailAddressVM>()).ToList();
                    var table4 = (await multi.ReadAsync<MailAddressVM>()).ToList();
                    
                    return Json(new {Table1 = table1,Table2 = table2,Table3 = table3, Table4 = table4 });
                }
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetDepartmentByUnit(int UnitId)
        {
            var DepartmentList = await unitOfWork.department.GetAll().Where(x => x.UnitId == UnitId).OrderBy(x=>x.DisplayNo).ToListAsync();
            return Json(new { data = DepartmentList });
        }


        #endregion
    }
}
