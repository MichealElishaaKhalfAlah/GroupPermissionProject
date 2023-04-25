using GroupPermissionsProject.Core.Shared;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace GroupPermissionsProject.Core.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        T GetById(int id);
        IEnumerable<T> GetAll();
        IEnumerable<T> Get(Expression<Func<T, bool>> predicate);
        void Insert(T entity);
        void BulkInsert(List<T> entity);
        void BulkDelete(List<T> entity);
        void Delete(T entity);
        void HardDelete(T entity);
        void BulkHardDelete(List<T> entity);
        void Update(T entity);
        bool Exists(int id);
    }
}
