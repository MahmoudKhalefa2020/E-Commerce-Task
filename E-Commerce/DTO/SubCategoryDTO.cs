namespace E_Commerce.DTO
{
    public class SubCategoryDTO
    {
        public int SubCategoryID { get; set; }
        public string SubCategoryName { get; set; }
        public List<ProductDTO> SubCategoryProducts { get; set; }=new List<ProductDTO>();
    }
}
