using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPS.DocumentUploader.Application.Services.Documents.Dto
{
    public class DocumentDto
    {
        public int ChunkNumber { get; set; }
        public int TotalChunks { get; set; }
        public string FileName { get; set; }
        public IFormFile FileData { get; set; }
    }
}
