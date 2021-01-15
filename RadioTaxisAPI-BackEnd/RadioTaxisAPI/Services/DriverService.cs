using System;
using System.Collections.Generic;
using RadioTaxisAPI.Models;
using System.Linq;
using AutoMapper;
using RadioTaxisAPI.Data.Entities;
using RadioTaxisAPI.Data.Repository;
using RadioTaxisAPI.Exceptions;
using System.Threading.Tasks;

namespace RadioTaxisAPI.Services
{
    public class DriverService : IDriverService
    {
        ILibraryRepository _libraryRepository;
        private IMapper _mapper;

        public DriverService(IMapper mapper, ILibraryRepository libraryRepository)
        {
            _mapper = mapper;
            _libraryRepository = libraryRepository;
        }

        public async Task<DriverModel> CreateDriver(int businessId, DriverModel driver)
        {
            await validateBusiness(businessId);
            var driverEntity = _mapper.Map<DriverEntity>(driver);
            _libraryRepository.CreateDriver(driverEntity);
            var saveResult = await _libraryRepository.SaveChangesAsync();
            if (!saveResult)
            {
                throw new Exception("Error saving Driver");
            }

            var modelToReturn = _mapper.Map<DriverModel>(driverEntity);
            modelToReturn.BusinessId = businessId;
            return modelToReturn;
        }

        public async Task<DeleteModel> DeleteDriver(int driverId, int businessId)
        {
            await GetDriver(driverId, businessId);
            _libraryRepository.DeleteDriver(driverId);
            var saveResult = await _libraryRepository.SaveChangesAsync();
            if (!saveResult)
            {
                throw new Exception("Error while saving.");
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

        public async Task<DriverModel> GetDriver(int driverId, int businessId)
        {
            await validateBusiness(businessId);
            await validateDriver(driverId);
            var driver = await _libraryRepository.GetDriver(driverId);
            if (driver.Business.Id != businessId)
            {
                throw new RequestNotFoundException($"Th Driver id:{driverId} does not exist for Business id:{businessId}");
            }
            return _mapper.Map<DriverModel>(driver);
        }

        public async Task<IEnumerable<DriverModel>> GetDrivers(int businessId)
        {
            await validateBusiness(businessId);
            var driver = await _libraryRepository.GetDrivers(businessId);
            return _mapper.Map<IEnumerable<DriverModel>>(driver);
        }

        public async Task<DriverModel> UpdateDriver(int driverId, int businessId, DriverModel driver)
        {
            await GetDriver(driverId, businessId);
            driver.Id = driverId;
            await _libraryRepository.UpdateDriver(_mapper.Map<DriverEntity>(driver));
            var saveResult = await _libraryRepository.SaveChangesAsync();
            if (!saveResult)
            {
                throw new Exception("Error while saving");
            }
            return driver;
        }

        private async Task validateBusiness(int businessId)
        {
            var business = await _libraryRepository.GetBusiness(businessId); //_libraryRepository.GetCompany(companyId);
            if (business == null)
            {
                throw new RequestNotFoundException($"the business id:{businessId}, does not exist");
            }
        }

        private async Task validateDriver(int driverId)
        {
            var videogame = await _libraryRepository.GetDriver(driverId);
            if (videogame == null)
            {
                throw new RequestNotFoundException($"the driver id:{driverId}, does not exist");
            }
        }

        //public ActorsService(ILibraryRepository libraryRepository, IMapper mapper)
        //{
        //    _libraryRepository = libraryRepository;
        //    _mapper = mapper;
        //}

        //public ActorModel CreateActor(ActorModel actor)
        //{
        //    var actorEntity = _mapper.Map<ActorEntity>(actor);
        //    var returnedActor = _libraryRepository.CreateActor(actorEntity);
        //    return _mapper.Map<ActorModel>(returnedActor);
        //}

        //public ActorModel GetActor(int actorId)
        //{
        //    var actor = _libraryRepository.GetActor(actorId);
        //    if(actor == null)
        //    {
        //        throw new RequestNotFoundException($"Actor with Id {actorId} was not found");
        //    }
        //    return _mapper.Map<ActorModel>(actor);
        //}

        //public IEnumerable<ActorModel> GetActors()
        //{
        //    var entityList = _libraryRepository.GetActors();
        //    var modelList = _mapper.Map<IEnumerable<ActorModel>>(entityList);
        //    return modelList;
        //}


    }
}
