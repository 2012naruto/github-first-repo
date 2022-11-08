using AutoMapper;

namespace NZWalks.API.Profiles
{
    public class RegionsProfile: Profile     //this profile is coming from automapper
    {
        public RegionsProfile()
        {
            CreateMap<Models.Domain.Region,Models.DTO.Region>()   //we can use this line alone when both the colum names in src and destination class are same
           //.ForMember(dest => dest.Id,options =>options.MapFrom(src => src.RegionId)); here src id can be any name so we can use this line when we have different column names   
                
                .ReverseMap();
        
        }



    }
}
