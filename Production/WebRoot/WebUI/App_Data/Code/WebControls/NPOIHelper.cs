using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Web;
using NPOI.HSSF.UserModel;
using NPOI.HPSF;
using NPOI.SS.UserModel;
using NPOI.SS.Util;





namespace HP.ElementsCPS.Apps.WebUI.WebControls
{
    public class NPOIHelper
    {
        /// <summary>
        ///   Export to Excel by MemoryStream
        /// </summary>
        /// <param name="dtSource"></param>
        /// <param name="strHeaderText"></param>
        public static MemoryStream Export(DataTable dtSource, string strHeaderText, List<int> nums)
        {
                HSSFWorkbook workbook = new HSSFWorkbook();
                HSSFSheet sheet = (HSSFSheet)workbook.CreateSheet();
                var num1 = new List<int>(nums);
                foreach (int num in num1)
                {
                    sheet.SetColumnHidden(num, true);
                }

                #region Property
                {
                    DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
                    dsi.Company = "NPOI";
                    workbook.DocumentSummaryInformation = dsi;

                    SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
                    si.Author = "Author";
                    si.ApplicationName = "ApplicationName";
                    si.LastAuthor = "LastAuthor";
                    si.Comments = "Comments";
                    si.Title = "Title";
                    si.Subject = "Subject";
                    si.CreateDateTime = DateTime.Now;
                    workbook.SummaryInformation = si;
                }
                #endregion

                HSSFCellStyle dateStyle = (HSSFCellStyle)workbook.CreateCellStyle();
                HSSFDataFormat format = (HSSFDataFormat)workbook.CreateDataFormat();
                dateStyle.DataFormat = format.GetFormat("yyyy-mm-dd");

                #region get colnums width

                int[] arrColWidth = new int[dtSource.Columns.Count];
                foreach (DataColumn item in dtSource.Columns)
                {
                    arrColWidth[item.Ordinal] = Encoding.GetEncoding(936).GetBytes(item.ColumnName.ToString()).Length;
                }
                for (int i = 0; i < dtSource.Rows.Count; i++)
                {
                    for (int j = 0; j < dtSource.Columns.Count; j++)
                    {
                        int intTemp = Encoding.GetEncoding(936).GetBytes(dtSource.Rows[i][j].ToString()).Length;
                        if (intTemp > arrColWidth[j])
                        {
                            arrColWidth[j] = intTemp;
                        }
                    }
                }

                int rowIndex = 0;
                #endregion

                foreach (DataRow row in dtSource.Rows)
                {
                    #region new sheet ,fill data ,set style
                    int sheetNum = (int)Math.Ceiling((decimal)rowIndex / 65535);
                    if (rowIndex == 65535 * (sheetNum))
                    {
                        if (rowIndex != 0)
                        {
                            sheet = (HSSFSheet)workbook.CreateSheet();
                            var num2 = new List<int>(nums);
                            foreach (int num in num2)
                            {
                                sheet.SetColumnHidden(num, true);
                            }
                        }

                        #region headerRow and style
                        {
                            HSSFRow headerRow = (HSSFRow)sheet.CreateRow(0);
                            headerRow.HeightInPoints = 25;
                            headerRow.CreateCell(0).SetCellValue(strHeaderText);

                            HSSFCellStyle headStyle = (HSSFCellStyle)workbook.CreateCellStyle();
                            headStyle.Alignment = HorizontalAlignment.LEFT;
                            HSSFFont font = (HSSFFont)workbook.CreateFont();
                            font.FontHeightInPoints = 20;
                            font.Boldweight = 700;
                            headStyle.SetFont(font);
                            headerRow.GetCell(0).CellStyle = headStyle;
                            sheet.AddMergedRegion(new Region(0, 0, 0, dtSource.Columns.Count - 1));
                           //headerRow.Dispose();
                        }
                        #endregion

                        #region colnumname and style
                        {
                            HSSFRow colnumname = (HSSFRow)sheet.CreateRow(1);
                            HSSFCellStyle headStyle = (HSSFCellStyle)workbook.CreateCellStyle();
                            headStyle.Alignment = HorizontalAlignment.CENTER;
                            HSSFFont font = (HSSFFont)workbook.CreateFont();
                            font.FontHeightInPoints = 10;
                            font.Boldweight = 700;
                            headStyle.SetFont(font);

                            foreach (DataColumn column in dtSource.Columns)
                            {
                                colnumname.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);
                                colnumname.GetCell(column.Ordinal).CellStyle = headStyle;

                                //set colnums width

                                sheet.SetColumnWidth(column.Ordinal, (arrColWidth[column.Ordinal]) * 256 <= 255 * 256 ? (arrColWidth[column.Ordinal]) * 256 : 255 * 256);
                            }
                            //colnumname.Dispose();

                        }
                        #endregion

                        rowIndex = 2;
                    }
                    #endregion

                    #region fill data
                    HSSFRow dataRow = (HSSFRow)sheet.CreateRow(rowIndex);

                    foreach (DataColumn column in dtSource.Columns)
                    {
                        HSSFCell newCell = (HSSFCell)dataRow.CreateCell(column.Ordinal);

                        string drValue = row[column].ToString();

                        switch (column.DataType.ToString())
                        {
                            case "System.String":
                                newCell.SetCellValue(drValue);
                                break;
                            case "System.DateTime":
                                DateTime dateV;
                                DateTime.TryParse(drValue, out dateV);
                                newCell.SetCellValue(dateV);

                                newCell.CellStyle = dateStyle;
                                break;
                            case "System.Boolean":
                                bool boolV = false;
                                bool.TryParse(drValue, out boolV);
                                newCell.SetCellValue(boolV);
                                break;
                            case "System.Int16":
                            case "System.Int32":
                            case "System.Int64":
                            case "System.Byte":
                                int intV = 0;
                                int.TryParse(drValue, out intV);
                                newCell.SetCellValue(intV);
                                break;
                            case "System.Decimal":
                            case "System.Double":
                                double doubV = 0;
                                double.TryParse(drValue, out doubV);
                                newCell.SetCellValue(doubV);
                                break;
                            case "System.DBNull":
                                newCell.SetCellValue("");
                                break;
                            default:
                                newCell.SetCellValue("");
                                break;
                        }

                    }
                    #endregion

                    rowIndex++;
                }
                using (MemoryStream ms = new MemoryStream())
                {
                    if (HttpContext.Current.Response.IsClientConnected)
                    {
                        workbook.Write(ms);
                        ms.Flush();
                        ms.Position = 0;
                        ms.Dispose();
                    }
                    else
                    {
                        HttpContext.Current.Response.End();
                    } 
                    workbook.Dispose();
                    return ms;
                } 
        }

        /// <summary>
        /// export by web
        /// </summary>
        /// <param name="dtSource"></param>
        /// <param name="strHeaderText"></param>
        /// <param name="strFileName"></param>
        public static void ExportByWeb(DataTable dtSource, string strHeaderText, string strFileName, List<int> nums)
        {
            HttpContext curContext = HttpContext.Current;
            curContext.Response.BufferOutput = true;
            curContext.Response.ContentType = "application/vnd.ms-excel";
            curContext.Response.AppendHeader("Content-Disposition",
                "attachment;filename=" + strFileName);
            curContext.Response.Clear();
            curContext.Response.BinaryWrite(Export(dtSource, strHeaderText, nums).GetBuffer());
            curContext.ApplicationInstance.CompleteRequest();
            curContext.Response.End();
        }
    }
}
