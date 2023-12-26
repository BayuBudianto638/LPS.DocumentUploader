using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPS.DocumentUploader.Database.Models
{
    [Table("MstUser", Schema = "dbo")]
    public class MstUser : TableData
    {
        [Required]
        [Column(TypeName = "Nvarchar(100)")]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required]
        [Column(TypeName = "Nvarchar(100)")]
        [Display(Name = "Password")]
        public string Password { get; set; }
        public string SurName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int NoHP { get; set; }
        public string PasswordSalt { get; set; }
    }
}
