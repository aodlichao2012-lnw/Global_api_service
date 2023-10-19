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
        string[] propertyName = new string[10000];
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
            int count = 0;
            foreach (var propertyInfo in model.GetType().GetProperties())
            {
                propertyName[count] = propertyInfo.Name;
                count++;
            }
            List<ValidationContext> validationContextsList = new List<ValidationContext>();
            foreach(var  propertNameCount in propertyName)
            {
              ValidationContext ValidationContext =  new ValidationContext(model, null, null)
                {
                    MemberName = propertNameCount
                };
                validationContextsList.Add(ValidationContext);
            }
            
            foreach(var validationContext_item in validationContextsList)
            {
                var results = new List<ValidationResult>();
                var isValid = Validator.TryValidateProperty(values, validationContext_item, results);
                if (!isValid)
                {
                    foreach (ValidationResult result in results)
                        message = result.ErrorMessage.ToString();
                }
                else
                    return "ok";

            }
          
            return message;
        }
    }
}
