using AutoMapper;
using createWebApi_DominModels.Models.Domain;
using createWebApi_DominModels.Models.DTO;

namespace createWebApi_DominModels.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            //CreateMap<資料來源, 資料目的地>
            //RegionsController API - 處理回傳"取的所有資料"和"單筆資料"
            CreateMap<Region, RegionDto>().ReverseMap();

            //RegionsController API - 處理"建立單筆資料"的映射
            CreateMap<AddRegionRequestDto, Region>().ReverseMap();

            //RegionsController API - 處理"更新單筆資料"的映射
            CreateMap<UpdateRegionRequestDto, Region>().ReverseMap();
            
            
            //WalksController API - 處理回傳"取的所有資料"和"單筆資料"
            CreateMap<Walk,  WalkDto>().ReverseMap();

            //WalksController API - 處理"建立單筆資料"的映射
            CreateMap<AddWalkRequestDto, Walk>().ReverseMap();

            //WalksController API - 處理"更新單筆資料"的映射
            CreateMap<UpdateWalkRequestDto, Walk>().ReverseMap();


            //DificultyController API - 處理回傳"取的所有資料"和"單筆資料"
            CreateMap<Difficulty, DifficultyDto>().ReverseMap();
        }
    }
    
}
