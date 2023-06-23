using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagementInfo.Models
{
    public class Employee
    {
        [Key]
        public int empNo { get; set; }
        [Required]
        public string empName { get; set; }
        public DateTime DOB { get; set; }
        public string gender { get; set; }

        [Display(Name = "Department")]
        public int DepartmentId { get; set; }
        
        public virtual Department Department { get; set; }

        public int positionId { get; set; }
    }
}
