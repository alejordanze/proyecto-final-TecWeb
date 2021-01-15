using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RadioTaxisAPI.Data.Entities;

namespace RadioTaxisAPI.Data.Repository
{
    public interface ILibraryRepository
    {
        Task<IEnumerable<BusinessEntity>> GetBusinesses();
        void CreateBusiness(BusinessEntity businessEntity);
        Task<IEnumerable<BusinessEntity>> GetBusinesses(string orderBy);
        Task<BusinessEntity> GetBusiness(int businessId);
        Task<bool> DeleteBusiness(int businessId);
        bool UpdateBusiness(BusinessEntity business);

        Task<IEnumerable<DriverEntity>> GetDrivers(int businessId);
        void CreateDriver(DriverEntity driverEntity);
        Task<DriverEntity> GetDriver(int driverId);
        Task<bool> DeleteDriver(int driverId);
        Task<bool> UpdateDriver(DriverEntity driver);

        Task<bool> SaveChangesAsync();
    }

}
