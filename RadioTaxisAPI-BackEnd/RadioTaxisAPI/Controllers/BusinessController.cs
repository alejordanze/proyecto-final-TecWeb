using System;
using System.Collections.Generic;
using RadioTaxisAPI.Exceptions;
using RadioTaxisAPI.Models;
using RadioTaxisAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace RadioTaxisAPI.Controllers
{
    [Authorize()]
    [Route("api/[controller]")]
    public class BusinessController : ControllerBase
    {
        private IBusinessService businessService;

        public BusinessController(IBusinessService businessService)
        {
            this.businessService = businessService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BusinessModel>>> GetMovies(string orderBy = "id")
        {
            try
            {
                return Ok(await businessService.GetBusinesses(orderBy));
            }
            catch (BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InternalServerException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{businessId:int}", Name = "GetBusiness")]
        public async Task<ActionResult<BusinessModel>> GetMovie(int businessId)
        {
            try
            {
                return Ok(await businessService.GetBusiness(businessId));
            }
            catch(RequestNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch(InternalServerException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Something bad happened: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<BusinessModel>> CreateMovie([FromBody] BusinessModel business)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var url = HttpContext.Request.Host;
                var createdBusiness = await businessService.CreateBusiness(business);
                return CreatedAtRoute("GetBusiness", new { businessId = createdBusiness.Id }, createdBusiness);
            }
            catch(InternalServerException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
           
        }

        [HttpDelete("{businessId:int}")]
        public async Task<ActionResult<DeleteModel>> DeleteMovie(int businessId)
        {
            try
            {
                return Ok(await businessService.DeleteBusiness(businessId));
            }
            catch(RequestNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch(InternalServerException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("{businessId:int}")]
        public async Task<IActionResult> UpdateMovie(int businessId, [FromBody] BusinessModel businessModel)
        {
            try
            {
                return Ok(await businessService.UpdateBusiness(businessId, businessModel));
            }
            catch (RequestNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("report")]
        public async Task<ActionResult<ReportModel>> GetReport()
        {
            try
            {
                return Ok(await businessService.GetReport());
            }
            catch (RequestNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InternalServerException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Something bad happened: {ex.Message}");
            }
        }
        //[HttpGet("moviesToCsv")]
        //public ActionResult<IEnumerable<MovieEarningsModel>> PostMoviesFromCsv()
        //{
        //    var movies = businessService.GetMovies("id");
        //    try
        //    {
        //        var sb = businessService.getMoviesCsv();
        //        return File(Encoding.UTF8.GetBytes(sb.ToString()), "text/csv", "movies.csv");
        //    }
        //    catch (InternalServerException ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        //    }
        //}

    }
}
