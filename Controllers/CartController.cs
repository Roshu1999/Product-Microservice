using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PM_MS.Models;
using PM_MS.Repository;
using System.Data;
using System.Data.SqlClient;

namespace PM_MS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IRepo _repo;

        public CartController(IConfiguration configuration, IRepo repo)
        {
            _configuration = configuration;
            _repo = repo;
        }

        [HttpGet]
        [Route("username")]
        
        public IEnumerable<cartlist> getcartlist(string username)
        {
            return _repo.getcartlist(username);
            
        }


        [HttpPost]
        [Route("addtocart")]
        public IActionResult Create([FromBody] cartlist cart)
        {

            var status = _repo.addcart(cart);

            if (status == "Ok")
            {
                return Ok(new { message = "product added to successfully!" });
            }
            else
            {
                return StatusCode(429, status);
            }
        }

        [HttpDelete("{productid}")]
        public JsonResult Delete(int productid)
        {
            string query = @"delete from dbo.cartlist where productid=@productid";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ProdConn");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@productid", productid);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Deleted Product");
        }



    }
}