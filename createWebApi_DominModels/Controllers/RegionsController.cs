using AutoMapper;
using createWebApi_DominModels.CustomActionFilters;
using createWebApi_DominModels.Data;
using createWebApi_DominModels.Models.Domain;
using createWebApi_DominModels.Models.DTO;
using createWebApi_DominModels.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Collections.Generic;

namespace createWebApi_DominModels.Controllers
{
    //https://localhost:Port_Number/api/Regions
    [Route("api/[controller]")]
    [ApiController]
    //加入身分授權Microsoft.AspNetCore.Authorization; (Authorize 加在這就是使用者正確授權後都可以進行讀取和編輯的功能)
    //[Authorize]
    public class RegionsController : ControllerBase
    {
        private readonly WebApiSampleDbContext dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(WebApiSampleDbContext dbContext, IRegionRepository regionRepository, 
            IMapper mapper)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }


        /// <summary>
        /// GetAll() REGIONS 取全部資料
        /// GET https://localhost:port_number/api/Regions
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Reader")]  //授權只有讀取的權限
        public async Task<IActionResult> GetALL()
        {
            //將資料從資料庫中取出
            //var regions = await dbContext.Regions.ToListAsync();
            //改成使用SQL儲存庫的方式
            var regions = await regionRepository.GetAllAsync();


            //轉換使用DTO方式 Map Models 傳遞需要的資料給前端
            //var regionsDto = new List<RegionDto>();
            //foreach (var region in regions)
            //{
            //    regionsDto.Add(new RegionDto()
            //    {
            //        Id = region.Id,
            //        Code = region.Code,
            //        Name = region.Name,
            //        RegionImageUrl = region.RegionImageUrl,
            //    });
            //}

            //改成使用AutoMapper處理DTO
            var regionsDto = mapper.Map<List<RegionDto>>(regions);


            //return Ok(regions); 原本傳遞資料方式改成下面使用DTO的方式傳遞
            return Ok(regionsDto);
        }


        /// <summary>
        /// GET SINGLE REGION 取單筆資料
        /// GET https://localhost:port_number/api/Regions/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            //第一種方式 Find(id)
            //var region = dbContext.Regions.Find(id);

            //第二種方式 FirstOrDefault(region => region.Id == id)
            //var region = await dbContext.Regions.FirstOrDefaultAsync(region => region.Id == id);

            //改成使用SQL儲存庫的方式
            var region = await regionRepository.GetByIdAsync(id);

            if (region == null)
            {
                return NotFound();
            }

            //轉換使用DTO方式 Map Models 傳遞需要的資料給前端
            //var regionsDto = new RegionDto
            //{
            //    Id = region.Id,
            //    Code = region.Code,
            //    Name = region.Name,
            //    RegionImageUrl = region.RegionImageUrl,
            //};

            //改成使用AutoMapper處理DTO
            var regionsDto = mapper.Map<RegionDto>(region);


