using System;
using System.Collections.Generic;
using System.Text;
using RadioTaxisAPI.Models;
using System.Threading.Tasks;

namespace RadioTaxisAPI.Services
{
    public interface IBusinessService
    {
        Task<IEnumerable<BusinessModel>> GetBusinesses(string orderBy);
        Task<BusinessModel> GetBusiness(int businessId);
        Task<BusinessModel> CreateBusiness(BusinessModel businessModel);
        Task<DeleteModel> DeleteBusiness(int businessId);
        Task<BusinessModel> UpdateBusiness(int businessId, BusinessModel businessModel);
        Task<List<ReportModel>> GetReport();
    }
}
