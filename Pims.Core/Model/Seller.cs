using Pims.Core.CommonModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pims.Core.Model
{
   public class Seller:BaseDomain
    {
        public int Id { get; set; }
        public string IdNo { set; get; }
        public string Name { set; get; }
        public string Gender { get; set; }
        public DateTime JoinjngDate { get; set; }
        public DateTime BirithDate { get; set; }
        public string MeritialStatus { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public string Address { get; set; }
        public bool IsDelete { get; set; }
    }
}
