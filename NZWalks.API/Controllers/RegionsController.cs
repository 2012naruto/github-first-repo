using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Repositories;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using System.Data;


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
        public async Task<IActionResult >GetAllRegionsAsync()
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
        
        
        [HttpGet]
        [Route("{id:guid}")]    //id-tomatch the id value,guid-to restrict only guid value not int any other
        [ActionName("GetRegionsAsync")]   //we used below in add block
        public async Task<IActionResult> GetRegionsAsync(Guid id)
        {
            var domainregion = await regionRepository.GetAsync(id);

            if (domainregion == null)
            {
                
                return NotFound();
            }

            var DTOregion=mapper.Map<Models.DTO.Region>(domainregion);

            return Ok(DTOregion);
        }

        [HttpPost]
        public async Task<IActionResult> AddRegionsAsync(Models.DTO.AddRegionRequest addRegionRequest)
        {

            
            //request(DTO) to domain model
            var region = new Models.Domain.Region()     //assigning dto class object to add region request class objects
            {
                Code = addRegionRequest.Code,
                Name = addRegionRequest.Name,
                Area = addRegionRequest.Area,
                Lat = addRegionRequest.Lat,
                Long = addRegionRequest.Long,
                Population = addRegionRequest.Population
            };

            //pass details to repository

            region = await regionRepository.AddnewAsync(region);


            //convert back to DTO
            //var RegionDTO = new Models.DTO.Region()      //assigning  add region request class objects after passing to DTO objects
            //{

            //    Id = region.Id,
            //    Code = region.Code,
            //    Name = region.Name,
            //    Area = region.Area,
            //    Lat = region.Lat,
            //    Long = region.Long,
            //    Population = region.Population
            //};


            //convert back to DTO by using mapping

            var RegionDTO = mapper.Map<Models.DTO.Region>(region);
            return CreatedAtAction(nameof(GetRegionsAsync),new { id=RegionDTO.Id},RegionDTO);
        }

        [HttpDelete]
        [Route("{id:guid}")]    //id-to match the id value,guid-to restrict only guid value not int any other

        public async Task<IActionResult> DeleteRegionAsync(Guid id)
        {
            //Get region from database

            var region=await regionRepository.DeleteAsync(id);

            // If null not found
            if(region == null)
            {
                return NotFound();
            }

            //else convert response back to DTO

            var DTOregion = mapper.Map<Models.DTO.Region>(region);



            //return Ok response
            return Ok(DTOregion);

        }

        [HttpPut]
        [Route("{id:guid}")]
       
        public async Task<IActionResult> UpdateRegionAsync([FromRoute] Guid id,
           [FromBody] Models.DTO.UpdateRegionRequest updateRegionRequest)
        {
           

            // Convert DTO to Domain model
            var region = new Models.Domain.Region()
            {
                Code = updateRegionRequest.Code,
                Area = updateRegionRequest.Area,
                Lat = updateRegionRequest.Lat,
                Long = updateRegionRequest.Long,
                Name = updateRegionRequest.Name,
                Population = updateRegionRequest.Population
            };


            // Update Region using repository
            region = await regionRepository.UpdateAsync(id, region);


            // If Null then NotFound
            if (region == null)
            {
                return NotFound();
            }

            // Convert Domain back to DTO
            var regionDTO = new Models.DTO.Region
            {
                Id = region.Id,
                Code = region.Code,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Name = region.Name,
                Population = region.Population
            };


            // Return Ok response
            return Ok(regionDTO);
        }
    }
}
