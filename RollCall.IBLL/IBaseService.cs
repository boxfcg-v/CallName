using RollCall.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RollCall.IBLL
{
    public interface IBaseService<T> where T : class, new()
    {
         IBaseDal<T> CurrentDal { get; set; }

         IDbSession DbSession
        {
            get;



            set;
        }


        //查询 whereLambda u=>u.id>0 条件
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
