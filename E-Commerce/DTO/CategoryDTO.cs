namespace E_Commerce.DTO
{
    public class CategoryDTO
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public List<SubCategoryDTO>SubCategoreisName { get; set; } = new List<SubCategoryDTO>();
    }
}
