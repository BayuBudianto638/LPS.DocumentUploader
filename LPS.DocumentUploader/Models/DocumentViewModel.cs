namespace LPS.DocumentUploader.Models
{
    public class DocumentViewModel
    {
        public int ChunkNumber { get; set; }
        public int TotalChunks { get; set; }
        public string FileName { get; set; }
        public IFormFile FileData { get; set; }
    }
}
