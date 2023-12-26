using LPS.DocumentUploader.Application.Services.Documents.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPS.DocumentUploader.Application.Services.Documents
{
    public interface IDocumentAppService
    {
        Task<string> UploadFile(DocumentDto documentDto, string userEmail);
    }
}
