using Microsoft.AspNetCore.Authorization;
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
    public class ProductController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IRepo _repo;

        public ProductController(IConfiguration configuration , IRepo repo)
        {
            _configuration = configuration;
            _repo = repo;
        }

        
        [HttpGet]
        public IEnumerable<productList> getproducts()
        {
            return _repo.getproducts();
        }
        [HttpPost]
        [Route("createproduct")]
        public IActionResult Create([FromBody] productList c)
        {

            var status = _repo.createproduct(c);

            if (status == "OK")
            {
                return Ok(new { message = "product added successfully!" });
            }
            else
            {
                return StatusCode(429, status);
            }
        }
    }
}