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

        public List<Warranty> GetAll()
        {
            return _context.Warranties.ToList();
        }

        public bool CreateCustomer(Warranty warranty)
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
