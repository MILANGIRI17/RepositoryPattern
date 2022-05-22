using RepositoryPattern.Models;

namespace RepositoryPattern.Repository.Interface
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAll();
        Task<Employee> GetById(Guid Id);
        Task Insert(Employee employee);
        void Update(Employee employee);
        void Delete(Employee employee);
        Task Save();

    }
}
