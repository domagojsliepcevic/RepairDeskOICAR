using System;

namespace RepairDesk.Api.Models.Repair
{
    public class RepairListModel
    {
        public int Id { get; set; }
        public string RepairNumber { get; set; }
        public DateTime RequestDate { get; set; }
        public string Description { get; set; }
        public DateTime? FinishDate { get; set; }
        public int UserId { get; set; }
        public string User { get; set; }
        public int StatusId { get; set; }
        public string Status { get; set; }

    }
}
