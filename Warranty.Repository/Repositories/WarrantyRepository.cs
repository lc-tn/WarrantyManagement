using Microsoft.EntityFrameworkCore;
using WarrantyRepository.IRepositories;
using WarrantyManagement.Entities;

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

        public async Task<List<Warranty>> GetAllPagination(int pageNumber)
        {
            return await _context.Warranties.OrderByDescending(w => w.CreateDate)
                .Skip(pageNumber).Take(3).ToListAsync();
        }

        public async Task<List<Warranty>> GetAll()
        {
            return await _context.Warranties.OrderByDescending(w => w.CreateDate).ToListAsync();
        }

        public Task<int> Total()
        {
            return _context.Warranties.CountAsync();
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

        public async Task<List<Warranty>> GetByCustomer(string customerId)
        {
            return await _context.Warranties.OrderByDescending(w => w.CreateDate)
                .Where(w => w.CustomerId.Equals(customerId)).ToListAsync();
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
