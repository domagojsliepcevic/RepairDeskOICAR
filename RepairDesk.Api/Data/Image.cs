namespace RepairDesk.Api.Data
{
    public class Image : BaseEntity
    {
        public string Path { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
