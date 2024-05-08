using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pims.Core.ViewModel
{
    public class AuthorViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Country { get; set; }
    }
}
