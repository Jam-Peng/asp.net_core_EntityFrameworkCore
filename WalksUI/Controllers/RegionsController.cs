using Microsoft.AspNetCore.Mvc;
using WalksUI.Models.DTO;

namespace WalksUI.Controllers
{
    public class RegionsController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;

        public RegionsController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }


        /// <summary>
        /// Regions的Index首頁要取得所有Region API資料
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {

            List<RegionDto> response = new List<RegionDto>();

            try
            {
                //呼叫取得所有 Region 資料的 API  
                var client = httpClientFactory.CreateClient();

                var httpResponseMessage = await client.GetAsync("https://localhost:7243/api/regions");

                httpResponseMessage.EnsureSuccessStatusCode();

                //var stringResponseBody = await httpResponseMessage.Content.ReadAsStringAsync();
                //調整使用 DTO，不使用 ReadAsStringAsync()
                response.AddRange(await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<RegionDto>>());

                //調整使用 DTO，不使用 ReadAsStringAsync()，就不需要下面這段
                //ViewBag.Response = stringResponseBody;
            }
            catch (Exception ex)
            {
                //紀錄異常
                
            }

            return View(response);
        }


    }
}
