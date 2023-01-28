using HR_SYSTEM_V1.Models;
using System.ComponentModel.DataAnnotations;

namespace HR_SYSTEM_V1.Validation
{
    public class AttendanceAttribute : ValidationAttribute
    {

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            DateTime endTime = Convert.ToDateTime(value);
            Attendance attendance = (Attendance)validationContext.ObjectInstance;

            if (DateTime.Compare(endTime, attendance.StartTimeWork.Value) > 0 || endTime != null)
            {
                return ValidationResult.Success;
            }
            return new ValidationResult("End Time Can Not Be Before Start Time ");
           
        }


    }
}
