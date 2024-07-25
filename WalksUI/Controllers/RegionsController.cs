using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.Text;
using System.Text.Json;
using WalksUI.Models;
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
        [HttpGet]
        public async Task<IActionResult> Index()
        {

            List<RegionDto> response = new List<RegionDto>();

            try
            {
                //建立一個發送http的物件
                var client = httpClientFactory.CreateClient();

				//呼叫取得所有 Region 資料的 API
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


        /// <summary>
        /// 建立表單視圖(一般檢視畫面不需要非同步)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// 發送表單 - 建立一筆資料(連接 API)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(CreateRegionViewModel model)
        {
			//建立一個發送http的物件
			var client = httpClientFactory.CreateClient();

            //建立要發送的內容和格使
            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://localhost:7243/api/regions"),
                Content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json")
			};

            var httpResponseMessage = await client.SendAsync(httpRequestMessage);

            httpResponseMessage.EnsureSuccessStatusCode();

			var response = await httpResponseMessage.Content.ReadFromJsonAsync<RegionDto>();

            if (response is not null)
            {
                return RedirectToAction("Index", "Regions");
            }

            return View();
		}


        /// <summary>
        /// 取得要更新的一筆資料(連接 API)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            //建立一個發送http的物件
            var client = httpClientFactory.CreateClient();

            var response = await client.GetFromJsonAsync<RegionDto>($"https://localhost:7243/api/regions/{id.ToString()}");

            if(response is not null)
            {
                return View(response);
            }

            return View(null);
        }


        /// <summary>
        /// 進行資料更新(連接 API)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Edit(RegionDto request)
        {
			//建立一個發送http的物件
			var client = httpClientFactory.CreateClient();

			//建立要發送的內容和格使
			var httpRequestMessage = new HttpRequestMessage()
			{
				Method = HttpMethod.Put,
				RequestUri = new Uri($"https://localhost:7243/api/regions/{request.Id}"),
				Content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json")
			};

            var httpResponseMessage = await client.SendAsync(httpRequestMessage);

            httpResponseMessage.EnsureSuccessStatusCode();

			var response = await httpResponseMessage.Content.ReadFromJsonAsync<RegionDto>();

			if (response is not null)
			{
				return RedirectToAction("Index", "Regions");
			}

			return View();
		}


	}
}
