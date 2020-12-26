using System;
using System.IO;
using System.Data;
using System.Reflection;
using System.Diagnostics;
using System.Collections;
using System.Data.OleDb;
using Aspose.Cells;
using System.Collections.Generic;

namespace com.Origin.CommonLibrary.Framework
{
    public class OfficeHelper
    {
        Aspose.Cells.Workbook A_workbook = new Workbook();//工作簿

        #region 已作废方法
        /// <summary>
        /// 导出Excel，适合于无Excel模板，有Title与中文列名
        /// </summary>
        /// <param name="dTable">数据表</param>
        /// <param name="sheetIndex">写入工作薄索引</param>
        /// <param name="titleText">Excel列表Title</param>
        /// <param name="cellNameArray">数据列名列表</param>
        /// <param name="cellNameSplitstr">数据列名列表分割符</param>
        /// <param name="cellChineseNameArray">中文列名列表(顺序与数据列名列表一样)</param>
        /// <param name="cellChineseNameSplit">中文列名列表分割符</param>
        public void ListToExcel<T>(System.Collections.Generic.List<T> dTable, int sheetIndex, string titleText, string cellNameArray, char cellNameSplitstr, string cellChineseNameArray, char cellChineseNameSplit)
        {
            try
            {
                //List行数
                int rowCount = dTable.Count;
                //List列数
                int colCount = cellNameArray.Split(cellNameSplitstr).Length;

                //英文列名（键）
                string[] arrayCellName = cellNameArray.Split(cellNameSplitstr);
                //中文列名（显示名）
                string[] arrayChineseCellName = cellChineseNameArray.Split(cellChineseNameSplit);

                DataTable dt = new DataTable();
                for (int i = 0; i < arrayChineseCellName.Length; i++)
                {
                    dt.Columns.Add(arrayChineseCellName[i]);
                }
                //组装填充数据
                DataTable dt1 = new DataTable();//英文键
                for (int i = 0; i < arrayCellName.Length; i++)
                {
                    dt1.Columns.Add(arrayCellName[i]);
                }
                for (int j = 0; j < rowCount; j++)
                {
                    DataRow dr = dt1.NewRow();
                    int flag = 1;
                    for (int k = 0; k < colCount; k++)
                    {
                        T t = dTable[j];
                        Type type = t.GetType(); //获取类型
                        System.Reflection.PropertyInfo propertyInfo = type.GetProperty(arrayCellName[k]); //获取指定名称的属性
                        var value = propertyInfo.GetValue(t, null); //获取属性值

                        dr[arrayCellName[k]] = value;
                        if (flag == colCount)
                            dt1.Rows.Add(dr);
                        flag++;
                    }
                }

                //Workbook A_workbook = new Workbook(); //工作簿 
                Worksheet sheet = A_workbook.Worksheets[0]; //工作表 
                Cells cells = sheet.Cells;//单元格 

                //标题样式
                Style style = A_workbook.Styles[A_workbook.Styles.Add()];//新增样式 
                style.HorizontalAlignment = TextAlignmentType.Center;//文字居中 
                style.Font.Name = "宋体";//文字字体 
                style.Font.Size = 10;//文字大小 
                style.Font.IsBold = true;//粗体 
                style.IsTextWrapped = true;//单元格内容自动换行 

                //生成列名行 
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    cells[0, i].PutValue(dt.Columns[i].ColumnName);
                    cells[0, i].SetStyle(style);
                    cells.SetRowHeight(0, 25);
                }

                //生成数据行 
                for (int i = 1; i <= dt1.Rows.Count; i++)
                {
                    for (int k = 0; k < dt1.Columns.Count; k++)
                    {
                        cells[i, k].PutValue(dt1.Rows[i - 1][k].ToString());
                    }
                }
            }
            catch (Exception exp)
            {
                this.Dispose();
                throw exp;
            }
        }
        #endregion

