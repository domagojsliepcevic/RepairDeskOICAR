namespace RepairDesk.Api.Models
{
    public class PagedResult<T>
    {
        public IEnumerable<T> Items { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
}
