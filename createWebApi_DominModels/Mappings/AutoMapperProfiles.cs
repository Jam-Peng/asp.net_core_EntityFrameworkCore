using AutoMapper;
using createWebApi_DominModels.Models.Domain;
using createWebApi_DominModels.Models.DTO;

namespace createWebApi_DominModels.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            //建立映射處理"取的所有資料"和"單筆資料" CreateMap<資料來源, 資料目的地>
            CreateMap<Region, RegionDto>().ReverseMap();

            //處理"建立單筆資料"的映射
            CreateMap<AddRegionRequestDto, Region>().ReverseMap();

            //處理"更新單筆資料"的映射
            CreateMap<UpdateRegionRequestDto, Region>().ReverseMap();

        }
    }
    
}
