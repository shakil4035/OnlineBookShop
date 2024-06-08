using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pims.Core.Model
{
   public  class Receive_Details
    {
        public int Id { set; get; }
        public int ReceiveId { get; set; }
        public Receive Receive { get; set; }
        public double Quantity { set; get; }
        public double Price { set; get; }
        public string Unit { set; get; }
        public double LineTotal { set; get; }
        public int BookId { set; get; }
        public string BookName { set; get; }
    }
}
