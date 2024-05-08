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
  public class AuthorManager
    {
        public BookDbContext _dbContext;

        public AuthorManager()
        {
            _dbContext = new BookDbContext();
        }
        public AuthorViewModel Get(int id)
        {
            var authors = _dbContext.Authors.SingleOrDefault(c => c.Id == id);
            //var author = new AuthorViewModel()
            //{
            //    Id = authors.Id,
            //    Name=authors.Name,
            //    Gender=authors.Gender,
            //    Country=authors.Country
            //};
            return (Mapper.Map<Author, AuthorViewModel>(authors));

        }
        public IEnumerable <AuthorViewModel>GetAll()
        {
            var authors = _dbContext.Authors.ToList().Select(Mapper.Map <Author,AuthorViewModel>);
            return authors;
        }

        public int Add(AuthorViewModel vm)
        {
            var entity = Mapper.Map<AuthorViewModel, Author>(vm);
            _dbContext.Authors.Add(entity);
            var isSave = _dbContext.SaveChanges();
            return isSave;
        }
        public int Update(int id,AuthorViewModel vm)
        {
            var entity = _dbContext.Authors.SingleOrDefault(c => c.Id == vm.Id);
            Mapper.Map(vm,entity);
            var isUpdate = _dbContext.SaveChanges();
            return isUpdate;
        }
        public int Remove (int id)
        {
            var entity = _dbContext.Authors.SingleOrDefault(c => c.Id == id);
            _dbContext.Authors.Remove(entity);
            var isDelete = _dbContext.SaveChanges();
            return isDelete;
        }


    }
}
