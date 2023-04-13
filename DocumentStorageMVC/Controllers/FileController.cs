using DocumentStorageMVC.Core;
using DocumentStorageMVC.Data;
using DocumentStorageMVC.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace DocumentStorageMVC.Controllers
{
    [Authorize]
    public class FileController : Controller
    {
        private readonly IWebHostEnvironment _appEnv;
        private readonly IRepository<Document> _repository;
        private readonly IMediator _mediator;

        public FileController(IWebHostEnvironment appEnvironment, IRepository<Document> repository, IMediator mediator)
        {
            _appEnv = appEnvironment;
            _repository = repository;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> List(string title, SortState sortOrder = SortState.TitleAsc)
        {           
            var request = new GetDocumentListQuery()
                { TitleFilter = title, SortOrder = sortOrder };

            try
            {
                var viewModel = await _mediator.Send(request);
                return View(viewModel);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "File");
            }

            
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult AddFile()
        {
            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> AddFile(CreateDocumentCommand request)
        {
            if (!ModelState.IsValid)
            {
                return View("List");
            }

            request.Author = HttpContext.User.Identity.Name;

            try
            {
                await _mediator.Send(request);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "File");
            }

            return RedirectToAction("List", "File");
        }

        public async Task<IActionResult> GetFile(Guid id)
        {
            var document = await _repository.GetById(id);
            if (document != null)
            {
                string filePath = _appEnv.WebRootPath + document.Path;
                string fileExtension = filePath.Substring(filePath.LastIndexOf('.'));
                return PhysicalFile(filePath, "application/octet-stream", document.Title + fileExtension);
            }
            return RedirectToAction("Error", "File");
        }
    }
}
