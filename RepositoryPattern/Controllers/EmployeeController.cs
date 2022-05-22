using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryPattern.Models;
using RepositoryPattern.Repository.Interface;
using RepositoryPattern.ViewModel;

namespace RepositoryPattern.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository repository;

        public EmployeeController(IEmployeeRepository repository)
        {
            this.repository = repository;
        }
        public async Task<ActionResult> Index()
        {
            var employees = await repository.GetAll();
            var employeeVm = new List<EmployeeViewModel>();
            foreach (var employee in employees) {
                employeeVm.Add(new EmployeeViewModel
                {
                    Id = employee.Id,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    Email = employee.Email,
                    PhoneNumber=employee.PhoneNumber,
                }); 
            }
            return View(employeeVm);
        }

        // GET: EmployeeController/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            var employee =await repository.GetById(id);
            var employeeVm = new EmployeeViewModel(){
                Id=employee.Id,
                FirstName=employee.FirstName,
                LastName=employee.LastName,
                Email = employee.Email,
                PhoneNumber=employee.PhoneNumber
            };
            return View(employeeVm);
        }

        // GET: EmployeeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EmployeeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(EmployeeViewModel employeeViewModel)
        {
            try
            {
                var employee = new Employee()
                {
                    Id = employeeViewModel.Id,
                    FirstName = employeeViewModel.FirstName,
                    LastName = employeeViewModel.LastName,
                    Email = employeeViewModel.Email,
                    PhoneNumber = employeeViewModel.PhoneNumber,
                };
                await repository.Insert(employee);
                await repository.Save();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EmployeeController/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            var employee = await repository.GetById(id);
            var employeeVm = new EmployeeViewModel()
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
            };
            return View(employeeVm);
        }

        // POST: EmployeeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EmployeeViewModel employeeViewModel)
        {
            try
            {
                var employee = new Employee()
                {
                    Id = employeeViewModel.Id,
                    FirstName = employeeViewModel.FirstName,
                    LastName = employeeViewModel.LastName,
                    Email = employeeViewModel.Email,
                    PhoneNumber=employeeViewModel.PhoneNumber,
                };
                repository.Update(employee);
                await repository.Save();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> Delete(Guid id)
        {
            try
            {
                var employee = await repository.GetById(id);
                repository.Delete(employee);
                await repository.Save();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View();
            }
        }


    }
}
