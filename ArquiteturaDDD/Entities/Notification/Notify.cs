using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Notification
{
    public class Notify
    {
        public Notify()
        {
            Notification = new List<Notify>();
        }

        [NotMapped]
        public string PropertyName { get; set; }

        [NotMapped]
        public string Message { get; set; }

        [NotMapped]
        public List<Notify> Notification { get; set; }

        /// <summary>
        ///  Validação para verificar se a propriedade veio vazia.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="propertyName"></param>
        /// <returns>true or false</returns>
        public bool ValidateStringProperty(string value, string propertyName)
        {
            if (string.IsNullOrWhiteSpace(value) || string.IsNullOrWhiteSpace(propertyName))
            {
                Notification.Add(new Notify { Message = "Campo Obrigatório", PropertyName = propertyName }); return false;
            }

            return true;
        }
        /// <summary>
        /// Validação para verificar se a propriedade veio zerada ou vazia. 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="propertyName"></param>
        /// <returns>true or false</returns>
        public bool ValidateDecimaProperty(decimal value, string propertyName)
        {
            if (value < 1 || string.IsNullOrWhiteSpace(propertyName))
            {
                Notification.Add(new Notify { Message = "Campo Obrigatório", PropertyName = propertyName }); return false;
            }

            return true;
        }
    }
}
