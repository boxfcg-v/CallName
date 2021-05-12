using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RollCall.IDAL
{
    public interface IBaseDal<T> where T : class, new()
    {

        //查询 whereLambda u=>u.id>0 条件

       T FirstOrDefault(Expression<Func<T, bool>> seleWhere);
        
           

        
        IQueryable<T> GetEntity(Expression<Func<T, bool>> whereLambda);




            IQueryable<T> GetPageEntitys<S>(int pageSize, int pageIndex, out int total,
                                                 Expression<Func<T, bool>> whereLambda,
                                                 Expression<Func<T, S>> orderByLambda,
                                                 bool isAse);

            T Add(T entity);


            bool Update(T entity);


            bool Delete(T entity);


        
    }
}
