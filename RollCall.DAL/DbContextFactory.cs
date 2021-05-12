using RollCall.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace RollCall.DAL
{
    public class DbContextFactory
    {
        public static DbContext GetCurrentDbContext()
        {
            //CallContext：是线程内部唯一的独用的数据槽（一块内存空间）
            //传递DbContext进去获取实例的信息，在这里进行强制转换。
            //一次請求公用一個實例，上下文都可以做到切換
            DbContext db = CallContext.GetData("DbContext") as DbContext;
            if (db == null)
            {
                db = new DataModelContainer(); 
                CallContext.SetData("DbContext", db);
            }
            return db;
        }
           
    }
}
