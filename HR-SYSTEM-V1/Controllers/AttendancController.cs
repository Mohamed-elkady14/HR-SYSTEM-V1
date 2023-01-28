using Microsoft.AspNetCore.Mvc;
using HR_SYSTEM_V1.Repository;
using HR_SYSTEM_V1.Repository.ClassRepository;
using HR_SYSTEM_V1.Repository.InterfaceRepository;
using HR_SYSTEM_V1.ViewModel;
using HR_SYSTEM_V1.Models;
using HR_SYSTEM_V1.Constants;

namespace HR_SYSTEM_V1.Controllers
{
    public class AttendancController : Controller
    {
        IAttendanceRepository attendanceRepo;
        IEmployee empRepository;
        IGeneralSetting generalSettingRepo;
        public AttendancController(IAttendanceRepository attendanceRepo, IEmployee empRepository, IGeneralSetting generalSettingRepo)
        {
            this.attendanceRepo = attendanceRepo;
            this.empRepository = empRepository;
            this.generalSettingRepo = generalSettingRepo;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewData["employees"] = empRepository.getAll();
            return View(attendanceRepo.getAll());
        }

        [HttpGet]
        public IActionResult newAttendance()
        {
            ViewData["employees"] = empRepository.getAll();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult newAttendance(int id, Attendance attendance)
        {
            if (ModelState.IsValid)
            {
                

                if(attendance.EndTimeWork != null)
                {


                    Holiday holiday = generalSettingRepo.getLastHoliday();
                    double empSalary = empRepository.getSalary(attendance.Emp_Id);
                    int originalStartTime = empRepository.getStartTime(attendance.Emp_Id);
                    int originalEndTime = empRepository.getEndTime(attendance.Emp_Id);
                    int orignalSubtractionTime = originalEndTime - originalStartTime;
                    
                    // ================ Price Hour =================
                    double hourPrice = CheckHoliday.getHourPrice(attendance.Emp_Id, holiday, attendance, empSalary, orignalSubtractionTime);


                    int startTime = int.Parse(attendance.StartTimeWork?.ToString("HH"));
                    int endTime = int.Parse(attendance.EndTimeWork?.ToString("HH"));

                    int subtruction = endTime - startTime;
                    decimal discount = generalSettingRepo.getLastExtraDiscount().Discount;
                    decimal extra = generalSettingRepo.getLastExtraDiscount().Extra;
                    decimal originalHour = Convert.ToDecimal(hourPrice);
                    
                    if (subtruction < orignalSubtractionTime)
                    {
                        decimal discountHours = orignalSubtractionTime - subtruction;
                        attendance.DiscountTime = discountHours * discount * originalHour;
                    }
                    else if(subtruction > orignalSubtractionTime)
                    {
                        decimal overTimeHours = subtruction - orignalSubtractionTime;
                        attendance.ExtraTime = overTimeHours * extra * originalHour;
                    }

                    Console.WriteLine(hourPrice);
                }

                int flag = 0;
                if (attendanceRepo.getById(id) == null)
                {
                    List<Attendance> attendance1 = attendanceRepo.getAll();
                    if (attendance1 != null)
                    {

                        foreach (var item in attendance1)
                        {
                            if (attendance.Emp_Id == item.Emp_Id && attendance.Day_Date == item.Day_Date)
                            {
                                flag = 1;
                            }
                        }

                    }
                    if (flag == 0)
                    {
                        attendanceRepo.add(attendance);
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "this empolee already attended");
                        ViewData["employees"] = empRepository.getAll();

                        return View();
                    }
                    //  return RedirectToAction("Index");
                }

                    //if (attendanceRepo.getById(id) == null) 
                    //{

                    //    attendanceRepo.add(attendance);
                    //    return RedirectToAction("Index");
                    //}
                    ViewData["employees"] = empRepository.getAll();
                attendanceRepo.update(id, attendance);
                return RedirectToAction("Index");
            }
            ViewData["employees"] = empRepository.getAll();
            return View("Index", attendanceRepo.getAll());
        }

        [HttpGet]
        public IActionResult delete(int id)
        {
            attendanceRepo.delete(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult edit(int id)
        {
            var attendance = attendanceRepo.getById(id);
            ViewData["employees"] = empRepository.getAll();

            return View("newAttendance", attendance);
        }
        
    }
}
