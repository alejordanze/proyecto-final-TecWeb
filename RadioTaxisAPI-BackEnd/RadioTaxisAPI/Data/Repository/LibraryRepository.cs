using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RadioTaxisAPI.Data.Entities;

namespace RadioTaxisAPI.Data.Repository
{
    public class LibraryRepository : ILibraryRepository
    {
        private LibraryDbContext _dbContext;

        public LibraryRepository(LibraryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void CreateBusiness(BusinessEntity business)
        {
            _dbContext.Business.Add(business);
        }

        public async Task<BusinessEntity> GetBusiness(int businessId)
        {
            IQueryable<BusinessEntity> query = _dbContext.Business;
            query = query.AsNoTracking();
            query = query.Include(c => c.Drivers);

            return await query.FirstOrDefaultAsync(c => c.Id == businessId);
        }

        public async Task<IEnumerable<BusinessEntity>> GetBusinesses()
        {
            IQueryable<BusinessEntity> query = _dbContext.Business;
            query = query.AsNoTracking();
            query = query.Include(c => c.Drivers);


            return await query.ToListAsync();
        }

        public async Task<IEnumerable<BusinessEntity>> GetBusinesses(string orderBy)
        {
            IQueryable<BusinessEntity> query = _dbContext.Business;
            query = query.AsNoTracking();
            query = query.Include(c => c.Drivers);

            switch (orderBy)
            {
                case "foundation-date":
                    query = query.OrderBy(m => m.FoundationDate);
                    break;
                case "name":
                    query = query.OrderBy(m => m.Name);
                    break;
                default:
                    query = query.OrderBy(m => m.Id);
                    break;
            }

            return await query.ToListAsync();
        }

        public bool UpdateBusiness(BusinessEntity business)
        {
            var businessToUpdate = _dbContext.Business.FirstOrDefault(c => c.Id == business.Id);

            _dbContext.Entry(businessToUpdate).CurrentValues.SetValues(business);

            return true;
        }

        public async Task<bool> DeleteBusiness(int businessId)
        {
            var businessToDelete = await _dbContext.Business.FirstOrDefaultAsync(c => c.Id == businessId);
            _dbContext.Business.Remove(businessToDelete);

            return true;
        }


        public async Task<IEnumerable<DriverEntity>> GetDrivers(int businessId)
        {
            IQueryable<DriverEntity> query = _dbContext.Drivers;
            query = query.Where(v => v.Business.Id == businessId);
            query = query.Include(v => v.Business);
            query = query.AsNoTracking();

            return await query.ToArrayAsync();
        }

        public void CreateDriver(DriverEntity driverEntity)
        {
            if (driverEntity.Business != null)
            {
                _dbContext.Entry(driverEntity.Business).State = EntityState.Unchanged;
            }
            _dbContext.Drivers.Add(driverEntity);
        }

        public async Task<DriverEntity> GetDriver(int driverId)
        {
            IQueryable<DriverEntity> query = _dbContext.Drivers;
            query = query.Include(v => v.Business);
            query = query.AsNoTracking();
            var driver = await query.SingleOrDefaultAsync(v => v.Id == driverId);
            return driver;
        }

        public async Task<bool> DeleteDriver(int driverId)
        {
            var driverToDelete = await _dbContext.Drivers.FirstOrDefaultAsync(c => c.Id == driverId);
            _dbContext.Drivers.Remove(driverToDelete);
            return true;
        }

        public async Task<bool> UpdateDriver(DriverEntity driver)
        {
            var driverToUpdate = await _dbContext.Drivers.FirstOrDefaultAsync(v => v.Id == driver.Id);
            _dbContext.Entry(driverToUpdate).CurrentValues.SetValues(driver);
            return true;
        }

        //public IEnumerable<ActorEntity> GetActors(int MovieId)
        //{
        //    IQueryable<ActorEntity> query = _dbContext.Actors;
        //    query = query.Where(v => v.Movies.Where(v.Movies.Where(m => m.Id == MovieId));
        //    query = query.Include(v => v.Company);
        //    query = query.AsNoTracking();

        //    return await query.ToArrayAsync();
        //}

        //public ActorEntity GetActor(int actorId)
        //{
        //    return actors.FirstOrDefault(a => a.Id == actorId);
        //}

        //public ActorEntity CreateActor(ActorEntity actor)
        //{
        //    int newId;
        //    if (actors.Count == 0)
        //    {
        //        newId = 1;
        //    }
        //    else
        //    {
        //        newId = actors.OrderByDescending(a => a.Id).FirstOrDefault().Id + 1;
        //    }

        //    actor.Id = newId;

        //    actors.Add(actor);
        //    return actor;
        //}

        public async Task<bool> SaveChangesAsync()
        {
            try
            {
                var res = await _dbContext.SaveChangesAsync();
                return res > 0;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}
