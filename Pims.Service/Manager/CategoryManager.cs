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
   public class CategoryManager
    {
        public BookDbContext _dbContext;
        public CategoryManager()
        {
            _dbContext = new BookDbContext();
        }

        public CategotyViewModel Get(int id)
        {
            var entity = _dbContext.categories.SingleOrDefault(c => c.Id == id);
            return (Mapper.Map<Category, CategotyViewModel>(entity));
        }
        public IEnumerable<CategotyViewModel> GetAll()
        {
            var enities = _dbContext.categories.ToList().Select(Mapper.Map<Category, CategotyViewModel>);
            return enities;
        }
        public int Add(CategotyViewModel vm)
        {
            var entity = Mapper.Map<CategotyViewModel, Category>(vm);
            _dbContext.categories.Add(entity);
            entity.CreateBy = "user";
            entity.CreateDate = DateTime.Now;
            entity.IsDelete = false;
            _dbContext.SaveChanges();
            return entity.Id;
        }
        public int Update(int id,CategotyViewModel vm)
        {
            var entity = _dbContext.categories.SingleOrDefault(c => c.Id == id);
            Mapper.Map(vm,entity);
            entity.UpdateBy = "user";
            entity.UpdateDate = DateTime.Now;
            _dbContext.SaveChanges();
            return entity.Id;
        }
        public int Remove(int id)
        {
            var entity = _dbContext.categories.SingleOrDefault(c => c.Id == id);
            _dbContext.categories.Remove(entity);
            entity.DeleteBy = entity.CreateBy;
            entity.DeleteDate = DateTime.Now;
            entity.IsDelete = true;
            var isUpdate = _dbContext.SaveChanges();
            return isUpdate;
        }
    }
}
