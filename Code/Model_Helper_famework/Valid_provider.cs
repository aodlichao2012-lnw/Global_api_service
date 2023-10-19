using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace Model_Helper_famework
{
    public class Valid_provider
    {

        private Valid_provider()
        {

        }

        private static Valid_provider Instance = null;
        public static Valid_provider Instances
        {
            get
            {
                if (Instance == null)
                    Instance = new Valid_provider();
                return Instance;
            }
        }

        public string isvaild(object model, object values, ref string message)
        {

            foreach(var values_item in values.GetType().GetProperties())
            {
                
                if (!model.GetType().GetProperties().Any(x => x.PropertyType == values_item.PropertyType))
                {
                    return "type "+values_item.Name+" ไม่ตรงกัน ไม่สามารถทำงานต่อได้";
                }
                else
                    message = "ok";

            }
            return message;
        }
    }
}
