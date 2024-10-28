using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class Log
    {
        [Key]
        public int EntryId { get; set; }
        public string ServiceName { get; set; }
        public string LogType { get; set; } = "Info";
        public string UserName { get; set; }
        public DateTime LogTime { get; set; } = DateTime.Now;
        public string Message { get; set; }
    }
}
