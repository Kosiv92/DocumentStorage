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
        private readonly IMediator _mediator;

        public FileController(IMediator mediator)
            => _mediator = mediator;
       
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
        public IActionResult UploadFile()
        {
            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(CreateDocumentCommand request)
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

        public async Task<IActionResult> DownloadFile(Guid id)
        {            
            try
            {
                var dto = await _mediator.Send(new UploadDocumentQuery(id));
                return PhysicalFile(dto.FilePath, "application/octet-stream", dto.FileName);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "File");
            }                        
        }
    }
}
