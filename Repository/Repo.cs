
using Microsoft.EntityFrameworkCore;
using PM_MS.Models;
using Microsoft.Data.SqlClient;

namespace PM_MS.Repository
{
    public class Repo : IRepo
    {
        private readonly customdbContext _customdbContext;

        public Repo(customdbContext customdContext)
        {
            _customdbContext = customdContext;
        }


        public IEnumerable<productList> getproducts()
        {
            return _customdbContext.Products;
        }

        public string createproduct(productList p)
        {
            _customdbContext.Products.Add(p);
            _customdbContext.SaveChanges();
            return "OK";
        }

        public IEnumerable<cartlist> getcartlist(string username)
        {
            var param = new SqlParameter[]
            {
                new SqlParameter(){
                    ParameterName = "@username",
                    SqlDbType=System.Data.SqlDbType.NVarChar,Direction=System.Data.ParameterDirection.Input,Value=username}
            };
            
            return _customdbContext.getCartListbyUserName.FromSqlRaw("[dbo].[getCartListbyUserName] @username", param).AsEnumerable().ToList();
        }


        public string addcart(cartlist cart)
        {
            var param = new SqlParameter[]
            {
                new SqlParameter()
                {
                ParameterName = "@productid",
                SqlDbType = System.Data.SqlDbType.Int,
                Direction = System.Data.ParameterDirection.Input,
                Value = cart.productid

                },

                new SqlParameter()
                {
                    ParameterName = "@details",
                    SqlDbType = System.Data.SqlDbType.NVarChar,
                    Direction = System.Data.ParameterDirection.Input,
                    Value =cart.details
                },

                new SqlParameter()
                {
                    ParameterName = "@price",
                    SqlDbType = System.Data.SqlDbType.NVarChar,
                    Direction = System.Data.ParameterDirection.Input,
                    Value =cart.price
                },
                 new SqlParameter()
                {
                    ParameterName = "@name",
                    SqlDbType = System.Data.SqlDbType.NVarChar,
                    Direction = System.Data.ParameterDirection.Input,
                    Value =cart.name
                },
                 new SqlParameter()
                {
                    ParameterName = "@image",
                    SqlDbType = System.Data.SqlDbType.NVarChar,
                    Direction = System.Data.ParameterDirection.Input,
                    Value =cart.image
                },

                 new SqlParameter()
                {
                    ParameterName = "@quantity",
                    SqlDbType = System.Data.SqlDbType.NVarChar,
                    Direction = System.Data.ParameterDirection.Input,
                    Value =cart.quantity
                },
                 new SqlParameter()
                {
                    ParameterName = "@totalprice",
                    SqlDbType = System.Data.SqlDbType.NVarChar,
                    Direction = System.Data.ParameterDirection.Input,
                    Value =cart.totalprice
                },
                 new SqlParameter()
                {
                    ParameterName = "@username",
                    SqlDbType = System.Data.SqlDbType.NVarChar,
                    Direction = System.Data.ParameterDirection.Input,
                    Value =cart.username
                },
            };

            _customdbContext.SaveChanges(); 

            int intitem = _customdbContext.Database.ExecuteSqlRaw("[dbo].[createcartListforUser] @productid,@details,@price,@name,@image,@quantity,@totalprice,@username", param);


            return "Ok";
        }
    }
}
