using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model_HelperCore;
using Newtonsoft.Json;
using System.Data;
using System.Data.Common;
using System.Text.Json.Serialization;
using Webapi_Global.Class_pool;
using static Webapi_Global.Class_pool.function_sql_Oracle;

namespace Webapi_Global.Controllers
{
    [Route("Service")]
    [ApiController]
    public class Oracle_Class_Controller : ControllerBase
    {
        function_sql_Oracle function_;
        Extention Extention_;
        DataTable dt;
        public Oracle_Class_Controller(function_sql_Oracle function_Sql_ , Extention extention)
        {
            function_ = function_Sql_;
            Extention_ = extention;
        }
        [HttpPost]
        [Route("UpdateCNFG_Agent_Info")]
        public string UpdateCNFG_Agent_Info(string status, string Agen = "", string IP = "")
        {
           
            string strUpdate;
            strUpdate = "";
            strUpdate = "UPDATE CNFG_AGENT_INFO  ";
            strUpdate += " SET  TERMINAL_IP   = '" + IP + "' ,";
            strUpdate += " STATUS_ID = " + status + " ,";
            strUpdate += "CALL_COUNT = 0,";
            strUpdate += "LOGON_EXT = " + status + ",";
            strUpdate += " LOGIN_TIME   = sysdate ";
            strUpdate += " WHERE AGENT_ID = '" + Agen+ "'";

            try
            {
                {
                  function_.Function_Excute_Update_And_Insert_And_Delete (strUpdate);
                    return "200";
                }
            }
            catch (Exception ex)
            {
                return "ระบบมีปัญหา กรุณาติดต่อ Admin ค่ะ" + ex.Message + "ผลการตรวจสอบ";
            }


        }
        [HttpGet]
        [Route("GetJigSaw")]
        public string GetJigSaw()
        {
           return Extention_.Jigsaw();
        }  
        
        [HttpGet]
        [Route("GetPredictAgenT")]
        public string GetPreditAgenT(string txtUsername ,string txtPassword)
        {
            dt = new DataTable();
            function_.Funtion_Select_Sql("SELECT * FROM PREDIC_AGENTS" +
                "WHERE (LOGIN = "+ txtUsername +" )"               
           +" and (PASSWORD= "+ txtPassword + " ) AND ROWNUM = 1 ", null, null, ref dt);
            string json = JsonConvert.SerializeObject(dt);
            return json;
        }     
        
        [HttpGet]
        [Route("Get_All_DataFromTable_Where")]
        public string Get_All_DataFromTable_Where(string Table ,string column , string values)
        {
            string sql = string.Empty;
            sql += $@"SELECT * FROM {Table}  ";
            int i = 0;
            if (column != null)
            {
                sql += $@" WHERE ";
                foreach (string column_item in column.Split(','))
                {
                    if (i < column.Split(',').Length - 1)
                    {
                        sql += $@"{column} = {values[i]} AND ";

                    }
                    else
                    {
                        sql += $@"{column} = {values[i]} ";
                    }

                    i++;
                }
            }
            dt = new DataTable();
            function_.Funtion_Select_Sql(sql, null, null, ref dt);
            string json = JsonConvert.SerializeObject(dt);
            return json;
        } 
        
        
        [HttpGet]
        [Route("Get_column_DataFromTable_Where")]
        public string Get_column_DataFromTable_Where(string Table ,string column_select,string column_where , string values_where)
        {
            string sql = string.Empty;
            sql += $@"SELECT ";
            int i = 0;
            foreach(string item in column_select.Split(','))
            {
                if (i < column_select.Split(',').Length - 1)
                {
                    sql += $@"{item} , ";

                }
                else
                {
                    sql += $@"{item} ";
                }

                i++;
            }
            sql += $@" FROM {Table} ";

            if(column_where != null)
            {
                int e = 0;
                sql += $@" WHERE ";
                foreach (string column_item in column_where.Split(','))
                {
                    if(e < column_where.Split(',').Length - 1)
                    {
                        sql += $@"{column_where} = {values_where[e]} AND ";
                    
                    }
                    else
                    {
                        sql += $@"{column_where} = {values_where[e]} ";
                    }
            
                    e++;
                }
            }
          
            dt = new DataTable();
            function_.Funtion_Select_Sql(sql, null, null, ref dt);
            string json = JsonConvert.SerializeObject(dt);
            return json;
        }
    }
}