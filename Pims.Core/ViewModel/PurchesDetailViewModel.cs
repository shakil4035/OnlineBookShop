using Pims.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pims.Core.ViewModel
{
   public  class PurchesDetailViewModel
    {
        public int Id { set; get; }
        public int PurchesId { get; set; }
        public Purches Purches { get; set; }
        public double Quantity { set; get; }
        public double Price { set; get; }
        public string Unit { set; get; }
        public double LineTotal { set; get; }
        public int BookId { set; get; }
        public string BookName { set; get; }
    }
}
