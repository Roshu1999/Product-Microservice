using System.ComponentModel.DataAnnotations;

namespace PM_MS.Models
{
    public class productList
    {
        [Key]
        public int productid { get; set; }
        public string details { get; set; }
        public string price { get; set; }
        public string name { get; set; }
        public string image { get; set; }
    }
}
