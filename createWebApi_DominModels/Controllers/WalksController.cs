using AutoMapper;
using createWebApi_DominModels.CustomActionFilters;
using createWebApi_DominModels.Data;
using createWebApi_DominModels.Models.Domain;
using createWebApi_DominModels.Models.DTO;
using createWebApi_DominModels.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace createWebApi_DominModels.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository;

        //沒有引用 WebApiSampleDbContext因為將 SQL操作搬移到 SQLWalkRepository並使用 IWalkRepository介面控制
        public WalksController(IMapper mapper, IWalkRepository walkRepository)
        {
            this.mapper = mapper;
            this.walkRepository = walkRepository;
        }


        /// <summary>
        /// 建立一筆資料
        /// POST https://localhost:port_number/api/Walks 
        /// </summary>
        /// <param name="addWalkRequestDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateModel]  //使用客製化驗證
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
        {
            //使用 AutoMapper處理建立 DTO model
            var walkModel = mapper.Map<Walk>(addWalkRequestDto);

            //使用 SQL語句儲存庫的方式
            walkModel = await walkRepository.CreateAsync(walkModel);

            //使用AutoMapper處理傳給使用者資料的DTO
            var WalkDto = mapper.Map<WalkDto>(walkModel);

            return Ok(WalkDto); 
        }


        /// <summary>
        /// GetAll() Walks 取全部資料
        /// GET: /api/walks?filterOn=Name&filterQuery=Track&sortBy=Name&isAscending=true&pageNumber=1&pageSize=10
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        // public async Task<IActionResult> GetAll()

        //改成可篩選資料的方式
        public async Task<IActionResult> GetAll(
            [FromQuery] string? filterOn, [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy, [FromQuery] bool? isAscending,
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 1000)
        {
            //如果不使用全局log日誌寫法的作法，每一個API中都要寫一樣的 try/catch
            //try
            //{
            //    throw new Exception("這是一個測試異常"); //正常不需要寫這段

            //    //使用 SQL語句儲存庫的方式
            //    var walks = await walkRepository.GetAllAsync(filterOn, filterQuery,
            //        sortBy, isAscending ?? true, pageNumber, pageSize);

            //    //使用 AutoMapper 處理 DTO
            //    var walkDto = mapper.Map<List<WalkDto>>(walks);

            //    return Ok(walkDto);
            //}
            //catch (Exception ex)
            //{
            //    return Problem("出現問題", null, (int)HttpStatusCode.InternalServerError);
            //}


            //使用 SQL語句儲存庫的方式
            var walks = await walkRepository.GetAllAsync(filterOn, filterQuery,
                sortBy, isAscending ?? true, pageNumber, pageSize);

            //log 紀錄 Exception 異常測試用
            //throw new Exception("這是一個新的 Exception");

            //使用 AutoMapper 處理 DTO
            var walkDto = mapper.Map<List<WalkDto>>(walks);

            return Ok(walkDto);
        }


        /// <summary>
        /// GET SINGLE Walk 取單筆資料
        /// GET https://localhost:port_number/api/Walks/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            //使用SQL儲存庫的方式
            var walk = await walkRepository.GetByIdAsync(id);

            if (walk == null)
            {
                return NotFound();
            }

            //使用 AutoMapper 處理 DTO 回傳給使用者的資訊
            var walkDto = mapper.Map<WalkDto>(walk);

            return Ok(walkDto);
        }


        /// <summary>
        /// PUT SINGLE Walk 更新單筆資料
        /// PUT https://localhost:port_number/api/Walks/{id} 
        /// </summary>
        /// <param name="id"></param>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]  //使用客製化驗證
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateWalkRequestDto updateWalkRequestDto)
        {
            //使用AutoMapper處理更新DTO model(和建立的概念一樣)
            var walkModel = mapper.Map<Walk>(updateWalkRequestDto);

            //使用SQL儲存庫的方式操作更新資料庫
            walkModel = await walkRepository.UpdateAsync(id, walkModel);

            if (walkModel == null)
            {
                return NotFound();
            }

            //使用AutoMapper處理DTO
            var walkDto = mapper.Map<WalkDto>(walkModel);

            return Ok(walkDto); 
        }


        /// <summary>
        /// DELETE SINGLE Walk 刪除單筆資料
        /// DELETE https://localhost:port_number/api/Walks/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            //使用SQL儲存庫的方式
            var walkModel = await walkRepository.DeleteAsync(id);

            if (walkModel == null)
            {
                return NotFound();
            }

            //使用AutoMapper處理DTO 回傳給使用者刪除的資料
            var walkDto = mapper.Map<WalkDto>(walkModel);

            return Ok(walkDto);
        }
    }
}
