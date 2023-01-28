using HR_SYSTEM_V1.Models;
namespace HR_SYSTEM_V1.Repository.InterfaceRepository
{
    public interface IEmployee
    {
        public List<Employee> getAll();
        public Employee getByID ( int id);
        public void Add(Employee emp);
        public void updateEmployee (Employee employee);
        public void delete(int id);
        //------------------- Get Salary -------------------------

        public int getSalary(int id);

        //------------------- Get start and end time -------------

        public int getStartTime(int id);

        public int getEndTime(int id);
    }
}
