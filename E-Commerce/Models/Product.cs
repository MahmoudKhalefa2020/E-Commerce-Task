using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce.Dtos
{
    public class Product
    { 
    
        public int ID{ get; set; }
        public string Name { get; set; }
        public int Price { get; set; }

        [ForeignKey("SubCategory")]
        public int SubCategoryID { get; set; }
        public virtual SubCategory SubCategory { get; set; }


    }
}
