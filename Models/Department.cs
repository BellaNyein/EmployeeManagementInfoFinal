using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementInfo.Models
{
    public class Department
    {
        [Key]
        public int depId { get; set; }
        public string depname { get; set; }

    }
}
