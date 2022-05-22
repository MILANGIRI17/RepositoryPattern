using Microsoft.EntityFrameworkCore;
using RepositoryPattern.Data;
using RepositoryPattern.Models;
using RepositoryPattern.Repository.Interface;

namespace RepositoryPattern.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext context;

        public EmployeeRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Employee>> GetAll()
        {
            return await context.Employees.ToListAsync();
        }
        public async Task<Employee> GetById(Guid Id)
        {
            var employee =await context.Employees.FindAsync(Id);
            return employee;
        }

        public async Task Insert(Employee employee)
        {
           await context.Employees.AddAsync(employee);
        }
        public void Update(Employee employee)
        {
             context.Employees.Update(employee);
        }
        public void Delete(Employee employee)
        {
            context.Employees.Remove(employee);
        }
        public async Task Save()
        {
            await context.SaveChangesAsync();
        }


    }
}
