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
   public class PurchesManager
    {
        public BookDbContext _dbContext;

        public PurchesManager()
        {
            _dbContext = new BookDbContext();
        }

        public PurchesViewModel Get(int id)
        {
            var entity = _dbContext.purchess.SingleOrDefault(c => c.Id == id);
            return (Mapper.Map<Purches, PurchesViewModel>(entity));
        }

        public IEnumerable<PurchesViewModel> GetAll()
        {
            var enities = _dbContext.purchess.Include("Author").Where(c => c.IsDelete == false)
                .ToList().Select(Mapper.Map<Purches, PurchesViewModel>);
            return enities;
        }

        public int Add(PurchesViewModel vm)
        {
            var entity = Mapper.Map<PurchesViewModel, Purches>(vm);
            _dbContext.purchess.Add(entity);
            entity.CreateBy = "user";
            entity.CreateDate = DateTime.Now;
            entity.IsDelete = false;
            _dbContext.SaveChanges();
            return entity.Id;
        }
        
    }
}
