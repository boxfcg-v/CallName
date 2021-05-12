using GIGABYTE.DG.Helpers;
using RollCall.UI.Commo;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;

namespace RollCall.UI.Controllers
{
    public class ExcelIOController : BaseController
    {
       

        // GET: ExcelIO
        public ActionResult Index()
        {
            return View();
        }

        //導入
        public ActionResult In()
        {
            
            return View();
        }



        /// <summary>  
        /// 附件上传  
        /// </summary>  
        /// <returns></returns>  
        [HttpPost]
        public ActionResult AjaxUpload()
        {
            HttpFileCollection files = System.Web.HttpContext.Current.Request.Files;
            if (files.Count == 0) return Json("Faild", JsonRequestBehavior.AllowGet);
            MD5 md5Hasher = new MD5CryptoServiceProvider();
            /*计算指定Stream对象的哈希值*/
            byte[] arrbytHashValue = md5Hasher.ComputeHash(files[0].InputStream);
            /*由以连字符分隔的十六进制对构成的String，其中每一对表示value中对应的元素；例如“F-2C-4A”*/
            //string strHashData = System.BitConverter.ToString(arrbytHashValue).Replace("-", "");
            //string FileEextension = Path.GetExtension(files[0].FileName);//後綴名字
            string FileName = Path.GetFileName(files[0].FileName);
            //string uploadDate = DateTime.Now.ToString("yyyyMMddHHmmss");
            //string virtualPath = string.Format("../Shared/Upload/{0}/{1}{2}", uploadDate, strHashData, FileEextension);
            string virtualPath = string.Format("../File/Excel/{0}", FileName);
            string fullFileName = Server.MapPath(virtualPath);
            //创建文件夹，保存文件  
            string path = Path.GetDirectoryName(fullFileName);
            Directory.CreateDirectory(path);
            //判断文件是不是存在
            if (System.IO.File.Exists(fullFileName))
            {
                //如果存在则删除
                System.IO.File.Delete(fullFileName);
            }

            if (!System.IO.File.Exists(fullFileName))
            {
                files[0].SaveAs(fullFileName);
            }

            bool chliec = true;
            DataTable dt = NOPIHelper.GetTable(fullFileName, true, 1);
            if (dt.Columns.Count < 8) { chliec = false; }

            int aa=dt.Rows.Count;
            ViewData["js"] = aa;
            //判斷格式 職位是不是 線長 線員 ，白班是不是白晚班
            foreach (DataRow dr in dt.Rows)
            {
                for (int i = 0; i < 8; i++)
                {
                    if (string.IsNullOrEmpty(dr[i].ToString()))
                    {
                        chliec = false; break;
                    }
                    
                }
                if (!chliec) { break; }
                string a = dr.Field<string>("班別").ToString();
                if (dr.Field<string>("職位").ToString() != "線長" && dr.Field<string>("職位").ToString() != "線員" && dr.Field<string>("職位").ToString() != "主管")
                {
                    chliec = false; break;
                }
                if (dr.Field<string>("班別").ToString() != "白班" && dr.Field<string>("班別").ToString() != "晚班")
                {
                    chliec = false; break;
                }

            }
            if (chliec)
            {
                ExcelIOCommon comm = new ExcelIOCommon();
                comm.addStaff(dt);
                comm.addPhoneUsers(dt);
                comm.addLienNumber(dt);
            }

           
          

            string fileName = files[0].FileName.Substring(files[0].FileName.LastIndexOf("\\") + 1, files[0].FileName.Length - files[0].FileName.LastIndexOf("\\") - 1);
            string fileSize = GetFileSize(files[0].ContentLength);
            //ViewData["seccuss"] = "OK";
           
            if (chliec)
                return Json(new { FileName = fileName, FilePath = virtualPath, FileSize = fileSize }, "text/html", JsonRequestBehavior.AllowGet);
            else
                return View();
        }

        private string GetFileSize(long bytes)
        {
            long kblength = 1024;
            long mbLength = 1024 * 1024;
            if (bytes < kblength)
                return bytes.ToString() + "B";
            if (bytes < mbLength)
                return decimal.Round(decimal.Divide(bytes, kblength), 2).ToString() + "KB";
            else
                return decimal.Round(decimal.Divide(bytes, mbLength), 2).ToString() + "MB";
        }
    }
}