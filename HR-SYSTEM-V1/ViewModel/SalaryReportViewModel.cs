namespace HR_SYSTEM_V1.ViewModel
{
    public class SalaryReportViewModel
    {

        public string EmployeeName { get; set; }
        public string phone { get; set; }
        public int salary { get; set; }
        public int attendanceDays { get; set; }
        public int absentDays { get; set; }
        public decimal overTimeHours { get; set; }
        public decimal discountHours { get; set; }
        public decimal extra { get; set; }
        public decimal discount { get; set; }
        public decimal total { get; set; }
    }
}
