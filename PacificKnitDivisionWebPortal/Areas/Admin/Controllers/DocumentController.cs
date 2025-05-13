using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class DocumentController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly IUnitOfWork unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public DocumentController(IUnitOfWork _unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            unitOfWork = _unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Admin/Document
        public async Task<IActionResult> Index()
        {
            var DocumentList = unitOfWork.Document.GetAll().OrderBy(x=>x.FileType).ThenBy(x=>x.DisplayOrder).ToArrayAsync();
            return View(await DocumentList);
        }

        // GET: Admin/Document/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var document = await unitOfWork.Document
                .Get(m => m.Id == id);
            if (document == null)
            {
                return NotFound();
            }

            return View(document);
        }

        // GET: Admin/Document/Create
        public IActionResult Create()
        {
            return View();
        }

        public IActionResult GetDisplayOrder(string fileType)
        {
            var displayOrder = unitOfWork.Document.GetAll().Where(x=>x.FileType==fileType).Select(x=>x.DisplayOrder).Max();
            return Json(new {data =  displayOrder});
        }

        public async Task<IActionResult> Upsert(int? Id)
        {
            if (Id == null)
            {
                return View(new DocumentModel());
            }
            else
            {
                var document = await unitOfWork.Document.Get(m => m.Id == Id);
                if (document == null)
                {
                    return NotFound();
                }
                return View(document);
            }
        }



        // POST: Admin/Document/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(DocumentModel document, IFormFile? file) 
            //[Bind("Id,Name,DisplayOrder,FileType,IsDelete,FileUrl")] 
        {
            if (ModelState.IsValid)
            {
                var wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    if (document.FileType == "Notice")
                    {
                        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                        var documentFolder = Path.Combine(wwwRootPath, @"Assets\Notice\");
                        if (!string.IsNullOrEmpty(document.FileUrl))
                        {
                            var oldImagePath = Path.Combine(wwwRootPath, document.FileUrl.TrimStart('\\'));
                            if (System.IO.File.Exists(oldImagePath))
                            {
                                System.IO.File.Delete(oldImagePath);
                            }
                        }
                        using (var fileStream = new FileStream(Path.Combine(documentFolder, fileName), FileMode.Create))
                        {
                            file.CopyTo(fileStream);
                        }
                        document.FileUrl = @"\Assets\Notice\" + fileName;
                    }
                    if (document.FileType == "Info")
                    {
                        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                        var documentFolder = Path.Combine(wwwRootPath, @"Assets\Info\");
                        if (!string.IsNullOrEmpty(document.FileUrl))
                        {
                            var oldImagePath = Path.Combine(wwwRootPath, document.FileUrl.TrimStart('\\'));
                            if (System.IO.File.Exists(oldImagePath))
                            {
                                System.IO.File.Delete(oldImagePath);
                            }
                        }
                        using (var fileStream = new FileStream(Path.Combine(documentFolder, fileName), FileMode.Create))
                        {
                            file.CopyTo(fileStream);
                        }
                        document.FileUrl = @"\Assets\Info\" + fileName;

                    }
                    if (document.FileType == "Files")
                    {
                        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                        var documentFolder = Path.Combine(wwwRootPath, @"Assets\Info\");
                        if (!string.IsNullOrEmpty(document.FileUrl))
                        {
                            var oldImagePath = Path.Combine(wwwRootPath, document.FileUrl.TrimStart('\\'));
                            if (System.IO.File.Exists(oldImagePath))
                            {
                                System.IO.File.Delete(oldImagePath);
                            }
                        }
                        using (var fileStream = new FileStream(Path.Combine(documentFolder, fileName), FileMode.Create))
                        {
                            file.CopyTo(fileStream);
                        }
                        document.FileUrl = @"\Assets\Info\" + fileName;

                    }
                }


                if (document.Id == null)
                {
                    TempData["success"] = "Product Created Successfully!";
                    unitOfWork.Document.Add(document);
                }
                else
                {
                    TempData["success"] = "Product Updated Successfully!";
                    unitOfWork.Document.Update(document);
                }
                unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(document);
        }

        // GET: Admin/Document/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var document = await unitOfWork.Document.Get(x=>x.Id==id);
            if (document == null)
            {
                return NotFound();
            }
            return View(document);
        }

        // POST: Admin/Document/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DocumentModel document)
            //[Bind("Id,Name,DisplayOrder,FileType,IsDelete,FileUrl")]
        {
            if (id != document.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await unitOfWork.Document.Update(document);
                    unitOfWork.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DocumentModelExists(document.Id))
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
            return View(document);
        }

        // GET: Admin/Document/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var document = await unitOfWork.Document
                .Get(m => m.Id == id);
            if (document == null)
            {
                return NotFound();
            }

            return View(document);
        }

        // POST: Admin/Document/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var document = await unitOfWork.Document.Get(x=>x.Id==id);
            if (document != null)
            {
                await unitOfWork.Document.Remove(document);
            }

            unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool DocumentModelExists(int id)
        {
            return _context.documents.Any(e => e.Id == id);
        }
    }
}
