using Entities.Notification;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entites
{
    [Table("TB_NEWS")]
    public class News : Notify
    {
        [Column("NTC_ID")]
        public int Id { get; set; }

        [Column("NTC_TITLE")]
        [MaxLength(255)]
        public string Title { get; set; }
        
        [Column("NTC_INFORMATION")]
        [MaxLength(255)]
        public string Information { get; set; }

        [Column("NTC_ACTIVE")]
        public bool Active { get; set; }

        [Column("NTC_DATEREGISTER")]
        [DataType(DataType.Date, ErrorMessage ="Data Invalida")]
        public DateTime DateRegister { get; set; }

        [Column("NTC_DATECHANGE")]
        [DataType(DataType.Date, ErrorMessage = "Data Invalida")]
        public DateTime DateChange { get; set; }

        [ForeignKey("AplicationUser")]
        [Column(Order = 1)]
        public string UserId {  get; set; }

        public virtual AplicationUser AplicationUser { get; set; }

    }
}
