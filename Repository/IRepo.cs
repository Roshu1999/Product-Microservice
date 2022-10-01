using PM_MS.Models;

namespace PM_MS.Repository
{
    public interface IRepo
    {
        public IEnumerable<productList> getproducts();

        public string createproduct(productList p);
        public string addcart(cartlist cart);

        public IEnumerable<cartlist> getcartlist(string username);
    }
}
