using Microsoft.AspNetCore.Mvc;
using OnlineBookOrderManagementSystem.Repositories.IRepository;
using PacificKnitDivisionWebPortal.Data;

namespace PacificKnitDivisionWebPortal.Areas.Admin.Controllers
{
    public class HomeController:Controller
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

        public IActionResult Index()
        {
            return View();
        }
    }
}
