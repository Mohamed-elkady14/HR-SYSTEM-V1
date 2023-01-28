using HR_SYSTEM_V1.Constants;
using HR_SYSTEM_V1.Data;
using HR_SYSTEM_V1.Models;
using HR_SYSTEM_V1.Repository.InterfaceRepository;
using System.ComponentModel.DataAnnotations;

namespace HR_SYSTEM_V1.Validation
{
    public class CheckHolidayDayAttribute: ValidationAttribute
    {

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            ApplicationDbContext db = new ApplicationDbContext();

            DateTime dayDate = Convert.ToDateTime(value);
           
            Holiday holiday = db.Holidays.OrderBy(x => x.Edit_Day_General).LastOrDefault();

            var checkDay = CheckHoliday.checkDay(holiday);

            foreach (var day in checkDay)
            {
                if (day == dayDate.DayOfWeek.ToString())
                {
                    return new ValidationResult("Today Not Work Day");
                }
            }
            return ValidationResult.Success;
        }
    }
}
