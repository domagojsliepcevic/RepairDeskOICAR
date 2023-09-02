namespace RepairDesk.Api.Models.Notification
{
    public class NotificationDetailsModel
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public int TypeId { get; set; }
        public string TypeName { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
    }

}
