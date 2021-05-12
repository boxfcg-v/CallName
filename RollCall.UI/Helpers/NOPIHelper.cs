
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;

namespace GIGABYTE.DG.Helpers
{
    public class NOPIHelper
    {
        /// <summary>
        /// 將Excel文件里的數據整理成一個dateTable
        /// </summary>
        /// <param name="filePath">Excel文件路徑</param>
        /// <param name="isColumnName">有沒有標題行（標題要在內容的上一行）</param>
        /// <param name="index">標題從第幾行開始</param>
        /// <returns>DataTable</returns>
        public static DataTable GetTable(string filePath, bool isColumnName,int index=1)
        {
            
            DataTable dataTable = null;
            FileStream fs = null;
            DataColumn column = null;
            DataRow dataRow = null;
            IWorkbook workbook = null;
            ISheet sheet = null;
            IRow row = null;
            ICell cell = null;
            int startRow = 0;          
            try
            {
                using (fs = File.OpenRead(filePath))
                {
                    // 2007版本
                    if (filePath.IndexOf(".xlsx") > 0)
                        workbook = new XSSFWorkbook(fs);
                    // 2003版本
                    else if (filePath.IndexOf(".xls") > 0 || filePath.IndexOf(".XLS") > 0)
                        workbook = new HSSFWorkbook(fs);

                    if (workbook != null)
                    {
                        sheet = workbook.GetSheetAt(0);//读取第一个sheet，当然也可以循环读取每个sheet
                        dataTable = new DataTable();
                        if (sheet != null)
                        {
                            int rowCount = sheet.LastRowNum;//总行数
                            if (rowCount > 0)
                            {
                                //IRow firstRow = sheet.GetRow(0);//第一行
                                //int cellCount = firstRow.LastCellNum;//列数
                                IRow firstRow = sheet.GetRow(index-1);//標題列
                                int cellCount = firstRow.LastCellNum;//列数

                                //构建datatable的列
                                if (isColumnName)
                                {
                                    //startRow = 1;//如果第一行是列名，则从第二行开始读取
                                    startRow = 2;//如果第二行是列名，则从第三行开始读取
                                    for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                                    {
                                        cell = firstRow.GetCell(i);
                                        if (cell != null)
                                        {
                                            if (cell.StringCellValue != null)
                                            {
                                                column = new DataColumn(cell.StringCellValue);
                                                dataTable.Columns.Add(column);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                                    {
                                        column = new DataColumn("column" + (i + 1));
                                        dataTable.Columns.Add(column);
                                    }
                                }

                                //填充行
                                for (int i = index; i <= rowCount; ++i)
                                {
                                    row = sheet.GetRow(i);
                                    if (row == null) continue;

                                    dataRow = dataTable.NewRow();
                                    for (int j = row.FirstCellNum; j < cellCount; ++j)
                                    {
                                        
                                        cell = row.GetCell(j);
                                        if (cell == null)
                                        {
                                            dataRow[j] = "";
                                        }
                                        else
                                        {
                                            //CellType(Unknown = -1,Numeric = 0,String = 1,Formula = 2,Blank = 3,Boolean = 4,Error = 5,)
                                            switch (cell.CellType)
                                            {
                                                case CellType.Blank:
                                                    dataRow[j] = "";
                                                    break;
                                                case CellType.Numeric:
                                                    short format = cell.CellStyle.DataFormat;
                                                    //对时间格式（2015.12.5、2015/12/5、2015-12-5等）的处理
                                                    if (format == 14 || format == 31 || format == 57 || format == 58)
                                                        dataRow[j] = cell.DateCellValue;
                                                    else
                                                        dataRow[j] = cell.NumericCellValue;
                                                    break;
                                                case CellType.String:
                                                    dataRow[j] = cell.StringCellValue;
                                                    break;
                                            }
                                        }
                                    }
                                    if(!string.IsNullOrEmpty(dataRow[0].ToString()))
                                    dataTable.Rows.Add(dataRow);
                                }
                            }
                        }
                    }
                }
                if (fs != null)
                {
                    fs.Close();
                }
                return dataTable;
            }
            catch (Exception)
            {
                if (fs != null)
                {
                    fs.Close();
                }
                dataTable.Clear();
                return dataTable;
            }
        }

        /// <summary>
        /// 導出Excel文件
        /// </summary>
        /// <param name="dateTable"></param>
        /// <param name="width">寬度以,分隔 "20,10,50"</param>
        /// <returns></returns>
        public static HSSFWorkbook ToExcel(DataTable dateTable,string width="0") 
        {
            
            try
            {
                HSSFWorkbook wb = new HSSFWorkbook();
                HSSFSheet sheet = (HSSFSheet)wb.CreateSheet("Sheel"); //创建工作表
                sheet.CreateFreezePane(0, 1); //冻结列头行
                HSSFRow row_Title = (HSSFRow)sheet.CreateRow(0); //创建列头行
                row_Title.HeightInPoints = 19.5F; //设置列头行高

                #region 设置列宽
                if (width == "0")//默認列寬
                {
                    for (int i = 0; i < dateTable.Columns.Count; i++)
                    {
                        sheet.SetColumnWidth(i, 20 * 256);
                    }
                }
                else
                {
                    string[] Uwidth = width.Split(',');
                    if (Uwidth.Count() == dateTable.Columns.Count)//設置的列寬
                    {
                        for (int i = 0; i < dateTable.Columns.Count; i++)
                        {
                            sheet.SetColumnWidth(i, int.Parse(Uwidth[i]) * 256);
                        }
                    }
                    else//默認列寬
                    {
                        for (int i = 0; i < dateTable.Columns.Count; i++)
                        {
                            if(i< Uwidth.Count())
                            sheet.SetColumnWidth(i, int.Parse(Uwidth[i]) * 256);
                            else
                             sheet.SetColumnWidth(i, int.Parse(Uwidth[Uwidth.Count()-1]) * 256);

                        }
                    }
                    
                }            
                #endregion

                #region 设置列头单元格样式                
                HSSFCellStyle cs_Title = (HSSFCellStyle)wb.CreateCellStyle(); //创建列头样式
                cs_Title.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center; //水平居中
                cs_Title.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center; //垂直居中
                HSSFFont cs_Title_Font = (HSSFFont)wb.CreateFont(); //创建字体
                //cs_Title_Font.IsBold = true; //字体加粗
                cs_Title_Font.FontHeightInPoints = 12; //字体大小
                cs_Title.SetFont(cs_Title_Font); //将字体绑定到样式
                #endregion

                #region 生成列头
                for (int i = 0; i < dateTable.Columns.Count; i++)
                {
                    HSSFCell cell_Title = (HSSFCell)row_Title.CreateCell(i); //创建单元格
                    cell_Title.CellStyle = cs_Title; //将样式绑定到单元格
                    cell_Title.SetCellValue(dateTable.Columns[i].ToString());
                  
                }
                #endregion


                int k = 0;
                foreach (DataRow dr in dateTable.Rows)
                {

                    #region 设置内容单元格样式
                    HSSFCellStyle cs_Content = (HSSFCellStyle)wb.CreateCellStyle(); //创建列头样式
                    cs_Content.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center; //水平居中
                    cs_Content.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center; //垂直居中
                    #endregion

                    #region 生成内容单元格
                    HSSFRow row_Content = (HSSFRow)sheet.CreateRow(k + 1); //创建行
                    k++;
                    row_Content.HeightInPoints = 16;
                    for (int j = 0; j < 13; j++)
                    {
                        HSSFCell cell_Conent = (HSSFCell)row_Content.CreateCell(j); //创建单元格
                        cell_Conent.CellStyle = cs_Content;
                        cell_Conent.SetCellValue(dr[j].ToString());
                       
                        
                    }
                    #endregion

                }
                return wb;
            }
            catch
            {

            }
            return null;


        }


       









    }
}