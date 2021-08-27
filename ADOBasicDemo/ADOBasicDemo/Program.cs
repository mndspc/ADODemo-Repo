using System;


namespace ADOBasicDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("1.Save Employee 2.Delete Employee 3.Update Employee 4.Select Employee 5.Show all Employees:");
            int response = int.Parse(Console.ReadLine());
            ConnectedModel connectedModel = new ConnectedModel();
            switch (response)
            {
                case 1:
                        EmpMaster empMaster = new EmpMaster { EmpCode = 6, EmpName = "Marry", DateOfBirth = DateTime.Parse("03-05-1981"), Email = "marry@gmail.com", DeptCode = 102 };                     
                        Console.WriteLine(connectedModel.SaveEmployee(empMaster) ? "Employee Information Saved" : "Error'while saving Employee Information");
                        Console.WriteLine("Employee Count is:{0}",connectedModel.EmployeeCount());
                break;
                case 2:
                        int EmpCode = 3;
                        Console.WriteLine(connectedModel.DeleteEmployee(EmpCode) ? "Employee Deleted" : "Error while deleting Employee");
                        Console.WriteLine("Employee Count is:{0}", connectedModel.EmployeeCount());
                    break;
                case 3:
                    EmpMaster empMaster1 = new EmpMaster { EmpCode = 2, EmpName = "Scott123", DateOfBirth = DateTime.Parse("03-03-1980"), Email = "scott123@gmail.com", DeptCode = 101 };
                    Console.WriteLine(connectedModel.UpdateEmployee(empMaster1) ? "Employee Info. Updated" :"Error while updating Employee info");
                 break;
                case 4:
                    int EmpCode1 = 2;
                    var emp= connectedModel.GetEmployeeByCode(EmpCode1);
                    if (emp != null)
                    {
                        Console.WriteLine("Code={0} Name={1} Date of Birth={2} Email={3} Dept Code={4}", emp.EmpCode, emp.EmpName, emp.DateOfBirth, emp.Email, emp.DeptCode);
                    }
                    else
                    {
                        Console.WriteLine("Employee does not exist");
                    }
                break;
                case 5:
                    var employees = connectedModel.GetAllEmployees();
                    foreach(var employee in employees)
                    {
                        Console.WriteLine("Code={0} Name={1} Date of Birth={2} Email={3} Dept Code={4}", employee.EmpCode, employee.EmpName, employee.DateOfBirth, employee.Email, employee.DeptCode);

                    }
                    break;
            }
            Console.ReadLine();
        }
    }
}
