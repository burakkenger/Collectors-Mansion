using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class GenericManager<T> : IGenericService<T> where T : class
    {
        IGenericDal<T> genericDal;

        public GenericManager(IGenericDal<T> EfRepository)
        {
            genericDal = EfRepository;
        }

        public void Insert(T t)
        {
            genericDal.genericRep_Insert(t);
        }

        public void Update(T t)
        {
            genericDal.genericRep_Update(t);
        }

        public void Edit(Expression<Func<T, bool>> condition, Expression<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>> expression)
        {
            genericDal.genericRep_Edit(condition, expression);
        }

        public void Delete(T t)
        {
            genericDal.genericRep_Delete(t);
        }

        public T GetByID(int ID)
        {
            return genericDal.genericRep_GetByID(ID);
        }

        public T Get(Expression<Func<T, bool>> predicate)
        {
            return genericDal.genericRep_Get(predicate);
        }

        public List<T> GetAll()
        {
            return genericDal.genericRep_GetAll();
        }

        public List<T> GetAll(Expression<Func<T, bool>> predicate)
        {
            return genericDal.genericRep_GetAll(predicate);
        }

    }
}
