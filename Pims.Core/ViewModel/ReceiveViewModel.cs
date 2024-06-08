using Pims.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pims.Core.ViewModel
{
   public class ReceiveViewModel
    {
        public ReceiveViewModel()
        {
            ReceiveDitail = new List<ReceiveDetailsViewModel>();
        }

        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string Date { get; set; }
        public string ReceiveDate { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }
        public int PurchesId { get; set; }
        public Purches Purches { get; set; }
        public decimal? TotalBill { get; set; }
        public decimal? Discount { get; set; }
        public decimal? DiscountPercentage { get; set; }
        public decimal? Tax { get; set; }
        public decimal? TaxPercentage { get; set; }
        public decimal? Payable { get; set; }
        public bool IsDelete { set; get; }
        public ICollection<ReceiveDetailsViewModel> ReceiveDitail { set; get; }
    }
}
