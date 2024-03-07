using Microsoft.EntityFrameworkCore;
using System.Drawing.Printing;
using WarrantyManagement.Authorization;
using WarrantyManagement.Entities;

namespace WarrantyManagement.Repositories
{
    public class WarrantyRepository
    {
        private readonly WarrantyManagementDbContext _context;

        public WarrantyRepository(WarrantyManagementDbContext context)
        {
            _context = context;
        }

        public bool CheckExistence(int id)
        {
            return _context.Warranties.Any(c => c.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public async Task<List<Warranty>> GetAll(int pageNumber)
        {
            return await _context.Warranties.OrderByDescending(w => w.CreateDate)
                .Skip(pageNumber).Take(3).ToListAsync();
        }

        public Task<int> Total()
        {
            return _context.Warranties.CountAsync();
        }

        public async Task<Warranty> GetWarrantyById(int id)
        {
            return await _context.Warranties.SingleOrDefaultAsync(w => w.Id == id);
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
