using Microsoft.VisualStudio.TestTools.UnitTesting;
using CS4760Group1.Data;
using CS4760Group1.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;


namespace CS4670Group1UnitTests
{
    [TestClass]
    public class UnitTests
    {
        private CS4760Group1Context _context;
        [TestInitialize]
        public void Setup()
        {
            // Use SQLite in-memory database
            var connectionString = "DataSource=:memory:";
            var options = new DbContextOptionsBuilder<CS4760Group1Context>()
                .UseSqlite(connectionString)
                .Options;

            _context = new CS4760Group1Context(options);

            // Open a connection to SQLite in-memory database
            _context.Database.OpenConnection();
            _context.Database.EnsureCreated();

            // Add sample data
            _context.Department.Add(new Department { 
                Id = 1, 
                Name = "Engineering",
                CollegeID = 13, 
                Dean = "Jane Doe", 
                Location = "Odgen" , 
                Allowance = 100000  });
            _context.SaveChanges();
        }

        [TestCleanup]
        public void Cleanup()
        {
            // Ensure database is deleted after tests
            _context.Database.CloseConnection();
            _context.Dispose();
        }


        [TestMethod]
        public void TestMethod1()
        {

            int initialDepartmentCount = _context.Department.Count();

            // Create a new department
            var newDepartment = new Department
            {
                Id = 2,
                Name = "Physics",
                CollegeID = 14,
                Dean = "John Smith",
                Location = "Salt Lake City",
                Allowance = 120000
            };

            _context.Department.Add(newDepartment);
            _context.SaveChanges();

            // Find out how many departments there are now
            int updatedDepartmentCount = _context.Department.Count();

            // It should be initial count + 1
            Assert.AreEqual(initialDepartmentCount + 1, updatedDepartmentCount);

        }
    }
}