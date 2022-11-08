using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Repositories;


namespace NZWalks.API.Controllers
{

    [ApiController]  //this basically means api contoller
    [Route("[controller]")]  //this means instead of controller it takes its name Regions or we can directly put Regions instead also
    public class RegionsController : Controller
    {
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(IRegionRepository regionRepository,IMapper mapper)  //we are connecting with interfaces and automapper
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult >GetAllRegions()
        {
            //Type 1 using model class
            //var regions = regionRepository.GetAll();  //it calls the GetAll meth from Regionrepository class
            //return Ok(regions);

            // above code is not secure so use dto for good practise



            // type2 using dto class obj
            //below code is assigning the values from domain model to dto model and then sends to user which is secure and good practise


            //var domainmodelregion = regionRepository.GetAll();
            //var regionsDTO = new List<Models.DTO.Region>();
            //domainmodelregion.ToList().ForEach(modelregion =>
            //{
            //    var regionDTO = new Models.DTO.Region()
            //    {
            //        Id = modelregion.Id,
            //        Code= modelregion.Code,
            //        Name = modelregion.Name,
            //        Area = modelregion.Area,
            //        Lat = modelregion.Lat,
            //        Long =  modelregion.Long,
            //        Population = modelregion.Population,

            //    };
            //    regionsDTO.Add(regionDTO);


            //});




            //type 3 using automapper
            //use this code for automapper

            var regions = await regionRepository.GetAllAsync();
            var regionsDTO=mapper.Map<List<Models.DTO.Region>>(regions);
            
            return Ok(regionsDTO);

        }
    }
}
