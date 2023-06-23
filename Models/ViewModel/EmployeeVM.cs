using Microsoft.AspNetCore.Mvc.Rendering;

namespace EmployeeManagementInfo.Models.ViewModel
{
    public class EmployeeVM
    {
        public int empNo { get; set; }
        public string empName { get; set; }
        public DateTime DOB { get; set; }
        public string gender{ get; set;}
        public string depname { get; set; }
        public string positionName { get; set; }
    }
}
