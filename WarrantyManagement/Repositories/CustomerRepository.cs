using WarrantyManagement.Entities;

namespace WarrantyManagement.Repositories
{
    public class CustomerRepository
    {
        private readonly WarrantyManagementDbContext _context;

        public CustomerRepository(WarrantyManagementDbContext context)
        {
            _context = context;
        }

        public bool CheckExistence(string id)
        {
            return _context.Customers.Any(c => c.Id.Equals(id));
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public List<Customer> GetAll()
        {
            return _context.Customers.ToList();
        }

        public Customer GetCustomerById(string id)
        {
            return _context.Customers.Where(c => c.Id.Equals(id)).SingleOrDefault();
        }

        public bool CreateCustomer(Customer customer)
        {
            _context.Add(customer);
            return Save();
        }

        public bool UpdateCustomer(Customer customer)
        {
            _context.Update(customer);
            return Save();
        }
    }
}
