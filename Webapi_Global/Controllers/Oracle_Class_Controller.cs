using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model_HelperCore;
using Newtonsoft.Json;
using System.Data;
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
            sql += $@"SELECT * FROM {Table} WHERE ";
            int i = 0;
            foreach(string column_item in column.Split(','))
            {
                sql += $@"{column} = {values[i]} AND";
                i++;
            }
            dt = new DataTable();
            function_.Funtion_Select_Sql(sql, null, null, ref dt);
            string json = JsonConvert.SerializeObject(dt);
            return json;
        } 
        
        
        [HttpGet]
        [Route("Get_column_DataFromTable_Where")]
        public string Get_column_DataFromTable_Where(string Table ,string column_select,string column , string values)
        {
            string sql = string.Empty;
            sql += $@"SELECT ";
            int i = 0;
            foreach(string item in column_select.Split(','))
            {
                sql += @$"{item} ,";
            }
            sql += $@" FROM {Table}";

            if(column != null)
            {
                sql += $@"WHERE";
                foreach (string column_item in column.Split(','))
                {
                    sql += $@"{column} = {values[i]} AND";
                    i++;
                }
            }
          
            dt = new DataTable();
            function_.Funtion_Select_Sql(sql, null, null, ref dt);
            string json = JsonConvert.SerializeObject(dt);
            return json;
        }
    }
}
