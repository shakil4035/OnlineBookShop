using AutoMapper;
using Pims.Core.Model;
using Pims.Core.ViewModel;
using Pims.Persistent.DbFile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pims.Service.Manager
{
   public class SellerManager
    {
        public BookDbContext _dbcontext;

        public SellerManager()
        {
            _dbcontext = new BookDbContext();
        }
        public SellerViewModel Get(int id)
        {
            var entity = _dbcontext.Sellers.SingleOrDefault(c => c.Id == id);
            return (Mapper.Map<Seller,SellerViewModel>(entity));
        }
        public IEnumerable<SellerViewModel> GetAll()
        {
            var entites = _dbcontext.Sellers.ToList().Select(Mapper.Map<Seller, SellerViewModel>);
            return entites;
        }
        public int Add(SellerViewModel vm)
        {
            var entity = Mapper.Map<SellerViewModel, Seller>(vm);
            _dbcontext.Sellers.Add(entity);
            entity.CreateBy = "user";
            entity.CreateDate = DateTime.Now;
            entity.IsDelete = false;
            _dbcontext.SaveChanges();
            return entity.Id;
        }

        public int Update(int id,SellerViewModel vm)
        {
            var entity = _dbcontext.Sellers.SingleOrDefault(c => c.Id == id);
            Mapper.Map(vm, entity);
            entity.UpdateBy = "user";
            entity.UpdateDate = DateTime.Now;
            _dbcontext.SaveChanges();
            return entity.Id;
        }
        public int remove(int id)
        {
            var entity = _dbcontext.Sellers.SingleOrDefault(c => c.Id == id);
            _dbcontext.Sellers.Remove(entity);
            entity.DeleteBy = entity.CreateBy;
            entity.DeleteDate = DateTime.Now;
            entity.IsDelete = true;
            var isUpdate = _dbcontext.SaveChanges();
            return isUpdate;
        }
        public string CreateSellerId()
        {
            int parsonalNo = 0;

            var list = _dbcontext.Sellers.ToList()
                .OrderByDescending(c => c.Id).FirstOrDefault();

            if (list == null)
            {
                var code = "OSB-" + "000001";
                return code;
            }

            {
                string[] parts = list.IdNo.Split('-');
                parsonalNo = Convert.ToInt32(parts[1]);
            }

            var SelesParsonalNo = "OSB-" + (parsonalNo + 1).ToString().PadLeft(5, '0');
            return SelesParsonalNo;
        }
    }
}
