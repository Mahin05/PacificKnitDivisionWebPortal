using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineBookOrderManagementSystem.Repositories.IRepository;
using PacificKnitDivisionWebPortal.Models;

namespace PacificKnitDivisionWebPortal.Controllers;

[Area("User")]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    private readonly IUnitOfWork unitOfWork;

    public HomeController(ILogger<HomeController> logger, IUnitOfWork _unitOfWork)
    {
        _logger = logger;
        unitOfWork = _unitOfWork;
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

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
