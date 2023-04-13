using MediatR;
using System.ComponentModel.DataAnnotations;

namespace DocumentStorageMVC.Core
{
    public class CreateDocumentCommand : IRequest<Guid>
    {
        [Required(ErrorMessage = "Укажите наименование (не более 50 символов)")]
        [StringLength(35, ErrorMessage = "Укажите наименование не более {1} символов")]
        [Display(Name = "Наименование документа")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Укажите файл для загрузки")]
        [Display(Name = "Файл")]
        public IFormFile File { get; set; }

        [Required(ErrorMessage = "Укажите тип документа")]
        [Display(Name = "Тип документа")]
        public DocumentType DocumentType { get; set; }

        public string? Author { get; set; }

        public string? Path { get; set; }
    }
}
