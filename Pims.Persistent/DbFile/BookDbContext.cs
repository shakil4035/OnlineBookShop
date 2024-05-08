using Pims.Core.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pims.Persistent.DbFile
{
   public class BookDbContext:DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Category> categories { set; get; }
        public DbSet<Seller> Sellers { set; get; }
        public DbSet<Books> Books { get; set; }
        public DbSet<Purches> purchess { set; get; }
        public DbSet<PurchesDetails> purchesDetailss { set; get; }
    }
}
