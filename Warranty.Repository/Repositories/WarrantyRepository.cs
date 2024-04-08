using Microsoft.EntityFrameworkCore;
using WarrantyRepository.IRepositories;
using WarrantyManagement.Entities;
using System.Collections.Generic;
using Microsoft.IdentityModel.Tokens;

namespace WarrantyManagement.Repositories
{
    public class WarrantyRepository : IWarrantyRepository
    {
        private readonly WarrantyManagementDbContext _context;

        public WarrantyRepository(WarrantyManagementDbContext context)
        {
            _context = context;
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public async Task<List<Warranty>> GetAllPagination(int pageNumber, int pageSize)
        {
            return await _context.Warranties.OrderByDescending(w => w.CreateDate)
                .Skip(pageNumber).Take(pageSize).ToListAsync();
        }

        public async Task<List<Warranty>> GetAll()
        {
            return await _context.Warranties.OrderByDescending(w => w.CreateDate).ToListAsync();
        }

        public Task<int> Total()
        {
            return _context.Warranties.CountAsync();
        }

        public Task<int> TotalByUser(string userId)
        {
            return _context.Warranties.Where(w => w.CustomerId.Equals(userId) || w.Sale.Equals(userId) ||
                        w.Technician.Equals(userId)).CountAsync();
        }

        public Task<int> TotalNewWarrantySale()
        {
            return _context.Warranties.Where(w => w.Sale.Equals("")).CountAsync();
        }

        public Task<int> TotalNewWarrantyTech()
        {
            return _context.Warranties.Where(w => w.Technician.Equals("") && w.AppointmentDate != null).CountAsync();
        }

        public async Task<Warranty> GetWarrantyById(int id)
        {
            return await _context.Warranties.SingleOrDefaultAsync(w => w.Id == id);
        }

        public async Task<List<Warranty>> GetByStatus(string status)
        {
            return await _context.Warranties.OrderByDescending(w => w.CreateDate)
                .Where(w => w.Status.Equals(status)).ToListAsync();
        }

        public async Task<List<Warranty>> GetByUser(string userId)
        {
            List <Warranty> warranties =  await _context.Warranties.OrderByDescending(w => w.CreateDate)
                .Where(w => w.CustomerId.Equals(userId)).ToListAsync();
            return warranties;
        }

        public async Task<List<Warranty>> GetByUserPagination(string userId, int pageNumber, int pageSize)
        {
            List<Warranty> warranties = await _context.Warranties.OrderByDescending(w => w.CreateDate)
                .Where(w => w.CustomerId.Equals(userId) || w.Sale.Equals(userId) ||
                        w.Technician.Equals(userId)).Skip(pageNumber).Take(pageSize).ToListAsync();
            return warranties;
        }

        public async Task<List<Warranty>> GetNewWarrantySale(int pageNumber, int pageSize)
        {
            List<Warranty> warranties = await _context.Warranties.OrderByDescending(w => w.CreateDate)
                .Where(w => w.Sale == null).Skip(pageNumber).Take(pageSize).ToListAsync();
            return warranties;
        }

        public async Task<List<Warranty>> GetNewWarrantyTech(int pageNumber, int pageSize)
        {
            List<Warranty> warranties = await _context.Warranties.OrderByDescending(w => w.CreateDate)
                .Where(w =>  w.Technician == null && w.AppointmentDate != null)
                .Skip(pageNumber).Take(pageSize).ToListAsync();
            return warranties;
        }

        public bool CreateWarranty(Warranty warranty)
        {
            var entry = _context.Add(warranty);
            if (entry.State == EntityState.Added)
            {
                return true;
            }
            return false;
        }

        public bool EditWarranty(Warranty warranty)
        {
            var entry = _context.Update(warranty);
            if(entry.State == EntityState.Modified)
            {
                return true;
            }
            return false;
        }

        public bool DeleteWarranty(Warranty warranty)
        {
            _context.Remove(warranty);
            return Save();
        }
    }
}
