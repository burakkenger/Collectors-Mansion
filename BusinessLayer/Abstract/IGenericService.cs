using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IGenericService<T> where T : class
    {
        void Insert(T t);
        void Delete(T t);
        void Update(T t);
        void Edit(Expression<Func<T, bool>> condition, Expression<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>> expression);
        T GetByID(int ID);
        T Get(Expression<Func<T, bool>> predicate);
        List<T> GetAll();
        List<T> GetAll(Expression<Func<T, bool>> predicate);
    }
}
