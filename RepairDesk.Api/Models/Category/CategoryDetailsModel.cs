
namespace RepairDesk.Api.Models.Category
{
    public class CategoryDetailsModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
		public bool IsSpecial { get; set; }
        public string ImagePath { get; set; }
    }
}
