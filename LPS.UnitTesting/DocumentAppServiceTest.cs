using AutoMapper;
using LPS.DocumentUploader.Application.Helpers;
using LPS.DocumentUploader.Application.Services.Documents;
using LPS.DocumentUploader.Application.Services.Documents.Dto;
using LPS.DocumentUploader.Application.Services.Notifications;
using LPS.DocumentUploader.Database.Databases;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPS.UnitTesting
{
    public class DocumentAppServiceTest : Mock<IDocumentAppService>
    {
        [Fact]
        public async Task UploadFile_ValidDocument_Success()
        {
            // Arrange
            var documentDto = new DocumentDto
            {
                FileData = new FormFile(Stream.Null, 0, 0, "file", "example.xlsx"),
                ChunkNumber = 1,
                TotalChunks = 1,
                FileName = "example.xlsx"
            };

            string userEmail = "test@example.com";

            var mockDocAppService = new Mock<IDocumentAppService>();
            //mockDocAppService.Setup(x => x.UploadFile(documentDto, userEmail))
            //        .ReturnsAsync("Success");

            //// Act
            //var result = await mockDocAppService.Object.UploadFile(documentDto, userEmail);

            //// Assert
            //Assert.Matches("Success", result);
        }
    }
}
