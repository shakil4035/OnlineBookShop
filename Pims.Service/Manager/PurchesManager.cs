﻿using AutoMapper;
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
            var details = _dbContext.purchesDetailss.Where(s => s.PurchesId == id).ToList();
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
        public int Update(int id, PurchesViewModel vm)
        {
            var entity = _dbContext.purchess.SingleOrDefault(c => c.Id == id);
            Mapper.Map(vm, entity);
            entity.UpdateBy = "user";
            entity.UpdateDate = DateTime.Now;

            //remove details item
            var details = _dbContext.purchesDetailss.Where(c => c.PurchesId == entity.Id).ToList();

            foreach (var ps in details)
            {
                _dbContext.purchesDetailss.Remove(ps);
            }

            foreach (var ps in vm.PurchesDetail)
            {
                var model = new PurchesDetails()
                {
                    Id = ps.Id,
                    PurchesId = entity.Id,
                    BookId = ps.BookId,
                    Unit = ps.Unit,
                    BookName = ps.BookName,
                    Price = ps.Price,
                    Quantity = ps.Quantity,
                    LineTotal = ps.LineTotal
                };
                _dbContext.purchesDetailss.Add(model);
            }

            _dbContext.SaveChanges();
            return entity.Id;
        }

        public int Remove(int id)
        {
            var entity = _dbContext.purchess.SingleOrDefault(c => c.Id == id);
            _dbContext.purchess.Remove(entity);
            entity.DeleteBy = entity.CreateBy;
            entity.DeleteDate = DateTime.Now;
            entity.IsDelete = true;
            var isUpdate = _dbContext.SaveChanges();
            return isUpdate;
        }
        
    }
}
