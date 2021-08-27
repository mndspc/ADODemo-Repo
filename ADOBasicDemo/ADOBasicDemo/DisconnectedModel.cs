using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace ADOBasicDemo
{
    
    class DisconnectedModel
    {
        #region Database Objects
        SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ADOBasicDemo.Settings1.ConStr"].ConnectionString);
        SqlDataAdapter sqlDataAdapter=new SqlDataAdapter();
        DataSet dataSet = new DataSet();
        #endregion

        public bool SaveEmployee(EmpMaster empMaster)
        {
            try
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = SP.SaveEmployee.ToString();
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add("@EmpCode", SqlDbType.Int).Value = empMaster.EmpCode;
                sqlCommand.Parameters.Add("@EmpName", SqlDbType.VarChar, 50).Value = empMaster.EmpName;
                sqlCommand.Parameters.Add("@DateOfBirth", SqlDbType.DateTime).Value = empMaster.DateOfBirth;
                sqlCommand.Parameters.Add("@Email", SqlDbType.VarChar, 50).Value = empMaster.Email;
                sqlCommand.Parameters.Add("@DeptCode", SqlDbType.Int).Value = empMaster.DeptCode;          
                sqlDataAdapter.SelectCommand = sqlCommand;
                sqlDataAdapter.Fill(dataSet);
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
           

        }

        public bool DeleteEmployee(int EmpCode)
        {
            try
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = SP.DeleteEmployee.ToString();
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add("@EmpCode", SqlDbType.Int).Value = EmpCode;
                sqlDataAdapter.SelectCommand = sqlCommand;
                sqlDataAdapter.Fill(dataSet);
                return true;
            }catch(SqlException ex)
            {
                return false;
            }
        }

        public EmpMaster GetEmployeeByCode(int EmpCode)
        {
            EmpMaster empMaster = new EmpMaster();
            try
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = SP.GetEmpByCode.ToString();
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add("@EmpCode", SqlDbType.Int).Value = EmpCode;
                sqlDataAdapter.SelectCommand = sqlCommand;
                dataSet.Reset();
                sqlDataAdapter.Fill(dataSet,"Emp");
                if (dataSet.Tables["Emp"].Rows.Count > 0)
                {
                    foreach (DataRow row in dataSet.Tables["Emp"].Rows)
                    {
                        empMaster.EmpCode = Convert.ToInt32(row["EmpCode"]);
                        empMaster.EmpName = Convert.ToString(row["EmpName"]);
                        empMaster.DateOfBirth = DateTime.Parse(row["DateOfBirth"].ToString());
                        empMaster.Email = Convert.ToString(row["Email"]);
                        empMaster.DeptCode = Convert.ToInt32(row["DeptCode"]);
                    }

                }
                return empMaster;
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return empMaster;
            }
        }

        public List<EmpMaster> GetAllEmployees()
        {
            List<EmpMaster> empMasters = new List<EmpMaster>();
            try
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = "Select * from EmpMaster";
                sqlCommand.CommandType = CommandType.Text;
                sqlDataAdapter.SelectCommand = sqlCommand;
                dataSet.Reset();
                sqlDataAdapter.Fill(dataSet,"Employees");
                if (dataSet.Tables["Employees"].Rows.Count > 0)
                {
                    foreach (DataRow row in dataSet.Tables["Employees"].Rows)
                    {
                        EmpMaster empMaster = new EmpMaster();
                        empMaster.EmpCode = Convert.ToInt32(row["EmpCode"]);
                        empMaster.EmpName = Convert.ToString(row["EmpName"]);
                        empMaster.DateOfBirth = DateTime.Parse(row["DateOfBirth"].ToString());
                        empMaster.Email = Convert.ToString(row["Email"]);
                        empMaster.DeptCode = Convert.ToInt32(row["DeptCode"]);
                        empMasters.Add(empMaster);
                    }

                }
                return empMasters;
            }
            catch(SqlException ex)
            {
                return empMasters;
            }
        }
    }
}
