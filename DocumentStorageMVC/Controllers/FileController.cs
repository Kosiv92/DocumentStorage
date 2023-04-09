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

        public IActionResult List()
        {
            var documents = _dbContext.Documents.AsNoTracking();
            return View(documents);
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