            //return Ok(region); 原本傳遞資料方式改成下面使用DTO的方式傳遞
            return Ok(regionsDto);
        }


        /// <summary>
        /// POST 建立一筆資料
        /// POST https://localhost:port_number/api/Regions 
        /// </summary>
        /// <param name="addRegionRequestDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateModel]  //使用客製化驗證
        [Authorize(Roles = "Writer")]  //授權可編輯的權限
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            //建立DTO model
            //var regionModel = new Region
            //{
            //    Code = addRegionRequestDto.Code,
            //    Name = addRegionRequestDto.Name,
            //    RegionImageUrl = addRegionRequestDto.RegionImageUrl,
            //};

            //改成使用AutoMapper處理建立DTO model
            var regionModel = mapper.Map<Region>(addRegionRequestDto);


            //將資料新增到資料庫(資料庫中的資料表新增regionModel這筆物件資料)
            //await dbContext.Regions.AddAsync(regionModel);
            //await dbContext.SaveChangesAsync();
            //改成使用SQL儲存庫的方式
            regionModel = await regionRepository.CreateAsync(regionModel);

            //回傳給使用的資訊，使用DTO
            //var regionDto = new RegionDto
            //{
            //    Id = regionModel.Id,
            //    Code = regionModel.Code,
            //    Name = regionModel.Name,
            //    RegionImageUrl = regionModel.RegionImageUrl,
            //};

            //改成使用AutoMapper處理DTO
            var regionDto = mapper.Map<RegionDto>(regionModel);

            return CreatedAtAction(nameof(GetById), new { id = regionModel.Id }, regionDto); 
        }


        /// <summary>
        /// PUT SINGLE REGION 更新單筆資料
        /// PUT https://localhost:port_number/api/Regions/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]  //使用客製化驗證
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            //重新建立DTO物件映射
            //var regionDominModel = new Region
            //{
            //    Code = updateRegionRequestDto.Code,
            //    Name = updateRegionRequestDto.Name,
            //    RegionImageUrl = updateRegionRequestDto.RegionImageUrl
            //};

            //改成使用AutoMapper處理更新DTO model(和建立的概念一樣)
            var regionDominModel = mapper.Map<Region>(updateRegionRequestDto);


            //從資料庫中取得該筆ID的資料 
            //var region = await dbContext.Regions.FirstOrDefaultAsync(region => region.Id == id);

            //改成使用SQL儲存庫的方式
            regionDominModel = await regionRepository.UpdateAsync(id, regionDominModel);


            //if (region == null)
            //{
            //    return NotFound();
            //}
            //改成使用SQL儲存庫的方式判斷
            if (regionDominModel == null)
            {
                return NotFound();
            }

            //更新資料
            //region.Code = updateRegionRequestDto.Code;
            //region.Name = updateRegionRequestDto.Name;
            //region.RegionImageUrl = updateRegionRequestDto.RegionImageUrl;

            //await dbContext.SaveChangesAsync();

            //convert 回傳給使用者資料 使用DTO
            //var regionDto = new RegionDto
            //{
            //    Id = region.Id,
            //    Code = region.Code,
            //    Name = region.Name,
            //    RegionImageUrl = region.RegionImageUrl,
            //};

            //使用SQL儲存庫的方式判斷
            //var regionDto = new RegionDto
            //{
            //    Id = regionDominModel.Id,
            //    Code = regionDominModel.Code,
            //    Name = regionDominModel.Name,
            //    RegionImageUrl = regionDominModel.RegionImageUrl,
            //};

            //改成使用AutoMapper處理DTO
            var regionDto = mapper.Map<RegionDto>(regionDominModel);

            return Ok(regionDto);
        }


        /// <summary>
        /// DELETE SINGLE REGION 刪除單筆資料
        /// DELETE https://localhost:port_number/api/Regions/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer,Reader")]  //授權Reader和Writer兩個角色都可進行刪除
        public async Task<IActionResult> delete([FromRoute] Guid id)
        {
            //var region = await dbContext.Regions.FirstOrDefaultAsync(region => region.Id == id);
            //改成使用SQL儲存庫的方式
            var region = await regionRepository.DeleteAsync(id);

            if (region == null)
            {
                return NotFound();
            }

            //刪除資料
            //dbContext.Regions.Remove(region);
            //await dbContext.SaveChangesAsync();

            //回傳刪除的資料(可回傳也可以不回傳)
            //var regionDto = new RegionDto
            //{
            //    Id = region.Id,
            //    Code = region.Code,
            //    Name = region.Name,
            //    RegionImageUrl = region.RegionImageUrl,
            //};

            //改成使用AutoMapper處理DTO 回傳給使用者刪除的資料
            var regionDto = mapper.Map<RegionDto>(region);


            return Ok(regionDto);
            //return Ok(); 也可以不回傳刪除的資料
        }
    }

}
