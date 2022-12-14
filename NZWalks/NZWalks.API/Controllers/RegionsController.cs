using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
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
        
    }
}
