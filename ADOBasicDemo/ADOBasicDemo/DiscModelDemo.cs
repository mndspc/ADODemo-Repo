using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADOBasicDemo
{
    class DiscModelDemo
    {
        static void Main()
        {
            Console.Write("Enter Email:");
            string email = Console.ReadLine();
            Console.Write("Enter Password:");
            string pass = Console.ReadLine();
            DisconnectedModel disconnectedModel = new DisconnectedModel();
            if (disconnectedModel.ValidateUser(email, pass))
            {
                Console.Write("1.Save Employee 2.Delete Employee 3.Update Employee 4.Select Employee 5.Show all Employees:");
                int response = int.Parse(Console.ReadLine());
                switch (response)
                {
                    case 1:
                        EmpMaster empMaster = new EmpMaster { EmpCode = 7, EmpName = "Jhones", DateOfBirth = DateTime.Parse("03-05-1981"), Email = "jhones@gmail.com", DeptCode = 100 };
                        Console.WriteLine(disconnectedModel.SaveEmployee(empMaster) ? "Employee Saved Successfully" : "Error while saving info.");
                        break;
                    case 2:
                        int EmpCode = 2;
                        Console.WriteLine(disconnectedModel.DeleteEmployee(EmpCode) ? "Employee Deleted" : "Wrror while deleting info.");
                        break;
                    case 3:
                        break;
                    case 4:
                        int EmpCode1 = 5;
                        var emp = disconnectedModel.GetEmployeeByCode(EmpCode1);
                        Console.WriteLine("Code={0} Name={1} Date of Birth={2} Email={3} Dept Code={4}", emp.EmpCode, emp.EmpName, emp.DateOfBirth, emp.Email, emp.DeptCode);

                        break;
                    case 5:
                        var employees = disconnectedModel.GetAllEmployees();
                        foreach (var employee in employees)
                        {
                            Console.WriteLine("Code={0} Name={1} Date of Birth={2} Email={3} Dept Code={4}", employee.EmpCode, employee.EmpName, employee.DateOfBirth, employee.Email, employee.DeptCode);

                        }
                        break;
                }
            }
            else
            {
                Console.WriteLine("Incorrect Email or Password");
            }
            Console.ReadLine();
        }
    }
}
