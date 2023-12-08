using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using WebApplication2.Models;
using WebApplication2.Services;

namespace WebApplication2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShopController : ControllerBase
    {
        private readonly IConfiguration _config;
        [HttpGet("listitem")]
        public IActionResult GetItems(int productID)
        {
            var Data = ShopService.GetItems(productID);
            return Ok(Data);
        }

        [HttpPost("editItem")]
        public IActionResult PostItem([FromBody] Item item)
        {
            var Post = ShopService.PostItems(item);
            return Ok(Post);
        }

        [HttpGet("cart")]
        public IActionResult GetCart()
        {
            var Data = ShopService.GetCart();
            return Ok(Data);
        }

        [HttpGet("report")]
        public IActionResult report()
        {
            var Data = ShopService.report();
            return Ok(Data);
        }

        [HttpPost("editcart")]
        public IActionResult PostCart([FromBody] Cart cart)
        {
            var Post = ShopService.PostCart(cart);
            return Ok(Post);
        }

        [HttpDelete("{cartid}")]
        public IActionResult DeleteCart(int cartid)
        {
            var Delete = ShopService.DeleteCart(cartid);
            return Ok(Delete);
        }
    }
}
