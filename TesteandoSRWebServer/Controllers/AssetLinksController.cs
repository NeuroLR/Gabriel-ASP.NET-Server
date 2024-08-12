using Microsoft.AspNetCore.Mvc;

namespace TesteandoSRWebServer.Controllers
{
    [ApiController]
    [Route(".well-known")]
    public class AssetLinksController : ControllerBase
    {
        [HttpGet("assetlinks.json")]
        public async Task<IActionResult> GetAssetLinks()
        {
            try
            {
                using FileStream stream = new(@"assetlinks.json", FileMode.Open);
                using StreamReader reader = new(stream);
                string content = await reader.ReadToEndAsync();
                return Content(content, "application/json; charset=utf-8");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
