using LPS.DocumentUploader.Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPS.DocumentUploader.Database.Databases
{
    public class LPSDBContext: DbContext
    {
        public DbSet<MstUser> Users { get; set; }
        public DbSet<MstDocument> Documents { get; set; }
        public LPSDBContext(DbContextOptions<LPSDBContext> options) : base(options)
        {

        }
    }
}
