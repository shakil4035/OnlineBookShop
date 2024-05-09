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
   public class BookManager
    {
        public BookDbContext _dbContext;
        public BookManager()
        {
            _dbContext = new BookDbContext();
        }

        public BookViewModel Get(int id)
        {
            var entity = _dbContext.Books.SingleOrDefault(c => c.Id == id);
            return (Mapper.Map<Books, BookViewModel>(entity));
        }

        public IEnumerable<BookViewModel> GetAll()
        {
            var enities = _dbContext.Books.Include("Author").Include("Category").Where(c => c.IsDelete == false)
                .ToList().Select(Mapper.Map<Books, BookViewModel>);
            return enities;
        }
        public int Add(BookViewModel vm)
        {
            var entity = Mapper.Map<BookViewModel, Books>(vm);
            _dbContext.Books.Add(entity);
            entity.CreateBy = "user";
            entity.CreateDate = DateTime.Now;
            entity.IsDelete = false;
            _dbContext.SaveChanges();
            return entity.Id;
        }
        public int Update(int id, BookViewModel vm)
        {
            var entity = _dbContext.Books.SingleOrDefault(c => c.Id == id);
            Mapper.Map(vm, entity);
            entity.UpdateBy = "user";
            entity.UpdateDate = DateTime.Now;
            _dbContext.SaveChanges();
            return entity.Id;
        }
        public int Remove(int id)
        {
            var entity = _dbContext.Books.SingleOrDefault(c => c.Id == id);
            _dbContext.Books.Remove(entity);
            entity.DeleteBy = entity.CreateBy;
            entity.DeleteDate = DateTime.Now;
            entity.IsDelete = true;
            var Update = _dbContext.SaveChanges();
            return Update;
        }
    }
}
