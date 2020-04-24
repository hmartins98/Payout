using System;

namespace CustomerService.Repository.SQL
{
    public interface ICustomerRepository
    {
        public Customer GetCustomerByID(Guid customerID);
        public void CreateCustomer(Customer customer);
        public void UpdateCustomer(Customer customer);
        public void DeleteCustomer(Guid customerID);
    }

    public class CustomerRepository : ICustomerRepository
    {
        //MySQL
        //private DataContext _context;

        public CustomerRepository(/*DataContext context*/)
        {
            //_context = context;
        }

        public Customer GetCustomerByID(Guid customerID)
        {
            //if (_context == null)
            //    throw new Exception("Connection with datebase closed.");

            //try to get user by ID

            //login is valid and successful
            return new Customer()
            {
                Id = new Guid(),
                Name = "testeUser"
            };
        }

        public void CreateCustomer(Customer customer)
        {
            //if (_context == null)
            //    throw new Exception("Connection with datebase closed.");

            //Insert the new object in the DB
        }

        public void UpdateCustomer(Customer customer)
        {
            //if (_context == null)
            //    throw new Exception("Connection with datebase closed.");

            //Insert the new object in the DB
        }

        public void DeleteCustomer(Guid customerID)
        {
            //if (_context == null)
            //    throw new Exception("Connection with datebase closed.");

            //Insert the new object in the DB
        }
    }
}
