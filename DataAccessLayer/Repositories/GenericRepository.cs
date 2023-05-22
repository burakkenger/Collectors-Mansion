using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class GenericRepository<T> : IGenericDal<T> where T : class
    {
        Context data = new Context();
        public void genericRep_Insert(T entity)
        {
            data.Add(entity);
            data.SaveChanges();
        }

        public void genericRep_Update(T entity)
        {
            data.Update(entity);
            data.SaveChanges();
        }

        public void genericRep_Edit(Expression<Func<T, bool>> condition, Expression<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>> expression)
        {
            data.Set<T>().Where(condition).ExecuteUpdate(expression);
        }

        public void genericRep_Delete(T entity) 
        {
            data.Remove(entity);
            data.SaveChanges();
        }
                                                
        public T genericRep_GetByID(int ID)
        {
            return data.Set<T>().Find(ID);
        }

        public T genericRep_Get(Expression<Func<T, bool>> predicate)
        {
            return data.Set<T>().FirstOrDefault(predicate);
        }

        public List<T> genericRep_GetAll()
        {
            return data.Set<T>().ToList();
        }

        public List<T> genericRep_GetAll(Expression<Func<T, bool>> predicate)
        {
            return data.Set<T>().Where(predicate).ToList();
        }

    }
}
