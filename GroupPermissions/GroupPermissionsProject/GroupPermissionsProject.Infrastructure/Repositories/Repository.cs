using GroupPermissionsProject.Core.Interfaces;
using GroupPermissionsProject.Core.Shared;
using GroupPermissionsProject.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace GroupPermissionsProject.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly GroupPermissionsProjectContext _dbContext;

        public Repository(GroupPermissionsProjectContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Insert(T entity)
        {
            entity.CreatedDate = DateTime.UtcNow;
            _dbContext.Set<T>().Add(entity);
            _dbContext.SaveChanges();
        }

        public void BulkInsert(List<T> entity)
        {
            entity.All(a => { a.CreatedDate = DateTime.UtcNow; return true; });
            _dbContext.Set<T>().AddRange(entity);
            _dbContext.SaveChanges();
        }
        public void BulkDelete(List<T> entity)
        {
            entity.All(a => { a.ModifiedDate = DateTime.UtcNow; a.IsDeleted = true; return true; });
            _dbContext.Set<T>().UpdateRange(entity);
            _dbContext.SaveChanges();
        }
        public void Delete(T entity)
        {
            // soft delete
            entity.ModifiedDate = DateTime.UtcNow;
            entity.IsDeleted = true;
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void HardDelete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            _dbContext.SaveChanges();
        }

        public void Update(T entity)
        {
            entity.ModifiedDate = DateTime.UtcNow;
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public virtual T GetById(int id)
        {
            return _dbContext.Set<T>().Find(id);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _dbContext.Set<T>().Where(a => a.IsDeleted != true || a.IsDeleted == null)
                //.Include(a => a.CreatedByName)
                //.Include(a => a.ModifiedByName)                
                .AsEnumerable();
        }

        public IEnumerable<T> Get(Expression<Func<T, bool>> predicate)
        {
            return _dbContext.Set<T>()
               .Where(predicate)
               .AsEnumerable();
        }

        public bool Exists(int id)
        {
            return (GetById(id) != null);
        }

        public void BulkHardDelete(List<T> entity)
        {
            _dbContext.Set<T>().RemoveRange(entity);
            _dbContext.SaveChanges();
        }
    }
}
