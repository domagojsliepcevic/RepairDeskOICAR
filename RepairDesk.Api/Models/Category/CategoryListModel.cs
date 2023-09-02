namespace RepairDesk.Api.Models.Category
{

    public class CategoryListModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
		public bool IsSpecial { get; set; }
        public string ImagePath { get; set; }
    }
}
