using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPS.DocumentUploader.Database.Models
{
    [Table("MstDocument", Schema = "dbo")]
    public class MstDocument : TableData
    {
        public int ChunkNumber { get; set; }
        public int TotalChunks { get; set; }
        public string FileName { get; set; }
        public string TempFolder { get; set; }
    }
}
