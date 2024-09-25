using CrudAngularWebApi.Models;
using System.Data;
using System.Data.SqlClient;

namespace CrudAngularWebApi.Data
{
    public class EmployeeData
    {
        private readonly string connection;

        public EmployeeData(IConfiguration configuration)
        {
            connection = configuration.GetConnectionString("MyConnection")!;
        }

        public async Task<List<Employee>> List()
        {
            List<Employee> employees = new List<Employee>();

            using (var con = new SqlConnection(connection))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("sp_listEmployees", con);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        employees.Add(new Employee()
                        {
                            IdEmployee = Convert.ToInt32(reader["IdEmployee"]),
                            FullName = reader["FullName"].ToString(),
                            Email = reader["Email"].ToString(),
                            Salary = Convert.ToDouble(reader["Salary"]),
                            ContractDate = reader["ContractDate"].ToString()
                        });
                    }
                }
            }
            return employees;
        }

        public async Task<Employee> Get(int IdEmployee)
        {
            Employee employee = new Employee();

            using (var con = new SqlConnection(connection))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("sp_get_employee", con);
                cmd.Parameters.AddWithValue("@_IdEmployee", IdEmployee);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        employee = new Employee()
                        {
                            IdEmployee = Convert.ToInt32(reader["IdEmployee"]),
                            FullName = reader["FullName"].ToString(),
                            Email = reader["Email"].ToString(),
                            Salary = Convert.ToDouble(reader["Salary"]),
                            ContractDate = reader["ContractDate"].ToString()
                        };
                    }
                }
            }
            return employee;
        }


        public async Task<bool> Create(Employee employee)
        {
            bool response = true;
            using (var con = new SqlConnection(connection))
            {
                SqlCommand cmd = new SqlCommand("sp_create_employee", con);
                cmd.Parameters.AddWithValue("@_FullName", employee.FullName);
                cmd.Parameters.AddWithValue("@_Email", employee.Email);
                cmd.Parameters.AddWithValue("@_Salary", employee.Salary);
                cmd.Parameters.AddWithValue("@_ContractDate", employee.ContractDate);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    await con.OpenAsync();
                    response = await cmd.ExecuteNonQueryAsync() > 0 ? true : false;
                }
                catch { 
                response = false;
                }
            }
            return response;
        }

        public async Task<bool> Update(Employee employee)
        {
            bool response = true;
            using (var con = new SqlConnection(connection))
            {
                SqlCommand cmd = new SqlCommand("sp_update_employee", con);
                cmd.Parameters.AddWithValue("@_IdEmployee", employee.IdEmployee);
                cmd.Parameters.AddWithValue("@_FullName", employee.FullName);
                cmd.Parameters.AddWithValue("@_Email", employee.Email);
                cmd.Parameters.AddWithValue("@_Salary", employee.Salary);
                cmd.Parameters.AddWithValue("@_ContractDate", employee.ContractDate);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    await con.OpenAsync();
                    response = await cmd.ExecuteNonQueryAsync() > 0 ? true : false;
                }
                catch
                {
                    response = false;
                }
            }
            return response;
        }


        public async Task<bool> Delete(int IdEmployee)
        {
            bool response = true;
            using (var con = new SqlConnection(connection))
            {
                SqlCommand cmd = new SqlCommand("sp_delete_employee", con);
                cmd.Parameters.AddWithValue("@_IdEmployee", IdEmployee);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    await con.OpenAsync();
                    response = await cmd.ExecuteNonQueryAsync() > 0 ? true : false;
                }
                catch
                {
                    response = false;
                }
            }
            return response;
        }
    }
}