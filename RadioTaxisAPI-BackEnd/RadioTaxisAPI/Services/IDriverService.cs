using System;
using System.Collections.Generic;
using RadioTaxisAPI.Models;
using System.Threading.Tasks;

namespace RadioTaxisAPI.Services
{
    public interface IDriverService
    {
        //IEnumerable<ActorModel> GetActors();
        //ActorModel GetActor(int actorId);
        //ActorModel CreateActor(ActorModel actor);
        Task<DriverModel> CreateDriver(int businessId, DriverModel driver);
        Task<DriverModel> GetDriver(int driverId, int businessId);
        Task<IEnumerable<DriverModel>> GetDrivers(int businessId);
        Task<DriverModel> UpdateDriver(int driverId, int businessId, DriverModel driver);
        Task<DeleteModel> DeleteDriver(int driverId, int businessId);
    }
}
