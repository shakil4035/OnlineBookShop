using Pims.Core.CommonModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pims.Core.Model
{
   public class Books: BaseDomain
    {
        public int Id { get; set; }
        public string BookTitel { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public bool IsDelete { get; set; }

    }
}
