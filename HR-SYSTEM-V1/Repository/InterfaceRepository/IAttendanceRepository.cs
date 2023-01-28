using HR_SYSTEM_V1.Models;

namespace HR_SYSTEM_V1.Repository.InterfaceRepository
{
    public interface IAttendanceRepository
    {
        public List<Attendance> getAll();
        public void add(Attendance attendance);
        public void delete(int id);
        public Attendance getById(int id);
        public void update(int id, Attendance newAttendance);
        public int attendanceDays(int em_id);
        public decimal extraSum(int id);
        public decimal discountSum(int id);
        
    }
}
