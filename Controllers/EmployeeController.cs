using EmployeeManagementInfo.Data;
using EmployeeManagementInfo.Models;
using EmployeeManagementInfo.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Packaging;

namespace EmployeeManagement.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ApplicationDBContext _db;

        public EmployeeController(ApplicationDBContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
           var objList = from emp in _db.Employee join pos in _db.Position on emp.positionId equals pos.posId
                                              join dep in _db.Department on emp.DepartmentId equals dep.depId
                                            select new { emp.empNo,emp.empName,emp.DOB,emp.gender,dep.depname,pos.positionName};
            
            var emplist = new List<EmployeeVM>();
            foreach (var item in objList)
            {
                emplist.Add(new EmployeeVM { empNo = item.empNo , empName = item.empName , DOB = item.DOB , gender = item.gender , depname = item.depname , positionName = item.positionName});
            }
            return View(emplist);
        }

        //POST - INDEX
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Search(string searchStr)
        {
            int searchEmpNo;
            List<EmployeeVM> objList = new List<EmployeeVM>();

            bool success = int.TryParse(searchStr, out searchEmpNo);

            if (success)
            {
                var employeelist = _db.Employee.Where(u => u.empNo == searchEmpNo).ToList();
                foreach (var item in employeelist)
                {
                    var departmentname = _db.Department.FirstOrDefault(d => d.depId == item.DepartmentId).depname;
                    var positionname = _db.Position.FirstOrDefault(p => p.posId == item.positionId).positionName;
                    objList.Add(new EmployeeVM() { empNo = item.empNo , empName = item.empName , DOB = item.DOB , gender = item.gender , depname =departmentname , positionName=positionname});
                }
                
            }
            else
            {
                var employeelist = _db.Employee.Where(u =>u.empName == searchStr);
                foreach (var item in employeelist)
                {
                    var departmentname = _db.Department.FirstOrDefault(d => d.depId == item.DepartmentId).depname;
                    var positionname = _db.Position.FirstOrDefault(p => p.posId == item.positionId).positionName;
                    objList.Add(new EmployeeVM() { empNo = item.empNo, empName = item.empName, DOB = item.DOB, gender = item.gender, depname = departmentname, positionName = positionname });
                
                }
                
                var departmentId = _db.Department.Where(d => d.depname == searchStr);
                foreach (var item in departmentId)
                {
                    var emplist = _db.Employee.Where(u => u.DepartmentId == item.depId);
                    foreach (var e in emplist)
                    {
                        
                        var positionname = _db.Position.FirstOrDefault(p => p.posId == e.positionId).positionName;
                        objList.Add(new EmployeeVM() { empNo = e.empNo, empName = e.empName, DOB = e.DOB, gender = e.gender, depname = item.depname, positionName = positionname });

                    }
                }

                var positionId = _db.Position.Where(d => d.positionName == searchStr);
                foreach (var item in positionId)
                {
                    var emplist = _db.Employee.Where(u => u.positionId == item.posId);
                    foreach (var p in emplist)
                    {

                        var departmentname = _db.Department.FirstOrDefault(p => p.depId == p.depId).depname;
                        objList.Add(new EmployeeVM() { empNo = p.empNo, empName = p.empName, DOB = p.DOB, gender = p.gender, depname = departmentname, positionName = item.positionName });

                    }
                }

            }

            return View(objList);
        }

        //GET - CREATE
        public IActionResult Create()
        {
            var emp = new EmployeeViewListModel();
            
            emp.DepartmentSelectList = _db.Department.ToList(); 
            //foreach (var item in _db.Department)
            //{
            //    emp.DepartmentSelectList.Add(new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem() { Text = item.depname , Value = item.depId.ToString()});
            //}

            emp.PositionSelectList = _db.Position.ToList();
            //var depid = _db.Department.OrderBy(d => d.depId).FirstOrDefault().depId;
            //emp.departmentId = depid;
            //foreach (var item in _db.Position)
            //{
            //    emp.PositionSelectList.Add(new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem() { Text = item.positionName, Value = item.posId.ToString()});
            //}
            //emp.positionId = emp.PositionSelectList.FirstOrDefault().posId;
            return View(emp);
        }

        //POST - CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EmployeeViewListModel obj)
        {
                _db.Employee.Add(new Employee { empNo=obj.Employee.empNo , empName=obj.Employee.empName, DOB=obj.Employee.DOB , gender=obj.Employee.gender, DepartmentId=obj.departmentId, positionId=obj.positionId});
                _db.SaveChanges();
                return RedirectToAction("Index");
            
         //   return View(obj);
        }

        //GET - EDIT
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            
            var emp = new EmployeeViewListModel();
            emp.Employee = _db.Employee.Find(id);
            if (emp.Employee == null)
            {
                return NotFound();
            }
            emp.DepartmentSelectList = _db.Department.ToList();
            //foreach (var item in _db.Department)
            //{
            //    emp.DepartmentSelectList.Add(new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem() { Text = item.depname , Value = item.depId.ToString()});
            //}

            emp.PositionSelectList = _db.Position.ToList();
            //var depid = _db.Department.OrderBy(d => d.depId).FirstOrDefault().depId;
            //emp.departmentId = depid;
            //foreach (var item in _db.Position)
            //{
            //    emp.PositionSelectList.Add(new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem() { Text = item.positionName, Value = item.posId.ToString()});
            //}
            //emp.positionId = emp.PositionSelectList.FirstOrDefault().posId;
            return View(emp);
        }

        //POST - EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EmployeeViewListModel obj)
        {
                var emp = _db.Employee.Where( e => e.empNo == obj.Employee.empNo).FirstOrDefault();
                emp.empName = obj.Employee.empName;
                emp.DOB = obj.Employee.DOB;
                emp.gender = obj.Employee.gender;
                emp.positionId = obj.positionId;
                emp.DepartmentId = obj.departmentId;

                _db.Employee.Update(emp);
                _db.SaveChanges();
                return RedirectToAction("Index");
           
           
        }

        //GET - DELETE
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var emp = new EmployeeViewListModel();

            emp.Employee = _db.Employee.Find(id);
            if (emp.Employee == null)
            {
                return NotFound();
            }
            emp.DepartmentSelectList = _db.Department.ToList();
            //foreach (var item in _db.Department)
            //{
            //    emp.DepartmentSelectList.Add(new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem() { Text = item.depname , Value = item.depId.ToString()});
            //}

            emp.PositionSelectList = _db.Position.ToList();
            //var depid = _db.Department.OrderBy(d => d.depId).FirstOrDefault().depId;
            //emp.departmentId = depid;
            //foreach (var item in _db.Position)
            //{
            //    emp.PositionSelectList.Add(new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem() { Text = item.positionName, Value = item.posId.ToString()});
            //}
            //emp.positionId = emp.PositionSelectList.FirstOrDefault().posId;
            return View(emp);
        }

        //POST - DELETE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _db.Employee.Find(id);
            Console.WriteLine(obj);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Employee.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
