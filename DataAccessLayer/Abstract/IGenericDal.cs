using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IGenericDal<T> where T : class
    {
        void genericRep_Insert(T entity);
        void genericRep_Delete(T entity);
        void genericRep_Update(T entity);
        void genericRep_Edit(Expression<Func<T, bool>> condition, Expression<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>> expression);
        T genericRep_GetByID(int ID);
        T genericRep_Get(Expression<Func<T, bool>> predicate);
        List<T> genericRep_GetAll();
        List<T> genericRep_GetAll(Expression<Func<T, bool>> predicate);

    }
}
