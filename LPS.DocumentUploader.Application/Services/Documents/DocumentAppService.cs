using AutoMapper;
using LPS.DocumentUploader.Application.Helpers;
using LPS.DocumentUploader.Application.Services.Documents.Dto;
using LPS.DocumentUploader.Application.Services.Notifications;
using LPS.DocumentUploader.Database.Databases;
using LPS.DocumentUploader.Database.Models;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPS.DocumentUploader.Application.Services.Documents
{
    public class DocumentAppService : IDocumentAppService, IDisposable
    {
        private readonly IHostingEnvironment _environment;
        private readonly LPSDBContext _databaseContext;
        private IMapper? _mapper;
        private readonly IEmailAppService _emailAppService;

        public DocumentAppService(IHostingEnvironment environment, LPSDBContext databaseContext, IMapper mapper, IEmailAppService emailAppService)
        {
            _environment = environment;
            _databaseContext = databaseContext;
            _mapper = mapper;
            _emailAppService = emailAppService;
        }

        public async Task<string> UploadFile(DocumentDto documentDto, string userEmail)
        {
            try
            {
                if (documentDto == null || documentDto.FileData == null || documentDto.FileData.Length == 0)
                {
                    throw new ArgumentException("Invalid document");
                }

                // Check file extension
                string[] allowedExtensions = { ".xlsx", ".pdf" };
                string fileExtension = Path.GetExtension(documentDto.FileName).ToLower();

                if (!allowedExtensions.Contains(fileExtension))
                {
                    throw new ArgumentException("Invalid file format. Supported formats: .xlsx, .pdf");
                }

                string uploadsFolder = Path.Combine(_environment.ContentRootPath, "Uploads");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                string fileName = Guid.NewGuid().ToString() + "_" + documentDto.FileName;
                string filePath = Path.Combine(uploadsFolder, fileName);

                FileMode fileMode = documentDto.ChunkNumber == 1 ? FileMode.Create : FileMode.Append;

                using (var stream = new FileStream(filePath, fileMode))
                {
                    await documentDto.FileData.CopyToAsync(stream);
                }

                if (documentDto.ChunkNumber == documentDto.TotalChunks)
                {
                    await CompleteFile(documentDto.FileName, documentDto.TotalChunks);
                    await SaveToDatabase(documentDto);
                    await _emailAppService.SendEmailAsync(userEmail, "Document Uploaded", "Your document has been successfully uploaded.");
                }

                return filePath;
            }
            catch (Exception ex)
            {
                throw new Exception($"File upload failed: {ex.Message}");
            }
        }

        private async Task SaveToDatabase(DocumentDto documentDto)
        {
            try
            {
                using (var transaction = await _databaseContext.Database.BeginTransactionAsync())
                {
                    try
                    {
                        var newUser = _mapper.Map<MstDocument>(documentDto);

                        _databaseContext.Documents.Add(newUser);
                        await _databaseContext.SaveChangesAsync();

                        await transaction.CommitAsync();
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync();
                    }
                }
            }
            catch (Exception outerEx)
            {
                
            }
        }

        private async Task CompleteFile(string fileName, int totalChunks)
        {
            try
            {
                string uploadsFolder = Path.Combine(_environment.ContentRootPath, "Uploads");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                string assembledFilePath = Path.Combine(uploadsFolder, fileName);

                using (var assembledStream = new FileStream(assembledFilePath, FileMode.Create, FileAccess.Write))
                {
                    for (int chunkNumber = 1; chunkNumber <= totalChunks; chunkNumber++)
                    {
                        string chunkFilePath = Path.Combine(uploadsFolder, $"{fileName}_chunk_{chunkNumber}");

                        using (var chunkStream = new FileStream(chunkFilePath, FileMode.Open, FileAccess.Read))
                        {
                            await chunkStream.CopyToAsync(assembledStream);
                        }
                        File.Delete(chunkFilePath);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to assemble file: {ex.Message}");
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _databaseContext?.Dispose();
                _mapper = null;
            }
        }
    }
}
