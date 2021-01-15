using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CsvHelper;
using RadioTaxisAPI.Data.Entities;
using RadioTaxisAPI.Data.Repository;
using RadioTaxisAPI.Exceptions;
using RadioTaxisAPI.Models;

namespace RadioTaxisAPI.Services
{
    public class BusinessService : IBusinessService
    {
        ILibraryRepository _libraryRepository;
        private IMapper _mapper;

        private HashSet<string> allowedOrderByParameters = new HashSet<string>()
        {
            "id",
            "name",
            "foundation-date",
        };


        public BusinessService(ILibraryRepository libraryRepository, IMapper mapper)
        {
            _libraryRepository = libraryRepository;
            _mapper = mapper;
        }

        public async Task<BusinessModel> CreateBusiness(BusinessModel businessModel)
        {
            var businessEntity = _mapper.Map<BusinessEntity>(businessModel);
            _libraryRepository.CreateBusiness(businessEntity);
            var result = await _libraryRepository.SaveChangesAsync();

            if (result)
            {
                return _mapper.Map<BusinessModel>(businessEntity);
            }

            throw new Exception("Database Error");
        }

        public async Task<IEnumerable<BusinessModel>> GetBusinesses(string orderBy)
        {
            if (!allowedOrderByParameters.Contains(orderBy.ToLower()))
            {
                throw new BadRequestException($"Order Movies by: {orderBy} is not supported");
            }
            var entityList = await _libraryRepository.GetBusinesses(orderBy);
            var modelList = _mapper.Map<IEnumerable<BusinessModel>>(entityList);
            return modelList;
        }

        public async Task<BusinessModel> GetBusiness(int businessId)
        {
            var business = await _libraryRepository.GetBusiness(businessId);
            if(business == null)
            {
                throw new RequestNotFoundException($"Business with ID {businessId} does not exist");
            }
            return _mapper.Map<BusinessModel>(business);

        }

        public async Task<BusinessModel> UpdateBusiness(int businessId, BusinessModel businessModel)
        {
            var businessEntity = _mapper.Map<BusinessEntity>(businessModel);
            await GetBusiness(businessId);
            businessEntity.Id = businessId;
            _libraryRepository.UpdateBusiness(businessEntity);

            var saveResult = await _libraryRepository.SaveChangesAsync();

            if (!saveResult)
            {
                throw new Exception("Database Error");
            }
            return businessModel;
        }
            

        public async Task<DeleteModel> DeleteBusiness(int businessId)
        {
            await GetBusiness(businessId);
            var drivers = await _libraryRepository.GetDrivers(businessId);

            foreach(var driver in drivers)
            {
                var delete = await _libraryRepository.DeleteDriver(driver.Id);
            }

            var DeleteResult = await _libraryRepository.DeleteBusiness(businessId);

            var saveResult = await _libraryRepository.SaveChangesAsync();

            if (!saveResult || !DeleteResult)
            {
                throw new Exception("Database Error");
            }


            if (saveResult)
            {
                return new DeleteModel()
                {
                    Success = saveResult,
                    Message = "The business was deleted."
                };
            }
            else
            {
                return new DeleteModel()
                {
                    Success = saveResult,
                    Message = "The business was not deleted."
                };
            }
        }

        public async Task<List<ReportModel>> GetReport()
        {
            List<ReportModel> report = new List<ReportModel>();
            var business = await _libraryRepository.GetBusinesses();
            foreach(var bis in business)
            {
                var categories = getEmptyCategories();
                var businessReport = new ReportModel { BusinessId = bis.Id, Name = bis.Name, ReportDate = new DateTime()};
                foreach(var driver in bis.Drivers)
                {
                    foreach(var cat in categories)
                    {
                        if (driver.Categoria.Equals(cat.Category))
                        {
                            cat.Quantity++;
                            cat.Drivers.Add(driver);
                        }
                    }
                }
                businessReport.Drivers = categories;
                report.Add(businessReport);
            }
            return report;
        }


        private List<DriverReportModel> getEmptyCategories()
        {
            return new List<DriverReportModel>
            {
               new DriverReportModel(){Category="M", Quantity=0, Drivers=new List<DriverEntity>()},
               new DriverReportModel(){Category="P", Quantity=0, Drivers=new List<DriverEntity>()},
               new DriverReportModel(){Category="A", Quantity=0, Drivers=new List<DriverEntity>()},
               new DriverReportModel(){Category="B", Quantity=0, Drivers=new List<DriverEntity>()},
               new DriverReportModel(){Category="C", Quantity=0, Drivers=new List<DriverEntity>()},
            };
        }

        //public StringBuilder getMoviesCsv()
        //{
        //    var movies = _libraryRepository.GetMovies();
        //    StringBuilder sb = new StringBuilder();
        //    sb.AppendLine("Titulo,Duracion,Director,Productor,Presupuesto,Recaudacion");
        //    foreach (var movie in movies)
        //    {
        //        sb.AppendLine($"{movie.Title},{movie.Duration},{string.Join("-",movie.Directors)},{movie.Producer},{movie.Budget},{movie.BoxOffice}");
        //    }
        //    return sb;
        //}
    }
}
