
using Microsoft.SqlServer.Server;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;


namespace Model_Helper_famework
{
    public class Extention
    {
       //Application excel;
       //Workbook excelworkBook;
       //Worksheet excelSheet;
       //Range excelCellrange;
        public string Jigsaw()
        {
            Random random = new Random();
            string path = string.Empty;
            int number_from_random = random.Next(0, 10);
            using (Bitmap image = new Bitmap($@"D:\Api\Image\{number_from_random}.jpg"))
            {
                int piecesWight = image.Width / 2;
                int piecesHeight = image.Width / 2;
                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        System.Drawing.Rectangle piecseRetangle = new System.Drawing.Rectangle(i * piecesWight, j * piecesHeight, piecesWight, piecesHeight);
                        using (Bitmap picese = new Bitmap(piecesWight, piecesWight))
                        {
                            using (Graphics g = Graphics.FromImage(picese))
                            {
                                g.DrawImage(image, new System.Drawing.Rectangle(0, 0, piecesWight, piecesWight), piecseRetangle, GraphicsUnit.Pixel);
                            }
                            path += "http://172.21.140.104:8084/imageOutput/piece_" + number_from_random.ToString() + "_i" + i + "_j" + j + ".jpg" + ";";
                            picese.Save("D:\\Api\\imageOutput\\piece_" + number_from_random.ToString() + "_i" + i + "_j" + j + ".jpg");
                        }
                    }
                }
            }
            return path;
        }
        public void Log(string message)
        {
            try
            {
                string path = HttpContext.Current.Server.MapPath("~\\bin") + "\\InformationLog_Sql_And_Event\\";
                if (!Directory.Exists(path))

                {
                    Directory.CreateDirectory(path);
                }
                using (StreamWriter steam = new StreamWriter(path + DateTime.Now.ToString("yyyyMMdd") + ".txt", true))
                {

                    steam.WriteLine(": " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss") + ": = " + message);
                }
            }
            catch
            {

            }


        }

        public void LogSql(string message)
        {
            try
            {
                string path = HttpContext.Current.Server.MapPath("~\\bin") + "\\Information_sql\\";
                if (!Directory.Exists(path))

                {
                    Directory.CreateDirectory(path);
                }
                using (StreamWriter steam = new StreamWriter(path + DateTime.Now.ToString("yyyyMMdd") + ".txt", true))
                {

                    steam.WriteLine(": " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss") + ": = " + message);
                }
            }
            catch
            {

            }


        }

        public void Log_Save_information(string Cookie_AgenId, string Datetimes)
        {
            try
            {

                string path = HttpContext.Current.Server.MapPath("~\\bin") + "\\Log_Save_AgenID\\" + DateTime.Now.ToString("yyyyMMdd") + "\\";
                if (!Directory.Exists(path))

                {
                    Directory.CreateDirectory(path);
                }
                using (StreamWriter steam = new StreamWriter(path + "_" + Cookie_AgenId + "_" + Datetimes + ".txt", true))
                {

                    steam.WriteLine(DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss") + "=" + Cookie_AgenId + ",");
                }
            }
            catch
            {

            }


        }

        public string Log_Get_information(string Cookie_AgenId, string Datetimes)
        {
            string messages = string.Empty;
            try
            {

                string path = HttpContext.Current.Server.MapPath("~\\bin") + "\\Log_Save_AgenID\\" + DateTime.Now.ToString("yyyyMMdd") + "\\";

                using (StreamReader steam = new StreamReader(path + "_" + Cookie_AgenId + "_" + Datetimes + ".txt", true))
                {

                    messages = steam.ReadToEnd();
                }
                return messages;
            }
            catch (Exception ex)
            {
                LogSql(ex.Message.ToString());
                return string.Empty;
            }
        }

        public string Log_Get_information_SaveData_And_Edit(string result = "", string type = "", string Cookie_AgenId = "", string Datetimes = "", ViewModel Model = null)

        {
            string messages = string.Empty;
            try
            {

                string path = HttpContext.Current.Server.MapPath("~\\bin") + "\\SaveData_And_Edit\\" + DateTime.Now.ToString("yyyyMMdd") + "\\";
                if (!Directory.Exists(path))

                {
                    Directory.CreateDirectory(path);
                }
                using (StreamWriter steam = new StreamWriter(path + "_" + Cookie_AgenId + "_" + Datetimes + ".txt", true))
                {
                    steam.WriteLine(DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss") + " , Result = " + result + " , Type = " + type + " ," + "=" + $@" txtTel_No :{Model.txtTel_No} , txtName : {Model.txtName} , txtSName : {Model.txtSName} , cboDate : {Model.cboDate} , cboMouth : {Model.cboMouth} , txtYear : {Model.txtYear} , cboSex : {Model.cboSex} , cboStatus : {Model.cboStatus.ToString().Replace(" ", "")} , cbocity :{Model.cbocity}");

                }


                return messages;
            }
            catch (Exception ex)
            {
                LogSql(ex.Message.ToString());
                return string.Empty;
            }


        }

        public int Log_Get_information_lenght(string Cookie_AgenId, string Datetimes)
        {
            int count = 0;
            try
            {

                string path = HttpContext.Current.Server.MapPath("~\\bin") + "\\Log_Save_AgenID\\" + DateTime.Now.ToString("yyyyMMdd") + "\\";

                using (StreamReader steam = new StreamReader(path + "_" + Cookie_AgenId + "_" + Datetimes + ".txt", true))
                {

                    count = steam.ReadToEnd().Split(',').Length - 1;
                }
                return count;

            }
            catch (Exception ex)
            {
                LogSql(ex.Message.ToString());
                return count = 0;
            }


        }

        public void ExportExcel(System.Data.DataTable dt , string sheetname ="Sheet1" ,string path = "D:\\Excel\\")
        {
            try
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                using (ExcelPackage pck = new ExcelPackage(path + DateTime.Now.ToString("yyyy_MM-dd_mm_ss") + ".xlsx"))
                {
                    ExcelWorksheet ws = pck.Workbook.Worksheets.Add(sheetname);
                    ws.Cells["A1"].LoadFromDataTable(dt, true);
                    pck.Save();
                }
            }
            catch(Exception ex)
            {
              Error_providers.Instances.CustomsExceptions(ex);
            }
         
        }
         
        public System.Data.DataTable ImportExcel(string pathfile , string sheetName = "Sheet1", bool hasHeader = true)
        {
            var dt = new System.Data.DataTable();
            try
            {

                var fi = new FileInfo(pathfile);
                // Check if the file exists
                if (!fi.Exists)
                    throw new Exception("File " + pathfile + " Does Not Exists");

                ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                var xlPackage = new ExcelPackage(fi);
                // get the first worksheet in the workbook
                var worksheet = xlPackage.Workbook.Worksheets[sheetName];

                dt = worksheet.Cells[1, 1, worksheet.Dimension.End.Row, worksheet.Dimension.End.Column].ToDataTable(c =>
                {
                    c.FirstRowIsColumnNames = true;
                });

                return dt;
            }
            catch(Exception ex)
            {
                Error_providers.Instances.CustomsExceptions(ex);
                return dt;
            }
           
        }


    }
}

