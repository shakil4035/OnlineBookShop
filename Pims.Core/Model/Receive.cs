using Pims.Core.CommonModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pims.Core.Model
{
   public  class Receive: BaseDomain
    {
        public Receive()
        {
            ReceiveDitail = new List<Receive_Details>();
        }

        public int Id { get; set; }
        public string CustomerName { get; set; }
        public DateTime Date { get; set; }
        public DateTime ReceiveDate { get; set; }
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
        public ICollection<Receive_Details> ReceiveDitail { set; get; }
    }
}
