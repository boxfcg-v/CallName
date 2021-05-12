using RollCall.DALFactory;
using RollCall.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RollCall.BLL
{
 
        /// <summary>
        /// 父类要逼迫自己给父类的一个属性赋值
        /// 赋值的操作必须在父类的方法调用之前先执行
        /// </summary>
        /// 
        public abstract class BaseService<T> where T : class, new()
        {

        public IBaseDal<T> CurrentDal { get; set; }

        public IDbSession DbSession
        {
            get
            {
                return DbSessionFactory.GetCurrentDbSession();
            }
            set { }
        }




        #region MyRegion
        //基类的构造函数
        public BaseService()
        {
      
            SetCurrentDal();//抽象方法 
        }

        public abstract void SetCurrentDal();//抽象方法：要求子类必须实现。
        #endregion





        #region//查询 whereLambda u=>u.id>0 条件
        public IQueryable<T> GetEntity(Expression<Func<T, bool>> whereLambda)
            {
                return CurrentDal.GetEntity(whereLambda);
            }




        public IQueryable<T> GetPageEntitys<S>(int pageSize, int pageIndex, out int total,
                                                 Expression<Func<T, bool>> whereLambda,
                                                 Expression<Func<T, S>> orderByLambda,
                                                 bool isAse)
            {
                return CurrentDal.GetPageEntitys(pageSize, pageIndex, out total, whereLambda,
                    orderByLambda, isAse);
            }



            #endregion






            public T Add(T entity)
            {
                CurrentDal.Add(entity);
                DbSession.SaveChanges();
                return entity;
            }



            public bool Update(T entity)
            {
                CurrentDal.Update(entity);
                return DbSession.SaveChanges() > 0;
            }


            public bool Delete(T entity)
            {
                CurrentDal.Delete(entity);
                return DbSession.SaveChanges() > 0;
            }
        }
  }

