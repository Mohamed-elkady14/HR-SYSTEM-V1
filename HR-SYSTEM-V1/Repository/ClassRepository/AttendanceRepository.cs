using HR_SYSTEM_V1.Models;
using HR_SYSTEM_V1.Repository.InterfaceRepository;
using HR_SYSTEM_V1.Data;
using Microsoft.EntityFrameworkCore;

namespace HR_SYSTEM_V1.Repository.ClassRepository
{
    public class AttendanceRepository : IAttendanceRepository
    {
        ApplicationDbContext db;
        public AttendanceRepository(ApplicationDbContext db)
        {
            this.db = db;
        }

        public List<Attendance> getAll()
        {
            return db.Attendances.Include(n => n.employee).ToList();
        }

        public void add(Attendance attendance)
        {
            db.Attendances.Add(attendance);
            db.SaveChanges();
        }

        public decimal extraSum(int id)
        {
            return db.Attendances.Where(n => n.Emp_Id == id).Select(n => n.ExtraTime).Sum();
        }
        public decimal discountSum(int id)
        {
            return db.Attendances.Where(n => n.Emp_Id == id).Select(n => n.DiscountTime).Sum();
        }
        public Attendance getById(int id)
        {
            return db.Attendances.FirstOrDefault(n => n.Attend_Id == id);
        }

        public int attendanceDays(int em_id)
        {
            return db.Attendances.Where(n => n.Emp_Id == em_id).ToList().Count;
        }

        public void update(int id, Attendance newAttendance)
        {
            Attendance oldAttendance = db.Attendances.FirstOrDefault(n => n.Attend_Id == id);

            
            oldAttendance.EndTimeWork = newAttendance.EndTimeWork;
            oldAttendance.ExtraTime = newAttendance.ExtraTime;
            oldAttendance.DiscountTime = newAttendance.DiscountTime;

            db.SaveChanges();
        }

        public void delete(int id)
        {
            Attendance attendance = db.Attendances.FirstOrDefault(n => n.Attend_Id == id);
            db.Attendances.Remove(attendance);
            db.SaveChanges();
        }
    }
}
