namespace E_Commerce.Dtos
{
    public class Category
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public virtual List<SubCategory> SubCategories { get; set; }
    }
}
