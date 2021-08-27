using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADOBasicDemo
{
    class SalaryInfo
    {
        public int SalarySheetNo { get; set; }
        public int EmpCode { get; set; }
        public DateTime DateOfSalary { get; set; }
        public double Basic { get; set; }
        public double? Hra { get; set; }
        public double? Da { get; set; }
        public double NetSalary { get; set; }

    }
}
