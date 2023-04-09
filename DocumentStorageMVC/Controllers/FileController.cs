using DocumentStorageMVC.Core;
using DocumentStorageMVC.Data;
using DocumentStorageMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace DocumentStorageMVC.Controllers
{
    [Authorize]
    public class FileController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IWebHostEnvironment _appEnv;
        private ApplicationDbContext _dbContext;

        public FileController(ILogger<HomeController> logger, IWebHostEnvironment appEnvironment, ApplicationDbContext dbContext)
        {
            _logger = logger;
            _appEnv = appEnvironment;
            _dbContext = dbContext;
        }

        public async Task<IActionResult> List(SortState sortOrder = SortState.TitleAsc)
        {
            IQueryable<Document> documents = _dbContext.Documents;

            ViewData["TitleSort"] = sortOrder == SortState.TitleAsc ? SortState.TitleDesc : SortState.TitleAsc;
            ViewData["DateSort"] = sortOrder == SortState.DateAsc ? SortState.DateDesc : SortState.DateAsc;
            ViewData["AuthorSort"] = sortOrder == SortState.AuthorAsc ? SortState.AuthorDesc : SortState.AuthorAsc;
            ViewData["DocumentTypeSort"] = sortOrder == SortState.DocumentTypeAsc ? SortState.DocumentTypeDesc : SortState.DocumentTypeAsc;

            documents = sortOrder switch
            {
                SortState.TitleAsc => documents.OrderBy(s => s.Title),
                SortState.TitleDesc => documents.OrderByDescending(s => s.Title),
                SortState.DateAsc => documents.OrderBy(s => s.Date),
                SortState.DateDesc => documents.OrderByDescending(s => s.Date),
                SortState.AuthorAsc => documents.OrderBy(s => s.Author),
                SortState.AuthorDesc => documents.OrderByDescending(s => s.Author),
                SortState.DocumentTypeAsc => documents.OrderBy(s => s.DocumentType),
                SortState.DocumentTypeDesc => documents.OrderByDescending(s => s.DocumentType)
            };

            return View(await documents.AsNoTracking().ToListAsync());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult AddFile()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> AddFile(UploadFileViewModel uploadFileViewModel)
        {
            if (uploadFileViewModel.File != null)
            {
                string path = "/Files/" + uploadFileViewModel.File.FileName;

                using (var fs = new FileStream(_appEnv.WebRootPath + path, FileMode.Create))
                {
                    await uploadFileViewModel.File.CopyToAsync(fs);
                }

                var document = new Document
                {
                    Title = uploadFileViewModel.Title,
                    Date = DateTime.Now,
                    Author = HttpContext.User.Identity.Name,
                    DocumentType = uploadFileViewModel.DocumentType,
                    Path = path
                };
                _dbContext.Documents.Add(document);
                _dbContext.SaveChanges();
            }

            return RedirectToAction("List", "File");
        }
    }
}
