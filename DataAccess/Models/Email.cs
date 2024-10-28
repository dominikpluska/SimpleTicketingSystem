using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class Email
    {
        public string RecipientMailAddress { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string[] AttachmentPaths { get; set; } = null;
    }
}
