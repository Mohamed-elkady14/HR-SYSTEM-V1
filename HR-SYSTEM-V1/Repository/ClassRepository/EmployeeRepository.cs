using HR_SYSTEM_V1.Data;
using HR_SYSTEM_V1.Models;
using HR_SYSTEM_V1.Repository.InterfaceRepository;
namespace HR_SYSTEM_V1.Repository.ClassRepository
{
    public class EmployeeRepository : IEmployee
    {
        private readonly ApplicationDbContext _db;
        public EmployeeRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        //-------------------- Get ------------------------------
        public List<Employee> getAll()
        {
            return _db.Employees.ToList();
        }

        //-------------------- Get By Id ------------------------------

        public Employee getByID(int id)
        {
            return _db.Employees.FirstOrDefault(x=>x.Emp_Id == id); 
        }

        //------------------- Get Salary -------------------------

        public int getSalary(int id)
        {
            return _db.Employees.Where(n => n.Emp_Id == id).Select(n => n.Salary).FirstOrDefault();
        }

        //------------------- Get start and end time -------------

        public int getStartTime(int id)
        {
            DateTime startTime = _db.Employees.Where(n => n.Emp_Id == id).Select(n => n.StartTime).FirstOrDefault();

            return int.Parse(startTime.ToString("HH"));
        }

        public int getEndTime(int id)
        {
            DateTime endTime = _db.Employees.Where(n => n.Emp_Id == id).Select(n => n.EndTime).FirstOrDefault();

            return int.Parse(endTime.ToString("HH"));
        }

        //-------------------- Add ------------------------------

        public void Add(Employee emp)
        {
            _db.Employees.Add(emp);
            _db.SaveChanges();
        }


        //-------------------- Update -----------------------


        public void updateEmployee(Employee emp)
        {
            _db.Employees.Update(emp);
            _db.SaveChanges();
        }


        //------------------------- Delete ------------------------


        public void delete(int id)
        {
            Employee emp = _db.Employees.FirstOrDefault(n => n.Emp_Id == id);

            _db.Employees.Remove(emp);
            _db.SaveChanges();
        }

    }
}
