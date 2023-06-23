using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagementInfo.Models
{
    public class Position
    {
        [Key]
        public int posId { get; set; }
        public string positionName { get; set; }

        [Display(Name = "Department")]
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }
    }
}
