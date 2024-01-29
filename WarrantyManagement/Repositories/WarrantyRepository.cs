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

        public async Task<List<Warranty>> GetAll()
        {
            return _context.Warranties.ToList();
        }

        public async Task<Warranty> GetWarrantyById(int id)
        {
            return _context.Warranties.Where(w => w.Id == id).SingleOrDefault();
        }

        public bool CreateWarranty(Warranty warranty)
        {
            _context.Add(warranty);
            return Save();
        }

        public bool UpdateCustomer(Warranty warranty)
        {
            _context.Update(warranty);
            return Save();
        }
    }
}
