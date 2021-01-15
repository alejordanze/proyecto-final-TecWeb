using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RadioTaxisAPI.Exceptions;
using RadioTaxisAPI.Models;
using RadioTaxisAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace RadioTaxisAPI.Controllers
{
    [Authorize()]
    [Route("api/business/{businessId:int}/[controller]")]
    public class DriversController : ControllerBase
    {
        private IDriverService driverService;
        public DriversController(IDriverService driverService)
        {
            this.driverService = driverService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DriverModel>>> GetDrivers(int businessId)
        {
            try
            {
                return Ok(await driverService.GetDrivers(businessId));
            }
            catch(BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(InternalServerException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{driverId:int}", Name = "GetDriver")]
        public async Task<ActionResult<DriverModel>> GetDriver(int driverId, int businessId)
        {
            try
            {
                return await driverService.GetDriver(driverId, businessId);
            }
            catch(RequestNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(InternalServerException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<DriverModel>> CreateDriver(int businessId, [FromBody] DriverModel driver)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var url = HttpContext.Request.Host;
                var createdDriver = await driverService.CreateDriver(businessId, driver);
                return CreatedAtRoute("GetDriver", new { businessId = businessId, driverId = createdDriver.Id }, createdDriver);
            }
            catch (InternalServerException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("{driverId:int}")]
        public async Task<ActionResult<DriverModel>> UpdateDriver(int businessId, int driverId, [FromBody] DriverModel driver)
        {
            try
            {
                return Ok(await driverService.UpdateDriver(driverId, businessId, driver));
            }
            catch (RequestNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Something happend: {ex.Message}");
            }
        }

        [HttpDelete("{driverId:int}")]
        public async Task<ActionResult<bool>> DeleteVideogameAsync(int businessId, int driverId)
        {
            try
            {
                return Ok(await driverService.DeleteDriver(driverId, businessId));
            }
            catch (RequestNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Something happend: {ex.Message}");
            }
        }

    }
}
