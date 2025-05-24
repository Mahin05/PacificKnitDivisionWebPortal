using System.Data;
using System.Diagnostics;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using OnlineBookOrderManagementSystem.Repositories.IRepository;
using PacificKnitDivisionWebPortal.Data;
using PacificKnitDivisionWebPortal.Models;
using PacificKnitDivisionWebPortal.Models.ViewModels;

namespace PacificKnitDivisionWebPortal.Controllers;

[Area("User")]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    private readonly ApplicationDBContext _context;
    private readonly IUnitOfWork unitOfWork;

    public HomeController(ILogger<HomeController> logger, IUnitOfWork _unitOfWork, ApplicationDBContext context)
    {
        _logger = logger;
        unitOfWork = _unitOfWork;
        _context = context;
    }

    public IActionResult List()
    {
        return View();
    }
    public IActionResult GetAllDocumentList()
    {
        var documents = unitOfWork.Document.GetAll().OrderBy(x=>x.DisplayOrder).Where(x=>x.IsDelete==false).ToList();
        return Json(new { data = documents });
    }
    public IActionResult Index()
    {
        return View();
    }

    public async Task<IActionResult> IPPhoneIndex()
    {
        var list = unitOfWork.iPPhoneDetails.GetAll().Include(i => i.Department);
        return View(await list.ToListAsync());
    }
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public IActionResult ViewPdf()
    {
        return View();
    }
    #region API

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

                return Json(new { Table1 = table1, Table2 = table2, Table3 = table3 });
            }
        }
    }
    [HttpGet]
    public async Task<IActionResult> GetAllIPPhoneDetails()
    {
        var iPPhoneDetails = unitOfWork.iPPhoneDetails.GetAll().Include(x => x.Department).ThenInclude(x => x.Unit);
        var department = unitOfWork.department.GetAll();
        var unitList = await unitOfWork.unit.GetAll().ToListAsync();
        return Json(new { data = iPPhoneDetails, dept = department, unit = unitList });
    }


    [HttpGet]
    public IActionResult GetAllSearch(string value, int Dept, int Unit)
    {
        if (value == null && Dept == 0 && Unit == 0)
        {
            var products = unitOfWork.iPPhoneDetails.GetAll().Include(x => x.Department).ThenInclude(x => x.Unit);
            return Json(new { data = products });
        }
        if (value == null && Dept != 0 && Unit != 0)
        {
            var products = unitOfWork.iPPhoneDetails.GetAll().Include(x => x.Department).ThenInclude(x => x.Unit).Where(x => x.DeptId == Dept && x.UnitId == Unit);
            return Json(new { data = products });
        }
        if (value == null && Dept != 0 && Unit == 0)
        {
            var products = unitOfWork.iPPhoneDetails.GetAll().Include(x => x.Department).ThenInclude(x => x.Unit).Where(x => x.DeptId == Dept);
            return Json(new { data = products });
        }
        if (value == null && Dept == 0 && Unit != 0)
        {
            var products = unitOfWork.iPPhoneDetails.GetAll().Include(x => x.Department).ThenInclude(x => x.Unit).Where(x => x.UnitId == Unit);
            return Json(new { data = products });
        }
        if (value != null && Dept != 0 && Unit == 0)
        {
            var products = unitOfWork.iPPhoneDetails.GetAll().Include(x => x.Department).ThenInclude(x => x.Unit).Where(x => x.DeptId == Dept && (x.DisplayName.Contains(value) ||
                x.IPNo.Contains(value) ||
                x.Department.Name.Contains(value)));
            return Json(new { data = products });
        }
        if (value != null && Dept == 0 && Unit != 0)
        {
            var products = unitOfWork.iPPhoneDetails.GetAll().Include(x => x.Department).ThenInclude(x => x.Unit).Where(x => x.UnitId == Unit && (x.DisplayName.Contains(value) ||
                x.IPNo.Contains(value) ||
                x.Department.Name.Contains(value)));
            return Json(new { data = products });
        }
        if (value != null && Dept != 0 && Unit != 0)
        {
            var products = unitOfWork.iPPhoneDetails.GetAll().Include(x => x.Department).ThenInclude(x => x.Unit).Where(x => x.UnitId == Unit && x.DeptId == Dept && (x.DisplayName.Contains(value) ||
                x.IPNo.Contains(value) ||
                x.Department.Name.Contains(value)));
            return Json(new { data = products });
        }
        else
        {
            var products = unitOfWork.iPPhoneDetails.GetAll().Include(x => x.Department).ThenInclude(x => x.Unit).Where(
                x => x.DisplayName.Contains(value) ||
                x.IPNo.Contains(value) ||
                x.Department.Name.Contains(value)
                );
            return Json(new { data = products });
        }

    }
    #endregion
}