        /// <summary>
        /// 列类
        /// </summary>
        public class Column
        {
            /// <summary>
            /// 列名称
            /// </summary>
            public string columnName { get; set; }
            /// <summary>
            /// 显示名称
            /// </summary>
            public string displayName { get; set; }
            /// <summary>
            /// 渲染方法,对于特殊的单元格,如需特定代码处理文字,重写这个代理
            /// </summary>
            /// <param name="value">单元格的值</param>
            /// <returns>想要输出的值</returns>
            public delegate string CellRenderDelegate(string value);
            /// <summary>
            /// 渲染方法,对于特殊的单元格,如需特定代码处理文字,重写这个代理
            /// </summary>
            /// <param name="value">单元格的值</param>
            /// <returns>想要输出的值</returns>
            public CellRenderDelegate CellRender { get; set; }
            /// <summary>
            /// 渲染值,与CellRender冲突时,优先使用此属性,优先按此属性设置的显示值显示,不在范围中,则显示原值
            /// </summary>
            public List<KeyValuePair<string, string>> RenderValues { get; set; }
        }

        /// <summary>
        /// 列数组
        /// </summary>
        public List<Column> columns { get; set; }

        /// <summary>
        /// 根据列名返回列名对象所在的索引
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public int getColumnIndex(string columnName)
        {
            if (columns == null)
                return -1;
            for (int i = 0; i < this.columns.Count; i++)
            {
                if (this.columns[i].columnName == columnName)
                {
                    return i;
                }
            }
            return -1;
        }

        public OfficeHelper()
        {
            this.columns = new List<Column>();
        }

        /// <summary>
        /// 导出Excel，适合于无Excel模板，有Title与中文列名
        /// </summary>
        /// <param name="dTable">数据表</param>
        /// <param name="sheetIndex">写入工作薄索引</param>
        /// <param name="titleText">Excel列表Title</param>
        public void ListToExcel<T>(System.Collections.Generic.List<T> dTable, int sheetIndex, string titleText)
        {
            try
            {
                //List行数
                int rowCount = dTable.Count;
                //List列数
                int colCount = this.columns.Count;

                //Workbook A_workbook = new Workbook(); //工作簿 
                while (A_workbook.Worksheets.Count < sheetIndex + 1)
                {
                    //说明需要增加工作表
                    A_workbook.Worksheets.Add();
                }
                Worksheet sheet = A_workbook.Worksheets[sheetIndex]; //工作表 
                sheet.Name = titleText;
                Cells cells = sheet.Cells;//单元格 

                //标题样式
                Style style = A_workbook.Styles[A_workbook.Styles.Add()];//新增样式 
                style.HorizontalAlignment = TextAlignmentType.Center;//文字居中 
                style.Font.Name = "宋体";//文字字体 
                style.Font.Size = 10;//文字大小 
                style.Font.IsBold = true;//粗体 
                style.IsTextWrapped = true;//单元格内容自动换行 

                //生成列名行 
                for (int i = 0; i < colCount; i++)
                {
                    cells[0, i].PutValue(this.columns[i].displayName);
                    cells[0, i].SetStyle(style);
                }
                cells.SetRowHeight(0, 25);

                //数据样式
                Style style1 = A_workbook.Styles[A_workbook.Styles.Add()];//新增样式 
                style1.HorizontalAlignment = TextAlignmentType.Left;//文字居中 
                style1.Font.Name = "宋体";//文字字体 
                style1.Font.Size = 10;//文字大小 

                //生成数据行 
                for (int i = 0; i < rowCount; i++)
                {
                    for (int k = 0; k < colCount; k++)
                    {
                        T t = dTable[i];
                        Type type = t.GetType(); //获取类型
                        System.Reflection.PropertyInfo propertyInfo = type.GetProperty(this.columns[k].columnName); //获取指定名称的属性
                        var value = propertyInfo.GetValue(t, null); //获取属性值
                        cells[i+1, k].PutValue(Convert(this.columns[k], value == null ? "" : value.ToString()));
                        cells[i+1, k].SetStyle(style1);
                    }
                }
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        /// <summary>
        /// 根据Cell的设置将单元格的值转换成想要显示的值
        /// </summary>
        /// <param name="c"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private string Convert(Column c, string value)
        {
            if (c.RenderValues != null)
            {
                for (int i = 0; i < c.RenderValues.Count; i++)
                {
                    if (c.RenderValues[i].Key == value)
                    {
                        value = c.RenderValues[i].Value;
                        break;
                    }
                }
            }
            else if (c.CellRender != null)
            {
                value = c.CellRender.Invoke(value);
            }
            return value;
        }

        /// <summary>
        /// 另存文件
        /// </summary>
        /// <param name="fileName">文件名</param>
        public void SaveFile(string fileName)
        {
            try
            {
                A_workbook.Save(fileName);
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                this.Dispose();
            }
        }

        private void Dispose()
        {
            A_workbook = null;

            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }
}
