using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce.Dtos
{
    public class SubCategory
    {
        public int ID { get; set; }
        public string Name { get; set; }


        [ForeignKey("Categoty")]
        public int CategoryID {  get; set; }

        public virtual Category Categoty { get; set; }
        public virtual List<Product> Product { get; set; }
    }
}
