using RollCall.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RollCall.DAL
{
    public class BaseDal<T> where T:class ,new()
    {
       private DbContext db
        {
            get { return DbContextFactory.GetCurrentDbContext(); }

        }


        //DataModelContainer db = new DataModelContainer();
        #region  查询
        /// <summary>  
        /// 查找单个  
        /// </summary>  
        /// <param name="seleWhere">查询条件</param>  
        /// <returns></returns>  
        public T FirstOrDefault(Expression<Func<T, bool>> seleWhere)
        {
                return db.Set<T>().FirstOrDefault(seleWhere);
            
        }


        //查询 whereLambda u=>u.id>0 条件
        public IQueryable<T> GetEntity(Expression<Func<T, bool>> whereLambda)
        {
            DataModelContainer db = new DataModelContainer();
            return db.Set<T>().Where(whereLambda).AsQueryable();
        }



        public IQueryable<T> GetPageEntitys<S>(int pageSize, int pageIndex, out int total,
                                               Expression<Func<T, bool>> whereLambda,
                                               Expression<Func<T, S>> orderByLambda,
                                               bool isAse)
        {
            total = db.Set<T>().Where(whereLambda).Count();
            if (isAse)
            {
                var temp = db.Set<T>().Where(whereLambda)
                                .OrderBy<T, S>(orderByLambda)
                                .Skip(pageSize * (pageIndex - 1))
                                .Take(pageSize).AsQueryable();
                return temp;
            }
            else
            {
                var temp = db.Set<T>().Where(whereLambda)
                                .OrderByDescending<T, S>(orderByLambda)
                                .Skip(pageSize * (pageIndex - 1))
                                .Take(pageSize).AsQueryable();
                return temp;
            }
        }
        #endregion


        #region cud
        public T Add(T entity)
        {
            db.Set<T>().Add(entity);
         // db.SaveChanges();
            return entity;
        }

        public bool Update(T entity)
        {
            //db.Entry(T).State = EntityState.Unchanged;
            //return db.SaveChanges() > 0;
            db.Entry(entity).State = EntityState.Modified;
            // return db.SaveChanges() > 0;
            return true;


        }

        public bool Delete(T entity)
        {
            db.Entry(entity).State = EntityState.Deleted;

            // return db.SaveChanges() > 0;
            return true;
        }
        #endregion

        #region 批處理 add，update

        #endregion

    }
}
