using DocumentStorageMVC.Core;
using System.ComponentModel.DataAnnotations;

namespace DocumentStorageMVC.Models
{
    public class UploadFileViewModel
    {        
        public string Title { get; set; }

        public IFormFile File { get; set; }

        public DocumentType DocumentType { get; set; }
    }
}
