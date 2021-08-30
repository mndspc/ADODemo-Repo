using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace ADOBasicDemo
{
    class ConnectedModel
    {
        #region Database Objects
        SqlConnection sqlConnection = 
            new SqlConnection(ConfigurationManager.ConnectionStrings["ADOBasicDemo.Settings1.ConStr"].ConnectionString);
        SqlCommand sqlCommand = new SqlCommand();
        SqlDataReader dr;
        #endregion
        public bool SaveEmployee(EmpMaster empMaster)
        {
            try
            {
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = SP.SaveEmployee.ToString();      
                sqlCommand.Parameters.Add("@EmpCode", SqlDbType.Int).Value = empMaster.EmpCode;
                sqlCommand.Parameters.Add("@EmpName", SqlDbType.VarChar,50).Value = empMaster.EmpName;
                sqlCommand.Parameters.Add("@DateOfBirth", SqlDbType.DateTime).Value = empMaster.DateOfBirth;
                sqlCommand.Parameters.Add("@Email",SqlDbType.VarChar,50).Value=empMaster.Email;
                sqlCommand.Parameters.Add("@DeptCode", SqlDbType.Int).Value = empMaster.DeptCode;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                if (sqlConnection.State == ConnectionState.Closed)
                {
                    sqlConnection.Open();
                }
                sqlCommand.ExecuteNonQuery();
                return true;
            }catch(SqlException ex)
            {
                
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public bool DeleteEmployee(int EmpCode)
        {
            try
            {
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = SP.DeleteEmployee.ToString();
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add("@EmpCode",SqlDbType.Int).Value = EmpCode;
                if (sqlConnection.State == System.Data.ConnectionState.Closed)
                {
                    sqlConnection.Open();
                }
                sqlCommand.ExecuteNonQuery();
                return true;
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public bool UpdateEmployee(EmpMaster empMaster)
        {
            try
            {
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText="Update EmpMaster set EmpName='" +empMaster.EmpName +"',DateOfBirth='" + empMaster.DateOfBirth+ "',Email='" + empMaster.Email +"',DeptCode="+ empMaster.DeptCode+" where EmpCode=" + empMaster.EmpCode + "";
                sqlCommand.CommandType = CommandType.Text;
                if (sqlConnection.State == System.Data.ConnectionState.Closed)
                {
                    sqlConnection.Open();
                }
                sqlCommand.ExecuteNonQuery();
                return true;
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public EmpMaster GetEmployeeByCode(int EmpCode)
        {
            EmpMaster empMaster = new EmpMaster();
            try
            {
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = "select * from EmpMaster where EmpCode=" + EmpCode + "";
                sqlCommand.CommandType = CommandType.Text;
                if (sqlConnection.State == System.Data.ConnectionState.Closed)
                {
                    sqlConnection.Open();
                }
                dr = sqlCommand.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                  
                    empMaster.EmpCode = Convert.ToInt32(dr["EmpCode"]);
                    empMaster.EmpName = Convert.ToString(dr["EmpName"]);
                    empMaster.DateOfBirth = DateTime.Parse(dr["DateOfBirth"].ToString());
                    empMaster.Email = Convert.ToString(dr["Email"]);
                    empMaster.DeptCode = Convert.ToInt32(dr["DeptCode"]);
                    return empMaster;
                }
                else
                {
                    return null;
                }
               
            }
            catch(SqlException ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            finally
            {
                dr.Close();
                sqlConnection.Close();
            }
        }

        public List<EmpMaster> GetAllEmployees()
        {
            List<EmpMaster> empMasters = new List<EmpMaster>();
            try
            {
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = "select * from EmpMaster";
                sqlCommand.CommandType = CommandType.Text;
                if (sqlConnection.State == System.Data.ConnectionState.Closed)
                {
                    sqlConnection.Open();
                }
                dr = sqlCommand.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        EmpMaster empMaster = new EmpMaster();
                        empMaster.EmpCode = Convert.ToInt32(dr["EmpCode"]);
                        empMaster.EmpName = Convert.ToString(dr["EmpName"]);
                        empMaster.DateOfBirth = DateTime.Parse(dr["DateOfBirth"].ToString());
                        empMaster.Email = Convert.ToString(dr["Email"]);
                        empMaster.DeptCode = Convert.ToInt32(dr["DeptCode"]);
                        empMasters.Add(empMaster);
                    }
                    return empMasters;
                }
                else
                {
                    return null;
                }
            }
            catch(Exception ex)
            {
                return null;
            }
            finally
            {
                dr.Close();
                sqlConnection.Close();
            }
        }

        public  int EmployeeCount()
        {
            try
            {
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = "select count(*) from EmpMaster";
                sqlCommand.CommandType = CommandType.Text;
                if (sqlConnection.State == System.Data.ConnectionState.Closed)
                {
                    sqlConnection.Open();
                }
                int count =Convert.ToInt32( sqlCommand.ExecuteScalar());
                return count;
            }catch(SqlException ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public bool ValidateUser(string email,string pass)
        {
            try
            {
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = SP.ValiadateEmployee.ToString();
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add("@Email", SqlDbType.VarChar, 50).Value = email;
                sqlCommand.Parameters.Add("@Pass", SqlDbType.VarChar, 20).Value = pass;
                if (sqlConnection.State == ConnectionState.Closed)
                {
                    sqlConnection.Open();
                }
                dr = sqlCommand.ExecuteReader();
                if (dr.HasRows)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }catch(Exception ex)
            {
                return false;
            }
            finally
            {
              
                sqlConnection.Close();
            }
        }
    }
}
