using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;
using System.Reflection.Metadata.Ecma335;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegionsController : Controller
    {
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(IRegionRepository regionRepository, IMapper mapper)
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllRegions()
        {

            /* var regions = new List<Region>
             {
                 new Region
                 {
                     Id = Guid.NewGuid(),
                     Name = "Wellington",
                     Code = "WLG",
                     Area = 227755,
                     Lat = -1.8822,
                     Long = 299.88,
                     Population = 5000000
                 },
                  new Region
                 {
                     Id = Guid.NewGuid(),
                     Name = "Auckland",
                     Code = "AUCK",
                     Area = 227755,
                     Lat = -1.8822,
                     Long = 299.88,
                     Population = 5000000
                 }
             };*/
            var regions = await regionRepository.GetAllAsync();
            //return DTO regions
           /* var regionsDTO = new List<Models.DTO.Region>();
            regions.ToList().ForEach(domainRegion =>
            {
                var regionDTO = new Models.DTO.Region()
                {
                    Id = domainRegion.Id,
                    Code = domainRegion.Code,
                    Name = domainRegion.Name,
                    Area = domainRegion.Area,
                    Lat = domainRegion.Lat,
                    Long = domainRegion.Long,
                    Population = domainRegion.Population

                };
                regionsDTO.Add(regionDTO);
            });*/

            var regionsDTO = mapper.Map<List<Models.DTO.Region>>(regions);
            return Ok(regionsDTO);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetRegionAsync")]
        public async Task<IActionResult> GetRegionAsync(Guid id)
        {
            var region = await regionRepository.GetAsyncRegion(id);
            if(region == null) return NotFound();
            var regionDTO = mapper.Map<Models.DTO.Region>(region);
            return Ok(regionDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddRegionAsync(Models.DTO.AddRegionRequest addRegionRequest)
        {
            // Request(DTO) to Domain model
            var region = new Models.Domain.Region()
            {
                Code = addRegionRequest.Code,
                Name = addRegionRequest.Name,
                Area = addRegionRequest.Area,
                Lat = addRegionRequest.Lat,
                Long = addRegionRequest.Long,
                Population = addRegionRequest.Population
            };

            // Pass details to Repository
           region = await regionRepository.AddAsync(region);

            // Convert back to DTO
            var regionDTO = new Models.DTO.Region()
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Population = region.Population
            };

            return CreatedAtAction(nameof(GetRegionAsync), new { id = regionDTO.Id }, regionDTO);
        }

        [HttpDelete]
        [Route("{id:guid}")]
       public async Task<IActionResult> DeleteRegionAsync(Guid id)
        {
            //Get region from database
            var region = await regionRepository.DeleteAsyncRegion(id);

            //If null NotFound
            if(region == null)
            {
                return NotFound();
            }

            // Convert response back to DTO
            var regionDTO = new Models.DTO.Region()
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Population = region.Population
            };

            //return Ok response
            return Ok(regionDTO);

        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateRegion([FromRoute]Guid id, [FromBody]Models.DTO.UpdateRegionRequest updatedregion)
        {
            //Convert DTO to Donain model
            var region = new Models.Domain.Region()
            {
                Code = updatedregion.Code,
                Name = updatedregion.Name,
                Area = updatedregion.Area,
                Lat = updatedregion.Lat,
                Long = updatedregion.Long,
                Population = updatedregion.Population
            };
            // Update Region using repository
             region = await regionRepository.UpdateAsyncRegion(id, region);

            //If Null then NotFound
            if(region == null)
            {
                return NotFound();
            }

            //Convert Domain back to DTO
            var regionDTO = new Models.DTO.Region()
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Population = region.Population
            };
            //Return Ok response
            return Ok(regionDTO);
        }
    }
}
