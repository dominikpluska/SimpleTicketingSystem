using DataAccess.Models;
using DataAccess.StaticData;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TicketsAPI.Dto
{
    public class TicketDto
    {
        public int? TicketId { get; set; }
        public string UserName { get; set; }
        public string TicketType { get; set; }
        public string CategoryName { get; set; } = "Other";
        public string Severity { get; set; }
        public string AssigmentGroup { get; set; } = StaticData.defaultAssingmentGroup;
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public DateTime LastModifiedDate { get; set; } = DateTime.Now;
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; } = "New";
    }
}
