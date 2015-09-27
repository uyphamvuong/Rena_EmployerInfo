using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace EmployerInfoUpdate
{
    class FuncHelp
    {
        public static string GetSource(string source_url, bool delete_breakline = true)
        {
            string s_ = "";
            using (var webpage = new WebClient())
            {
                //webpage.Headers[HttpRequestHeader.UserAgent] = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/535.2 (KHTML, like Gecko) Chrome/15.0.874.121 Safari/535.2";
                webpage.Encoding = System.Text.Encoding.UTF8;
                try
                {
                    s_ = webpage.DownloadString(source_url);
                    if (delete_breakline) { s_ = s_.Replace("\r","").Replace("\n", "").Replace("\t", "").Replace("\"", "'").Replace("  ", " ").Replace("  ", " ").Replace("  ", " "); }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Quá trình lấy dữ liệu gặp lỗi!!!\n\n" + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }                
            }
            return s_;
        }

        public static string GetSourceWithCookie(string source_url,ref CookieContainer cookie)
        {
            string data = "";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(source_url);
            request.CookieContainer = cookie;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                Stream receiveStream = response.GetResponseStream();
                StreamReader readStream = null;

                if (response.CharacterSet == null)
                {
                    readStream = new StreamReader(receiveStream);
                }
                else
                {
                    readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
                }
                data = readStream.ReadToEnd();
                data = data.Replace("\r\n", "").Replace("\n", "").Replace("\t", "").Replace("\"", "'").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace("> <", "><");

                response.Close();
                readStream.Close();
            }
            return data;
        }

        public static string CutFrom(string strCut, string strFrom)
        {
            if (strCut.IndexOf(strFrom) >= 0)
                return strCut.Substring(strCut.IndexOf(strFrom) + strFrom.Length, strCut.Length - strCut.IndexOf(strFrom) - strFrom.Length);
            return "";
        }

        public static string CutTo(string strCut, string strTo)
        {
            if (strCut.IndexOf(strTo) >= 0)
                return strCut.Substring(0, strCut.IndexOf(strTo));
            return "";
        }

        public static string CutFromTo(string strCut, string strFrom, string strTo)
        {
            strCut = CutFrom(strCut, strFrom);
            strCut = CutTo(strCut, strTo);
            return strCut;
        }

        public static void ExportExcel(DataTable tbl, string filename, Dictionary<int,int> colwidth = null)
        {
            FileInfo newFile = new FileInfo(filename);
            if (newFile.Exists)    {newFile.Delete();}

            using (ExcelPackage pck = new ExcelPackage())
            {
                //Create the worksheet
                ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Employer Info");

                //Load the datatable into the sheet, starting from cell A1. Print the column names on row 1
                ws.Cells["A1"].LoadFromDataTable(tbl, true);

                //Format the header for column 1-3
                using (ExcelRange rng = ws.Cells[1,1,1,tbl.Columns.Count])
                {
                    rng.Style.Font.Bold = true;
                    rng.Style.Fill.PatternType = ExcelFillStyle.Solid;                      //Set Pattern for the background to Solid
                    rng.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(79, 129, 189));  //Set color to dark blue
                    rng.Style.Font.Color.SetColor(Color.White);
                    rng.AutoFilter = true;
                    rng.AutoFitColumns();                    
                }

                if (colwidth != null)
                {
                    foreach (KeyValuePair<int, int> col in colwidth)
                    {
                        ExcelColumn ec = ws.Column(col.Key);
                        ec.Width = col.Value;
                    }
                }                

                //Example how to Format Column 1 as numeric 
                //using (ExcelRange col = ws.Cells[2, 1, 2 + tbl.Rows.Count, 1])
                //{
                //    col.Style.Numberformat.Format = "#,##0.00";
                //    col.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                //}
                pck.SaveAs(newFile);
                
            }

        }
    }
}
        
