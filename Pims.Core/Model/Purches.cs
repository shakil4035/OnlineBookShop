using Pims.Core.CommonModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pims.Core.Model
{
   public  class Purches: BaseDomain
    {
        public Purches()
        {
            PurchesDetail = new List<PurchesDetails>();
        }

        public int Id { get; set; }
        public string CustomerName { get; set; }
        public DateTime Date { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }
        public decimal? TotalBill { get; set; }
        public decimal? Discount { get; set; }
        public decimal? DiscountPercentage { get; set; }
        public decimal? Tax { get; set; }
        public decimal? TaxPercentage { get; set; }
        public decimal? Payable { get; set; }
        public bool IsDelete { set; get; }
        public ICollection<PurchesDetails> PurchesDetail { set; get; }
    }
}
