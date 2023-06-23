using Microsoft.AspNetCore.Mvc.Rendering;

namespace EmployeeManagementInfo.Models.ViewModel
{
    public class EmployeeViewListModel
    {
        public Employee Employee { get; set; }
        public List<Department> DepartmentSelectList { get; set; }
        public List<Position> PositionSelectList { get; set; }

        public int positionId { get; set; }
        public int departmentId { get; set; }   
    }
}
