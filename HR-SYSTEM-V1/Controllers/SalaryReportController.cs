using HR_SYSTEM_V1.Constants;
using HR_SYSTEM_V1.Repository.InterfaceRepository;
using HR_SYSTEM_V1.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace HR_SYSTEM_V1.Controllers
{
    public class SalaryReportController : Controller
    {
        IAttendanceRepository attendanceRepo;
        IGeneralSetting generalSettingRepo;
        IEmployee employeeRepo;
        public SalaryReportController(IAttendanceRepository attendanceRepo, IGeneralSetting generalSettingRepo, IEmployee employeeRepo)
        {
            this.attendanceRepo = attendanceRepo;
            this.generalSettingRepo = generalSettingRepo;
            this.employeeRepo = employeeRepo;
        }

        public IActionResult Index()
        {
            List<SalaryReportViewModel> salaryReportListVM = new List<SalaryReportViewModel>();

            var attendanceInfo = attendanceRepo.getAll();

            foreach (var attendance in attendanceInfo)
            {
                SalaryReportViewModel salaryReportVM = new SalaryReportViewModel();

                int month = int.Parse(attendance.Day_Date.ToString("MM"));
                int year = int.Parse(attendance.Day_Date.ToString("yyyy"));


                int attendanceDays = attendanceRepo.attendanceDays(attendance.Emp_Id);

                int daysInMonthWithNoHoliday = CheckHoliday.getDaysOfMonthWithNoHoliday(generalSettingRepo.getLastHoliday(), attendance);
                int absentDays = daysInMonthWithNoHoliday - attendanceDays;

                decimal total = employeeRepo.getSalary(attendance.Emp_Id) / DateTime.DaysInMonth(year, month);
                

                salaryReportVM.EmployeeName = attendance.employee.Name;
                salaryReportVM.salary = attendance.employee.Salary;
                salaryReportVM.phone = attendance.employee.Phone;
                salaryReportVM.attendanceDays = attendanceDays;
                salaryReportVM.absentDays = absentDays;
                salaryReportVM.extra = attendanceRepo.extraSum(attendance.Emp_Id);
                salaryReportVM.discount = attendanceRepo.discountSum(attendance.Emp_Id);
                salaryReportVM.total = (total * attendanceDays) + attendanceRepo.extraSum(attendance.Emp_Id) - attendanceRepo.discountSum(attendance.Emp_Id);
                salaryReportListVM.Add(salaryReportVM);

            }

            return View(salaryReportListVM.DistinctBy(n => n.EmployeeName).ToList());
        }
    }
}
